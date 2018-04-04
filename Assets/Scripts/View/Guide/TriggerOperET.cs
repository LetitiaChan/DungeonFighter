using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Kernal;

namespace View
{
    public class TriggerOperET : MonoBehaviour, IGuideTrigger
    {
        public static TriggerOperET Instance;
        public GameObject goBackground;

        private bool _isExistNextDialogRecord = false;
        private Image _imgGuideET;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _imgGuideET = transform.parent.Find("ImgET").GetComponent<Image>();
            RigisterGuideET();
        }

        public bool CheckCondition()
        {
            //Log.Write(GetType() + "/CheckCondition");
            return _isExistNextDialogRecord;
        }

        public bool RunOperation()
        {
            //Log.Write(GetType() + "/RunOperation");
            _isExistNextDialogRecord = false;

            goBackground.SetActive(false);
            _imgGuideET.gameObject.SetActive(false);
            View_PlayerInfoResponse.Instance.DisplayET();

            StartCoroutine("ResumeDialog");

            return true;
        }

        public void DisplayGuideET()
        {
            _imgGuideET.gameObject.SetActive(true);
        }

        private void RigisterGuideET()
        {
            if (_imgGuideET != null)
            {
                EventTriggerListener.Get(_imgGuideET.gameObject).onClick += GuideETOperation;
            }
        }
        private void UnRigisterGuideET()
        {
            if (_imgGuideET != null)
            {
                EventTriggerListener.Get(_imgGuideET.gameObject).onClick -= GuideETOperation;
            }
        }

        private void GuideETOperation(GameObject go)
        {
            if (go == _imgGuideET.gameObject)
            {
                _isExistNextDialogRecord = true;
            }
        }

        IEnumerator ResumeDialog()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_3);

            View_PlayerInfoResponse.Instance.HideET();
            TriggerDialogs.Instance.RegisterDialog();
            TriggerDialogs.Instance.RunOperation();//直接显示下一条对话
            goBackground.SetActive(true);
        }
    }
}