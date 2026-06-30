using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static InventoryManagementSystem.Main;

namespace InventoryManagementSystem
{
    public partial class ProductAddForm : Form
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        string? mainConn = Class_DbConfig.ConnectionString;


        public ProductAddForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// キャンセルボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //
            MessageBox.Show("キャンセルします。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            //テキストボックス初期化の関数呼び出し
            txtReset();
            return;
        }

        /// <summary>
        /// 登録ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // 各項目を変数化
            var pCode = txtProductCode.Text; //商品コード
            var pName = txtProductName.Text; //商品名
            var pPrice = txtPrice.Text; //商品単価

            // 各項目のバックカラー変更【初期化】
            var colorTargets = new TextBox[] { txtProductCode, txtProductName, txtPrice };
            foreach (var tbColor in colorTargets)
            {
                tbColor.BackColor = SystemColors.Window;
            }

            // 商品コードの入力チェック
            if (string.IsNullOrWhiteSpace(pCode))
            {
                //メッセージを表示する
                MessageBox.Show("商品コードが未入力です。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //商品コードのテキストボックスのバックカラーを変更する
                txtProductCode.BackColor = Color.MistyRose;
                //商品コードのテキストボックスにカーソルを戻す
                txtProductCode.Focus();
                return;
            }
            // 商品名の入力チェック
            if (string.IsNullOrWhiteSpace(pName))
            {
                //メッセージを表示する
                MessageBox.Show("商品名が未入力です。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //商品コードのテキストボックスのバックカラーを変更する
                txtProductName.BackColor = Color.MistyRose;
                //商品コードのテキストボックスにカーソルを戻す
                txtProductName.Focus();
                return;
            }
            // 商品単価の入力チェック
            if (string.IsNullOrWhiteSpace(pPrice))
            {
                //メッセージを表示する
                MessageBox.Show("商品単価が未入力です。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //単価のテキストボックスのバックカラーを変更する
                txtPrice.BackColor = Color.MistyRose;
                //単価のテキストボックスにカーソルを戻す
                txtPrice.Focus();
                return;
            }
            // 商品単価の数値チェック
            if (!int.TryParse(pPrice, out int price))
            {
                //メッセージを表示する
                MessageBox.Show("商品単価が不正な値です。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //単価のテキストボックスのバックカラーを変更する
                txtPrice.BackColor = Color.MistyRose;
                //単価のテキストボックスにカーソルを戻す
                txtPrice.Focus();
                return;
            }

            // DataStore(Products)に入力した商品コードと同じ商品コードがある場合の処理
            if (Class_DataStore.Products.Any(cd => cd.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase)))
            {
                //メッセージを表示する
                MessageBox.Show("この商品コードは既に登録されています。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //商品コードのテキストボックスのバックカラーを変更する
                txtProductCode.BackColor = Color.MistyRose;
                //商品コードのテキストボックスにカーソルを戻す
                txtProductCode.Focus();
                return;
            }

            //sql文記述用1(商品マスタ)
            var sbsqlProducts = new StringBuilder();
            {
                string sql =
                    @"INSERT INTO ""Products""
                        (""ProductCode"",""ProductName"",""Price"")
                        VALUES
                        (@ProductCode,@ProductName,@Price)
                        RETURNING ""ProductId""";
                sbsqlProducts.AppendLine(sql);
            }
            //sql文記述用2(在庫)
            var sbsqlInventory = new StringBuilder();
            {
                string sql =
                    @"INSERT INTO ""Inventory""
                        (""ProductId"",""ProductStock"")
                        VALUES
                        (@ProductId,@ProductStock)";
                sbsqlInventory.AppendLine(sql);
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
                        //sql文1の実行
                        using (var cmd = new NpgsqlCommand(sbsqlProducts.ToString(), conn))
                        {
                            try
                            {
                                //各項目を登録
                                cmd.Parameters.AddWithValue("@ProductCode", pCode); //商品コード
                                cmd.Parameters.AddWithValue("@ProductName", pName); //商品名
                                cmd.Parameters.AddWithValue("@Price", int.Parse(pPrice)); //単価

                                //取得した自動番号を受け取る
                                var ProductId = cmd.ExecuteScalar();

                                //sql文の実行(2回目)
                                using (var cmd2 = new NpgsqlCommand(sbsqlInventory.ToString(), conn))
                                {
                                    //新しく作ったコマンドを同じトランザクションに
                                    cmd2.Transaction = tran;

                                    //各項目を登録
                                    cmd2.Parameters.AddWithValue("@ProductId", Convert.ToInt32(ProductId)); //商品ID
                                    cmd2.Parameters.AddWithValue("@ProductStock", 0); //デフォルト値：0

                                    cmd2.ExecuteNonQuery();
                                }
                                tran.Commit();
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
            //商品マスタを更新する
            StoreSetProducts();

            //表に表示する
            DisplaySet();

            // 登録メッセージの表示
            MessageBox.Show("商品データを登録しました。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            //テキストボックス初期化
            txtReset();

        }

        /// <summary>
        /// 閉じるボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// テキストボックス初期化の関数
        /// </summary>
        private void txtReset()
        {
            // 各項目初期化
            var emptyTargets = new TextBox[] { txtProductCode, txtProductName, txtPrice };
            foreach (var txtEmpty in emptyTargets)
            {
                txtEmpty.Text = string.Empty;
            }
            //商品コードのテキストボックスにカーソルをセットする
            txtProductCode.Focus();
        }
        /// <summary>
        /// ロード時の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductAddForm_Load(object sender, EventArgs e)
        {
            //商品コードのテキストボックスにカーソルをセットする
            txtProductCode.Focus();
        }
        /// <summary>
        /// 表にデータを表示する関数
        /// </summary>
        private void DisplaySet()
        {
            //共通クラスを呼び出す
            var DataSet = new Class_ProductsDisplaySet();
            //データを取得する(sql処理)
            var Displaylist = DataSet.StoreDisplaySet();
            //DataStoreを空にする
            Class_DataStore.ProductDisplays.Clear();
            //取得した値をセットする
            foreach (var setlist in Displaylist)
            {
                Class_DataStore.ProductDisplays.Add(setlist);
            }
        }
        /// <summary>
        /// 商品マスタのデータを取得する関数
        /// </summary>
        private void StoreSetProducts()
        {
            // 共通クラスを呼び出す
            var DataClass = new Class_Database_Product();

            // データを取得する(sql処理)
            List<Class_Product> GetProducts = DataClass.ProductsAllSet();
            // DataStoreを空にする
            Class_DataStore.Products.Clear();
            //取得した値をセットする
            foreach (var setlist in GetProducts)
            {
                Class_DataStore.Products.Add(setlist);
            }

        }
    }
}