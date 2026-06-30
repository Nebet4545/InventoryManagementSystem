using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    /// <summary>
    /// 商品管理表画面に表示するリストのクラスを作成
    /// </summary>
    public class Class_ProductDisplay
    {
        //商品ID
        public int ProductId { get; set; }
        //商品コード
        public required string ProductCode { get; set; }
        //商品名
        public required string ProductName { get; set; }
        //単価
        public int Price { get; set; }
        //在庫数
        public int ProductStock { get; set; }

        public Class_ProductDisplay()
        {
            ProductCode = "";
            ProductName = "";
        }
    }
}
