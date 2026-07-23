using Microsoft.VisualBasic.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#region 履歴に関する処理(sql)をまとめるクラス

namespace InventoryManagementSystem
{
    public class Class_DatabaseStockLogs
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        private readonly string mainConn = default!;

        /// <summary>
        /// 接続情報を取得するコンストラクタ
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public Class_DatabaseStockLogs()
        {
            //接続文字列を取得する
            var ConnStr = Class_DbConfig.ConnectionString;

            //接続文字列がNullまたは""の場合エラーメッセージを呼び出し元にスローする
            if (string.IsNullOrEmpty(ConnStr))
            {
                throw new InvalidOperationException("接続文字列がありません。");
            }
            
            //接続情報を取得する
            mainConn = ConnStr;
        }

        /// <summary>
        /// 商品の入出庫履歴を管理するデータを取得する関数
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
                            @"SELECT
                            ""ProductId"",""ProductCode"",""ProductName"",""Price"",
                            ""Category"",""Quantity"",""LogDate"",""StaffName""
                            FROM ""StockLogs""
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
                //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                catch (Exception)
                {
                    throw;
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
        public void SaveToDatabase(int ProductId, string ProductCode, string ProductName, int Price, string Category, int quan, DateTime InDate, string staff)
        {
            //カテゴリーが出荷の場合は数値をマイナス変換する
            int Convertquan = 0;
            if (Category == "出庫")
            {
                Convertquan = quan * -1;
            }
            else
            {
                Convertquan = quan;
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
                                cmd.Parameters.AddWithValue("@Quantity", Convertquan); //入庫数
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
                                    cmd2.Parameters.AddWithValue("@Quantity", Convertquan); //入庫数
                                    cmd2.Parameters.AddWithValue("@ProductId", ProductId); //商品ID

                                    //Inventoryのsqlコマンドを終了する
                                    cmd2.ExecuteNonQuery();
                                }
                                tran.Commit();
                            }
                        //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                        catch (Exception)
                            {
                            //データベースへの変更を破棄し、処理前の状態へ戻す
                                tran.Rollback();
                                throw;
                            }
                        }
                    }
                }
            }
        /// <summary>
        /// データベースから最新の在庫数を取得する関数
        /// </summary>
        public List<Class_Inventory> UpDateStock()
        {
            //呼び出し元に返す用のリストを作成
            var StockList = new List<Class_Inventory>();

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
                                StockList.Add(new Class_Inventory
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductStock = Convert.ToInt32(reader["ProductStock"]) //在庫数
                                });
                            }
                        }
                    }
                }
                //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                catch (Exception)
                {
                    throw;
                }
            }
            //呼び出し元にリストを返す
            return StockList;
        }
        /// <summary>
        /// データベースから当月の入出庫履歴を取得する関数(デフォルトの設定)
        /// </summary>
        /// <returns></returns>
        public List<Class_Log> HistoryDataset()
        {
            //呼び出し元に返すリストを生成
            var Loglist = new List<Class_Log>();

            //当月の初日を取得(初期状態を当月分にするため)
            DateTime StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //当月の最終日を取得
            DateTime EndDate = StartDate.AddMonths(1).AddDays(-1);

            //sql文記述用
            var sbsql = new StringBuilder();
            {
                string sql =
                    @"SELECT 
                    s.""LogDate"",p.""ProductCode"",s.""ProductName"",
                    s.""Category"",s.""Quantity"",s.""StaffName""
                    FROM ""StockLogs"" s
                    INNER JOIN ""Products"" p
                    ON s.""ProductId"" = p.""ProductId""
                    WHERE s.""LogDate"" BETWEEN @StartDay AND @EndDay
                    AND s.""Quantity"" <> 0
                    ORDER BY s.""LogDate""";
                sbsql.Append(sql);
            }

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //空のsqlコマンドを宣言
                    using (var cmd = new NpgsqlCommand())
                    {
                        //表示する期間を代入
                        cmd.Parameters.AddWithValue("@StartDay", StartDate); //当月初日
                        cmd.Parameters.AddWithValue("@EndDay", EndDate); //当月末日

                        //sql文をコマンドに入れる
                        cmd.CommandText = sbsql.ToString();
                        cmd.Connection = conn;

                        //データベース読み込み
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //各項目をDataStoreに追加する
                                Loglist.Add(new Class_Log
                                {
                                    LogDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("LogDate")).ToDateTime(TimeOnly.MinValue), //入出庫日
                                    ProductCode = reader["ProductCode"].ToString(), //商品コード
                                    ProductName = reader["ProductName"].ToString(), //商品名
                                    Category = reader["Category"].ToString(), //カテゴリー
                                    Quantity = Convert.ToInt32(reader["Quantity"]), //入出庫数量
                                    StaffName = reader["StaffName"].ToString(), //担当者名

                                    //※注意：クラス側のrequired制約を満たすためのダミー値です。
                                    //このデータベース処理ではPrice(価格)は不要なためエラー回避のために0を代入しています。
                                    Price = 0
                                });
                            }
                        }
                    }
                }
                //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                catch (Exception)
                {
                    throw;
                }
            }
            //リストを呼び出し元に返す
            return Loglist;
        }
        /// <summary>
        /// データベースから指定された商品の入出庫履歴を取得する関数
        /// </summary>
        public List<Class_Log> HistoryLogSearch(string ProductCode,string staffName, DateTime? StartDate,DateTime? EndDate)
        {
            //呼び出し元に返すリストを生成する
            var SelectedList = new List<Class_Log>();

            //sql文記述
            var sbsql = new StringBuilder();
            {
                string sql =
                    @"SELECT s.""LogDate"",p.""ProductCode"",s.""ProductName"",
                    s.""Category"",s.""Quantity"",s.""StaffName""
                    FROM ""StockLogs"" s
                    INNER JOIN ""Products"" p
                    ON s.""ProductId"" = p.""ProductId""
                    WHERE 1 = 1 -- 以降の処理を進めるためのダミー条件
                    AND s.""Quantity"" <> 0 -- 数量0での入出庫は認められないため ";
                sbsql.AppendLine(sql);
            }

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                try
                {
                    //データベースを開く
                    conn.Open();
                    //空のsqlコマンドを作る
                    using (var cmd = new NpgsqlCommand())
                    {
                        //商品コードが入力されている場合の処理
                        if (!string.IsNullOrWhiteSpace(ProductCode))
                        {
                            //商品コードが一致した商品の履歴を取得する
                            sbsql.AppendLine("AND p.\"ProductCode\" = @ProductCode");
                            cmd.Parameters.AddWithValue("ProductCode", ProductCode);
                        }
                        //担当者が入力されている場合の処理
                        if (!string.IsNullOrWhiteSpace(staffName))
                        {
                            //担当者名が一致(部分)している商品の履歴を取得する
                            sbsql.AppendLine("AND s.\"StaffName\" LIKE @StaffName");
                            cmd.Parameters.AddWithValue("StaffName", $"%{staffName}%");
                        }
                        //期間指定がされている場合の処理
                        if (StartDate.HasValue && EndDate.HasValue)
                        {
                            //指定された期間の入庫・出庫データを取得する
                            sbsql.AppendLine("AND s.\"LogDate\" BETWEEN @StartDate AND @EndDate");
                            cmd.Parameters.AddWithValue("@StartDate", StartDate.Value);
                            cmd.Parameters.AddWithValue("@EndDate", EndDate.Value);
                        }
                        //入庫日・出庫日の昇順で取得する
                        sbsql.AppendLine("ORDER BY s.\"LogDate\"");

                        //空のコマンドに代入
                        cmd.CommandText = sbsql.ToString();
                        cmd.Connection = conn;

                        //データベースを読み込む
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //データベースから取得した値をセット
                                SelectedList.Add(new Class_Log
                                {
                                    LogDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("LogDate")).ToDateTime(TimeOnly.MinValue), //入庫。出庫日
                                    ProductCode = reader["ProductCode"].ToString(), //商品コード
                                    ProductName = reader["ProductName"].ToString(), //商品名
                                    Category = reader["Category"].ToString(), //区分
                                    Quantity = Convert.ToInt32(reader["Quantity"]), //入庫・出庫数量
                                    StaffName = reader["StaffName"].ToString(), //担当者名

                                    //※注意：クラス側のrequired制約を満たすためのダミー値です。
                                    //このデータベース処理ではPrice(価格)は不要なためエラー回避のために0を代入しています。
                                    Price = 0
                                });
                            }

                        }
                    }
                }
                //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                catch (Exception)
                {
                    throw;
                }
            }
            return SelectedList;
        }

    }
}
#endregion
