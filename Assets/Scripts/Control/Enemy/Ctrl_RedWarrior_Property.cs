/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：红色（敌人）战士属性
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.28
 *
 *	Version:
 *		1.0
 *
 *	Author:chenx
 *
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Global;
using Kernal;

namespace Control
{
    public class Ctrl_RedWarrior_Property : Ctrl_BaseEnemyProperty
    {
        public int IntHeroExpenrence = 20;                                     //英雄的经验数值
        public int IntATK = 10;                                                //攻击力
        public int IntDEF = 3;                                                 //防御力
        public int IntMaxHealth = 50;                                          //最大的生命数值

        void Start()
        {
            base.HeroExpenrence = IntHeroExpenrence;
            base.ATK = IntATK;
            base.DEF = IntDEF;
            base.MaxHealth = IntMaxHealth;
            //调用父类方法
            base.RunMethodInChilden();
        }
    }
}
