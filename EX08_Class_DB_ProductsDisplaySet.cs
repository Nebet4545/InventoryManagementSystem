using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    /// <summary>
    /// 商品マスタ・画面表示用
    /// </summary>
    public class Class_ProductsDisplaySet()
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        string? mainConn = Class_DbConfig.ConnectionString;

        /// <summary>
        /// 商品管理表に表示するデータを取得する関数
        /// </summary>
        public List<Class_ProductDisplay> StoreDisplaySet()
        {
            //取得したデータを返す為のリスト
            var DisplayList = new List<Class_ProductDisplay>();
            //sql文記述
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
                            p.""ProductId"",p.""ProductCode"",p.""ProductName"",p.""Price""
                            ,COALESCE(SUM(i.""ProductStock""),0) AS ""Stock""
                            FROM ""Products"" p
                            LEFT JOIN ""Inventory"" i
                            ON p.""ProductId"" = i.""ProductId""
                            GROUP BY p.""ProductId""
                            ORDER BY p.""ProductId""";
                        sbsql.Append(sql);
                    }
                    //sql文実行
                    using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                    {
                        //データベースを読み込む
                        using (var reader = cmd.ExecuteReader())
                        {
                            //リストに取得した値をセット
                            while (reader.Read())
                            {
                                DisplayList.Add(new Class_ProductDisplay
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductCode = Convert.ToString(reader["ProductCode"]) ?? "", //商品コード
                                    ProductName = Convert.ToString(reader["ProductName"]) ?? "", //商品名
                                    Price = Convert.ToInt32(reader["Price"]), //単価
                                    ProductStock = Convert.ToInt32(reader["Stock"]) //在庫数
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
            return DisplayList;
        }
        /// <summary>
        /// 表データの設定を行う関数
        /// </summary>
        public static List<Class_Log> DataList(string mainConn)
        {
            //返す用の空のリストを宣言する
            var Logs = new List<Class_Log>();
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
                            @"SELECT ""ProductId"",""ProductCode"",""ProductName"",""Price"",
                            ""Quantity"",""LogDate"",""StaffName""
                            FROM ""StockLogs""
                            ORDER BY ""ProductId""";
                        sbsql.Append(sql);
                    }
                    //sql文実行
                    using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                    {
                        //データベース読み込み
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //DataStoreに値をセット
                                Logs.Add(new Class_Log
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductCode = Convert.ToString(reader["ProductCode"]), //商品コード
                                    ProductName = Convert.ToString(reader["ProductName"]), //商品名
                                    Price = Convert.ToInt32(reader["Price"]), //単価
                                    Quantity = Convert.ToInt32(reader["Quantity"]), //入庫数
                                    LogDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("LogDate")).ToDateTime(TimeOnly.MinValue), //入庫日
                                    StaffName = Convert.ToString(reader["StaffName"]) //担当者
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
            //リストを呼び出し元に返す
            return Logs;
        }

    }
}
