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
    /// 商品マスタ画面
    /// </summary>
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// データ追加ボタンの設定
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //商品データ追加登録画面へ遷移
            var frmadd = new ProductAddForm();
            frmadd.ShowDialog();
        }

        /// <summary>
        /// ロード時の設定
        /// </summary>
        private void ProductForm_Load(object sender, EventArgs e)
        {
            //表に自動で列が増えないよう制限する
            dgvProducts.AutoGenerateColumns = false;
            ////行ヘッダー非表示
            dgvProducts.RowHeadersVisible = false;
            //表の表示データを表示用クラスの値にする
            DisplaySet();
            dgvProducts.DataSource = Class_DataStore.ProductDisplays;
        }

        /// <summary>
        /// データ編集ボタンの設定
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //商品データ更新画面へ遷移
            var frmEdit = new ProductEdit();
            frmEdit.ShowDialog();
        }

        /// <summary>
        /// データ削除ボタンの設定
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //商品データ削除画面へ遷移
            var frmDelete = new ProductDelete();
            frmDelete.ShowDialog();
        }

        /// <summary>
        /// 終了ボタンの設定
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
        /// 表示用データを取得する関数
        /// </summary>
        private void DisplaySet()
        {
            try
            {
                //共通のクラスを呼び出す
                var DataClass = new Class_ProductsDisplaySet();
                //データを取得する(sql処理)
                var GetDisplay = DataClass.StoreDisplaySet();
                //DataStoreを空にする
                Class_DataStore.ProductDisplays.Clear();
                //値をセットする
                foreach (var setlist in GetDisplay)
                {
                    Class_DataStore.ProductDisplays.Add(setlist);
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
    }
}

