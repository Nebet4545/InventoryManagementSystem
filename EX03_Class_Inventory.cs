using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    /// <summary>
    /// 商品の在庫データを管理するクラス
    /// </summary>
    public class Class_Inventory
    {
        //商品ID
        public int ProductId { get; set; }
        //在庫数
        public int ProductStock { get; set; }
    }
}
