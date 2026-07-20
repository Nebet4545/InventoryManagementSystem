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
    /// 商品入出庫履歴画面
    /// </summary>
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ロード時の設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistoryForm_Load(object sender, EventArgs e)
        {
            //コンボボックスに初期データをセット
            cmbSet();
            //テキストボックスを初期化
            txtReset();
            //ラジオボタンの初期設定
            RadioThisMonth.Checked = true;
            RadioStockAll.Checked = true;
            //デートタイムピッカー(dtpStartDate・dtpEndDate)の値を当日に設定する
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            //表に自動で列が増えないように制限する
            dgvHistory.AutoGenerateColumns = false;
            //表に初期データをセット
            ToMonthDataSet();
        }
        /// <summary>
        /// コンボボックス初期設定の関数
        /// </summary>
        private void cmbSet()
        {
            //コンボボックス初期化
            cmbProductCode.Items.Clear();
            //初期値に空白を入れる(全検索用)
            cmbProductCode.Items.Add("");

            //コンボボックスに値をセット(商品コード)
            foreach (var item in Class_DataStore.Products)
            {
                cmbProductCode.Items.Add(item.ProductCode);
            }
            //コンボボックスの初期インデックスを設定
            cmbProductCode.SelectedIndex = 0;
        }
        /// <summary>
        /// テキストボックス初期化の関数
        /// </summary>
        private void txtReset()
        {
            txtStaffName.Text = string.Empty;
        }
        /// <summary>
        /// 入出庫履歴の初期データ(当月分)を取得する関数
        /// </summary>
        private void ToMonthDataSet()
        {
            try
            {
                //DataStore(StockLogs)を初期化する
                Class_DataStore.StockLogs.Clear();
                //商品の当月分の入出庫履歴データを取得する(クラス呼び出し)
                var repo = new Class_DatabaseStockLogs();
                var list = repo.HistoryDataset();

                //DataStoreに当月分の入出庫履歴データを追加する
                foreach (var l in list)
                {
                    Class_DataStore.StockLogs.Add(l);
                }
                //取得したデータを表示する
                dgvHistory.DataSource = null; //表の初期化
                dgvHistory.DataSource = Class_DataStore.StockLogs;
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
        /// 各セルの色を変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //プロパティ名が"Category"かチェックする
            if (dgvHistory.Columns[e.ColumnIndex].DataPropertyName == "Category")
            {
                //区分("Category")列の値を文字列化する
                var category = e.Value?.ToString();

                //区分列の値が"出庫"の場合、セルの色を赤にする
                if (category == "出庫")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
            //プロパティ名が"Quantity"かチェックする
            if (dgvHistory.Columns[e.ColumnIndex].DataPropertyName == "Quantity")
            {
                //数量("Quantity")列の値を数値化する
                if (e.Value != null && int.TryParse(e.Value.ToString(), out int quan))
                {
                    //数量の値が0未満(マイナス)の場合、セルの色を赤にする
                    if (quan < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        //数値を絶対値に変換して表示する
                        e.Value = Math.Abs(quan);
                    }
                }
            }
        }
        /// <summary>
        /// ラジオボタン全期間が選択された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioAll_CheckedChanged(object sender, EventArgs e)
        {
            //ラジオボタンの状態(ON or OFF)
            bool IsAll = RadioAll.Checked;

            //ラジオボタンの状態に応じて変更
            dtpStartDate.Enabled = !IsAll; //デートタイムピッカーを操作不可にする
            dtpEndDate.Enabled = !IsAll; //デートタイムピッカーを操作不可にする
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
        /// 検索ボタンの設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //検索期間を指定する
            DateTime? StartDate = null; //検索開始日
            DateTime? EndDate = null;   //検索終了日

            string pCode = cmbProductCode.Text.Trim(); //商品コード
            string staffName = txtStaffName.Text.Trim(); //担当者名

            //ラジオボタンの状況に合わせて検索する期間を指定する
            //期間指定がされている場合の処理
            if (RadioBetween.Checked == true)
            {
                //各デートタイムピッカーに指定されている期間の履歴を表示
                StartDate = dtpStartDate.Value; //検索開始日
                EndDate = dtpEndDate.Value;     //検索終了日
            }
            //当月分の履歴を表示するラジオボタンがチェックされている場合の処理
            if (RadioThisMonth.Checked == true)
            {
                //クラス呼び出し
                var GetDate = new Class_ProductDef();

                //当月初日の日付と当月末日の日付を取得する
                GetDate.GetStartEndDate(out StartDate, out EndDate);
            }

            //データベースから検索条件に合わせた入出庫履歴のデータを取得する(クラス呼び出し)
            var repo = new Class_DatabaseStockLogs();
            //クラスから返ってきたリストを受け取るリスト
            var SelectedHistoryList = new List<Class_Log>();
            try
            {
               SelectedHistoryList = repo.HistoryLogSearch(pCode, staffName, StartDate, EndDate);
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

            //入庫のみ表示が選択された場合
            if (RadioStockIn.Checked == true)
            {
                //表に入庫数が1以上の履歴だけを表示する
                dgvHistory.DataSource = SelectedHistoryList
                    .Where(s => s.Quantity >= 1).ToList();
            }
            //出庫のみが選択された場合
            else if (RadioStockOut.Checked == true)
            {
                //表に出庫数が0未満(マイナス)の履歴だけを表示する
                dgvHistory.DataSource = SelectedHistoryList
                    .Where(s => s.Quantity < 0).ToList();
            }
            //入庫・出庫が選択された場合
            else
            {
                //表に履歴を表示する
                dgvHistory.DataSource = SelectedHistoryList;
            }
        }

        /// <summary>
        /// ラジオボタン当月分(デフォルト設定)が選択された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioThisMonth_CheckedChanged(object sender, EventArgs e)
        {
            //ラジオボタンの状態(ON or OFF)(デフォルト設定はON)
            bool ThisMonth = RadioThisMonth.Checked;

            //ラジオボタンの状態に応じて変更
            dtpStartDate.Enabled = !ThisMonth; //操作不可にする
            dtpEndDate.Enabled = !ThisMonth; //操作不可にする
        }
        /// <summary>
        /// コンボボックスの商品コードが選択された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //商品コードから商品名を取得する
            txtProductName.Text = Class_DataStore.Products
                .FirstOrDefault(p => p.ProductCode.Equals(cmbProductCode.Text, StringComparison.OrdinalIgnoreCase))
                ?.ProductName ?? "";
        }
    }
}
    