using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flying_war
{   //用单例类维护所有类
    class SingleObject
    {
        #region 设计单例类
        //1.构造私有函数
        private SingleObject()
        { }

        //2.声明全局唯一的单例对象
        private static SingleObject _single = null;

        //3.声明一个静态函数返回全局唯一的单例对象
        public static SingleObject GetSingle()
        {
            if (_single == null)
            {
                _single = new SingleObject();
            }
            return _single;
        }
        #endregion

         //在单例类中存储背景对象
        public Background BG { get; set; }
        //在单例类中存储游戏对象
        public HeroPlane HP { get; set; }
        //在单例类中存储飞机的集合
        public List<EnemyPlane> listEnemyPlan = new List<EnemyPlane>();
        //在单例类中存储子弹的集合  
        public List<HeroZiDan> listHeroZiDan = new List<HeroZiDan>();
        //在单例类中存储敌人子弹的集合 
        public List<EnemyZiDan> listEnemyZiDan = new List<EnemyZiDan>();
        //存储敌人爆炸的集合
        private List<EnemyBoom> listEnemyBoom = new List<EnemyBoom>();
        //存储玩家爆炸的集合
        private List<HeroBoom> listHeroBoom = new List<HeroBoom>();

        //声明一个属性来存玩家的分数
        public int Socore { get; set; }

        //写一个函数将游戏对象们添加进游戏场景中
        public void AddGameObject(GameObject go)
        {   
            if (go is Background)
            {//如果传进来的是背景对象，把值赋给SingleObject类中的BG属性
                this.BG = go as Background;
            }
            else if (go is HeroPlane)
            {   //如果传进来的是玩家对象，把值赋给SingleObject类中的PH属性
                this.HP = go as HeroPlane;
            }
            else if (go is EnemyPlane)
            {//如果传进来的是敌人对象，把值赋给SingleObject类中的listEnemyPlan属性
                this.listEnemyPlan.Add(go as EnemyPlane);
            }
            else if (go is HeroZiDan)
            { //如果传进来的是玩家子弹，把值赋给SingleObject类中的listHeroZiDan属性
                this.listHeroZiDan.Add(go as HeroZiDan);
            }
            else if (go is EnemyZiDan)
            {
                this.listEnemyZiDan.Add(go as EnemyZiDan);
            }
            else if (go is EnemyBoom)
            {
                this.listEnemyBoom.Add(go as EnemyBoom);
            }
            else if (go is HeroBoom)
            {
                this.listHeroBoom.Add(go as HeroBoom);
            }
        }

        //把所有游戏对象们绘制到窗体中都在这里封装
        public void DrawGameObject(Graphics g)
        {
            //绘画玩家飞机
            this.HP.Draw(g);
            //绘制敌人飞机
            for (int i = 0; i < listEnemyPlan.Count; i++)
            {
                listEnemyPlan[i].Draw(g);
            }
            //绘制玩家子弹
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {
                listHeroZiDan[i].Draw(g);
            }
            //绘制敌人子弹
            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                listEnemyZiDan[i].Draw(g);
            }
            //绘制敌人爆炸
            for (int i = 0; i < listEnemyBoom.Count; i++)
            {
                listEnemyBoom[i].Draw(g);
            }
            for (int i = 0; i < listHeroBoom.Count; i++)
            {
                listHeroBoom[i].Draw(g);
            }
        }

        //游戏中把敌人对象给移除
        public void RemoveGameObject(GameObject go)
        {
            if (go is EnemyPlane)
            {
                listEnemyPlan.Remove(go as EnemyPlane);
            }
            else if (go is HeroZiDan)
            {
                listHeroZiDan.Remove(go as HeroZiDan);
            }
            else if (go is EnemyZiDan)
            {
                listEnemyZiDan.Remove(go as EnemyZiDan);
            }
            else if (go is EnemyBoom)
            {
                listEnemyBoom.Remove(go as EnemyBoom);
            }
            else if (go is HeroBoom)
            {
                listHeroBoom.Remove(go as HeroBoom);
            }
        }

        //碰撞检测
        public  void PZJC()
        {
            #region 玩家子弹打敌人
            //检测玩家的子弹是否打中敌人飞机
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {   //玩家打出子弹进入for循环是否击中敌人飞机
                for (int j = 0; j < listEnemyPlan.Count; j++)
                {
                    if (listHeroZiDan[i].GetRectangle().IntersectsWith(listEnemyPlan[j].GetRectangle()))
                    {
                        // 如果成立，则表示玩家子弹击中了敌人
                        //敌人的生命值应该减少
                        listEnemyPlan[j].Lief -= listHeroZiDan[i].Power;

                        if (listEnemyPlan[j].Lief==0)
                        {   //判断飞机的属性来加分
                            AddSocore(j);
                        }

                        //判断是否死亡移除
                        listEnemyPlan[j].IsOver();

                        //移除子弹
                        listHeroZiDan.Remove(listHeroZiDan[i]);
                        break;
                    }
                }
            }
            #endregion

            //检测敌人的子弹是否打中玩家飞机
            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                if (listEnemyZiDan[i].GetRectangle().IntersectsWith(this.HP.GetRectangle()))
                {
                    this.HP.IsOver();
                    //移除敌人子弹
                    listEnemyZiDan.Remove(listEnemyZiDan[i]);
                    
                }
            }

            //检查玩家是否和敌人发生了碰撞
            for (int i = 0; i < listEnemyPlan.Count; i++)
            {
                if (listEnemyPlan[i].GetRectangle().IntersectsWith(this.HP.GetRectangle()))
                {
                    listEnemyPlan[i].Lief = 0;
                    if (listEnemyPlan[i].Lief == 0)
                    {   //判断飞机的属性来加分
                        AddSocore(i);
                    }

                    listEnemyPlan[i].IsOver();
                }
            }

            //检测玩家的子弹是否打中敌人子弹
            for (int i = 0; i < listHeroZiDan.Count; i++)
            {   //玩家打出子弹进入for循环是否击中敌人子弹
                for (int j = 0; j < listEnemyZiDan.Count; j++)
                {
                    if (listHeroZiDan[i].GetRectangle().IntersectsWith(listEnemyZiDan[j].GetRectangle()))
                    {
                        //移除子弹
                        listEnemyZiDan.Remove(listEnemyZiDan[j]);
                        listHeroZiDan.Remove(listHeroZiDan[i]);
                        break;
                    }
                }
            }

        }

        //加分的封装
        private void AddSocore(int j)
        {
            switch (listEnemyPlan[j].EnemType)
            {
                case 0: Socore += 100; break;
                case 1: Socore += 300; break;
                case 2: Socore += 600; break;
            }
        }
    }
}
