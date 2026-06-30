using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
