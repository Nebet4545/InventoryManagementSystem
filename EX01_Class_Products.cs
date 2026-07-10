using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    /// <summary>
    /// 商品データを扱うクラス
    /// </summary>
    /// 
    public class Class_Product
    {
        //商品ID
        public int ProductId { get; set; }
        //商品コード
        public required string ProductCode { get; set; }
        //商品名
        public required string ProductName { get; set; }
        //商品単価
        public required int Price { get; set; }
        //登録日
        public DateTime CreateDate { get; set; }
        //担当者
        public required string StaffName { get; set; }

    }
}

