using UnityEngine;
using Model;
using Global;

namespace Control
{
    public class Ctrl_HeroProperty : BaseControl
    {

        public static Ctrl_HeroProperty Instance;

        //玩家核心数值
        public float HP_Cur = 1000f;
        public float HP_Max = 1000f;
        public float MP_Cur = 1000f;
        public float MP_Max = 1000f;
        public float ATK_Cur = 10f;
        public float ATK_Max = 10f;
        public float DEF_Cur = 5f;
        public float DEF_Max = 5f;
        public float DEX_Cur = 45f;
        public float DEX_Max = 50f;

        public float ATKByProp = 0f;
        public float DEFByProp = 0f;
        public float DEXByProp = 0f;

        //玩家扩展数值 
        public int EXP = 0;
        public int Level = 0;
        public int KillNum = 0;
        public int Gold = 0;
        public int Diamonds = 0;

        //玩家背包数值
        public int BloodBottleNum = 0;
        public int MagicBottleNum = 0;
        public int PropATKNum = 0;
        public int PropDEFNum = 0;
        public int PropDEXNum = 0;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            PlayerKernalDataProxy playerKernalDataObject = new PlayerKernalDataProxy(HP_Cur, MP_Cur, ATK_Cur, DEF_Cur, DEX_Cur, HP_Max, MP_Max, ATK_Max, DEF_Max, DEX_Max, ATKByProp, DEFByProp, DEXByProp);
            PlayerExternalDataProxy playerExternalDataObj = new PlayerExternalDataProxy(EXP, KillNum, Level, Gold, Diamonds);
            PlayerPackageDataProxy playerPackageObj = new PlayerPackageDataProxy(BloodBottleNum, MagicBottleNum, PropATKNum, PropDEFNum, PropDEXNum);
        }

        #region 生命数值操作
        public void DecreaseHealthValues(float enemyAttackValue)
        {
            if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Dead) return;

            if (enemyAttackValue > 0)
            {
                Ctrl_HeroAnimationCtrl.Instance.Animation_HeroHurtEffect((int)enemyAttackValue);
                PlayerKernalDataProxy.GetInstance().DecreaseHealthValues(enemyAttackValue);

                if (GetCurrentHealth() <= 0)
                    Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Dead);
            }
        }
        public void IncreaseHealthValues(float healthValue)
        {
            if (healthValue > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(healthValue);
            }
        }
        public float GetCurrentHealth()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentHealth();
        }
        public void IncreaseMaxHealth(float increaseHealth)
        {
            if (increaseHealth > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxHealth(increaseHealth);
            }
        }
        public float GetMaxHealth()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxHealth();
        }
        #endregion

        #region 魔法数值操作
        public void DecreaseMagicValues(float magicValue)
        {
            if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Dead) return;

            if (magicValue > 0)
            {
                PlayerKernalDataProxy.GetInstance().DecreaseMagicValues(magicValue);
            }
        }
        public void IncreaseMagicValues(float MagicValue)
        {
            if (MagicValue > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(MagicValue);
            }
        }
        public float GetCurrentMagic()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentMagic();
        }
        public void IncreaseMaxMagic(float increaseMagic)
        {
            if (increaseMagic > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxMagic(increaseMagic);
            }
        }
        public float GetMaxMagic()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxMagic();
        }
        #endregion

        #region 攻击力数值操作
        public void UpdateATKValues(float newWeaponValues = 0)
        {
            PlayerKernalDataProxy.GetInstance().UpdateATKValues(newWeaponValues);
        }
        public float GetCurrentATK()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentATK();
        }
        public void IncreaseMaxATK(float increaseATK)
        {
            if (increaseATK > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxATK(increaseATK);
            }
        }
        public float GetMaxATK()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxATK();
        }
        #endregion

        #region 防御力数值操作
        public void UpdateDEFValues(float newWeaponDEFValues = 0)
        {
            PlayerKernalDataProxy.GetInstance().UpdateDEFValues(newWeaponDEFValues);
        }
        public float GetCurrentDEF()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentDEF();
        }
        public void IncreaseMaxDEF(float increaseDEF)
        {
            if (increaseDEF > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxDEF(increaseDEF);
            }
        }
        public float GetMaxDEF()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxDEF();
        }
        #endregion

        #region 敏捷度数值操作
        public void UpdateDEXValues(float newWeaponValues = 0)
        {
            PlayerKernalDataProxy.GetInstance().UpdateDEXValues(newWeaponValues);
        }
        public float GetCurrentDEX()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentDEX();
        }
        public void IncreaseMaxDEX(float increaseDEX)
        {
            if (increaseDEX > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxDEX(increaseDEX);
            }
        }
        public float GetMaxDEX()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxDEX();
        }
        #endregion

        #region 经验值
        public void AddExp(int addExp)
        {
            if (addExp > 0)
            {
                PlayerExternalDataProxy.GetInstance().AddExp(addExp);
            }
        }
        public int GetExp()
        {
            return PlayerExternalDataProxy.GetInstance().GetExp();
        }
        #endregion

        #region 杀敌数量
        public void AddKillNumber()
        {
            PlayerExternalDataProxy.GetInstance().AddKillNumber();
        }
        public int GetKillNumber()
        {
            return PlayerExternalDataProxy.GetInstance().GetKillNumber();
        }
        #endregion

        #region 等级
        public void AddLevel()
        {
            PlayerExternalDataProxy.GetInstance().AddLevel();
        }
        public int GetLevel()
        {
            return PlayerExternalDataProxy.GetInstance().GetLevel();
        }
        #endregion

        #region 金币
        public void AddGold(int goldNumber)
        {
            if (goldNumber > 0)
            {
                PlayerExternalDataProxy.GetInstance().AddGold(goldNumber);
            }
        }
        public int GetGold()
        {
            return PlayerExternalDataProxy.GetInstance().GetGold();
        }
        #endregion

        #region 钻石
        public void AddDiamonds(int diamondNumber)
        {
            if (diamondNumber > 0)
            {
                PlayerExternalDataProxy.GetInstance().AddDiamonds(diamondNumber);
            }
        }
        public int GetDiamonds()
        {
            return PlayerExternalDataProxy.GetInstance().GetDiamonds();
        }
        #endregion

        public void RecoverLife()
        {
            IncreaseHealthValues(GetMaxHealth());
            IncreaseMagicValues(GetMaxMagic());
        }
    }
}