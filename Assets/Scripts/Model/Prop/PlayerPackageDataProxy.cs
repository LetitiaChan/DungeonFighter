using UnityEngine;
using Kernal;
using Global;

namespace Model
{
    public class PlayerPackageDataProxy : PlayerPackageData
    {

        private static PlayerPackageDataProxy _Instance = null;

        public PlayerPackageDataProxy(int bloodBottleNum, int magicBottleNum, int atkNum, int defNum, int dexNum)
            : base(bloodBottleNum, magicBottleNum, atkNum, defNum, dexNum)
        {
            if (_Instance == null)
            {
                _Instance = this;
            }
            else
            {
                Debug.LogError(GetType() + "/PlayerPackageDataProxy()/不允许构造函数的重复实例化！");
            }
        }

        public static PlayerPackageDataProxy GetInstance()
        {
            if (_Instance != null)
                return _Instance;
            else
            {
                Debug.LogError("PlayerPackageDataProxy.cs/GetInstance()/请先调用构造函数！");
                return null;
            }
        }

        #region BloodBottle
        public void IncreaseBloodBottleNum(int num)
        {
            base.BloodBottleNum += Mathf.Abs(num);
        }
        public void DecreaseBloodBottleNum(int num)
        {
            if (base.BloodBottleNum - Mathf.Abs(num) >= 0)
            {
                base.BloodBottleNum -= Mathf.Abs(num);
            }
        }
        public int DisplayBloodBottleNum()
        {
            return base.BloodBottleNum;
        }
        #endregion

        #region MagicBottle
        public void IncreaseMagicBottleNum(int num)
        {
            base.MagicBottleNum += Mathf.Abs(num);
        }
        public void DecreaseMagicBottleNum(int num)
        {
            if (base.MagicBottleNum - Mathf.Abs(num) >= 0)
            {
                base.MagicBottleNum -= Mathf.Abs(num);
            }
        }
        public int DisplayMagicBottleNum()
        {
            return base.MagicBottleNum;
        }
        #endregion

        #region ATKProp
        public void IncreaseATKPropNum(int num)
        {
            base.PropATKNum += Mathf.Abs(num);
        }
        public void DecreaseATKPropNum(int num)
        {
            if (base.PropATKNum - Mathf.Abs(num) >= 0)
            {
                base.PropATKNum -= Mathf.Abs(num);
            }
        }
        public int DisplayATKPropNum()
        {
            return base.PropATKNum;
        }
        #endregion

        #region DEFProp
        public void IncreaseDEFPropNum(int num)
        {
            base.PropDEFNum += Mathf.Abs(num);
        }
        public void DecreaseDEFPropNum(int num)
        {
            if (base.PropDEFNum - Mathf.Abs(num) >= 0)
            {
                base.PropDEFNum -= Mathf.Abs(num);
            }
        }
        public int DisplayDEFPropNum()
        {
            return base.PropDEFNum;
        }
        #endregion

        #region DEXProp
        public void IncreaseDEXPropNum(int num)
        {
            base.PropDEXNum += Mathf.Abs(num);
        }
        public void DecreaseDEXPropNum(int num)
        {
            if (base.PropDEXNum - Mathf.Abs(num) >= 0)
            {
                base.PropDEXNum -= Mathf.Abs(num);
            }
        }
        public int DisplayDEXPropNum()
        {
            return base.PropDEXNum;
        }
        #endregion
    }
}