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

            //商品の入庫処理を行う(クラス呼び出し)
            var repo = new Class_DatabaseStockLogs(); 
            try
            {
                repo.SaveToDatabase(ProductId, pCode, foundProduct.ProductName, foundProduct.Price, Category, quan, InDate, staff);

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
