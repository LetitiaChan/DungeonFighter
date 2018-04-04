using UnityEngine;
using UnityEngine.UI;
using Kernal;
using Global;
using Contrl;
using Model;

namespace View
{
    public class NPCDialog_InMajorCity : MonoBehaviour
    {
        public GameObject goDialogPlane;
        public Sprite[] SprNPC_1;
        public Sprite[] SprNPC_2;
        public Sprite[] SprNPC_3;

        private Image _imgBGDialog;
        private CommonTriggerType _comTriggerType = CommonTriggerType.None;

        void Start()
        {
            _imgBGDialog = transform.parent.Find("Background").GetComponent<Image>();

            RigisterTriggerDialog();
            RigisterBGTexture();
            _imgBGDialog.gameObject.SetActive(false);
        }

        #region 对话准备阶段
        private void RigisterTriggerDialog()
        {
            TriggerCommonEvent.eveCommonTrigger += StartDialogPrepare;
        }
        private void StartDialogPrepare(CommonTriggerType CTT)
        {
            switch (CTT)
            {
                case CommonTriggerType.None:
                    break;
                case CommonTriggerType.NPC1_Dialog:
                    ActiveNPC1_Dialog();
                    break;
                case CommonTriggerType.NPC2_Dialog:
                    ActiveNPC2_Dialog();
                    break;
                case CommonTriggerType.NPC3_Dialog:
                    ActiveNPC3_Dialog();
                    break;
                default:
                    break;
            }
        }
        private void ActiveNPC1_Dialog()
        {
            DialogUIMgr._instance.SprNPC_Right = SprNPC_1;
            _comTriggerType = CommonTriggerType.NPC1_Dialog;
            View_PlayerInfoResponse.Instance.HideET();
            goDialogPlane.gameObject.SetActive(true);
            DisplayNextDialog(5);
        }
        private void ActiveNPC2_Dialog()
        {
            DialogUIMgr._instance.SprNPC_Right = SprNPC_2;
            _comTriggerType = CommonTriggerType.NPC2_Dialog;
            View_PlayerInfoResponse.Instance.HideET();
            goDialogPlane.gameObject.SetActive(true);
            DisplayNextDialog(6);
        }
        private void ActiveNPC3_Dialog()
        {
            DialogUIMgr._instance.SprNPC_Right = SprNPC_3;
            _comTriggerType = CommonTriggerType.NPC3_Dialog;
            View_PlayerInfoResponse.Instance.HideET();
            goDialogPlane.gameObject.SetActive(true);
            DisplayNextDialog(7);
        }
        #endregion

        #region 正式对话阶段
        public void DialogButtonOK()
        {
            switch (_comTriggerType)
            {
                case CommonTriggerType.None:
                    break;
                case CommonTriggerType.NPC3_Dialog:
                    //print("点击对话按钮“进入副本”");
                    View_PlayerInfoResponse_MajorCity.Instance.DisplayPlayerTask();
                    break;
                default:
                    break;
            }
        }
        private void RigisterBGTexture()
        {
            if (_imgBGDialog != null)
            {
                EventTriggerListener.Get(_imgBGDialog.gameObject).onClick += DisplayDialogByNpc;
            }
        }
        private void DisplayDialogByNpc(GameObject go)
        {
            switch (_comTriggerType)
            {
                case CommonTriggerType.None:
                    break;
                case CommonTriggerType.NPC1_Dialog:
                    DisplayNextDialog(5);
                    break;
                case CommonTriggerType.NPC2_Dialog:
                    DisplayNextDialog(6);
                    break;
                case CommonTriggerType.NPC3_Dialog:
                    DisplayNextDialog(7);
                    break;
                default:
                    break;
            }
        }
        private void DisplayNextDialog(int sectionNum)
        {
            bool result = DialogUIMgr._instance.DisplayNextDialog(DialogType.DoubleDialog, sectionNum);
            if (result)
            {
                goDialogPlane.gameObject.SetActive(false);
                View_PlayerInfoResponse.Instance.DisplayET();

                switch (_comTriggerType)
                {
                    case CommonTriggerType.None:
                        break;
                    case CommonTriggerType.NPC2_Dialog:
                        PlayerPackageDataProxy.GetInstance().IncreaseBloodBottleNum(5);
                        PlayerPackageDataProxy.GetInstance().IncreaseMagicBottleNum(5);
                        PlayerPackageDataProxy.GetInstance().IncreaseATKPropNum(1);
                        PlayerPackageDataProxy.GetInstance().IncreaseDEFPropNum(1);
                        View_PlayerInfoResponse_MajorCity.Instance.DisplayPlayerBag();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

    }
}