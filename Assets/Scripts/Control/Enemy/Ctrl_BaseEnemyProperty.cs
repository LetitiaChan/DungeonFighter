using UnityEngine;
using System.Collections;
using Global;
using Kernal;

namespace Control
{
    public class Ctrl_BaseEnemyProperty : BaseControl
    {
        internal int heroExpenrence = 0;
        internal int ATK = 0;
        internal int DEF = 0;
        internal int maxHealth = 0;

        public EnemyState CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; }
        }

        public virtual float CurrentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = value; }
        }

        private float _currentHealth = 0;
        private EnemyState _currentState = EnemyState.Idle;


        /*由于加入对象缓冲池，需要在OnEnable OnDisable里开启停止*/
        void OnEnable()
        {
            StartCoroutine("CheckLifeContinue");
        }
        void OnDisable()
        {
            StopCoroutine("CheckLifeContinue");
        }


        public void RunMethodInChilden()
        {
            CurrentHealth = maxHealth;
        }

        IEnumerator CheckLifeContinue()
        {
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);

                if (CurrentHealth <= maxHealth * 0.01 && _currentState != EnemyState.Dead)
                {
                    _currentState = EnemyState.Dead;

                    Ctrl_HeroProperty.Instance.AddExp(heroExpenrence);
                    Ctrl_HeroProperty.Instance.AddKillNumber();

                    /* 传统方式 - 销毁对象*/
                    //Destroy(this.gameObject, 5F);//5秒死亡延迟

                    /*缓冲池 回收对象*/
                    StartCoroutine("RecoverEnemys");
                }
            }
        }

        public virtual void OnHurt(int hurtValue)
        {
            _currentState = EnemyState.Hurt;
            if (Mathf.Abs(hurtValue) > 0)
            {
                CurrentHealth -= Mathf.Abs(hurtValue);
                //print("OnHurt() name = " + gameObject.name + ", hurtValue = " + hurtValue.ToString() + ", HP = " + CurrentHealth.ToString());
                if (ComboCountMgr.Instance)
                    ComboCountMgr.Instance.UpdateComboCount();
            }
        }

        IEnumerator RecoverEnemys()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_3);
            //敌人回收前状态重置
            CurrentHealth = maxHealth;
            _currentState = EnemyState.Idle;
            PoolManager.PoolsArray["_Enemys"].RecoverGameObjectToPools(this.gameObject);
        }

        public void ClearEnemy()
        {
            _currentState = EnemyState.Dead;
            StartCoroutine("RecoverEnemys");
        }
    }
}
