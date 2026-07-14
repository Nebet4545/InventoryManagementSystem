using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static InventoryManagementSystem.Main;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagementSystem
{
    public partial class StockOutForm : Form
    {
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

            //在庫数が0未満になる場合は出庫できないようにする
            //検索した商品コードの現在の在庫数を調べる
            var CurrentStock = Class_DataStore.Inventory
                .Where(p => p.ProductId == ProductId)
                .Sum(p => p.ProductStock);

            //出庫数が在庫数を越えている場合の処理
            if (CurrentStock - quan < 0)
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

            //商品の入庫処理を行う(クラス呼び出し)
            var repo = new Class_DatabaseStockLogs();
            try
            {
                repo.SaveToDatabase(ProductId, ProductCode, foundProduct.ProductName, foundProduct.Price, Category, quan, OutDate, staff);

            }
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。");
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}");
            }

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
                //出庫数だけを抽出し、絶対値に変換して表示する
                dgvStockOut.DataSource = Class_DataStore.StockLogs
                .Where(s => s.Quantity < 0)
                .Select(s => new
                {
                    s.ProductId,
                    s.ProductCode,
                    s.ProductName,
                    s.Price,
                    Quantity = Math.Abs(s.Quantity),
                    s.LogDate,
                    s.StaffName
                }).ToList();
            }
            //呼び出し先で発生したエラーを取得する（接続情報の取得エラー）
            catch (InvalidOperationException ex1)
            {
                MessageBox.Show($"エラーメッセージ：{ex1.Message}{Environment.NewLine}※configファイルの設定を確認してください。");
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}");
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
            }
            //呼び出し先で発生したエラーを取得する（その他のエラー）
            catch (Exception ex2)
            {
                MessageBox.Show($"エラーメッセージ：{ex2.Message}");
            }
        }
}
}
