C# OpenSSL.Net 實作測試

資料來源:https://www.cnblogs.com/azeri/p/8972432.html
https://bit.ly/3sZvZlG

Blog: https://bit.ly/38qJG5g

最近在工作中遇到需要對數據傳輸進行加密解密，一開始是.Net與.Net環境間進行交互，使用.Net下的【System.Security.Cryptography】完全沒有問題，但後來要與Java，Android ，IOS進行交互，結果是怎麼都對不上，在查看後得知三者平台都使用的OpenSSL進行的加解密，於是就翻出了OpenSSL.Net

