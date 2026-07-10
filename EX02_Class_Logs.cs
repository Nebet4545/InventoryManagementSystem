using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    /// <summary>
    /// 商品の履歴データを扱うクラス
    /// </summary>
    public class Class_Log
    {
        //商品ID
        public int ProductId { get; set; }
        //商品コード
        public required string? ProductCode { get; set; }
        //商品名
        public required string? ProductName { get; set; }
        //単価
        public required int Price { get; set; }
        //カテゴリー
        public required string? Category { get; set; }
        //入出庫数量
        public required int Quantity { get; set; }
        //入出庫日
        public required DateTime LogDate { get; set; }
        //担当者
        public required string? StaffName { get; set; }

    }

}
