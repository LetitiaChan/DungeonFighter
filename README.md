# DungeonFighter
DungeonFighter，APRG Mobile Game.<br>


##游戏介绍
<p> 主要包含战斗系统，背包，任务，新手引导对话系统，商城，副本BOSS，日志系统等。
    包含五个场景，登录场景、创角场景、新手引导及战斗场景、主城场景、副本场景。</p>

##开发工具
<p> Unity5.3.6 + UGUI + EasyTouch + DOTween</p>

##MVC架构

- Model层：1保存数据；2发送数据更新信息。
- View层：1接受用户界面操作；2根据M层的数据显示相应界面。
- Control层：1处理与界面无关的代码逻辑；2接受和处理网络数据。

##设计模式

- 观察者模式：自动更新视图层玩家数值。
- 代理模式：玩家核心数值、玩家扩展数值模型层代理。
- 责任链模式：新手引导，使多个对象都有机会处理引导请求，降低耦合。将这些对象练成一条链，并沿着这条链传递请求，直到有一个对象处理为止。

##主要优化方法：

- 贴图压缩：Android采用RGBA Compressed ETC2 8 bits
- 音频压缩：播放时长较长的音乐文件压缩成.mp3
- **对象缓冲池**：各场景各种怪物、特效等加入缓冲池
- **小物件层消隐**：针对装饰小物件分层，做距离判断层消隐。
- **手动遮挡剔除**:如副本场景划分区域做触发显示。
- 切换场景时强制垃圾回收
- 场景烘焙，禁用所有光源
- 去除多余的场景资源、烟雾等特效

<br>
【以下手游截图】

![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-01.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-02.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-03.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-04.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-05.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-06.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-07.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-08.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-09.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-10.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-11.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-12.jpg "手游截图")
![手游截图](https://github.com/LetitiaChan/DungeonFighter/blob/master/ReadMeFolder/S80408-13.jpg "手游截图")