using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;

namespace Flying_war
{
    class Background : GameObject
    {
        //添加背景图片
        private static Image img = Resources.background;

        //构造父类无参的构造函数并传值
        public Background(int x, int y, int speed) : base(x, y, img.Width, img.Height, speed, 0,Direction.Down)
        {
        }

        //画背景图
        public override void Draw(Graphics g)
        {   //调用父类的移动
            this.MoveToBorder();
            //绘画指定的大写
            g.DrawImage(img, this.X, this.Y);
        }

        //重写移动到游戏边框时候
        public override void MoveToBorder()
        {
            this.Y += this.Speed;
            if (this.Y>=0)
            {
                this.Y = -850;
            }
        }
    }
}
