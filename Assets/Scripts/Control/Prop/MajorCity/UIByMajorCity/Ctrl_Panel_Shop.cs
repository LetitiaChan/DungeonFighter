using UnityEngine;
using System.Collections;
using Kernal;
using Global;
using Model;

namespace Control
{
    public class Ctrl_Panel_Shop : BaseControl
    {
        public static Ctrl_Panel_Shop Instance;

        void Awake()
        {
            Instance = this;
        }

        public bool PayDiamonds()
        {
            int _propNum = 10;
            PlayerExternalDataProxy.GetInstance().AddDiamonds(_propNum);
            return true;
        }
        public bool PurchaseGolds()
        {
            int _propNum = 10;
            int _costDiamondNum = 1;

            bool result = false;
            bool flag = PlayerExternalDataProxy.GetInstance().SubDiamonds(_costDiamondNum);
            if (flag)
            {
                PlayerExternalDataProxy.GetInstance().AddGold(_propNum);
                result = true;
            }
            return result;
        }
        public bool PurchaseBooldBottle()
        {
            int _propNum = 5;       //购买道具数量
            int _costGoldNum = 50;  //购买道具花费

            bool result = false;
            bool flag = PlayerExternalDataProxy.GetInstance().SubGold(_costGoldNum);
            if (flag)
            {
                PlayerPackageDataProxy.GetInstance().IncreaseBloodBottleNum(_propNum);
                result = true;
            }
            return result;
        }
        public bool PurchaseMagicBottle()
        {
            int _propNum = 5;
            int _costGoldNum = 100;

            bool result = false;
            bool flag = PlayerExternalDataProxy.GetInstance().SubGold(_costGoldNum);
            if (flag)
            {
                PlayerPackageDataProxy.GetInstance().IncreaseMagicBottleNum(_propNum);
                result = true;
            }
            return result;
        }
        public bool PurchasePropATK()
        {
            int _propNum = 5;
            int _costGoldNum = 50;

            bool result = false;
            bool flag = PlayerExternalDataProxy.GetInstance().SubGold(_costGoldNum);
            if (flag)
            {
                PlayerPackageDataProxy.GetInstance().IncreaseATKPropNum(_propNum);
                result = true;
            }
            return result;
        }
        public bool PurchasePropDEF()
        {
            int _propNum = 5;
            int _costGoldNum = 30;

            bool result = false;
            bool flag = PlayerExternalDataProxy.GetInstance().SubGold(_costGoldNum);
            if (flag)
            {
                PlayerPackageDataProxy.GetInstance().IncreaseDEFPropNum(_propNum);
                result = true;
            }
            return result;
        }
        public bool PurchasePropDEX()
        {
            int _propNum = 5;
            int _costGoldNum = 20;

            bool result = false;
            bool flag = PlayerExternalDataProxy.GetInstance().SubGold(_costGoldNum);
            if (flag)
            {
                PlayerPackageDataProxy.GetInstance().IncreaseDEXPropNum(_propNum);
                result = true;
            }
            return result;
        }
    }
}