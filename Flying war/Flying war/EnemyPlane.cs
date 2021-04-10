using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;

namespace Flying_war
{   //敌人飞机类
    class EnemyPlane : PlaneFather
    {
        //存敌人飞机
            private static Image img1 = Resources.enemy0;
        private static Image img2 = Resources.enemy1;
        private static Image img3 = Resources.enemy2;

        //敌人飞机的类型
        public int EnemType { get; set; }

        //敌人飞机的类型返回图片
        public static Image GetImageWithType(int type)
        {
            switch (type)
            {
                case 0:return img1;
                case 1:return img2;
                case 2:return img3;
              
            }
            return null;
        }

        //敌人飞机的类型返回生命和速度
        public static int GetLifeWithType(int type)
        {
            switch (type)
            {
                case 0: return 1;
                case 1: return 2;
                case 2: return 3;

            }
            return 0;
        }

        //敌人飞机的类型返回速度
        public static int GetSpeedWithType(int type)
        {
            switch (type)
            {
                case 0: return 10;
                case 1: return 5;
                case 2: return 2;

            }
            return 0;
        }

        //重写构造函数
        public EnemyPlane(int x, int y, int type) : base(x, y,GetImageWithType(type),GetSpeedWithType(type),GetLifeWithType(type),Direction.Down)
        {
            this.EnemType = type;
        }

        //将敌人飞机绘制到图片上
        public override void Draw(Graphics g)
        {   //让飞机移动
            base.Move();
            //销毁飞机
            MoveToBorder();
            //根据当前飞机的类型 判断绘制哪一张图片
            switch (this.EnemType)
            {
                case 0:g.DrawImage(img1, this.X, this.Y);break;
                case 1: g.DrawImage(img2, this.X, this.Y); break;
                case 2: g.DrawImage(img3, this.X, this.Y); break;
            }
            if (r.Next(0,101)>80)
            {

                Fire();
            }
        }

        //飞机随机调位
        Random r = new Random();

        //敌人飞机移动到边框
        public override void MoveToBorder()
        {
            if (this.Y>=812)
            {
                //把敌人飞机销毁
                SingleObject.GetSingle().RemoveGameObject(this);
            }
            //写一个让小飞机到一定位置进行调位
            if (this.EnemType==0 && this.Y>=200)
            {
                if (this.X>=0&&this.X<=240)
                {
                    this.X += r.Next(0, 200);
                }
                else
                {
                    this.X -= r.Next(0, 200);
                }
            }
        }

        //敌人发射子弹
        public override void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new EnemyZiDan(this, this.EnemType));
        }

        //敌人死亡
        public override void IsOver()
        {
            if (this.Lief==0)
            {
                //爆炸效果
                SingleObject.GetSingle().AddGameObject(new EnemyBoom(this.X, this.Y, this.EnemType));
                //移除敌人飞机
                SingleObject.GetSingle().RemoveGameObject(this);
            }
        }
    }
}
