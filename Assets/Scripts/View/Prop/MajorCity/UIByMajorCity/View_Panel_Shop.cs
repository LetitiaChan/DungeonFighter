using UnityEngine;
using UnityEngine.UI;
using Kernal;
using Global;
using Control;

namespace View
{
    public class View_Panel_Shop : MonoBehaviour
    {
        public Image Item1;
        public Image Item2;
        public Image Item3;
        public Image Item4;
        public Image Item5;
        public Image Item6;
        public Image Item7;

        public Button btnItem1;
        public Button btnItem2;
        public Button btnItem3;
        public Button btnItem4;
        public Button btnItem5;
        public Button btnItem6;
        public Button btnItem7;

        public Text txtDesp;

        void Awake()
        {
            RegisterTxtAndBtn();
        }

        private void RegisterTxtAndBtn()
        {
            if (Item1 != null)
                EventTriggerListener.Get(Item1.gameObject).onClick += DisplayItem;
            if (Item2 != null)
                EventTriggerListener.Get(Item2.gameObject).onClick += DisplayItem;
            if (Item3 != null)
                EventTriggerListener.Get(Item3.gameObject).onClick += DisplayItem;
            if (Item4 != null)
                EventTriggerListener.Get(Item4.gameObject).onClick += DisplayItem;
            if (Item5 != null)
                EventTriggerListener.Get(Item5.gameObject).onClick += DisplayItem;
            if (Item6 != null)
                EventTriggerListener.Get(Item6.gameObject).onClick += DisplayItem;

            if (btnItem1 != null)
                EventTriggerListener.Get(btnItem1.gameObject).onClick += PurchaseProp;
            if (btnItem2 != null)
                EventTriggerListener.Get(btnItem2.gameObject).onClick += PurchaseProp;
            if (btnItem3 != null)
                EventTriggerListener.Get(btnItem3.gameObject).onClick += PurchaseProp;
            if (btnItem4 != null)
                EventTriggerListener.Get(btnItem4.gameObject).onClick += PurchaseProp;
            if (btnItem5 != null)
                EventTriggerListener.Get(btnItem5.gameObject).onClick += PurchaseProp;
            if (btnItem6 != null)
                EventTriggerListener.Get(btnItem6.gameObject).onClick += PurchaseProp;
        }

        private void DisplayItem(GameObject go)
        {
            if (go == Item1.gameObject)
            {
                txtDesp.text = "第一个道具的说明";
            }
            else if (go == Item2.gameObject)
            {
                txtDesp.text = "第二个道具的说明";
            }
            else if (go == Item3.gameObject)
            {
                txtDesp.text = "第三个道具的说明";
            }
            else if (go == Item4.gameObject)
            {
                txtDesp.text = "第四个道具的说明";
            }
            else if (go == Item5.gameObject)
            {
                txtDesp.text = "第五个道具的说明";
            }
            else if (go == Item6.gameObject)
            {
                txtDesp.text = "第六个道具的说明";
            }
        }

        private void PurchaseProp(GameObject go)
        {
            if (go == btnItem1.gameObject)
            {
                bool result = false;
                result = Ctrl_Panel_Shop.Instance.PayDiamonds();
                if (result)
                    txtDesp.text = "10颗钻石，充值成功！";
                else
                    txtDesp.text = "10颗钻石充值不成功，请联系管理员！";
            }
            else if (go == btnItem2.gameObject)
            {
                bool result = false;
                result = Ctrl_Panel_Shop.Instance.PurchaseGolds();
                if (result)
                    txtDesp.text = "购买10枚金币成功！";
                else
                    txtDesp.text = "元宝不足，请充值。";
            }
            else if (go == btnItem3.gameObject)
            {
                bool result = false;
                result = Ctrl_Panel_Shop.Instance.PurchaseBooldBottle();
                if (result)
                    txtDesp.text = "购买5个血瓶成功！";
                else
                    txtDesp.text = "金币不足，无法购买。";
            }
            else if (go == btnItem4.gameObject)
            {
                bool result = false;
                result = Ctrl_Panel_Shop.Instance.PurchaseMagicBottle();
                if (result)
                    txtDesp.text = "购买5个魔法瓶成功！";
                else
                    txtDesp.text = "金币不足，无法购买。";
            }
            else if (go == btnItem5.gameObject)
            {
                bool result = false;
                result = Ctrl_Panel_Shop.Instance.PurchasePropATK();
                if (result)
                    txtDesp.text = "购买攻击力道具成功！";
                else
                    txtDesp.text = "金币不足，无法购买。";
            }
            else if (go == btnItem6.gameObject)
            {
                bool result = false;
                result = Ctrl_Panel_Shop.Instance.PurchasePropDEF();
                if (result)
                    txtDesp.text = "购买防御力道具成功！";
                else
                    txtDesp.text = "金币不足，无法购买。";
            }
            else if (go == btnItem7.gameObject)
            {
                bool result = false;
                result = Ctrl_Panel_Shop.Instance.PurchasePropDEX();
                if (result)
                    txtDesp.text = "购买敏捷度道具成功！";
                else
                    txtDesp.text = "金币不足，无法购买。";
            }
        }
    }
}