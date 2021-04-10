using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;

namespace Flying_war
{
    class HeroBoom : Boom
    {
        private Image[] img1 =
        {
            Resources.hero_blowup_n1,
            Resources.hero_blowup_n2,
            Resources.hero_blowup_n3,
            Resources.hero_blowup_n4
        };

        public HeroBoom(int x, int y) : base(x, y)
        {
        }

        //画玩家图片
        public override void Draw(Graphics g)
        {
            for (int i = 0; i < img1.Length; i++)
            {
                g.DrawImage(img1[i], this.X, this.Y);
            }
            SingleObject.GetSingle().RemoveGameObject(this);
        }

        //bug不写
        public override void MoveToBorder()
        {
           
        }
    }
}
