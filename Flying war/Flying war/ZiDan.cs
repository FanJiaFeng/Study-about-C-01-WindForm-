using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;

namespace Flying_war
{   //子弹父类
    class ZiDan : GameObject
    {   //图片
        private Image imgZiDan;

        //子弹的威力
        public int Power { get; set; }

        //构造函数重写
        public ZiDan(PlaneFather pf,int x,int y,Image img,int speed, int power) : base(x, y,img.Width,img.Height, speed, 0, pf.Dir)
        {
            this.imgZiDan = img;
            this.Power = power;
        }

        //画子弹
        public override void Draw(Graphics g)
        {
            base.Move();
            g.DrawImage(imgZiDan, this.X, this.Y,this.Width/2,this.Height/2);
             
        }

        //移除对象
        public override void MoveToBorder()
        {
            if (this.Y<=0 || this.Y>812)
            {
                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }
    }
}
