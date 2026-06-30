using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static InventoryManagementSystem.Main;

namespace InventoryManagementSystem
{
    public partial class StockOutForm : Form
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        /// 
        string? mainConn = Class_DbConfig.ConnectionString;
        public StockOutForm()
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
        private void StockOutForm_Load(object sender, EventArgs e)
        {
            //コンボボックスに値をセット
            cmbSet();
            //テキストボックスを初期化
            txtReset();
            //デートタイムピッカー(StockOutDate)の値を当日に設定する
            StockOutDate.Value = DateTime.Now;
            //表に自動で列が増えないように制限する
            dgvStockOut.AutoGenerateColumns = false;
            //表に初期データをセット
            DataSet();
        }
        /// <summary>
        /// コンボボックス初期設定の関数
        /// </summary>
        private void cmbSet()
        {
            //コンボボックス初期化
            cmbProductCd.Items.Clear();

            //コンボボックスに値をセット(商品コード)
            foreach (var c in Class_DataStore.Products)
            {
                cmbProductCd.Items.Add(c.ProductCode);
            }
            //コンボボックスの初期インデックスを設定
            cmbProductCd.SelectedIndex = 0;
        }
        /// <summary>
        /// テキストボックス初期化の関数
        /// </summary>
        private void txtReset()
        {
            //テキストボックスをまとめる
            var txtboxs = new TextBox[] { txtProductCode, txtProductName, txtCurrentStock, txtStockOut, txtStaffName };

            //各テキストボックスを初期化
            foreach (var t in txtboxs)
            {
                t.Text = string.Empty;
            }
            //商品コードのコンボボックスにカーソルを設定する
            cmbProductCd.Focus();
        }
        /// <summary>
        /// 検索ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //テキストボックスに選択した商品コードをセット
            txtProductCode.Text = cmbProductCd.Text;
            string pCode = txtProductCode.Text; //商品コード

            //DataStore(Products)から商品コードを探す
            var foudName = Class_DataStore.Products
                .FirstOrDefault(p => p.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase));

            //商品コードが見つからなかった場合の処理
            if (foudName == null)
            {
                //メッセージを表示する
                MessageBox.Show("該当の商品が見つかりませんでした。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //各テキストボックスを初期化
                txtReset();
                //コンボボックスにフォーカス
                cmbProductCd.Focus();
                return;
            }

            //商品名を取得
            txtProductName.Text = foudName.ProductName;

            //商品IDから在庫数を取得する
            var Currquan = Class_DataStore.Inventory
                .Where(p => p.ProductId == foudName.ProductId)
                .Select(p => p.ProductStock).Sum();

            //現在の在庫数を取得
            txtCurrentStock.Text = Currquan.ToString();
        }

        /// <summary>
        /// 出庫登録ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStockOut_Click(object sender, EventArgs e)
        {
            //各項目を変数化
            int ProductId; //商品ID
            string ProductCode = txtProductCode.Text; //商品コード
            string ProductName = txtProductName.Text; //商品名
            int quan; //出庫数
            DateTime OutDate = StockOutDate.Value; //出庫日
            string staff = txtStaffName.Text.Trim(); //担当者
            string Category = "出庫"; //カテゴリー

            //数量が入力されていない場合の処理
            if (string.IsNullOrWhiteSpace(txtStockOut.Text))
            {
                //メッセージを表示する
                MessageBox.Show("出庫数量が入力されていません。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //バックカラー変更
                txtStockOut.BackColor = Color.MistyRose;
                //出庫数のテキストボックスにカーソルを戻す
                txtStockOut.Focus();
                return;
            }
            //出庫数の入力値が数値でない場合の処理
            if (!int.TryParse(txtStockOut.Text, out quan))
            {
                //メッセージを表示する
                MessageBox.Show("入力値が不正です。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //バックカラー変更
                txtStockOut.BackColor = Color.MistyRose;
                //テキストボックスを空にする
                txtStockOut.Text = string.Empty;
                //出庫数のテキストボックスにカーソルを戻す
                txtStockOut.Focus();
                return;
            }

            //商品コードから商品IDを取得
            var foundProduct = Class_DataStore.Products
                .FirstOrDefault(p => p.ProductCode.Equals(ProductCode, StringComparison.OrdinalIgnoreCase));
            if (foundProduct == null) return;
            ProductId = foundProduct.ProductId; //商品ID

            //出庫数が1以上かチェックする
            if (quan <= 0)
            {
                //メッセージを表示する
                MessageBox.Show("出庫可能数は1以上です。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //バックカラー変更
                txtStockOut.BackColor = Color.MistyRose;
                //出庫数のテキストボックスにカーソルを戻す
                txtStockOut.Focus();
                return;
            }
            //出庫数を正の値から負の値へ変換する
            quan = quan * -1;

            //在庫数が0未満になる場合は出庫できないようにする
            //検索した商品コードの現在の在庫数を調べる
            var CurrentStock = Class_DataStore.Inventory
                .Where(p => p.ProductId == ProductId)
                .Sum(p => p.ProductStock);

            //出庫数が在庫数を越えている場合の処理
            if (CurrentStock + quan < 0)
            {
                MessageBox.Show("在庫数量を越えた出庫はできません。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //バックカラー変更
                txtStockOut.BackColor = Color.MistyRose;
                //出庫数のテキストボックスにカーソルを戻す
                txtStockOut.Focus();
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
            //バックカラーを戻す
            txtStockOut.BackColor = SystemColors.Window;
            txtStaffName.BackColor = SystemColors.Window;

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
                        //sql文記述用1(ログ：StockLogs)
                        var sbsqlLog = new StringBuilder();
                        //sql記述用2(在庫：Inventory)
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

                        //sql文実行(StockLogs)
                        using (var cmd = new NpgsqlCommand(sbsqlLog.ToString(), conn))
                        {
                            try
                            {
                                //各項目をインサート(出庫)
                                cmd.Parameters.AddWithValue("@ProductId", ProductId); //商品ID
                                cmd.Parameters.AddWithValue("@ProductCode", ProductCode); //商品コード
                                cmd.Parameters.AddWithValue("@ProductName", ProductName); //商品名
                                cmd.Parameters.AddWithValue("@Price", foundProduct.Price); //単価
                                cmd.Parameters.AddWithValue("@Category", Category); //区分
                                cmd.Parameters.AddWithValue("@Quantity", quan); //出庫数
                                cmd.Parameters.AddWithValue("@LogDate", OutDate); //出庫日
                                cmd.Parameters.AddWithValue("@StaffName", staff); //担当者

                                //StockLogsのsqlコマンドを終了する
                                cmd.ExecuteNonQuery();

                                //sql文実行(Inventory)
                                using (var cmd2 = new NpgsqlCommand(sbsqlInventory.ToString(), conn))
                                {
                                    //トランザクションを紐づけ
                                    cmd2.Transaction = tran;

                                    //データベースを更新する
                                    cmd2.Parameters.AddWithValue("@Quantity", quan); //在庫数
                                    cmd2.Parameters.AddWithValue("@ProductId", ProductId); //商品ID

                                    //Inventoryのsqlコマンドを終了する
                                    cmd2.ExecuteNonQuery();
                                }
                                tran.Commit();

                                //DataStoreを更新&表に反映
                                DataSet();
                                //在庫数を更新する
                                UpDateStock();
                                //テキストボックス初期化
                                txtReset();

                                //出庫メッセージを表示
                                MessageBox.Show("出庫しました。", "確認",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 初期データの設定
        /// </summary>
        private void DataSet()
        {
            //DataStoreを初期化する
            Class_DataStore.StockLogs.Clear();

            //sql文記述用
            var sbsql = new StringBuilder();
            {
                string sql =
                    @"SELECT ""ProductId"",""ProductCode"",""ProductName"",""Price"",
                            ""Quantity"",""LogDate"",""StaffName""
                            FROM ""StockLogs""
                            ORDER BY ""ProductId""";
                sbsql.AppendLine(sql);
            }

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
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
                                    ProductName = reader["ProductName"].ToString(), //商品名
                                    Price = Convert.ToInt32(reader["Price"]), //単価
                                    Quantity = Convert.ToInt32(reader["Quantity"]), //出庫数
                                    LogDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("LogDate")).ToDateTime(TimeOnly.MinValue), //出庫日
                                    StaffName = reader["StaffName"].ToString() //担当者
                                });
                            }
                            //表に負の入庫(出庫)数だけを表示
                            dgvStockOut.DataSource = Class_DataStore.StockLogs
                                .Where(s => s.Quantity < 0)
                                .Select(s => new
                                {
                                    s.ProductId, //商品ID
                                    s.ProductCode, //商品コード
                                    s.ProductName, //商品名
                                    s.Price, //単価
                                    Quantity = Math.Abs(s.Quantity), //出庫数(絶対値変換したもの)
                                    s.LogDate, //出庫日
                                    s.StaffName //担当者
                                }).ToList();
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
                                //各項目をDataStoreに追加する
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
