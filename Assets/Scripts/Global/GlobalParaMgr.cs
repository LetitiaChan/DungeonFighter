/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:公共层： 全局参数管理器
 *
 *	Description:
 *		1.跨场景全局数值传递
 *
 *	Date:2017.02.20
 *
 *	Version:
 *		1.0
 *
 *	Author:chenx
 *
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Global
{
	public static class GlobalParaMgr {
        //下一场景名称
        public static EnumScenes NextSceneName = EnumScenes.ScenesLogin;
        //玩家名称
        public static string PlayerName="";
        //英雄类型
        public static EnumHeroType HeroType = EnumHeroType.SwordHero;


	}
}