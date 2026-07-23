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
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace InventoryManagementSystem
{
    /// <summary>
    /// 商品入庫画面
    /// </summary>
    public partial class StockInForm : Form
    {
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
            MessageBox.Show("案内：終了します。", "確認",
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
            string ErrMsg = ""; //エラーメッセージ
            //選択した商品コードを変数化する
            string cmbProductCode = cmbProductCd.Text.Trim();

            //クラスを呼び出す
            var repo = new Class_ProductDef();
            //商品コードが選択されているかをチェックする
            if (!repo.isValidProductCode(cmbProductCode, out ErrMsg))
            {
                //商品コードが選択されていない場合、エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //テキストボックスに選択した商品コードをセット
            txtProductCode.Text = cmbProductCode;
            string pCode = txtProductCode.Text; //商品コード

            //選択された商品コードの商品名をチェックする
            if (!repo.ProductNameCheck(pCode, out string ProductName, out ErrMsg)) 
            {
                //該当の商品が見つからなかった場合、エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}","確認",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //各テキストボックスを初期化
                txtReset();
                //コンボボックスにフォーカス
                cmbProductCd.Focus();
                return;
            }
            else
            {
                //商品名をセットする
                txtProductName.Text = ProductName;
            }
            //選択された商品コードの現在の在庫数を取得する
            int Curr = repo.GetCurrentStock(pCode);
            //現在の在庫数をセットする
            txtCurrentStock.Text = Curr.ToString();
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

            //エラーメッセージ
            string ErrMsg = "";
            string Stockinquan = txtStockIn.Text.Trim(); //入庫数

            //クラスを呼び出す
            var repo = new Class_ProductDef();

            //入庫数が適正の値かチェックする
            if (!repo.IsStockCheck(Stockinquan,Category,out ErrMsg,out quan))
            {
                //入庫数が適正でない場合、エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}","確認",MessageBoxButtons.OK,MessageBoxIcon.Error);
                //バックカラー変更
                txtStockIn.BackColor = Color.MistyRose;
                //入庫数のテキストボックスにカーソルを戻す
                txtStockIn.Focus();
                return;
            }

            //担当者名を代入する変数を生成する
            string StaffName = ""; 

            //担当者が入力されているかをチェックする
            if (repo.IsStaffNameCheck(staff))
            {
                //入力 or　未入力のメッセージを表示
                var result = MessageBox.Show($"案内：担当者が入力されていません。{Environment.NewLine}" +
                    $"未入力として登録しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //Yesが選択された場合の処理
                if (result == DialogResult.Yes)
                {
                    //担当者名を"未入力"として処理する
                    StaffName = "未入力";
                }
                //Noが選択された場合の処理
                else
                {
                    //担当者が入力されておらず、"未入力"で登録が選択されなかった場合、エラーメッセージを表示する
                    MessageBox.Show("エラーメッセージ：名前を入力してください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //テキストボックス初期化
                    txtStaffName.Text = string.Empty;
                    //バックカラー変更
                    txtStaffName.BackColor = Color.MistyRose;
                    //担当者のテキストボックスにカーソルを戻す
                    txtStaffName.Focus();
                    return;
                }
            }
            //担当者が入力されている場合
            else
            {
                //担当者名を代入する
                StaffName = staff;
            }

            //入力されていればバックカラーを戻す
            txtStockIn.BackColor = SystemColors.Window;
            txtStaffName.BackColor = SystemColors.Window;

            //商品コードから商品IDを取得する

            //商品IDを取得できなかった(商品が存在しない)場合の処理
            if (!repo.TryGetProductValues(pCode, out int ProductId, out string ProductName, out int Price, out ErrMsg))
            {
                //エラーメッセージを表示する
                MessageBox.Show($"エラーメッセージ：{ErrMsg}", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //商品の入庫処理を行う(クラス呼び出し)
            var repo2 = new Class_DatabaseStockLogs(); 
            try
            {
                repo2.SaveToDatabase(ProductId, pCode, ProductName, Price, Category, quan, InDate, StaffName);

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
            //DataStore(StockLogs)の更新＆表の更新
            DataSet();
            //在庫数を更新する
            UpDateStock();
            //入庫時のメッセージを表示
            MessageBox.Show("案内：入庫しました。", "確認",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            //テキストボックス初期化
            txtReset();
        }

        /// <summary>
        /// データベースから表に表示するデータを取得する関数
        /// </summary>
        private void DataSet()
        {
            try
            {  
                //商品データをクラスから取得する
                var allLog = new Class_ProductsDisplaySet();
                var list = allLog.DataList();

                //DataStoreを初期化する
                Class_DataStore.StockLogs.Clear();


                //取得した値をDataStoreに追加する
                foreach (var l in list)
                {
                    Class_DataStore.StockLogs.Add(l);
                }

                //表に入庫数だけを抽出し、表示する
                dgvStockIn.DataSource = Class_DataStore.StockLogs
                .Where(s => s.Quantity > 0).ToList();
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
        }
        /// <summary>
        /// 在庫数を更新する関数
        /// </summary>
        private void UpDateStock()
        {
            try
            {
                //DataStore(Inventory)を初期化する
                Class_DataStore.Inventory.Clear();
                //商品の在庫数の取得を行う（クラス呼び出し）
                var Inventory = new Class_DatabaseStockLogs();
                var list = Inventory.UpDateStock();

                //DataStoreに在庫データを追加する
                foreach (var l in list)
                {
                    Class_DataStore.Inventory.Add(l);
                }
            }
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。","確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}","確認",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
        }
    }
}
