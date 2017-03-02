/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：敌人战士属性
 *
 *	Description:
 *		1.敌人的各种属性
 *
 *	Date:2017.02.27
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
    public class Ctrl_Warrior_Property : Ctrl_BaseEnemyProperty
    {

        public int IntHeroExpenrence = 5;                                      //英雄的经验数值
        public int IntATK = 2;                                                 //攻击力
        public int IntDEF = 2;                                                 //防御力
        public int IntMaxHealth = 20;                                          //最大的生命值



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