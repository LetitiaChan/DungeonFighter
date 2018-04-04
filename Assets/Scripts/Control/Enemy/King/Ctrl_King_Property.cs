using UnityEngine;
using System.Collections;

namespace Control
{
    public class Ctrl_King_Property : Ctrl_BaseEnemyProperty
    {
        public int curHeroExpenrence = 50;
        public int curATK = 30;
        public int curDEF = 10;
        public int curMaxHealth = 50;


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
