using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flying_war
{
   abstract class Boom : GameObject
    {
        //重写父类的构造函数
        public Boom(int x, int y) : base(x, y, 0, 0, 0, 0,Direction.Up)
        {
        }
    }
}
