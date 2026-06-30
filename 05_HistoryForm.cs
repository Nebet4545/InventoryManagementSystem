using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static InventoryManagementSystem.Main;

namespace InventoryManagementSystem
{
    public partial class HistoryForm : Form
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        string? mainConn = Class_DbConfig.ConnectionString;

        public HistoryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロード時の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryForm_Load(object sender, EventArgs e)
        {
            //コンボボックスに初期データをセット
            cmbSet();
            //テキストボックスを初期化
            txtReset();
            //ラジオボタンの初期設定
            RadioThisMonth.Checked = true;
            RadioStockAll.Checked = true;
            //デートタイムピッカー(dtpStartDate・dtpEndDate)の値を当日に設定する
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            //表に自動で列が増えないように制限する
            dgvHistory.AutoGenerateColumns = false;
            //表に初期データをセット
            DataSet();
        }
        /// <summary>
        /// コンボボックス初期設定の関数
        /// </summary>
        private void cmbSet()
        {
            //コンボボックス初期化
            cmbProductCode.Items.Clear();
            //初期値に空白を入れる(全検索用)
            cmbProductCode.Items.Add("");

            //コンボボックスに値をセット(商品コード)
            foreach (var item in Class_DataStore.Products)
            {
                cmbProductCode.Items.Add(item.ProductCode);
            }
            //コンボボックスの初期インデックスを設定
            cmbProductCode.SelectedIndex = 0;
        }
        /// <summary>
        /// テキストボックス初期化の関数
        /// </summary>
        private void txtReset()
        {
            txtStaffName.Text = string.Empty;
        }
        /// <summary>
        /// データベースから初期データを取得する関数
        /// </summary>
        private void DataSet()
        {
            //DataStoreを初期化する
            Class_DataStore.StockLogs.Clear();

            //当月の初日を取得(初期状態を当月分にするため)
            DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //当月の最終日を取得
            DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);

            //sql文記述用
            var sbsql = new StringBuilder();
            {
                string sql =
                    @"SELECT 
                    s.""LogDate"",p.""ProductCode"",s.""ProductName"",
                    s.""Category"",s.""Quantity"",s.""StaffName""
                    FROM ""StockLogs"" s
                    INNER JOIN ""Products"" p
                    ON s.""ProductId"" = p.""ProductId""
                    WHERE s.""LogDate"" BETWEEN @StartDay AND @EndDay
                    AND s.""Quantity"" <> 0
                    ORDER BY s.""LogDate""";
                sbsql.Append(sql);
            }

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //空のsqlコマンドを宣言
                    using (var cmd = new NpgsqlCommand())
                    {
                        //表示する期間を代入
                        cmd.Parameters.AddWithValue("@StartDay", StartDate); //当月初日
                        cmd.Parameters.AddWithValue("@EndDay", EndDate); //当月末日

                        //sql文をコマンドに入れる
                        cmd.CommandText = sbsql.ToString();
                        cmd.Connection = conn;

                        //データベース読み込み
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //各項目をDataStoreに追加する
                                Class_DataStore.StockLogs.Add(new Class_Log
                                {
                                    LogDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("LogDate")).ToDateTime(TimeOnly.MinValue), //入出庫日
                                    ProductCode = reader["ProductCode"].ToString(), //商品コード
                                    ProductName = reader["ProductName"].ToString(), //商品名
                                    Category = reader["Category"].ToString(), //カテゴリー
                                    Quantity = Convert.ToInt32(reader["Quantity"]), //入出庫数量
                                    StaffName = reader["StaffName"].ToString() //担当者名
                                });
                            }
                            //表に取得した値を表示する
                            dgvHistory.DataSource = Class_DataStore.StockLogs;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"エラーメッセージ：{ex.Message}");
                }
            }
        }
        /// <summary>
        /// 各セルの色を変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //プロパティ名が"Category"かチェックする
            if (dgvHistory.Columns[e.ColumnIndex].DataPropertyName == "Category")
            {
                //区分("Category")列の値を文字列化する
                var category = e.Value?.ToString();

                //区分列の値が"出庫"の場合、セルの色を赤にする
                if (category == "出庫")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
            //プロパティ名が"Quantity"かチェックする
            if (dgvHistory.Columns[e.ColumnIndex].DataPropertyName == "Quantity")
            {
                //数量("Quantity")列の値を数値化する
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int quan))
                {
                    //数量の値が0未満(マイナス)の場合、セルの色を赤にする
                    if (quan < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        //数値を絶対値に変換して表示する
                        e.Value = Math.Abs(quan);
                    }
                }
            }
        }
        /// <summary>
        /// ラジオボタン全期間が選択された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioAll_CheckedChanged(object sender, EventArgs e)
        {
            //ラジオボタンの状態(ON or OFF)
            bool IsAll = RadioAll.Checked;

            //ラジオボタンの状態に応じて変更
            dtpStartDate.Enabled = !IsAll; //デートタイムピッカーを操作不可にする
            dtpEndDate.Enabled = !IsAll; //デートタイムピッカーを操作不可にする
        }
        /// <summary>
        /// 閉じるボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("終了します。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        /// <summary>
        /// 検索ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //DataStoreを空にする
            Class_DataStore.StockLogs.Clear();

            string pCode = cmbProductCode.Text.Trim(); //商品コード
            string staffName = txtStaffName.Text.Trim(); //担当者名

            //sql文記述
            var sbsql = new StringBuilder();
            {
                string sql =
                    @"SELECT s.""LogDate"",p.""ProductCode"",s.""ProductName"",
                    s.""Category"",s.""Quantity"",s.""StaffName""
                    FROM ""StockLogs"" s
                    INNER JOIN ""Products"" p
                    ON s.""ProductId"" = p.""ProductId""
                    WHERE 1 = 1
                    AND s.""Quantity"" <> 0 ";
                sbsql.AppendLine(sql);
            }

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //空のsqlコマンドを作る
                    using (var cmd = new NpgsqlCommand())
                    {
                        //商品コードが入力されている場合の処理
                        if (!string.IsNullOrWhiteSpace(pCode))
                        {
                            //商品コードが一致した商品の履歴を表示
                            sbsql.AppendLine("AND p.\"ProductCode\" = @ProductCode");
                            cmd.Parameters.AddWithValue("ProductCode", pCode);
                        }
                        //担当者が入力されている場合の処理
                        if (!string.IsNullOrWhiteSpace(staffName))
                        {
                            //担当者名が一致(部分)している商品の履歴を表示
                            sbsql.AppendLine("AND s.\"StaffName\" LIKE @StaffName");
                            cmd.Parameters.AddWithValue("StaffName", $"%{staffName}%");
                        }
                        //期間指定がされている場合の処理
                        if (RadioBetween.Checked == true)
                        {
                            //指定されている期間の履歴を表示
                            sbsql.AppendLine("AND \"LogDate\" BETWEEN @StartDate AND @EndDate");
                            cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
                            cmd.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
                        }
                        //当月分の履歴を表示するラジオボタンがチェックされている場合の処理
                        if (RadioThisMonth.Checked == true)
                        {
                            //当月初日の値を取得する
                            DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            //当月末尾の値を取得する
                            DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);

                            //期間を当月分に設定する
                            sbsql.AppendLine("AND \"LogDate\" BETWEEN @StartDate AND @EndDate");
                            cmd.Parameters.AddWithValue("@StartDate", StartDate);
                            cmd.Parameters.AddWithValue("@EndDate", EndDate);
                        }
                        //入庫日・出庫日の昇順で取得する
                        sbsql.AppendLine("ORDER BY s.\"LogDate\"");

                        //空のコマンドに代入
                        cmd.CommandText = sbsql.ToString();
                        cmd.Connection = conn;

                        //データベースを読み込む
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //データベースから取得した値をセット
                                Class_DataStore.StockLogs.Add(new Class_Log
                                {
                                    LogDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("LogDate")).ToDateTime(TimeOnly.MinValue), //入庫。出庫日
                                    ProductCode = reader["ProductCode"].ToString(), //商品コード
                                    ProductName = reader["ProductName"].ToString(), //商品名
                                    Category = reader["Category"].ToString(), //区分
                                    Quantity = Convert.ToInt32(reader["Quantity"]), //入庫・出庫数量
                                    StaffName = reader["StaffName"].ToString() //担当者名
                                });
                            }

                            //入庫のみ表示が選択された場合
                            if (RadioStockIn.Checked == true)
                            {
                                //表に入庫数が1以上の履歴だけを表示する
                                dgvHistory.DataSource = Class_DataStore.StockLogs
                                    .Where(s => s.Quantity >= 1).ToList();
                            }
                            //出庫のみが選択された場合
                            else if (RadioStockOut.Checked == true)
                            {
                                //表に出庫数が0未満(マイナス)の履歴だけを表示する
                                dgvHistory.DataSource = Class_DataStore.StockLogs
                                    .Where(s => s.Quantity < 0).ToList();
                            }
                            //入庫・出庫が選択された場合
                            else
                            {
                                //表に履歴を表示する
                                dgvHistory.DataSource = Class_DataStore.StockLogs;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"エラーメッセージ：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// ラジオボタン当月分(デフォルト設定)が選択された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            //ラジオボタンの状態(ON or OFF)(デフォルト設定はON)
            bool ThisMonth = RadioThisMonth.Checked;

            //ラジオボタンの状態に応じて変更
            dtpStartDate.Enabled = !ThisMonth; //操作不可にする
            dtpEndDate.Enabled = !ThisMonth; //操作不可にする
        }
        /// <summary>
        /// コンボボックスの商品コードが選択された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //商品コードから商品名を取得する
            txtProductName.Text = Class_DataStore.Products
                .FirstOrDefault(p => p.ProductCode.Equals(cmbProductCode.Text, StringComparison.OrdinalIgnoreCase))
                ?.ProductName ?? "";
        }
    }
}
