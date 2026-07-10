using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    //各クラス毎にまとめるデータを扱うクラス
    public static class Class_DataStore
    {
        //商品マスタ【商品を管理するデータ】
        public static BindingList<Class_Product> Products { get; set; } = new BindingList<Class_Product>();
        //ログリスト【入出庫等の履歴を管理するデータ】
        public static BindingList<Class_Log> StockLogs { get; set; } = new BindingList<Class_Log>();
        //表示【表示用にカスタムするデータ】
        public static BindingList<Class_ProductDisplay> ProductDisplays { get; set; } = new BindingList<Class_ProductDisplay>();
        //在庫管理リスト
        public static BindingList<Class_Inventory> Inventory { get; set; } = new BindingList<Class_Inventory>();
    }
}
