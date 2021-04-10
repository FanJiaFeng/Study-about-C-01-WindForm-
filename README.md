# Study-about-C-01-WindForm-
Study about C# 01 WindForm 
1、设计整个游戏中的顶级父类
	GameObject:
		属性成员：
			X、Y、Width、Height、Speed、Life、Direction
		构造函数
		Move()函数：在场景中进行移动
		abstract DrawGameObject(Graphics g)：绘制自己到窗体
		abstract MoveToBorder()：移动到边框要采取的措施
		GetRectangle():返回一个矩形，用于碰撞检测
		
		
2、实现窗体背景移动
	--->导入图片资源
	--->重写DrawGameObject()
	--->重写MoveToBorder()
	--->设计单例类，存储全局唯一的背景对象
		--->把背景对象作为属性封装到单例中(SingleObject)
		--->AddGameObject(GameObject go):添加游戏对象到窗体中
		--->在Load事件中调用InitialGameObject()
		--->在单例类中封装Draw函数，负责将所有的游戏对象都绘制到窗体中
		--->在窗体的Paint事件中调用单例类中Draw函数。
		--->拖入Timer，不停的刷新窗体
		--->窗体双缓冲



	
3、设计玩家飞机和敌人飞机共同的父类
	---->都可以发射子弹
	---->都可以被击落
	---->存储图片
	
	
	
4、设计玩家飞机类
	--->导入玩家飞机图片
	--->调用父类构造函数
	--->重写DrawGameObject()以及MoveToBorder()
	--->去单例类中声明一个属性，存储全局唯一的玩家对象
	--->在Form1类中的InitialGameObject()函数中创建玩家飞机对象
	--->添加MoveWithMouse()
	--->给Form窗体注册MouseMove事件，在事件调用玩家对象的MoveWithMouse()
	
	
	
5、设计敌人飞机类
	---->导入敌人的三种飞机图片
	---->设计类型属性 EnemyType
	---->根据属性返回不同值
		--->返回速度
		--->返回生命
		--->返回图片
	--->重写父类的DrawGameObject();根据不同类型的敌人飞机绘制不同的图片
    --->重写父类的MoveToBorder();当敌人超出窗体界限的时候，销毁敌人飞机对象
	--->在单例类中声明一个集合来存储敌人飞机对象，便于我们对敌人飞机的管理
	--->在单例类中写入RemoveGameObject()函数，将没用的游戏对象移出游戏
	--->在Form1中的InitialGameObject()函数中，创建敌人飞机对象
	--->判断敌人飞机是否够用 如果不够用 则重新创建敌人飞机对象
	--->改变飞机移动的轨道以及速度
	
	
6、设计子弹父类
7、设计玩家子弹类
8、设计敌人子弹类
9、发射子弹
10、设计玩家和敌人爆炸的父类
11、敌人爆炸
12、玩家爆炸
13、碰撞检测
14、实现连机对战
		---->在服务器端创建负责监听的Socket，等待客户端的连接
		---->写了一个线程，来执行Listen，不停的接收客户端的连接
		---->客户端开始创建负责连接服务器的Socket
		---->服务器给客户端发送数据，开始游戏。
		---->客户端接收服务器发送过来的数据
		---->指定一个游戏的运行时间，当时间到了的时候，停止游戏，并且客户端将分数传输
		给服务器
		---->服务器不停的接收客户端发送过来的消息

















