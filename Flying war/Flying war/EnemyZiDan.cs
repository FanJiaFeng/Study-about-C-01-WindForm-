using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;

namespace Flying_war
{   //敌人子弹
    class EnemyZiDan : ZiDan
    {
        //图片
        private static Image img = Resources.bullet21;

        //根据类型返回不一样的子弹
        public int Type { get; set; }

        public static int GetPwoerWithType(int type)
        {
            switch (type)
            {
                case 0: return 1;
                case 1: return 2;
                case 2: return 3;

            }
            return 0;
        }

        public static int GetSpeedWithType(int type)
        {
            switch (type)
            {
                case 0: return 20;
                case 1: return 10;
                case 2: return 5;

            }
            return 0;
        }

        //重写构造函数,计算飞机的子弹的在图片的位置
        public EnemyZiDan(PlaneFather pf, int type) : base(pf, pf.X + pf.Width / 2, pf.Y+pf.Height,img,GetSpeedWithType(type), GetPwoerWithType(type))
        {
            this.Type = Type;
        }
    }
}
