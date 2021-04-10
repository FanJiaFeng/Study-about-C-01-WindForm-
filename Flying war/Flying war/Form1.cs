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

namespace Flying_war
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialGame();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //随机数
        Random r = new Random();
        //当前游戏是否开始
        bool isStar = false;
        //记录客户端的成绩
        string result = string.Empty;
        //是否绘制结果字符串
        bool isPaintResult = false;

        //初始化对象们
        private void InitialGame()
        {
            //初始化背景对象 并且赋值
            SingleObject.GetSingle().AddGameObject(new Background(0, -850, 20));
            //初始化玩家对象
            SingleObject.GetSingle().AddGameObject(new HeroPlane(200, 200, 20, 1, Direction.Up));

        }

       //初始化敌人飞机对象
      private void IntiaEnemyPlane()
        {
            for (int i = 0; i < 4; i++)
            {
                SingleObject.GetSingle().AddGameObject(new EnemyPlane(r.Next(0, this.Width), -200, r.Next(0, 2)));
            }
            //让打飞机很少出现
            if (r.Next(1,101) > 60)
            {
                SingleObject.GetSingle().AddGameObject(new EnemyPlane(r.Next(0, this.Width), -200, r.Next(3)));
            }
        }

    //在paint中绘制各种游戏对象
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //让界面先运行图画
            SingleObject.GetSingle().BG.Draw(e.Graphics);

            if (isStar)
            {
                SingleObject.GetSingle().DrawGameObject(e.Graphics);

            }
            //绘制游戏的分数
            string s = SingleObject.GetSingle().Socore.ToString();
            e.Graphics.DrawString(s, new Font("微软雅黑", 20, FontStyle.Bold), Brushes.Red, new Point(0, 0));
            if (isPaintResult)
            {
                //绘制排名结果
                e.Graphics.DrawString(result, new Font("微软雅黑", 20, FontStyle.Bold), Brushes.Red, new Point(0, 200));
            }
           
        }

        //不停的让窗体重绘 
        private void timer1_Tick(object sender, EventArgs e)
        {   //不停的让窗体重绘 记得开双缓存在Doubkebuffered中
            this.Invalidate();
            //不停的判断当前飞机的数量
            if (SingleObject.GetSingle().listEnemyPlan.Count<=1)
            {
                IntiaEnemyPlane();
            }

            //碰撞检测
            SingleObject.GetSingle().PZJC();

        }

        //把鼠标移动赋值给玩家飞机的坐标
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //调用函数
            SingleObject.GetSingle().HP.MoveWithMouse(e);
        }

        //全局客户端的socket
        Socket socket;
        //计时
        int playTime = 0;

        //鼠标双击击发射子弹
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            SingleObject.GetSingle().HP.MouseDownLeft(e);
        }

        //进入游戏
        private void button1_Click(object sender, EventArgs e)
        {
            //在客户端创建负责连接服务器的socket
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //服务器的ip
            IPAddress ip = IPAddress.Parse(txtServer.Text);
            //服务器应用程序的端口号
            IPEndPoint point = new IPEndPoint(ip, int.Parse(txtPort.Text));
            //客户端连接服务器的应用程序
            socket.Connect(point);

            //不停的接受服务器发过来的消息
            Thread th = new Thread(Rec);
            th.IsBackground = true;
            th.Start();
        }

        //接受服务器的信息
        void Rec()
        {
            while (true)
            { byte[] buffer = new byte[1024*1024*5];
                //将接受到的数据放到buffer中
                int r=socket.Receive(buffer);
                if (buffer[0]==1)
                {
                    //发送的是开始游戏的信息
                    isStar = true;
                }
                else if (buffer[0]==2)
                {
                    //获得服务器的结果
                    result = Encoding.Default.GetString(buffer, 1, r - 1);
                    //绘制到paint
                    isPaintResult = true;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isStar)
            {
                playTime++;//如果开始游戏就开始计时间
                if (playTime==20)
                {//到达时间，停止游戏
                    isStar = false;
                    //将结果发送给服务器 转为字符数组才能发送
                    byte[] buffer= Encoding.Default.GetBytes( SingleObject.GetSingle().Socore.ToString());

                    socket.Send(buffer);
                    //绘制到paint
                    isPaintResult = true;
                }
            }
        }
    }
}
