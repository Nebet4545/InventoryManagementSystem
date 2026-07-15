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
    /// <summary>
    /// 商品データ登録画面
    /// </summary>
    public partial class ProductAddForm : Form
    {
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

            //商品データを追加登録し、データベースの更新を行う
            try
            {
                //クラスを呼び出す
                var ProductAdd = new Class_Database_Product();
                //引数を指定して商品の登録処理を行う(sql処理)
                ProductAdd.ProductAdd(pCode, pName, price, out string Msg);

                //商品データの登録が正常に行われた場合、メッセージを表示する
                MessageBox.Show($"{Msg}");
            }
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。");
                return;
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}");
                return;
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
            try
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
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。");
                return;
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}");
                return;
            }
        }
        /// <summary>
        /// 商品マスタのデータを取得する関数
        /// </summary>
        private void StoreSetProducts()
        {
            try
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
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。");
                return;
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}");
                return;
            }
        }
    }
}