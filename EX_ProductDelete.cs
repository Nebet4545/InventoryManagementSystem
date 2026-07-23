using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static InventoryManagementSystem.Main;


namespace InventoryManagementSystem
{
    /// <summary>
    /// 商品データ削除画面
    /// </summary>
    public partial class ProductDelete : Form
    {
        public ProductDelete()
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
            this.Close();
        }

        /// <summary>
        /// ロード時の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductDelete_Load(object sender, EventArgs e)
        {
            //コンボボックス初期設定関数呼び出し
            cmbReset();
            //テキストボックス初期化の関数呼び出し
            txtReset();
        }
        /// <summary>
        /// コンボボックス初期設定の関数
        /// </summary>
        private void cmbReset()
        {
            //コンボボックス初期化
            cmbProductList.Items.Clear();
            //コンボボックスに値をセット
            foreach (var c in Class_DataStore.Products)
            {
                cmbProductList.Items.Add(c.ProductCode);
            }
            //コンボボックスの初期インデックスを設定
            cmbProductList.SelectedIndex = 0;
            //コンボボックスにカーソルをセットする
            cmbProductList.Focus();
        }
        /// <summary>
        /// テキストボックス初期化の関数
        /// </summary>
        private void txtReset()
        {
            //初期化するテキストボックスを配列化
            var txtboxs = new TextBox[] { txtProductCode, txtProductName, txtPrice };
            //テキストボックスを初期化
            foreach (var t in txtboxs)
            {
                t.Text = string.Empty;
            }
        }
        /// <summary>
        /// 検索ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCdSearch_Click(object sender, EventArgs e)
        {
            var pCode = cmbProductList.Text.Trim(); //商品コード
            string ErrMsg = "";  //エラーメッセージ

            //選択した商品コードの商品名を取得する
            var repo = new Class_ProductDef();
            if (!repo.TryGetProductValues(pCode, out _, out string ProductName, out int Price, out ErrMsg))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //各テキストボックスに取得した値をセットする

            txtProductCode.Text = pCode; //商品コード
            txtProductName.Text = ProductName; //商品名
            txtPrice.Text = Price.ToString(); //商品単価

        }
        /// <summary>
        /// 削除ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //検索処理が行われていない場合
            if (string.IsNullOrWhiteSpace(txtProductCode.Text))
            {
                //エラーメッセージを表示する
                MessageBox.Show("各項目が表示されていません。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string pCode = txtProductCode.Text.Trim(); //商品コード

            //商品コードから商品IDを取得する
            var repo = new Class_ProductDef();

            //商品IDが取得出来なかった場合の処理
            if (!repo.GetProductId(pCode,out int ProductId, out string ErrMsg))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //テキストボックス初期化の関数呼び出し
                txtReset();
                return;
            }

            //選択した商品の現在の在庫数を取得する
            int stock = repo.GetCurrentStock(pCode);

            //在庫数が存在する場合の処理
            if (stock > 0)
            {
                //メッセージを表示
                MessageBox.Show($"エラーメッセージ：選択された商品は在庫が存在します。{Environment.NewLine}" +
                $"削除することはできません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //テキストボックス初期化の関数呼び出し
                txtReset();
                //コンボボックスにカーソルを戻す
                cmbProductList.Focus();
                return;
            }

            //商品データ削除時の最終チェックを行う
            if (!repo.FinalCheck("データ削除", out ErrMsg, $"選択された商品の在庫は 0 です。商品情報を削除してもよろしいですか？{Environment.NewLine}※履歴データは保持されます。"))
            {
                //メッセージを表示する
                MessageBox.Show($"案内：{ErrMsg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //テキストボックス初期化の関数呼び出し
                txtReset();
                return;
            }

            //商品データを削除し、データベースの更新を行う
            try
            {
                //クラスを呼び出す
                var ProductDelete = new Class_Database_Product();
                //引数を指定して、商品データの削除処理を行う(sql処理)
                ProductDelete.ProductDelete(ProductId,out string Msg);

                //商品データの削除が正常に行われた場合、メッセージを表示する
                MessageBox.Show($"案内：{Msg}","確認",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。","確認",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}","確認",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            //コンボボックス初期化の関数呼び出し
            cmbReset();
            //テキストボックス初期化の関数呼び出し
            txtReset();

            //商品マスタのデータを更新する
            StoreSetProducts();
            //表示画面のデータを取得する
            StoreSetDisplay();
            //表に反映
            Class_DataStore.ProductDisplays.ResetBindings();
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
            return;
        }
        /// <summary>
        /// 表示用のデータを取得する関数
        /// </summary>
        private void StoreSetDisplay()
        {
            try
            {
                //共通のクラスを呼び出す
                var DataClass = new Class_ProductsDisplaySet();
                //データを取得する(sql処理)
                var GetDisplay = DataClass.StoreDisplaySet();
                //DataStoreを空にする
                Class_DataStore.ProductDisplays.Clear();
                //取得した値をセットする
                foreach (var setlist in GetDisplay)
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
        /// <summary>
        /// 商品マスタのデータを取得する関数
        /// </summary>
        private void StoreSetProducts()
        {
            try
            {
                //共通のクラスを呼び出す
                var DataClass = new Class_Database_Product();
                //データを取得する(sql処理)
                var GetProducts = DataClass.ProductsAllSet();
                //DataStoreを空にする
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

