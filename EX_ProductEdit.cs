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
    public partial class ProductEdit : Form
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        public string? mainConn = Class_DbConfig.ConnectionString;


        public ProductEdit()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 検索ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCdSearch_Click(object sender, EventArgs e)
        {
            //コンボボックスに値が入っているかの入力チェック
            if (string.IsNullOrWhiteSpace(cmbProductList.Text))
            {
                MessageBox.Show("検索する商品コードが設定されていません。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //コンボボックスに値が設定されている場合
            else
            {
                //商品コードをテキストボックスにセットする
                txtProductCode.Text = cmbProductList.Text;

                //商品コードを変数化
                var pCode = txtProductCode.Text;

                //DataStore(Products)から一致した商品コードを抽出
                var pNameAndPrice = Class_DataStore.Products
                    .FirstOrDefault(p => p.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase));

                //一致した商品コードが見つからなかった場合の処理
                if (pNameAndPrice == null)
                {
                    //メッセージを表示する
                    MessageBox.Show("指定した商品コードが見つかりませんでした。", "確認",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //商品名と単価をテキストボックスにセットする
                txtProductName.Text = pNameAndPrice.ProductName;
                txtPrice.Text = pNameAndPrice.Price.ToString();
            }
        }
        /// <summary>
        /// コンボボックス初期設定の関数
        /// </summary>
        private void cmbReset()
        {
            //コンボボックス初期化
            cmbProductList.Items.Clear();

            //既存の商品コードを登録
            foreach (var c in Class_DataStore.Products)
            {
                cmbProductList.Items.Add(c.ProductCode);
            }
            //最初の項目を選択状態にする
            cmbProductList.SelectedIndex = 0;
        }
        /// <summary>
        /// ロード時の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductEdit_Load(object sender, EventArgs e)
        {
            //コンボボックスを読み取り専用にする
            cmbProductList.DropDownStyle = ComboBoxStyle.DropDownList;
            //テキストボックスの初期設定関数呼び出し
            txtReset();
            //コンボボックスの初期設定関数呼び出し
            cmbReset();
        }
        /// <summary>
        /// キャンセルボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //メッセージを表示する
            MessageBox.Show("キャンセルされました。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            //テキストボックス初期化の関数呼び出し
            txtReset();
        }
        /// <summary>
        /// 登録ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            //各項目を変数化
            var pCode = txtProductCode.Text; //商品コード
            var pName = txtProductName.Text; //商品名
            int pPrice; //単価

            //商品名が入力されていない場合の処理
            if (string.IsNullOrWhiteSpace(pName))
            {
                //メッセージを表示する
                MessageBox.Show("商品名が未入力です。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //バックカラー変更
                txtProductName.BackColor = Color.MistyRose;
                //商品名のテキストボックスにカーソルを戻す
                txtProductName.Focus();
                return;
            }

            //商品単価が入力されていない場合の処理
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                //メッセージを表示する
                MessageBox.Show("商品単価が未入力です。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //バックカラー変更
                txtPrice.BackColor = Color.MistyRose;
                //商品単価のテキストボックスにカーソルを戻す
                txtPrice.Focus();
                return;
            }
            //商品単価が数値でない場合の処理
            if (!int.TryParse(txtPrice.Text, out pPrice))
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

            //DataStore(Products)から一致する商品コードを取得
            var selectedData = Class_DataStore.Products
                .FirstOrDefault(c => c.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase));

            //更新データが見つからなかった場合の処理
            if (selectedData == null)
            {
                //メッセージを表示する
                MessageBox.Show("商品データが見つかりませんでした。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //テキストボックスのバックカラーを戻す
            txtProductName.BackColor = SystemColors.Window;
            txtPrice.BackColor = SystemColors.Window;

            //sql文記述用
            var sbsql = new StringBuilder();

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();

                    //トランザクション開始
                    using (var tran = conn.BeginTransaction())
                    {
                        //sql文記述
                        {
                            string sql =
                                @"UPDATE ""Products"" SET
                                ""ProductName"" = @ProductName,
                                ""Price"" = @Price
                                WHERE
                                ""ProductCode"" = @ProductCode";
                            sbsql.AppendLine(sql);
                        }

                        //sql文を実行
                        using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                        {
                            try
                            {
                                //データベースを更新
                                cmd.Parameters.AddWithValue("@ProductCode", pCode); //商品コード
                                cmd.Parameters.AddWithValue("@ProductName", pName); //商品名
                                cmd.Parameters.AddWithValue("@Price", pPrice); //単価

                                cmd.ExecuteNonQuery();
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
            //DataStoreを更新する。
            selectedData.ProductName = pName; //商品名
            selectedData.Price = pPrice; //単価

            //表示画面のデータを取得する
            DisplaySet();
            //表の反映
            Class_DataStore.ProductDisplays.ResetBindings();

            //テキストボックス初期設定関数呼び出し
            txtReset();
            //コンボボックス初期設定関数呼び出し
            cmbReset();
            cmbProductList.Focus();

            //メッセージを表示
            MessageBox.Show("商品データを更新しました。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        /// テキストボックス初期設定の関数
        /// </summary>
        private void txtReset()
        {
            //テキストボックスの各項目を初期化
            var txtboxs = new TextBox[] { txtProductCode, txtProductName, txtPrice };
            foreach (var box in txtboxs)
            {
                box.Text = string.Empty;
            }
        }
        /// <summary>
        /// 表にデータを表示する関数
        /// </summary>
        private void DisplaySet()
        {
            //共通クラスを呼び出す
            var DataClass = new Class_ProductsDisplaySet();
            //データを取得する(sql処理)
            var DisplayList = DataClass.StoreDisplaySet();
            //DataStoreを空にする
            Class_DataStore.ProductDisplays.Clear();
            //取得した値をセットする
            foreach (var setlist in DisplayList)
            {
                Class_DataStore.ProductDisplays.Add(setlist);
            }
        }
    }
}
