using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flying_war
{
    enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }
    abstract class GameObject
    {
        #region 属性成员
        //prop快捷键生成
        //游戏对象的X和Y坐标
        public int X { get; set; }
        public int Y { get; set; }

        //游戏对象的宽和高
        public int Width { get; set; }
        public int Height { get; set; }

        //游戏对象的速度
        public int Speed { get; set; }

        //游戏对象的生命
        public int Lief { get; set; }

        //游戏的方向  
        public Direction Dir { get; set; }

        #endregion

        //为了初始化游戏方便，添加构造函数
        public GameObject(int x, int y, int width, int height, int speed, int life, Direction dir)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Lief = life;
            this.Dir = dir;
        }

        //虚方法，子类不必重写
        //游戏对象移动的方法
        public virtual void Move()
        {
            //根据当前方向移动
            switch (this.Dir)
            {
                case Direction.Right:
                    this.X += this.Speed;
                    break;
                case Direction.Left:
                    //X轴以左边上角0点开始，从右边往左为左边方向为减
                    this.X -= this.Speed;
                    break;
                case Direction.Up:
                    //Y轴以左边上角0点开始，从下往上为减
                    this.Y -= this.Speed;
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
                default:
                    break;
            }
        }

        //抽象的,子类必须重写
        public abstract void Draw(Graphics g);

        // 当对象移动到窗体边缘的时候，对对象的处理
        public abstract void MoveToBorder();
        
        //获得游戏对象当前的矩形,用于碰撞检测
        public Rectangle GetRectangle()
        {   //创建一个矩形Rectangle
            return new Rectangle(this.X,this.Y,this.Width,this.Height);
        }
    }
}
