using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;

namespace Flying_war
{
    abstract class PlaneFather : GameObject
    {   //这里不加static 没意义 然后在函数中不能被this调用
        private Image imgPlane;

        public PlaneFather(int x, int y, Image img, int speed, int life, Direction dir) : base(x, y, img.Width,img.Height, speed, life, dir)
        {
            this.imgPlane = img;
        }
        //开火!
        public abstract void Fire();

        //判断是否死亡
        public abstract void IsOver();
    }
}
