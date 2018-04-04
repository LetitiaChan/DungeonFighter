using UnityEngine;
using Global;

namespace Model
{
    /// <summary>
    /// 玩家扩展数值代理类
    /// Description:
    ///     1> 本质是代理设计模式的应用（本类必须设计为带有构造函数的单例模式）
    /// </summary>
    public class PlayerExternalDataProxy : PlayerExternalData
    {
        private static PlayerExternalDataProxy _instance = null;

        public PlayerExternalDataProxy(int exp, int killNumber, int level, int gold, int diamonds)
            : base(exp, killNumber, level, gold, diamonds)
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Debug.LogError(GetType() + "/PlayerExternalDataProxy()/不允许构造函数重复实例化，请检查");
            }
        }

        public static PlayerExternalDataProxy GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                Debug.LogWarning("/GetInstance()/请先调用构造函数");
                return null;
            }
        }

        #region 经验值
        public void AddExp(int addExp)
        {
            base.Experience += Mathf.Abs(addExp);
            UpgradeRule.GetInstance().UpgradeCondition(base.Experience);
        }
        public int GetExp()
        {
            return base.Experience;
        }
        #endregion

        #region 杀敌数量
        public void AddKillNumber()
        {
            ++base.KillNumber;
        }
        public int GetKillNumber()
        {
            return base.KillNumber;
        }
        #endregion

        #region 等级
        public void AddLevel()
        {
            ++base.Level;
            UpgradeRule.GetInstance().UpgradeOperation((LevelName)base.Level);
        }
        public int GetLevel()
        {
            return base.Level;
        }
        #endregion

        #region 金币
        public void AddGold(int goldNumber)
        {
            base.Gold += Mathf.Abs(goldNumber);
        }

        public bool SubGold(int num)
        {
            if (base.Gold - Mathf.Abs(num) >= 0)
            {
                base.Gold -= Mathf.Abs(num);
                return true;
            }
            return false;
        }
        public int GetGold()
        {
            return base.Gold;
        }
        #endregion

        #region 钻石
        public void AddDiamonds(int diamondNumber)
        {
            base.Diamonds += Mathf.Abs(diamondNumber);
        }
        public bool SubDiamonds(int num)
        {
            if (base.Diamonds - Mathf.Abs(num) >= 0)
            {
                base.Diamonds -= Mathf.Abs(num);
                return true;
            }
            return false;
        }
        public int GetDiamonds()
        {
            return base.Diamonds;
        }
        #endregion

        public void DisplayAllOriginalValues()
        {
            base.Experience = base.Experience;
            base.KillNumber = base.KillNumber;
            base.Level = base.Level;
            base.Gold = base.Gold;
            base.Diamonds = base.Diamonds;
        }
    }
}