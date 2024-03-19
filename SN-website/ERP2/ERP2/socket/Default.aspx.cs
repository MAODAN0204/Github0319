using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ERP2.scoket
{
    public partial class Default : System.Web.UI.Page
    {
        static Socket SocketListener;

        protected void Page_Load(object sender, EventArgs e)
        {
            ServerEnd(6600, 10);                         //10個應用程序可以連線到此
            Thread th = new Thread(ServerCommunity);
            th.Start(SocketListener);
        }

        private static void ServerEnd(int port, int allowNum)
        {   //server端設定內容

            /*==============================
             AddressFamily.InterNetwork表示利用IP4協議
             SocketType.Stream 因為我們要使用TCP協議，需使用流式的Socket
             ProtocolType.Tcp 選用TCP協議 
            =================================*/
            SocketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress myip = IPAddress.Any;             //指定伺服器IP, IPAddress.Any是取得本機的IP
            int Listen_port = port;                     //設定傳輸端口

            IPEndPoint point = new IPEndPoint(myip, Listen_port); //將IP和port帶到socketListener中
            SocketListener.Bind(point);                 //將point綁定給socketListener
            //ShowMsg("Listening...");                    //開始文字
            SocketListener.Listen(allowNum);            //設定有幾個程序可以連線
        }

        private static void ServerCommunity(object obListener)  //開始傳輸
        {

            Socket temp = (Socket)obListener;           //Server用來傳送資料給客戶的 Socket

            while (true)
            {
                Socket socketSender = temp.Accept();    //用Accept接收監聽用 Socket 的資料

                //ShowMsg(("Client IP：" + socketSender.RemoteEndPoint.ToString()) + " Connect Success!");
                Thread ReceiveMsg = new Thread(ReceiveClient);  //New一個接收執行緒
                ReceiveMsg.IsBackground = true;         //接收程序在背景執行
                ReceiveMsg.Start(socketSender);         //讀取客戶端訊息
            }
        }

        private static void ReceiveClient(object socketSender)  //接受客戶端文字, 直到客戶端離開下(斷)線
        {
            Socket GsocketSender = socketSender as Socket;
            while (true)
            {
                if (GsocketSender.Poll(-1, SelectMode.SelectRead))
                {
                    byte[] buffer = new byte[1024];                 //創立一個數組來儲存客戶端所回傳的訊息
                    int rece = GsocketSender.Receive(buffer);       //讀取字節數

                    //檢查是否網路斷線
                    if (rece == 0)                                    //如果客戶端離開所得到的字節數會等於0，跳出此循環
                    {
                        // ShowMsg(string.Format("Client： {0} + 下線了", GsocketSender.RemoteEndPoint.ToString()));
                        break;

                    }
                    string clientMsg = Encoding.UTF8.GetString(buffer, 0, rece);//第一個引數代表要讀取的byte[], 第二個引數代表從左邊數來第幾個字開始讀取, 每次讀取的字節數
                    //ShowMsg(string.Format("{0}", clientMsg));
                    contain.InnerHtml = clientMsg;
                    //Split_type(clientMsg);
                }
            }
        }
    }
}