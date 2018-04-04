using UnityEngine;

namespace Model
{
    /// <summary>
    /// 玩家核心数值代理类
    /// Description:
    ///     1> 作用：数据的生成与复杂计算
    ///         本质是代理设计模式的应用
    ///         本类必须设计为带有构造函数的单例模式
    /// </summary>
    public class PlayerKernalDataProxy : PlayerKernalData
    {
        private static PlayerKernalDataProxy _instance = null;
        public const int ENEMY_MIN_ATK = 1;

        public PlayerKernalDataProxy(float health, float magic, float ATK, float DEF, float DEX,
                float maxHealth, float maxMagic, float maxATK, float maxDEF, float maxDEX,
                float ATKByProp, float DEFByProp, float DEXByProp)
            : base(health, magic, ATK, DEF, DEX, maxHealth, maxMagic, maxATK, maxDEF, maxDEX, ATKByProp, DEFByProp, DEXByProp)
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError(GetType() + "/PlayerKernalDataProxy()/不允许构造函数重复实例化，请检查");
            }
        }

        public static PlayerKernalDataProxy GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                Debug.LogWarning("PlayerKernalDataProxy/GetInstance()/请先调用构造函数");
                return null;
            }
        }

        #region 生命数值操作
        public void DecreaseHealthValues(float enemyAttackValue)
        {
            // 公式：_Health = _Health-（敌人攻击力-主角防御力-主角武器防御力）
            var enemyReallyATK = enemyAttackValue - base.Defence - base.DefenceByProp;
            enemyReallyATK = enemyReallyATK > 0 ? enemyReallyATK : ENEMY_MIN_ATK;
            base.Health = base.Health - enemyReallyATK > 0 ? Mathf.RoundToInt(base.Health - enemyReallyATK) : 0;

            this.UpdateATKValues();
            this.UpdateDEFValues();
            this.UpdateDEXValues();
        }
        public void IncreaseHealthValues(float healthValue)
        {
            var resHealth = base.Health + Mathf.Abs(healthValue);
            resHealth = resHealth < base.MaxHealth ? resHealth : base.MaxHealth;
            base.Health = Mathf.RoundToInt(resHealth);

            this.UpdateATKValues();
            this.UpdateDEFValues();
            this.UpdateDEXValues();
        }
        public float GetCurrentHealth()
        {
            return base.Health;
        }

        public void IncreaseMaxHealth(float increaseHealth)
        {
            base.MaxHealth = Mathf.RoundToInt(base.MaxHealth + Mathf.Abs(increaseHealth));
        }
        public float GetMaxHealth()
        {
            return base.MaxHealth;
        }
        #endregion

        #region 魔法数值操作
        public void DecreaseMagicValues(float magicValue)
        {
            // 公式：_Magic = _Magic - ( 释放一次“特定魔法”的损耗 )
            var resMagic = base.Magic - Mathf.Abs(magicValue);
            resMagic = resMagic > 0 ? resMagic : 0;
            base.Magic = Mathf.RoundToInt(resMagic);
        }
        public void IncreaseMagicValues(float MagicValue)
        {
            var resMagic = base.Magic + MagicValue;
            resMagic = resMagic < base.MaxMagic ? resMagic : base.MaxMagic;
            base.Magic = Mathf.RoundToInt(resMagic);
        }
        public float GetCurrentMagic()
        {
            return base.Magic;
        }
        public void IncreaseMaxMagic(float increaseMagic)
        {
            base.MaxMagic = Mathf.RoundToInt(base.MaxMagic + Mathf.Abs(increaseMagic));
        }
        public float GetMaxMagic()
        {
            return base.MaxMagic;
        }
        #endregion

        #region 攻击力数值操作
        public void UpdateATKValues(float newWeaponValues = 0)
        {
            // 公式：_AttackForce=MaxATK/2*(_Health/MaxHealth)+[“武器攻击力”]
            var reallyATKValues = 0f;

            if (newWeaponValues > 0)
            {
                base.AttackByProp = newWeaponValues;
            }
            reallyATKValues = base.MaxAttack / 2 * (base.Health / base.MaxHealth) + base.AttackByProp;
            reallyATKValues = reallyATKValues > base.MaxAttack ? base.MaxAttack : reallyATKValues;

            base.Attack = Mathf.RoundToInt(reallyATKValues);
        }
        public float GetCurrentATK()
        {
            return base.Attack;
        }
        public void IncreaseMaxATK(float increaseATK)
        {
            base.MaxAttack = Mathf.RoundToInt(base.MaxAttack + Mathf.Abs(increaseATK));
        }
        public float GetMaxATK()
        {
            return base.MaxAttack;
        }
        #endregion

        #region 防御力数值操作
        public void UpdateDEFValues(float newWeaponDEFValues = 0)
        {
            // 公式：_Defence=MaxDEF/2*(_Health/MaxHealth)+[武器防御力]

            float reallyDEFValues = 0F;

            if (newWeaponDEFValues == 0)
            {
                reallyDEFValues = base.MaxDefence / 2 * (base.Health / base.MaxHealth) + base.DefenceByProp;
            }
            else if (newWeaponDEFValues > 0)
            {
                base.DefenceByProp = newWeaponDEFValues;
                reallyDEFValues = base.MaxDefence / 2 * (base.Health / base.MaxHealth) + base.DefenceByProp;
            }
            reallyDEFValues = (reallyDEFValues > base.MaxDefence) ? base.MaxDefence : reallyDEFValues;

            base.Defence = Mathf.RoundToInt(reallyDEFValues);
        }
        public float GetCurrentDEF()
        {
            return base.Defence;
        }
        public void IncreaseMaxDEF(float increaseDEF)
        {
            base.MaxDefence = Mathf.RoundToInt(base.MaxDefence + Mathf.Abs(increaseDEF));
        }
        public float GetMaxDEF()
        {
            return base.MaxDefence;
        }
        #endregion

        #region 敏捷度数值操作
        public void UpdateDEXValues(float newWeaponValues = 0)
        {
            // 公式：_MoveSpeed=MaxMoveSpeed/2*(_Health/MaxHealth)-_Defence+[道具敏捷力]

            float reallyDEXValues = 0f;

            if (newWeaponValues == 0)
            {
                reallyDEXValues = base.MaxDexterity / 2 * (base.Health / base.MaxHealth) - base.Defence + base.DexterityByProp;
            }
            else if (newWeaponValues > 0)
            {
                base.DexterityByProp = newWeaponValues;
                reallyDEXValues = base.MaxDexterity / 2 * (base.Health / base.MaxHealth) - base.Defence + base.DexterityByProp;
            }
            reallyDEXValues = (reallyDEXValues > base.MaxDexterity) ? base.MaxDexterity : reallyDEXValues;

            base.Dexterity = Mathf.RoundToInt(reallyDEXValues);
        }
        public float GetCurrentDEX()
        {
            return base.Dexterity;
        }
        public void IncreaseMaxDEX(float increaseDEX)
        {
            base.MaxDexterity = Mathf.RoundToInt(base.MaxDexterity + Mathf.Abs(increaseDEX));
        }
        public float GetMaxDEX()
        {
            return base.MaxDexterity;
        }
        #endregion

        public void DisplayerAllOriginalValues()
        {
            base.MaxHealth = base.MaxHealth;
            base.MaxMagic = base.MaxMagic;
            base.MaxAttack = base.MaxAttack;
            base.MaxDefence = base.MaxDefence;
            base.MaxDexterity = base.MaxDexterity;

            base.Health = base.Health;
            base.Magic = base.Magic;
            base.Attack = base.Attack;
            base.Defence = base.Defence;
            base.Dexterity = base.Dexterity;

            base.AttackByProp = base.AttackByProp;
            base.DefenceByProp = base.DefenceByProp;
            base.DexterityByProp = base.DexterityByProp;
        }
    }
}