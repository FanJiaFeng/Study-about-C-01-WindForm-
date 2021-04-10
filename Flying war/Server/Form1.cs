using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //存储客户端的IP地址和服务器与之通信的Socket
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
        //存储客户端的IP和成绩 没有办法直接拿socket要通过ip去拿
        Dictionary<string, int> dicSocre = new Dictionary<string, int>();


        private void btnStart_Click(object sender, EventArgs e)
        {
            //创建一个负责监听的Socket
            Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //获得IP地址和端口号
            IPAddress ip = IPAddress.Parse(txtServer.Text);
            //端口号
            IPEndPoint point = new IPEndPoint(ip, int.Parse(txtPort.Text));
            //绑定，开始监听
            socketWatch.Bind(point);
            //设置监听队列
            socketWatch.Listen(10);

            //显示消息
            ShowMsg("正在等待客户端进入游戏!!!");

            Thread th = new Thread(Listen);
            th.IsBackground = true;
            th.Start(socketWatch);
        }
        //消息框的信息显示
        void ShowMsg(string str)
        {
            txtLog.AppendText(str + "\r\n");
        }

        //接受客户端的连接，接受客户端发来的消息
        void Listen(object o)
        {
            Socket socketWatch = o as Socket;
            while (true)
            {
                //负责监听的Socket接受客户端的连接，返回客户端通信的Socket
                Socket socketSend = socketWatch.Accept();
                //将远程客户端的IP地址和客户端的Socket存储到集合中
                dicSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend);
                //remoteEndPoint获得客户端的ip地址和端口号
                ShowMsg(socketSend.RemoteEndPoint.ToString() + "已经进入游戏！！！");

                //接受客户端发过来的消息
                Thread th2 = new Thread(Rec);
                th2.IsBackground = true;
                th2.Start(socketSend);
            }
        }

        //不停的接受客户端发过来的信息
        void Rec(object o)
        {
            Socket socketSen = o as Socket;
            while (true)
            {
                byte[] buffer = new byte[1024 * 1024 * 3];
                int r = socketSen.Receive(buffer);
                //接受玩家分数
                string strSoce = Encoding.Default.GetString(buffer, 0, r);
                //类型转换
                int soce = Convert.ToInt32(strSoce);
                //把数据添加到disScore集合中
                dicSocre.Add(socketSen.RemoteEndPoint.ToString(),soce);
                //对dicScore进行排列
                Compare();

            }
        }

        //对dicScore进行排列
        void Compare()
        {   //对dicSocre进行降序排列然后转换为list集合
            List<KeyValuePair<string, int>> list = dicSocre.OrderByDescending(n => n.Value).ToList();
            //把结果发给每一个客户端
            for (int i = 0; i < list.Count; i++)
            {
                string result = "您是排名" + (i + 1) + "名，您的成绩是" + list[i].Value;
                byte[] buffer = Encoding.Default.GetBytes(result);
                List<byte> listByte = new List<byte>();
                listByte.Add(2);
                listByte.AddRange(buffer);
                byte[] newbuffer = listByte.ToArray();
                //发送
                dicSocket[list[i].Key].Send(newbuffer);

                
            }
             
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //服务器的应用程序给应用程序发送开始
        private void button3_Click(object sender, EventArgs e)
        {
            //发送一个头，区别命令
            byte[] buffer = new byte[1];
            buffer[0] = 1;
            foreach (KeyValuePair <string,Socket> kv in dicSocket)
            {
                kv.Value.Send(buffer);
            }

        }
    }

}
