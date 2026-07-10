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
    public partial class ProductDelete : Form
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        string? mainConn = Class_DbConfig.ConnectionString;


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
            var pCode = cmbProductList.Text; //商品コード

            //DataStore(Products)から選択した商品コードと一致する商品コードを探す
            var SearchCd = Class_DataStore.Products
                .FirstOrDefault(c => c.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase));

            //検索した商品コードが見つからなかった場合の処理
            if (SearchCd == null)
            {
                //メッセージを表示する
                MessageBox.Show("商品コードが見つかりませんでした。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //検索した商品コードが見つかった場合の処理
            else
            {
                //各テキストボックスに対応する項目を転記
                txtProductCode.Text = pCode; //商品コード
                txtProductName.Text = SearchCd.ProductName; //商品名
                txtPrice.Text = SearchCd.Price.ToString(); //単価
            }
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
                //メッセージを表示する
                MessageBox.Show("各項目が表示されていません。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //削除確認を行う
            var result = MessageBox.Show("本当に削除しますか？", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Noが選択された場合の処理
            if (result == DialogResult.No)
            {
                //メッセージを表示する
                MessageBox.Show("キャンセルしました。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                //テキストボックス初期化の関数呼び出し
                txtReset();
                return;
            }

            string pCode = txtProductCode.Text; //商品コード

            //DataStore(Products)から検索した商品コードと一致する商品コードを探す
            var SelectCode = Class_DataStore.Products
                .FirstOrDefault(c => c.ProductCode.Equals(pCode, StringComparison.OrdinalIgnoreCase));

            //商品コードが見つからなかった場合の処理
            if (SelectCode == null)
            {
                //メッセージを表示する
                MessageBox.Show("該当の商品が見つかりません。", "確認",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //商品コードから商品IDを取得する
            int foundId = SelectCode.ProductId;

            //在庫数を取得する
            var stock = Class_DataStore.Inventory
                .Where(p => p.ProductId == foundId)
                .Sum(x => x.ProductStock);

            //在庫数が存在する場合の処理
            if (stock > 0)
            {
                //メッセージを表示
                MessageBox.Show($"選択された商品は在庫が存在します。{Environment.NewLine}" +
                $"削除することはできません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //テキストボックス初期化の関数呼び出し
                txtReset();
                //コンボボックスにカーソルを戻す
                cmbProductList.Focus();
                return;
            }

            //
            if (stock <= 0)
            {
                DialogResult DeleteCheck = MessageBox.Show($"選択された商品の在庫は 0 です。商品情報を削除してもよろしいですか？{Environment.NewLine}" +
                    $"※履歴データは保持されます。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //「いいえ」が選択された場合の処理
                if (DeleteCheck == DialogResult.No)
                {
                    //メッセージを表示する
                    MessageBox.Show("処理を中止します。", "確認",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //テキストボックス初期化の関数を呼び出す
                    txtReset();
                    //コンボボックスにカーソルを戻す
                    cmbProductList.Focus();
                    return;
                }
            }

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
                        //sql文記述用1(Inventory:在庫)
                        var sbsqlInventory = new StringBuilder();
                        {
                            string sql =
                                @"DELETE FROM ""Inventory""
                                WHERE
                                ""ProductId"" = @ProductId";
                            sbsqlInventory.AppendLine(sql);
                        }
                        //sql文記述用2(Products用)
                        var sbsqlProduct = new StringBuilder();
                        {
                            string sql =
                                @"DELETE FROM ""Products""
                                            WHERE 
                                            ""ProductId"" = @ProductId";
                            sbsqlProduct.AppendLine(sql);
                        }

                        //sql文実行(1回目)
                        using (var cmd = new NpgsqlCommand(sbsqlInventory.ToString(), conn))
                        {
                            try
                            {
                                //トランザクションを紐づけ
                                cmd.Transaction = tran;

                                //商品コードを削除する(商品IDで指定)
                                cmd.Parameters.AddWithValue("@ProductId", foundId);
                                cmd.ExecuteNonQuery();

                                //sql文実行(2回目)
                                using (var cmd2 = new NpgsqlCommand(sbsqlProduct.ToString(), conn))
                                {
                                    //トランザクションを紐づけ
                                    cmd2.Transaction = tran;
                                    //商品IDを削除する
                                    cmd2.Parameters.AddWithValue("@ProductId", foundId);
                                    cmd2.ExecuteNonQuery();
                                }
                                tran.Commit();

                                //削除時のメッセージ表示
                                MessageBox.Show("データを削除しました。", "確認",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            return;
        }
        /// <summary>
        /// 表示用のデータを取得する関数
        /// </summary>
        private void StoreSetDisplay()
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
        /// <summary>
        /// 商品マスタのデータを取得する関数
        /// </summary>
        private void StoreSetProducts()
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
    }
}

