using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flying_war.Properties;
namespace Flying_war
{
    class HeroZiDan : ZiDan
    {
        //导入玩家的子弹
        public static Image img = Resources.bullet1;
        
        //重写构造函数,计算飞机的子弹的在图片的位置
        public HeroZiDan(PlaneFather pf, int power, int speed) : base(pf, pf.X + pf.Width / 4-4, pf.Y,img, speed,power)
        {

        }
    }
}
