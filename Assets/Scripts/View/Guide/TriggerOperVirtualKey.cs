using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Kernal;

namespace View
{
    public class TriggerOperVirtualKey : MonoBehaviour, IGuideTrigger
    {
        public static TriggerOperVirtualKey Instance;
        public GameObject goBackground;

        private bool _isExistNextDialogRecord = false;
        private Image _imgGuideVK;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _imgGuideVK = transform.parent.Find("ImgVK").GetComponent<Image>();
            RigisterGuideVK();
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
            _imgGuideVK.gameObject.SetActive(false);
            View_PlayerInfoResponse.Instance.DisplayET();
            View_PlayerInfoResponse.Instance.DisplayMainATK();

            StartCoroutine("ResumeDialog");

            return true;
        }

        public void DisplayGuideVK()
        {
            _imgGuideVK.gameObject.SetActive(true);
        }

        private void RigisterGuideVK()
        {
            if (_imgGuideVK != null)
            {
                EventTriggerListener.Get(_imgGuideVK.gameObject).onClick += GuideVKOperation;
            }
        }
        private void UnRigisterGuideVK()
        {
            if (_imgGuideVK != null)
            {
                EventTriggerListener.Get(_imgGuideVK.gameObject).onClick -= GuideVKOperation;
            }
        }

        private void GuideVKOperation(GameObject go)
        {
            if (go == _imgGuideVK.gameObject)
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
