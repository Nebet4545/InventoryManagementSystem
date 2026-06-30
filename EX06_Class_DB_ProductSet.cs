using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public class Class_Database_Product
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        string? mainConn = Class_DbConfig.ConnectionString;

        /// <summary>
        /// 商品マスタ(全商品のデータ)を取得する関数
        /// </summary>
        /// <returns></returns>
        public List<Class_Product> ProductsAllSet()
        {
            //取得したデータを返す為のリスト
            var ProductList = new List<Class_Product>();

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
                            @"SELECT * FROM ""Products""";
                        sbsql.Append(sql);
                    }

                    //sql文を実行
                    using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                    {
                        //データベースを読み込み
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            //リストに取得した値を追加
                            while (reader.Read())
                            {
                                ProductList.Add(new Class_Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductCode = Convert.ToString(reader["ProductCode"]) ?? "", //商品コード
                                    ProductName = Convert.ToString(reader["ProductName"]) ?? "", //商品名
                                    Price = Convert.ToInt32(reader["Price"]), //単価
                                    CreateDate = reader.GetFieldValue<DateOnly>(reader.GetOrdinal("CreateDate")).ToDateTime(TimeOnly.MinValue), //登録日
                                    StaffName = Convert.ToString(reader["StaffName"]) ?? "" //担当者
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
            return ProductList;
        }


        /// <summary>
        /// 在庫数を取得する関数
        /// </summary>
        /// <returns></returns>
        public List<Class_Inventory> Inventorylist()
        {
            //取得したデータを返すためのリスト
            var InventoryList = new List<Class_Inventory>();

            //sql文記述用
            var sbsqlInvent = new StringBuilder();

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
                            @"SELECT p.""ProductId"",
                            COALESCE(SUM(i.""ProductStock""),0) AS ""Stock""
                            FROM ""Products"" p
                            LEFT JOIN ""Inventory"" i
                            ON p.""ProductId"" = i.""ProductId""
                            GROUP BY p.""ProductId""
                            ORDER BY p.""ProductId""";
                        sbsqlInvent.AppendLine(sql);
                    }
                    //sql文を実行する
                    using (var cmd = new NpgsqlCommand(sbsqlInvent.ToString(), conn))
                    {
                        //データベースを読み込む
                        using (var reader = cmd.ExecuteReader())
                        {
                            //リストに値をセットする
                            while (reader.Read())
                            {
                                InventoryList.Add(new Class_Inventory
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]), //商品ID
                                    ProductStock = Convert.ToInt32(reader["Stock"]), //在庫数
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
            return InventoryList;
        }
    }
}
