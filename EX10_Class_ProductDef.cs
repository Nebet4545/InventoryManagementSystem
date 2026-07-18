using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace InventoryManagementSystem
{
    /// <summary>
    /// フォーム内で使う一部の関数をまとめたクラス
    /// </summary>
    public class Class_ProductDef
    {
        /// <summary>
        /// 商品コードが入力(選択)されているかをチェックする関数
        /// </summary>
        public bool isValid(string cmbProductCode, out string ErrMsg)
        {
            ErrMsg = ""; //呼び出し元で表示するエラーメッセージ
            //入力チェック
            if (string.IsNullOrWhiteSpace(cmbProductCode))
            {
                //呼び出し元で表示するエラーメッセージ
                ErrMsg = "商品コードが選択されていません。";
                return false;
            }
            return true;
        }
        /// <summary>
        /// 商品コードを指定して、現在の在庫数を計算して取得する関数
        /// </summary>
        /// <returns></returns>
        public int GetCurrentStock(string ProductCode)
        {
            //指定した商品を探す
            var foundProduct = Class_DataStore.Products
                .FirstOrDefault(p => p.ProductCode.Equals(ProductCode, StringComparison.OrdinalIgnoreCase));

            //商品自体が存在しない場合の処理
            if (foundProduct == null)
            {
                //呼び出し元に在庫数を0で返す
                return  0;
            }
            //商品が見つかった場合の処理
            else
            {
                //呼び出し元に現在の在庫数を計算して返す
                return Class_DataStore.Inventory
                    .Where(p => p.ProductId == foundProduct.ProductId)
                    .Sum(x => x.ProductStock);
            }
        }
        /// <summary>
        /// 選択された商品コードの商品名を調べる関数
        /// </summary>
        /// <returns></returns>
        public bool ProductNameCheck(string ProductCode, out string ProductName, out string ErrMsg)
        {
            //商品名の初期化を行う
            ProductName = "";
            //呼び出し元で表示するエラーメッセージ
            ErrMsg = "";

            //選択された商品コードの商品名を取得する

            //DataStore(Products)から商品コードを探す
            var foundProductCode = Class_DataStore.Products
                .FirstOrDefault(n => n.ProductCode.Equals(ProductCode, StringComparison.OrdinalIgnoreCase));

            //商品コードが見つからなかった場合の処理
            if (foundProductCode == null)
            {
                ErrMsg = "該当の商品が見つかりませんでした。";
                return false;
            }
            else
            {
                //商品コードから商品名を取得する
                ProductName = foundProductCode.ProductName; //商品名
            }
            return true;
        }
        /// <summary>
        /// 入庫または出庫数が適正かチェックする関数
        /// </summary>
        /// <returns></returns>
        public bool IsStockCheck(string Checkquan,string Category, out string ErrMsg, out int quan, int Curr = 0)
        {
            //入庫または出庫数を初期化する
            quan = 0;
            //エラーメッセージを初期化する
            ErrMsg = "";
            //カテゴリーを可変式で識別する変数を生成
            string ActionName = "";

            //カテゴリーが入庫の場合、ActonNameを"入庫"にする
            if (Category == "入庫")
            {
                ActionName = "入庫";
            }
            //カテゴリーが出庫の場合、ActonNameを"出庫"にする
            else if (Category == "出庫")
            {
                ActionName = "出庫";
            }

            //入庫または出庫数が設定されているかチェック
            if (string.IsNullOrWhiteSpace(Checkquan))
            {
                //呼び出し元で表示するエラーメッセージ
                ErrMsg = $"{ActionName}数量が入力されていません。";
                return false;
            }
            //入庫または出庫数が数値じゃない場合の処理
            if (!int.TryParse(Checkquan, out quan))
            {
                //呼び出し元で表示するエラーメッセージ
                ErrMsg = "入力値が不正です。";
                return false;
            }
            //入庫または出庫数が1以上かチェックする
            if (quan <= 0)
            {
                //メッセージを表示する
                ErrMsg = $"{ActionName}可能数は1以上です。";
                return false;
            }

            //カテゴリーが出庫の場合、現在の在庫数を上回る出庫を行わないよう制御する
            if (Category == "出庫")
            {
                if (quan > Curr)
                {
                    ErrMsg = $"{ActionName}数が現在の在庫数を超過しています。";
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 担当者名が入力されているかをチェックする関数
        /// </summary>
        /// <returns></returns>
        public bool IsStaffNameCheck(string staff)
        {
            //担当者が入力されているかチェックする

            //入力されていなければtrue(フォーム内でのチェックに使うため)
            return string.IsNullOrWhiteSpace(staff);
        }
        /// <summary>
        /// 選択された商品コードの商品ID・商品名・単価を取得する関数
        /// </summary>
        /// <returns></returns>
        public bool TryGetProductValues(string ProductCode,out int ProductId,out string ProductName,out int Price, out string ErrMsg)
        {
            //エラーメッセージの初期化
            ErrMsg = "";

            //商品コードから商品の情報を取得する
            var foundProduct = Class_DataStore.Products
                .FirstOrDefault(p => p.ProductCode.Equals(ProductCode, StringComparison.OrdinalIgnoreCase));

            //商品自体が見つからなかった場合の処理
            if (foundProduct == null)
            {
                ProductId = 0; //商品IDを0にする(ダミーの値)
                ProductName = ""; //商品名を””にする(ダミーの値)
                Price = 0; //商品単価を0にする(ダミーの値)

                ErrMsg = "商品が見つかりませんでした。";
                return false;
            }
            //商品が見つかった場合の処理
            else
            {
                ProductId = foundProduct.ProductId; //商品IDを代入する
                ProductName = foundProduct.ProductName; //商品名を代入する
                Price= foundProduct.Price; //商品単価を代入する

                return true;
            }
        }
        /// <summary>
        /// 指定した商品の商品IDを取得する関数
        /// </summary>
        /// <returns></returns>
        public bool GetProductId(string ProductCode,out int ProductId,out string ErrMsg)
        {
            //エラーメッセージの初期化
            ErrMsg = "";
            //商品IDを初期化
            ProductId = 0;

            //商品コードから商品の情報を取得する
            var foundProduct = Class_DataStore.Products
                .FirstOrDefault(p => p.ProductCode.Equals(ProductCode, StringComparison.OrdinalIgnoreCase));

            //商品自体が見つからなかった場合の処理
            if (foundProduct == null)
            {
                ProductId = 0; //商品IDを0にする(ダミーの値)
                ErrMsg = "商品が見つかりませんでした。";
                return false;
            }
            //商品が見つかった場合の処理
            else
            {
                ProductId = foundProduct.ProductId; //商品IDを代入する
                return true;
            }
        }
        }
    }

