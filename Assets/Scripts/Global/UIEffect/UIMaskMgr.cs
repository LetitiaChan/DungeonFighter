using UnityEngine;

namespace Global
{
    /// <summary>
    /// UI遮罩管理器,实现模态窗体效果
    /// </summary>
    public class UIMaskMgr : MonoBehaviour
    {
        #region Unity Inspector Fields
        public GameObject goTopPlane;
        public GameObject goMaskPlane;
        #endregion

        private Camera _UICamera;
        private float _originalUICameraDepth;                                   //原始UI摄像机的层深


        void Start()
        {
            _UICamera = transform.parent.FindChild("UICamera").GetComponent<Camera>();
            if (_UICamera != null)
            {
                _originalUICameraDepth = _UICamera.depth;
            }
            else
            {
                Debug.LogError(GetType() + "/Start()/_UICamera is Null ,please Check!");
            }
        }

        public void SetMaskWindow(GameObject goDisplayPlane)
        {
            /* 顶层窗体下移；启用遮罩窗体；遮罩窗体下移；显示窗体下移；增加UI摄像机层深 */
            goTopPlane.transform.SetAsLastSibling();
            goMaskPlane.SetActive(true);
            goMaskPlane.transform.SetAsLastSibling();
            goDisplayPlane.transform.SetAsLastSibling();
            if (_UICamera != null)
            {
                _UICamera.depth = _UICamera.depth + 20;
            }
        }

        public void CancleMaskWindow()
        {
            goTopPlane.transform.SetAsFirstSibling();
            goMaskPlane.SetActive(false);
            _UICamera.depth = _originalUICameraDepth;
        }
    }
}
