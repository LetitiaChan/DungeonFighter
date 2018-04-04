using UnityEngine;
using UnityEngine.UI;
using Kernal;
using Global;

namespace View
{
    public class View_Panel_Skill : MonoBehaviour
    {
        public Image ImgATK1;
        public Image ImgATK2;
        public Image ImgATK3;
        public Image ImgATK4;
        public Image ImgATK5;

        public Text TxtSkillName;
        public Text TxtSkillDesp;

        void Awake()
        {
            RigisterAttack();
        }

        void Start()
        {
            TxtSkillName.text = "基础剑法";
            TxtSkillDesp.text = "普通连招打击，当升级不同等级时候，给敌人的打击会相应提高！";
        }

        /// <summary>
        /// 攻击贴图注册
        /// </summary>
        public void RigisterAttack()
        {
            if (ImgATK1 != null)
            {
                EventTriggerListener.Get(ImgATK1.gameObject).onClick += ATKHandler;
            }
            if (ImgATK2 != null)
            {
                EventTriggerListener.Get(ImgATK2.gameObject).onClick += ATKHandler;
            }
            if (ImgATK3 != null)
            {
                EventTriggerListener.Get(ImgATK3.gameObject).onClick += ATKHandler;
            }
            if (ImgATK4 != null)
            {
                EventTriggerListener.Get(ImgATK4.gameObject).onClick += ATKHandler;
            }
            if (ImgATK5 != null)
            {
                EventTriggerListener.Get(ImgATK5.gameObject).onClick += ATKHandler;
            }
        }

        public void ATKHandler(GameObject go)
        {
            if (go == ImgATK1.gameObject)
            {
                TxtSkillName.text = "基础剑法";
                TxtSkillDesp.text = "普通连招打击，当升级不同等级时候，给敌人的打击会相应提高！";
            }
            else if (go == ImgATK2.gameObject)
            {
                TxtSkillName.text = "战神刺杀";
                TxtSkillDesp.text = "战神刺杀，当升级不同等级时候，给敌人的打击会相应提高！";
            }
            else if (go == ImgATK3.gameObject)
            {
                TxtSkillName.text = "全月刀法";
                TxtSkillDesp.text = "全月刀法，当升级不同等级时候，给敌人的打击会相应提高！";
            }
            else if (go == ImgATK4.gameObject)
            {
                TxtSkillName.text = "火墙术";
                TxtSkillDesp.text = "火墙术，当升级不同等级时候，给敌人的打击会相应提高！";
            }
            else if (go == ImgATK5.gameObject)
            {
                TxtSkillName.text = "地狱雷光";
                TxtSkillDesp.text = "地狱雷光，当升级不同等级时候，给敌人的打击会相应提高！";
            }
        }
    }
}