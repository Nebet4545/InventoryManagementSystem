using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#region 履歴に関する処理をまとめるクラス

namespace InventoryManagementSystem
{
    public class Class_DatabaseStockLogs
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        string? mainConn = Class_DbConfig.ConnectionString;

        /// <summary>
        /// 履歴を管理するデータを取得する関数
        /// </summary>
        public List<Class_Log> StoreStockLogsSet()
        {
            //取得したデータを返す為のリスト
            var Loglist = new List<Class_Log>();
            //sql文記述用
            var sbsql = new StringBuilder();

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //sql文記述
                    {
                        string sql =
                            @"Select * FROM ""StockLogs""
                            ORDER BY ""LogDate""";
                        sbsql.AppendLine(sql);
                    }
                    //sql文を実行する
                    using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                    {
                        //データを読み込む
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //各項目をリストに追加する
                                Loglist.Add(new Class_Log
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductCode = Convert.ToString(reader["ProductCode"]) ?? "", //商品コード
                                    ProductName = Convert.ToString(reader["ProductName"]) ?? "", //商品名
                                    Price = Convert.ToInt32(reader["Price"]), //単価
                                    Category = Convert.ToString(reader["Category"]) ?? "", //区分(入庫・出庫・初期登録)
                                    Quantity = Convert.ToInt32(reader["Quantity"]), //入庫・出庫数量
                                    LogDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("LogDate")).ToDateTime(TimeOnly.MinValue), //入庫・出庫日
                                    StaffName = Convert.ToString(reader["StaffName"]) ?? "", //担当者
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"エラーメッセージ：{ex.Message}");
                }
            }
            return Loglist;
        }
        /// <summary>
        /// 商品の入出庫処理を行う関数
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="ProductCode"></param>
        /// <param name="ProductName"></param>
        /// <param name="Price"></param>
        /// <param name="Category"></param>
        /// <param name="quan"></param>
        /// <param name="InDate"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public bool SaveToDatabase(int ProductId, string ProductCode, string ProductName, int Price, string Category, int quan, DateTime InDate, string staff,
           out string errMs)
        {
            errMs = ""; //エラーメッセージを取得する箱

            //カテゴリーが出荷の場合は数値をマイナス変換する
            int Convetquan = 0;
            if (Category == "出庫")
            {
                Convetquan = quan * -1;
            }
            else
            {
                Convetquan = quan;
            }

            //sql文記述用1(ログ：StockLogs)
            var sbsqlLog = new StringBuilder();
            //sql文記述用2(在庫：Inventory)
            var sbsqlInventory = new StringBuilder();
            {
                string sql1 =
                        @"INSERT INTO ""StockLogs""
                            (""ProductId"",""ProductCode"",""ProductName"",""Price"",""Category"",
                                ""Quantity"",""LogDate"",""StaffName"")
                            VALUES
                            (@ProductId,@ProductCode,@ProductName,@Price,@Category,
                            @Quantity,@LogDate,@StaffName)";
                string sql2 =
                    @"UPDATE ""Inventory""
                        SET ""ProductStock"" = ""ProductStock"" + @Quantity
                        WHERE ""ProductId"" = @ProductId";
                sbsqlLog.AppendLine(sql1);
                sbsqlInventory.AppendLine(sql2);
            }

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //トランザクション開始
                    using (NpgsqlTransaction tran = conn.BeginTransaction())
                    {
                        //sql文実行(StockLogs)
                        using (var cmd = new NpgsqlCommand(sbsqlLog.ToString(), conn))
                        {
                            cmd.Transaction = tran;
                            try
                            {
                                //各項目をインサート(入庫)
                                cmd.Parameters.AddWithValue("@ProductId", ProductId); //商品ID
                                cmd.Parameters.AddWithValue("@ProductCode", ProductCode); //商品コード
                                cmd.Parameters.AddWithValue("@ProductName", ProductName); //商品名
                                cmd.Parameters.AddWithValue("@Price", Price); //単価
                                cmd.Parameters.AddWithValue("@Category", Category); //区分
                                cmd.Parameters.AddWithValue("@Quantity", Convetquan); //入庫数
                                cmd.Parameters.AddWithValue("@LogDate", InDate); //入庫日
                                cmd.Parameters.AddWithValue("@StaffName", staff); //担当者

                                //StockLogsのsqlコマンドを終了する
                                cmd.ExecuteNonQuery();

                                //sql文実行(Inventory)
                                using (var cmd2 = new NpgsqlCommand(sbsqlInventory.ToString(), conn))
                                {
                                    //トランザクションを紐づけ
                                    cmd2.Transaction = tran;

                                    //データベースを更新する(Inventory)
                                    cmd2.Parameters.AddWithValue("@Quantity", Convetquan); //入庫数
                                    cmd2.Parameters.AddWithValue("@ProductId", ProductId); //商品ID

                                    //Inventoryのsqlコマンドを終了する
                                    cmd2.ExecuteNonQuery();
                                }
                                tran.Commit();
                                return true;

                            }
                            catch (Exception ex2)
                            {
                                //エラーメッセージを取得する
                                errMs = ex2.Message;
                                tran.Rollback();
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex1)
                {
                    //エラーメッセージを取得する
                    errMs = ex1.Message;
                }

            }
            return false;

        }
        /// <summary>
        /// データベースから最新の在庫数を取得する関数
        /// </summary>
        public List<Class_Inventory> UpDateStock()
        {
            //呼び出し元に返す用のリストを作成
            var stockList = new List<Class_Inventory>();

            //sql文記述用
            var sbsql = new StringBuilder();
            {
                string sql =
                    @"SELECT ""ProductId"",""ProductStock"" FROM ""Inventory""";
                sbsql.AppendLine(sql);
            }
            //データベースに接続する
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //sql文を実行する
                    using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                    {
                        //データベースを読み込む
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //各項目をDataStoreに追加
                                stockList.Add(new Class_Inventory
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductStock = Convert.ToInt32(reader["ProductStock"]) //在庫数
                                });
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                    throw;
                }
            }
            //呼び出し元にリストを返す
            return stockList;
        }
    }
}
#endregion
