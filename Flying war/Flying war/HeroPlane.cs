using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;
using System.Windows.Forms;

namespace Flying_war
{
    class HeroPlane : PlaneFather
    {
        //导入玩家图片
        private static Image imgHero = Resources.hero1;

        //重新写父类构造函数
        public HeroPlane(int x, int y, int speed, int life, Direction dir) : base(x, y, imgHero, speed, life, dir)
        {
        }

        //绘制玩家图片
        public override void Draw(Graphics g)
        {
            //在画的时候时刻不让超边界
            this.MoveToBorder();
            g.DrawImage(imgHero, this.X, this.Y,this.Width/2,this.Height/2);
        }



        //让对象不超过边界
        public override void MoveToBorder()
        {
            if (this.X<=0)
            {
                this.X = 0;
            }
            if (this.Y<=0)
            { 
                this.Y = 0;
            }
            if (this.X>=480-this.Width/2)
            {
                this.X = 480 - this.Width/2;
            }
            if (this.Y>=850-this.Height/2)
            {
                this.Y = 850 - this.Height/2;
            }
        }

        //鼠标移动
        public void MoveWithMouse(MouseEventArgs e)
        {
            this.X = e.X;
            this.Y = e.Y;
        }

        //玩家发射子弹
        public override void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new HeroZiDan(this, 1, 20));
        }

        //玩家点击鼠标发射子弹
        public void MouseDownLeft(MouseEventArgs e)
        {
            if (e.Button==System.Windows.Forms.MouseButtons.Left)
            {
                Fire();
            }
        }

        public override void IsOver()
        {   //玩家死亡
            SingleObject.GetSingle().AddGameObject(new HeroBoom(this.X, this.Y));
        }
    }
}
