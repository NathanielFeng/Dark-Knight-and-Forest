# Dark-Knight-and-Forest

本游戏由我与 [@MeatBall](https://github.com/banbianzhang) 共同开发完成，感谢 MeatBall 好兄弟的鼎力相助。

<p align="center"><img src="Images/img1.png"/></p>

**Dark Knight and Forest（黑暗骑士与森林）** 是一款 2D 横版角色扮演游戏，采用 Unity 游戏引擎、为期半个月开发而成。游戏中，玩家会扮演 Dark Knight（黑暗骑士）探索森林，玩家需要灵活利用已有技能，打败敌人、穿越障碍，历经三个关卡才能到达终点。

游戏灵感来源：《空洞骑士》、《黑暗之魂》、《奥日与黑暗森林》


## 1. 游戏开发过程

初期准备

（1）确定游戏类型

（2）确定分工以及合作的规划

（3）熟悉 Unity 的使用

中期开发 

（4）绘制素材，创建角色和敌人的动画状态机

（5）开发角色脚本，初步建立敌人的 AI 代码

（6）设计并绘制地图，开发UI

（7）修复角色的动作 Bug

后期开发

（8）开发战斗系统

（9）添加背景音乐，音效

（10）优化敌人的 AI，调整平衡性

（11）最终测试


## 2. 开发软件

PC端：

​	Unity 2019.3.4f1（游戏引擎）

​	Adobe Photoshop CC（图像处理）

​	Adobe Audition CC（音频处理）

​	GitHub Desktop（项目管理）

​	Visual Studio 2017（代码编辑）

IOS端：

​	Procreate（图像绘制）


## 3. 任务分工

MeatBall：角色/BOSS 绘制、角色动画设计、角色控制代码、敌人AI优化、战斗系统制作、地图绘制与设计、音效和音乐制作、Bug 测试与修复

NathanielFeng：敌人绘制、敌人动画设计、敌人AI代码、粒子系统构建、主菜单与暂停菜单制作、游戏场景管理、游戏内UI制作、Bug 测试与修复


## 4. 游戏开发过程

### 4.1 素材绘制以及动画

在收集游戏素材的过程中，发现网上的素材并不能适用于我们的游戏类型，即使有也是好几款画风不同的素材，于是我们决定自己绘制。

<p align="center"><img src="Images/img2.gif" width=400 /></p>

Unity自带的动画状态机可以方便地帮我们轻松控制动画的转换，但是由于对其了解不多，开发过程中大多数角色控制Bug的产生都与动画状态机有关。

<p align="center"><img src="/Images/img3.png"/></p>

### 4.2 脚本编程

Unity可以给场景中的物件插入脚本，利用脚本中的代码实现玩家与游戏元素的交互，以及游戏元素之间的交互。

Unity中的每一个脚本的创建都会生成一个类，这个类的所有公共成员变量都可以直接在 Unity 的图形界面中赋值，方便了我们对一些需要经常调整的数值（生命上限、敌人攻击间隔，移动速度……）做修改。

不同物体之间的脚本（即不同类）可以轻易的相互调用，想要找到当前场景中的某一个对象，只需要使用 Unity 提供的函数即可（例如 FindGameObjectWithTag ）。或者直接将需要控制的物体直接 Unity 界面中拖入脚本对应位置，即可让该物体的 GameObject 对象在其他对象中的脚本中作为一个成员变量存在。

<p align="center"><img src="Images/img4.png"/></p>

### 4.3 碰撞体相关

Unity 中提供了多种形式的碰撞体组件，为我们省去了很多麻烦。Unity 的碰撞体可以作为触发器存在（即不会与其他物体发生物理碰撞，而是碰撞后可以产生消息），通过触发器碰撞体，可以很方便地实现一些互动效果，例如碰撞怪物受伤，走到特定位置显示教程等。

Unity 中的刚体（Rigid Body）组件可以直接为物件添加物理效果，实现最基本的重力、摩擦力效果。

在对角色是否站立在地面上的检测中，我们使用了射线类（RaycastHit2D）进行检测，射线可以返回它接触到的物体信息，通过它可以判断角色脚下踩着的是什么物件。

<p align="center"><img src="Images/img5.png"/></p>

### 4.4 地图

地图先是使用了 Unity 中的 TileMap 绘制基本框架，然后以一格100x100像素的标准在 Photoshop 中进行完全绘制。为了节省地图绘制的时间，我们采用先绘制基本元素，再用基本元素堆砌的方式来进行这一步工作。

<p align="center"><img src="Images/img6.png"/></p>

### 4.5 UI和音效

Unity 的对象提供基本的 UI 元素。UI 以 Canvas 对象为基础，可以向其中添加滑条、按钮等物件，并且直接在场景编辑器中进行编辑。通过脚本控制UI可以实现多种不同的功能。我们在游戏中添加了基本的血条，主菜单和暂停菜单。

音乐和音效通过 Audition 进行简单的剪辑和音量调整。背景音乐来自《空洞骑士》，音效则是来自 Adobe 提供的音效库。由于《空洞骑士》发布的原声大碟为完整音乐，所以直接用在游戏中会出现衔接不连贯的问题，靠剪辑和调整难以解决。

<p align="center"><img src="Images/img7.png"/></p>


## 5. 游戏测试与总结

### 5.1 Bug的处理 

在开发的过程中，我们遇到了以下 Bug，这些 Bug 已经基本得到解决：

（1）狂按攻击键导致角色动画卡死的情况（增加了动画状态机的条件检查，但是产生了攻击按键不灵敏的问题）

（2）角色从低往高处平台跳跃时，落地后仍然保持浮空姿态（添加了条件检查，增加了浮空状态向空闲状态的转换）

（3）怪物接近角色后，会在角色身边抽搐移动的情况

​    （在前期设计中，怪物永远面向角色的代码导致的问题，添加了一些让怪物不转身的条件解决）

### 5.2 不足之处总结

（1）主角攻击的手感略差

（2）怪物没有设计好攻击前摇，导致战斗系统缺乏深度

（3）角色控制代码和状态机在后期开发中趋于混乱，难以管理

（4）工期有限且前期想法过多，产生了一部分废案

（5）画面可以进一步优化（增加丁达尔光线、浮动光斑粒子，可动场景草丛等）

（6）打击感仍有进步的空间（增加镜头摇晃，怪物出血效果，强化怪物受伤反馈）


## 6. 游戏运行截图

<p align="center"><img src="Images/img8.png"/></p>

<p align="center"><img src="Images/img9.png"/></p>

<p align="center"><img src="Images/img10.png"/></p>

<p align="center"><img src="Images/img11.png"/></p>