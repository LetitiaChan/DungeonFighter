using UnityEngine;
using System.Collections;

namespace Control
{
    public class Ctrl_Archer_Property : Ctrl_BaseEnemyProperty
    {
        public int curHeroExpenrence = 10;
        public int curATK = 0;      //实际攻击力在道具上
        public int curDEF = 5;
        public int curMaxHealth = 10;


        void Start()
        {
            base.heroExpenrence = curHeroExpenrence;
            base.ATK = curATK;
            base.DEF = curDEF;
            base.maxHealth = curMaxHealth;

            base.RunMethodInChilden();
        }
    }
}