using UnityEngine;
using System.Collections;
using Global;

namespace Control
{
    public class Ctrl_BossBruce_Property : Ctrl_BaseEnemyProperty
    {
        public static event del_EnemyAttrUpdate eveBossAttr;

        public int curHeroExpenrence = 1500;
        public int curATK = 50;
        public int curDEF = 10;
        public int curMaxHealth = 10000;

        public GameObject goBossHurtLabelPrefab;

        private GameObject goUIPlayerInfo;
        public GameObject GoUIPlayerInfo
        {
            get
            {
                if (goUIPlayerInfo == null)
                    goUIPlayerInfo = GameObject.FindGameObjectWithTag(Tag.Tag_UIPlayerInfo);
                return goUIPlayerInfo;
            }
        }

        void Start()
        {
            base.heroExpenrence = curHeroExpenrence;
            base.ATK = curATK;
            base.DEF = curDEF;
            base.maxHealth = curMaxHealth;

            base.RunMethodInChilden();
        }

        public override float CurrentHealth
        {
            get { return base.CurrentHealth; }
            set
            {
                base.CurrentHealth = value;

                if (eveBossAttr != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("BossHPChange", new float[2] { base.CurrentHealth, base.maxHealth });
                    eveBossAttr(kv);
                }
            }
        }

        public override void OnHurt(int hurtValue)
        {
            base.OnHurt(hurtValue);

            StartCoroutine(LoadParticalEffectInPool_BossHurtLabel(GlobalParameter.INTERVAL_TIME_0DOT1, goBossHurtLabelPrefab,
                goBossHurtLabelPrefab.transform.position, Quaternion.identity, goBossHurtLabelPrefab.transform.localScale,
                hurtValue, GoUIPlayerInfo.transform));
        }

    }
}