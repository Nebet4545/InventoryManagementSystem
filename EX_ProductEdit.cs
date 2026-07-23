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
    /// <summary>
    /// 商品データ編集＆更新画面
    /// </summary>
    public partial class ProductEdit : Form
    {
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
            //商品コード
            string ProductCode = cmbProductList.Text.Trim();

            //クラス呼び出し
            var repo = new Class_ProductDef();

            //商品コードが選択されているかをチェックする
            if (!repo.isValidProductCode(ProductCode, out string ErrMsg))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //商品コードをテキストボックスにセットする
            txtProductCode.Text = ProductCode;

            //選択した商品コードから商品名と単価を取得する
            if (!repo.TryGetProductValues(ProductCode, out _, out string ProductName, out int Price, out ErrMsg))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //各テキストボックスを初期化する
                txtReset();
                return;
            }

            //商品名と単価をテキストボックスにセットする
            txtProductName.Text = ProductName;
            txtPrice.Text = Price.ToString();
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
            MessageBox.Show("案内：キャンセルされました。", "確認",
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
            var pCode = txtProductCode.Text.Trim(); //商品コード
            var pName = txtProductName.Text.Trim(); //商品名
            int Price = 0; //単価

            string pPrice = txtPrice.Text.Trim();

            //クラス呼び出し
            var repo = new Class_ProductDef();

            //商品名が入力されていない場合の処理
            if (!repo.IsValidProductName(pName, out string ErrMsg))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //バックカラー変更
                txtProductName.BackColor = Color.MistyRose;
                //商品名のテキストボックスにカーソルを戻す
                txtProductName.Focus();
                return;
            }

            //商品単価が入力されていない場合の処理
            if (!repo.IsValidProductPrice(pPrice, out ErrMsg))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //バックカラー変更
                txtPrice.BackColor = Color.MistyRose;
                //商品単価のテキストボックスにカーソルを戻す
                txtPrice.Focus();
                return;
            }
            //商品単価が数値でない場合の処理
            if (!repo.ConvertPrice(pPrice, out ErrMsg, out Price))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                //単価のテキストボックスのバックカラーを変更する
                txtPrice.BackColor = Color.MistyRose;
                //単価のテキストボックスにカーソルを戻す
                txtPrice.Focus();
                return;
            }

            //テキストボックスのバックカラーを戻す
            txtProductName.BackColor = SystemColors.Window;
            txtPrice.BackColor = SystemColors.Window;

            //商品データ更新時の最終チェックを行う

            if (!repo.FinalCheck("データ更新", out ErrMsg))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //各テキストボックスを初期化する
                txtReset();
                return;
            }

            //商品データを編集＆更新を行う
            try
            {
                //クラスを呼び出す
                var ProductEdit = new Class_Database_Product();
                //引数を指定して、商品データの編集及び更新を行う(sql処理)
                ProductEdit.ProductEdit(pCode, pName, Price, out string Msg);

                //商品データの更新が正常に行われた場合、メッセージを表示する
                MessageBox.Show($"案内：{Msg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //DataStore(Products)から一致する商品コードを取得
            var selectedData = Class_DataStore.Products
                .FirstOrDefault(c => c.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase));

            //更新データが見つからなかった場合の処理
            if (selectedData == null)
            {
                //エラーメッセージを表示する
                MessageBox.Show("エラーメッセージ：データの更新は完了しましたが、画面表示の更新に失敗しました。画面を開き直してください。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //DataStoreを更新する。
            selectedData.ProductName = pName; //商品名
            selectedData.Price = Price; //単価

            //表示画面のデータを取得する
            DisplaySet();
            //表の反映
            Class_DataStore.ProductDisplays.ResetBindings();

            //テキストボックス初期設定関数呼び出し
            txtReset();
            //コンボボックス初期設定関数呼び出し
            cmbReset();
            cmbProductList.Focus();
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
            try
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
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
