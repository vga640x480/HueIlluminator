using System;
using System.Collections.Generic;
using System.Text;
using WebSocket4Net;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using WebSocket4Net;

namespace WebSocketTest
{
    class Program
    {


        static void Main(string[] args)
        {



#if false
                    WebSocket websocket = new WebSocket("ws://localhost:8080");
                    websocket.Opened += (s, e) => { websocket.Send("Hello World!"); };
                    websocket.Error += (s, e) => { Debug.WriteLine(e.ToString()); }; 
                    //websocket.Closed += new EventHandler(websocket_Closed);
                    //websocket.MessageReceived += new EventHandler(websocket_MessageReceived);
                    websocket.Open();
                    Console.ReadLine();
#else
            //                        var ws = new WebSocket("ws://localhost:3000/brainwave");
            var ws = new WebSocket("ws://frozen-plains-4732.herokuapp.com/brainwave");

            /// 文字列受信
            ws.MessageReceived += (s, e) =>
            {
                Console.WriteLine("{0}:String Received:{1}", DateTime.Now.ToString(), e.Message);
            };

            /// バイナリ受信
            ws.DataReceived += (s, e) =>
            {
                Console.WriteLine("{0}:Binary Received Length:{1}", DateTime.Now.ToString(), e.Data.Length);
            };

            /// サーバ接続完了
            ws.Opened += (s, e) =>
            {
                Console.WriteLine("{0}:Server connected.", DateTime.Now.ToString());
            };

            ws.Error += (s, e) =>
            {
                Console.WriteLine("{0}:Error({1}).", DateTime.Now.ToString(), e.Exception.Message);
            };

            /// サーバ接続開始
            ws.Open();

            /// 送受信ループ
            while (true)
            {
                var str = Console.ReadLine();
                if (str == "END") break;

                if (ws.State == WebSocketState.Open)
                {
                    ws.Send(str);
                }
                else
                {
                    Console.WriteLine("{0}:wait...", DateTime.Now.ToString());
                }
            }


            /// ソケットを閉じる
            ws.Close();
#endif
        }
    }
}
