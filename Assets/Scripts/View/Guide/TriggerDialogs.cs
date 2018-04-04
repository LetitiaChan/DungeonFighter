using UnityEngine;
using UnityEngine.UI;
using Global;
using Kernal;

namespace View
{
    public class TriggerDialogs : MonoBehaviour, IGuideTrigger
    {
        public enum DialogStateStep
        {
            None,
            Step1_DoublePersionDialog,
            Step2_AliceSpeakET,
            Step3_AliceSpeakVirtualKey,
            Step4_AliceLastWord
        }

        public static TriggerDialogs Instance;
        public GameObject goBackground;

        private bool _isExistNextDialogRecord = false;
        private Image _imgTarget;
        private DialogStateStep _dialogState = DialogStateStep.None;


        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            #region 测试跳过引导
            //View_PlayerInfoResponse.Instance.DisplayET();
            //View_PlayerInfoResponse.Instance.DisplayAllUIKey();
            //View_PlayerInfoResponse.Instance.DisplayHeroUIInfo();
            //GameObject.Find("_SceneMgr/_SceneControl").GetComponent<View_LevelOneScenes>().enabled = true;
            //GameObject.Find("_SceneMgr/_SceneControl").GetComponent<Control.Ctrl_LevelOneScenes>().enabled = true;
            //goBackground.SetActive(false);
            #endregion

            _dialogState = DialogStateStep.Step1_DoublePersionDialog;
            _imgTarget = transform.parent.Find("Background").GetComponent<Image>();
            RegisterDialog();
            DialogUIMgr._instance.DisplayNextDialog(DialogType.DoubleDialog, 1);
        }

        public void RegisterDialog()
        {
            if (_imgTarget != null)
            {
                EventTriggerListener.Get(_imgTarget.gameObject).onClick += DisplayNextDialogRecord;
            }
        }
        private void UnRegisterDialog()
        {
            if (_imgTarget != null)
            {
                EventTriggerListener.Get(_imgTarget.gameObject).onClick -= DisplayNextDialogRecord;
            }
        }

        private void DisplayNextDialogRecord(GameObject go)
        {
            if (go == _imgTarget.gameObject)
            {
                _isExistNextDialogRecord = true;
            }
        }


        public bool CheckCondition()
        {
            //Log.Write(GetType() + "/CheckCondition");
            return _isExistNextDialogRecord;
        }

        public bool RunOperation()
        {
            //Log.Write(GetType() + "/RunOperation");

            bool result = false;
            bool currentDialogResult = false;
            _isExistNextDialogRecord = false;

            switch (_dialogState)
            {
                case DialogStateStep.None:
                    break;
                case DialogStateStep.Step1_DoublePersionDialog:
                    currentDialogResult = DialogUIMgr._instance.DisplayNextDialog(DialogType.DoubleDialog, 1);
                    break;
                case DialogStateStep.Step2_AliceSpeakET:
                    currentDialogResult = DialogUIMgr._instance.DisplayNextDialog(DialogType.SingleDialog, 2);
                    break;
                case DialogStateStep.Step3_AliceSpeakVirtualKey:
                    currentDialogResult = DialogUIMgr._instance.DisplayNextDialog(DialogType.SingleDialog, 3);
                    break;
                case DialogStateStep.Step4_AliceLastWord:
                    currentDialogResult = DialogUIMgr._instance.DisplayNextDialog(DialogType.SingleDialog, 4);
                    break;
                default:
                    break;
            }

            if (currentDialogResult)
            {
                switch (_dialogState)
                {
                    case DialogStateStep.None:
                        break;
                    case DialogStateStep.Step1_DoublePersionDialog:
                        break;
                    case DialogStateStep.Step2_AliceSpeakET:
                        TriggerOperET.Instance.DisplayGuideET();
                        UnRegisterDialog();
                        break;
                    case DialogStateStep.Step3_AliceSpeakVirtualKey:
                        TriggerOperVirtualKey.Instance.DisplayGuideVK();
                        UnRegisterDialog();
                        break;

                    case DialogStateStep.Step4_AliceLastWord:
                        View_PlayerInfoResponse.Instance.DisplayET();
                        View_PlayerInfoResponse.Instance.DisplayAllUIKey();
                        View_PlayerInfoResponse.Instance.DisplayHeroUIInfo();
                        GameObject.Find("_SceneMgr/_SceneControl").GetComponent<View_LevelOneScenes>().enabled = true;
                        GameObject.Find("_SceneMgr/_SceneControl").GetComponent<Control.Ctrl_LevelOneScenes>().enabled = true;

                        goBackground.SetActive(false);
                        result = true;
                        break;
                    default:
                        break;
                }

                EnterNextState();
            }

            return result;
        }

        private void EnterNextState()
        {
            switch (_dialogState)
            {
                case DialogStateStep.None:
                    break;
                case DialogStateStep.Step1_DoublePersionDialog:
                    _dialogState = DialogStateStep.Step2_AliceSpeakET;
                    break;
                case DialogStateStep.Step2_AliceSpeakET:
                    _dialogState = DialogStateStep.Step3_AliceSpeakVirtualKey;
                    break;
                case DialogStateStep.Step3_AliceSpeakVirtualKey:
                    _dialogState = DialogStateStep.Step4_AliceLastWord;
                    break;
                case DialogStateStep.Step4_AliceLastWord:
                    _dialogState = DialogStateStep.None;
                    break;
                default:
                    break;
            }
        }

    }
}
