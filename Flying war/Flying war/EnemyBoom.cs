using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;

namespace Flying_war
{
    class EnemyBoom : Boom
    {
        //导入敌人爆炸的图片
        private Image[] img1 =
        {
            Resources.enemy0_down11,
            Resources.enemy0_down2,
            Resources.enemy0_down3,
            Resources.enemy0_down4
        };

        private Image[] img2 =
        {
            Resources.enemy1_down11,
            Resources.enemy1_down2,
            Resources.enemy1_down3,
            Resources.enemy1_down4
        };


        private Image[] img3 =
        {
            Resources.enemy2_down11,
            Resources.enemy2_down2,
            Resources.enemy2_down3,
            Resources.enemy2_down4,
            Resources.enemy2_down5,
            Resources.enemy2_down6
        };

        //根据属性返回类型
        public int Type { get; set; }

        public EnemyBoom(int x, int y,int type) : base(x, y)
        {
            this.Type = type;
        }

        //根据属性画图片
        public override void Draw(Graphics g)
        {
            switch (this.Type)
            {
                case 0:
                    for (int i = 0; i < img1.Length; i++)
                    {
                        g.DrawImage(img1[i], this.X, this.Y);
                    }
                    break;
                case 1:
                    for (int i = 0; i < img2.Length; i++)
                    {
                        g.DrawImage(img2[i], this.X, this.Y);
                    }
                    break;
                case 2:
                    for (int i = 0; i < img3.Length; i++)
                    {
                        g.DrawImage(img3[i], this.X, this.Y);
                    }
                    break;
            }
            //画完移除
            SingleObject.GetSingle().RemoveGameObject(this);
        }


        //设计上的bug，无用
        public override void MoveToBorder()
        {
        }
    }
}
