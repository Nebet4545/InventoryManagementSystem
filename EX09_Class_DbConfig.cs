using System;
using System.Configuration;

namespace InventoryManagementSystem
{
    // 接続情報を管理するクラス
    public static class Class_DbConfig
    {
        // 家用・学校用のファイルを自動で切り替えて読み込む
        public static string? ConnectionString
        => Environment.GetEnvironmentVariable("DB_CONNECTION");
    }
}