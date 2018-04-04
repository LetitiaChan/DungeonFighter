using UnityEngine;

namespace Kernal
{
    public enum CommonTriggerType
    {
        None,
        NPC1_Dialog,
        NPC2_Dialog,
        NPC3_Dialog,
        NPC4_Dialog,
        NPC5_Dialog,
        Enumy1_Dialog,
        Enumy2_Dialog,
        Enumy3_Dialog,
        Enumy4_Dialog,
        Enumy5_Dialog,
        SaveDataOrScenes,
        LoadDataOrScenes,
        EnableScript1,
        EnableScript2,
        ActiveConfimWindows,
        ActiveDialogWindow,
    }

    public delegate void del_CommonTrigger(CommonTriggerType CTT);

    /// <summary>
    /// 通用触发脚本
    /// 功能：
    ///     1.NPC/敌人触发对话；
    ///     2.存盘/继续
    ///     3.加载与启用特定的脚本（如：产生敌人）
    ///     4.触发UI“对话/确认框”
    /// </summary>
    public class TriggerCommonEvent : MonoBehaviour
    {
        public static event del_CommonTrigger eveCommonTrigger;
        public CommonTriggerType TriggerType = CommonTriggerType.None;
        public string TagNameByHero = "Player";

        void OnTriggerEnter(Collider con)
        {
            if (con.gameObject.tag == TagNameByHero)
            {
                if (eveCommonTrigger != null)
                {
                    eveCommonTrigger(TriggerType);
                }
            }
        }

    }
}