using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// タイトル画面
    /// </summary>
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 商品管理表ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProductList_Click(object sender, EventArgs e)
        {
            //商品管理表画面へ遷移
            var frmProductLIst = new ProductForm();
            frmProductLIst.ShowDialog();
        }

        /// <summary>
        /// 商品入庫表ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProductIn_Click(object sender, EventArgs e)
        {
            //商品入庫画面へ遷移
            var frmProductIn = new StockInForm();
            frmProductIn.ShowDialog();
        }

        /// <summary>
        /// 商品出庫表ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProductOut_Click(object sender, EventArgs e)
        {
            //商品出庫画面へ遷移
            var frmProductOut = new StockOutForm();
            frmProductOut.ShowDialog();
        }

        /// <summary>
        /// 入出庫履歴ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHistory_Click(object sender, EventArgs e)
        {
            //入出庫履歴画面へ遷移
            var frmHistory = new HistoryForm();
            frmHistory.ShowDialog();
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
        /// 商品マスタのデータを取得する関数
        /// </summary>
        private void StoreSetProducts()
        {
            try
            {
                // 共通クラスを呼び出す
                var DataClass = new Class_Database_Product();
                // データを取得する(sql処理)
                var GetProducts = DataClass.ProductsAllSet();
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
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。","確認",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
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
        /// 履歴データを取得する関数
        /// </summary>
        private void StoreSetLog()
        {
            try
            {
                //共通クラスを呼び出す
                var DataClass = new Class_DatabaseStockLogs();
                //データを取得する(sql処理)
                var GetLogs = DataClass.StoreStockLogsSet();
                //DataStoreを空にする
                Class_DataStore.StockLogs.Clear();
                //取得した値をセットする
                foreach (var setlist in GetLogs)
                {
                    Class_DataStore.StockLogs.Add(setlist);
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
        /// 表示用データを取得する関数
        /// </summary>
        private void StoreSetDisplay()
        {
            try
            {
                //共通クラスを呼び出す
                var DataClass = new Class_ProductsDisplaySet();
                //データを取得する
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
        /// 在庫数を取得する関数
        /// </summary>
        private void StoreSetInventory()
        {
            try
            {
                //共通クラスを呼び出す
                var DataClass = new Class_Database_Product();
                //データを取得する
                var Inventory = DataClass.Inventorylist();
                //DataStoreを空にする
                Class_DataStore.Inventory.Clear();
                //取得した値をセットする
                foreach (var setlist in Inventory)
                {
                    Class_DataStore.Inventory.Add(setlist);
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
        /// 初期データをセットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void main_Load(object sender, EventArgs e)
        {
            //初期データをセットする
            StoreSetProducts(); //商品マスタ
            StoreSetLog(); //全ての履歴
            StoreSetDisplay(); //商品管理表【表示】
            StoreSetInventory(); //在庫数を取得する

        }
    }
}
