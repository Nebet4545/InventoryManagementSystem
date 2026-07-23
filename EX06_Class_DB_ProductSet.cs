using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

#region 商品のデータを取得する処理(sql)をまとめたクラス

namespace InventoryManagementSystem
{
    public class Class_Database_Product
    {
        /// <summary>
        /// 接続情報
        /// </summary>
        private readonly string mainConn = default!;

        /// <summary>
        /// 接続情報を取得するコンストラクタ
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public Class_Database_Product()
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
                            @"SELECT
                            ""ProductId"",""ProductCode"",""ProductName"",
                            ""Price"",""CreateDate"",""StaffName"" 
                            FROM ""Products""";
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
                //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                catch (Exception)
                {
                    throw;
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
                //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                catch (Exception)
                {
                    throw;
                }
            }
            return InventoryList;
        }
        /// <summary>
        /// 商品データを追加登録し、ProductとInventoryのデータベースを更新する処理
        /// </summary>
        /// <param name="ProductCode"></param>
        /// <param name="ProductName"></param>
        /// <param name="Price"></param>
        public void ProductAdd(string ProductCode, string ProductName, int Price,out string Msg)
        {
            Msg = ""; //呼び出し元で表示するメッセージを初期化する

            //sql文記述用1(商品マスタ)
            var sbsqlProducts = new StringBuilder();
            {
                string sql =
                    @"INSERT INTO ""Products""
                        (""ProductCode"",""ProductName"",""Price"")
                        VALUES
                        (@ProductCode,@ProductName,@Price)
                        RETURNING ""ProductId""";
                sbsqlProducts.AppendLine(sql);
            }
            //sql文記述用2(在庫)
            var sbsqlInventory = new StringBuilder();
            {
                string sql =
                    @"INSERT INTO ""Inventory""
                        (""ProductId"",""ProductStock"")
                        VALUES
                        (@ProductId,@ProductStock)";
                sbsqlInventory.AppendLine(sql);
            }

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                    //データベースを開く
                    conn.Open();
                    //トランザクション開始
                    using (NpgsqlTransaction tran = conn.BeginTransaction())
                    {
                        //sql文1の実行
                        using (var cmd = new NpgsqlCommand(sbsqlProducts.ToString(), conn))
                        {
                            //トランザクションを紐づけ
                            cmd.Transaction = tran;
                            try
                            {
                                //各項目を登録
                                cmd.Parameters.AddWithValue("@ProductCode", ProductCode); //商品コード
                                cmd.Parameters.AddWithValue("@ProductName", ProductName); //商品名
                                cmd.Parameters.AddWithValue("@Price", Price); //単価

                                //取得した自動番号を受け取る
                                var ProductId = cmd.ExecuteScalar();

                                //sql文の実行(2回目)
                                using (var cmd2 = new NpgsqlCommand(sbsqlInventory.ToString(), conn))
                                {
                                    //新しく作ったコマンドを同じトランザクションに
                                    cmd2.Transaction = tran;

                                    //各項目を登録
                                    cmd2.Parameters.AddWithValue("@ProductId", Convert.ToInt32(ProductId)); //商品ID
                                    cmd2.Parameters.AddWithValue("@ProductStock", 0); //デフォルト値：0

                                    cmd2.ExecuteNonQuery();
                                }
                                tran.Commit();

                            //商品データ登録時のメッセージ（呼び出し元で表示する）
                            Msg = "商品データを登録しました。";
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
        /// 商品データを削除する処理
        /// </summary>
        public void ProductDelete(int ProductId, out string Msg)
        {
            Msg = ""; //呼び出し元で表示するメッセージを初期化する
            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
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
                                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                                cmd.ExecuteNonQuery();

                                //sql文実行(2回目)
                                using (var cmd2 = new NpgsqlCommand(sbsqlProduct.ToString(), conn))
                                {
                                    //トランザクションを紐づけ
                                    cmd2.Transaction = tran;
                                    //商品IDを削除する
                                    cmd2.Parameters.AddWithValue("@ProductId", ProductId);
                                    cmd2.ExecuteNonQuery();
                                }
                                tran.Commit();

                            //削除時のメッセージ（呼び出し元で表示する）
                            Msg = "商品データを削除しました。";

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
        /// 商品データを編集し、更新する処理
        /// </summary>
        /// <param name="ProductCode"></param>
        /// <param name="ProductName"></param>
        /// <param name="Price"></param>
        /// <param name="Msg"></param>
        public void ProductEdit(string ProductCode,string ProductName,int Price,out string Msg)
        {
            Msg = ""; //呼び出し元で表示するメッセージを初期化する
            //sql文記述用
            var sbsql = new StringBuilder();

            //データベースに接続
            using (var conn = new NpgsqlConnection(mainConn))
            {
                    //データベースを開く
                    conn.Open();

                    //トランザクション開始
                    using (var tran = conn.BeginTransaction())
                    {
                        //sql文記述
                        {
                            string sql =
                                @"UPDATE ""Products"" SET
                                ""ProductName"" = @ProductName,
                                ""Price"" = @Price
                                WHERE
                                ""ProductCode"" = @ProductCode";
                            sbsql.AppendLine(sql);
                        }

                        //sql文を実行
                        using (var cmd = new NpgsqlCommand(sbsql.ToString(), conn))
                        {
                            //トランザクションを紐づけ
                            cmd.Transaction = tran;
                            try
                            {
                                //データベースを更新
                                cmd.Parameters.AddWithValue("@ProductCode", ProductCode); //商品コード
                                cmd.Parameters.AddWithValue("@ProductName", ProductName); //商品名
                                cmd.Parameters.AddWithValue("@Price", Price); //単価

                                cmd.ExecuteNonQuery();
                                tran.Commit();

                                //商品データ更新時のメッセージ（呼び出し元で表示する）
                                Msg = "商品データを更新しました。";
                            }
                            //エラーが出た場合処理を中断して呼び出し元に内容をスローする
                            catch (Exception)
                            {
                                //データベースへの変更を破棄し、処理前の状態に戻す
                                tran.Rollback();
                                throw;
                            }
                        }
                    }
                }
            }

        }
    }
    
    

#endregion