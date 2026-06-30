using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    /// <summary>
    /// StockLogsクラスを作成
    /// </summary>
    public class Class_Log
    {
        //商品ID
        public int ProductId { get; set; }
        //商品コード
        public string? ProductCode { get; set; }
        //商品名
        public string? ProductName { get; set; }
        //単価
        public int Price { get; set; }
        //カテゴリー
        public string? Category { get; set; }
        //入出庫数量
        public int Quantity { get; set; }
        //入出庫日
        public DateTime LogDate { get; set; }
        //担当者
        public string? StaffName { get; set; }

        public Class_Log()
        {
            ProductCode = "";
            ProductName = "";
            StaffName = "";
            Category = "";
        }
    }

}
