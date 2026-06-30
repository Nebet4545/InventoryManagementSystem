using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static InventoryManagementSystem.Main;


namespace InventoryManagementSystem
{
    public partial class StockInForm : Form
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        /// 
        string? mainConn = Class_DbConfig.ConnectionString;

        public StockInForm()
        {
            InitializeComponent();
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
        /// ロード時の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockInForm_Load(object sender, EventArgs e)
        {
            //コンボボックスに値をセット
            cmbReset();
            //テキストボックス初期化
            txtReset();
            //デートタイムピッカー(StockInDate)の値を当日に設定する
            StockInDate.Value = DateTime.Now;
            //表に自動で列が増えないように制限する
            dgvStockIn.AutoGenerateColumns = false;
            //表に初期データをセット
            DataSet();
        }
        /// <summary>
        /// コンボボックスの初期設定関数
        /// </summary>
        private void cmbReset()
        {
            //コンボボックス初期化
            cmbProductCd.Items.Clear();

            //コンボボックスに値をセット(商品コード)
            foreach (var c in Class_DataStore.Products)
            {
                cmbProductCd.Items.Add(c.ProductCode);
            }
            //初期インデックスを設定
            cmbProductCd.SelectedIndex = 0;
        }
        /// <summary>
        /// 検索ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //入力チェック
            if (string.IsNullOrWhiteSpace(cmbProductCd.Text))
            {
                //メッセージ表示
                MessageBox.Show("商品コードが選択されていません。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //テキストボックスに選択した商品コードをセット
            txtProductCode.Text = cmbProductCd.Text;

            string pCode = txtProductCode.Text; //商品コード

            //DataStore(Products)から商品コードを探す
            var foundProductCode = Class_DataStore.Products
                .FirstOrDefault(n => n.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase));

            //商品コードが見つからなかった場合の処理
            if (foundProductCode == null)
            {
                MessageBox.Show("該当の商品が見つかりませんでした。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //各テキストボックスを初期化
                txtReset();
                //コンボボックスにフォーカス
                cmbProductCd.Focus();
                return;
            }

            //商品名を取得
            txtProductName.Text = foundProductCode.ProductName.ToString();
            //商品コードから在庫数を取得
            var Currquan = Class_DataStore.Inventory
                .Where(p => p.ProductId == foundProductCode.ProductId)
                .Sum(x => x.ProductStock);

            //現在の在庫数をセット
            txtCurrentStock.Text = Currquan.ToString();
        }
        /// <summary>
        /// テキストボックス初期化の関数
        /// </summary>
        private void txtReset()
        {
            //各テキストボックスを配列化
            var txtboxs = new TextBox[] { txtProductCode, txtProductName, txtCurrentStock, txtStockIn, txtStaffName };

            //各テキストボックスを初期化
            foreach (var t in txtboxs)
            {
                t.Text = string.Empty;
            }
        }
        /// <summary>
        /// 入庫登録ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStockIn_Click(object sender, EventArgs e)
        {
            //各項目を変数化
            string pCode = txtProductCode.Text; //商品コード
            int quan; //入庫数
            DateTime InDate = StockInDate.Value; //入庫日
            string staff = txtStaffName.Text.Trim();//担当者名
            string Category = "入庫"; //カテゴリー

            //入庫数が設定されているかチェック
            if (string.IsNullOrWhiteSpace(txtStockIn.Text))
            {
                //メッセージ表示
                MessageBox.Show("入庫数量が入力されていません。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //バックカラー変更
                txtStockIn.BackColor = Color.MistyRose;
                //入庫数のテキストボックスにカーソルを戻す
                txtStockIn.Focus();
                return;
            }
            //入庫数が数値じゃない場合の処理
            if (!int.TryParse(txtStockIn.Text, out quan))
            {
                //メッセージを表示する
                MessageBox.Show("入力値が不正です。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //バックカラー変更
                txtStockIn.BackColor = Color.MistyRose;
                //テキストボックスを空にする
                txtStockIn.Text = string.Empty;
                //入庫数のテキストボックスにカーソルを戻す
                txtStockIn.Focus();
                return;
            }
            //入庫数が1以上かチェックする
            if (quan <= 0)
            {
                //メッセージを表示する
                MessageBox.Show("入庫可能数は1以上です。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //バックカラー変更
                txtStockIn.BackColor = Color.MistyRose;
                //入庫数のテキストボックスにカーソルを戻す
                txtStockIn.Focus();
                return;
            }

            //担当者が入力されているかチェックする
            if (string.IsNullOrWhiteSpace(txtStaffName.Text))
            {
                //入力 or　未入力のメッセージを表示
                var result = MessageBox.Show($"担当者が入力されていません。{Environment.NewLine}" +
                    $"未入力として登録しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //Yesが選択された場合の処理
                if (result == DialogResult.Yes)
                {
                    //入力文字を"未入力"として処理する
                    staff = "未入力";
                    //担当者のテキストボックスのバックカラーを戻す
                    txtStaffName.BackColor = SystemColors.Window;
                }
                //Noが選択された場合の処理
                else
                {
                    //メッセージを表示
                    MessageBox.Show("名前を入力してください。", "確認",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //テキストボックス初期化
                    txtStaffName.Text = string.Empty;
                    //バックカラー変更
                    txtStaffName.BackColor = Color.MistyRose;
                    //担当者のテキストボックスにカーソルを戻す
                    txtStaffName.Focus();
                    return;
                }
            }
            //入力されていればバックカラーを戻す
            txtStockIn.BackColor = SystemColors.Window;
            txtStaffName.BackColor = SystemColors.Window;

            //商品コードから商品IDを取得
            var foundProduct = Class_DataStore.Products
                .FirstOrDefault(p => p.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase));
            if (foundProduct == null) return;
            int ProductId = foundProduct.ProductId; //商品ID

            //sql文記述用1(ログ：StockLogs)
            var sbsqlLog = new StringBuilder();
            //sql文記述用2(在庫：Inventory)
            var sbsqlInventory = new StringBuilder();
            {
                string sql1 =
                        @"INSERT INTO ""StockLogs""
                            (""ProductId"",""ProductCode"",""ProductName"",""Price"",""Category"",
                                ""Quantity"",""LogDate"",""StaffName"")
                            VALUES
                            (@ProductId,@ProductCode,@ProductName,@Price,@Category,
                            @Quantity,@LogDate,@StaffName)";
                string sql2 =
                    @"UPDATE ""Inventory""
                        SET ""ProductStock"" = ""ProductStock"" + @Quantity
                        WHERE ""ProductId"" = @ProductId";
                sbsqlLog.AppendLine(sql1);
                sbsqlInventory.AppendLine(sql2);
            }

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //トランザクション開始
                    using (NpgsqlTransaction tran = conn.BeginTransaction())
                    {
                        //sql文実行(StockLogs)
                        using (var cmd = new NpgsqlCommand(sbsqlLog.ToString(), conn))
                        {
                            try
                            {
                                //各項目をインサート(入庫)
                                cmd.Parameters.AddWithValue("@ProductId", ProductId); //商品ID
                                cmd.Parameters.AddWithValue("@ProductCode", foundProduct.ProductCode); //商品コード
                                cmd.Parameters.AddWithValue("@ProductName", foundProduct.ProductName); //商品名
                                cmd.Parameters.AddWithValue("@Price", foundProduct.Price); //単価
                                cmd.Parameters.AddWithValue("@Category", Category); //区分
                                cmd.Parameters.AddWithValue("@Quantity", quan); //入庫数
                                cmd.Parameters.AddWithValue("@LogDate", InDate); //入庫日
                                cmd.Parameters.AddWithValue("@StaffName", staff); //担当者

                                //StockLogsのsqlコマンドを終了する
                                cmd.ExecuteNonQuery();

                                //sql文実行(Inventory)
                                using (var cmd2 = new NpgsqlCommand(sbsqlInventory.ToString(), conn))
                                {
                                    //トランザクションを紐づけ
                                    cmd2.Transaction = tran;

                                    //データベースを更新する(Inventory)
                                    cmd2.Parameters.AddWithValue("@Quantity", quan); //入庫数
                                    cmd2.Parameters.AddWithValue("@ProductId", ProductId); //商品ID

                                    //Inventoryのsqlコマンドを終了する
                                    cmd2.ExecuteNonQuery();
                                }
                                tran.Commit();

                                //DataStore(StockLogs)の更新＆表の更新
                                DataSet();
                                //在庫数を更新する
                                UpDateStock();
                                //入庫時のメッセージを表示
                                MessageBox.Show("入庫しました。", "確認",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //テキストボックス初期化
                                txtReset();
                            }
                            catch (Exception ex2)
                            {
                                MessageBox.Show($"エラーメッセージ：{ex2.Message}");
                                tran.Rollback();
                            }
                        }
                    }
                }
                catch (Exception ex1)
                {
                    MessageBox.Show($"エラーメッセージ：{ex1.Message}");
                }
            }
        }
        /// <summary>
        /// 表データの設定を行う関数
        /// </summary>
        private void DataSet()
        {
            //DataStoreを初期化する
            Class_DataStore.StockLogs.Clear();

            //sql文記述用
            var sbsql = new StringBuilder();

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //sql文記述
                    {
                        string sql =
                            @"SELECT ""ProductId"",""ProductCode"",""ProductName"",""Price"",
                            ""Quantity"",""LogDate"",""StaffName""
                            FROM ""StockLogs""
                            ORDER BY ""ProductId""";
                        sbsql.Append(sql);
                    }
                    //sql文実行
                    using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                    {
                        //データベース読み込み
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //DataStoreに値をセット
                                Class_DataStore.StockLogs.Add(new Class_Log
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductCode = Convert.ToString(reader["ProductCode"]), //商品コード
                                    ProductName = Convert.ToString(reader["ProductName"]), //商品名
                                    Price = Convert.ToInt32(reader["Price"]), //単価
                                    Quantity = Convert.ToInt32(reader["Quantity"]), //入庫数
                                    LogDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("LogDate")).ToDateTime(TimeOnly.MinValue), //入庫日
                                    StaffName = Convert.ToString(reader["StaffName"]) //担当者
                                });
                            }
                            //表に正の入庫数だけを表示
                            dgvStockIn.DataSource = Class_DataStore.StockLogs
                                .Where(s => s.Quantity > 0).ToList();
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
        /// 在庫数を更新する関数
        /// </summary>
        private void UpDateStock()
        {
            //DataStoreを空にする
            Class_DataStore.Inventory.Clear();

            //sql文記述用
            var sbsql = new StringBuilder();
            {
                string sql =
                    @"SELECT * FROM ""Inventory""";
                sbsql.AppendLine(sql);
            }
            //データベースに接続する
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //sql文を実行する
                    using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                    {
                        //データベースを読み込む
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //各項目をDataStoreに追加
                                Class_DataStore.Inventory.Add(new Class_Inventory
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductStock = Convert.ToInt32(reader["ProductStock"]) //在庫数
                                });
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
    }
}
