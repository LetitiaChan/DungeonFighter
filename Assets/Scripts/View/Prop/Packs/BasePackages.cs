using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    /// <summary>
    /// 定义装备系统的一般性操作
    /// </summary>
    public class BasePackages : MonoBehaviour
    {
        protected string strMoveToTargetGuidName;
        private CanvasGroup _canvasGroup;
        private Vector3 _originalPos;
        private Transform _myTransform;
        private RectTransform _myReTransform;

        /// <summary>
        /// 运行本类实例，通过子类执行
        /// </summary>
        protected void RunInstanceByChildClass()
        {
            Base_Start();
        }

        void Base_Start()
        {
            _canvasGroup = this.GetComponent<CanvasGroup>();
            _myReTransform = this.transform as RectTransform;
            _myTransform = this.transform;
        }

        public void Base_OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;  //忽略自身 可以穿透
            this.transform.SetAsLastSibling();
            _originalPos = _myTransform.position;
        }
        public void Base_OnDrag(PointerEventData eventData)
        {
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_myReTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
            {
                _myReTransform.position = globalMousePos;
            }
        }
        public void Base_OnEndDrag(PointerEventData eventData)
        {
            GameObject cur = eventData.pointerEnter;  //当前鼠标经过的“格子”
            if (cur != null)
            {
                if (cur.name.Equals(strMoveToTargetGuidName))
                {
                    _myTransform.position = cur.transform.position;
                    _originalPos = _myTransform.position;

                    InvokeMethodByEndDrag();//执行特定的装备方法
                }
                else
                {
                    //移动到背包其他有效位置

                    //同类背包道具，交换位置
                    if (cur.tag == eventData.pointerDrag.tag && cur.name != eventData.pointerDrag.name)
                    {
                        Vector3 targetPos = cur.transform.position;
                        cur.transform.position = _originalPos;
                        _myTransform.position = targetPos;
                        _originalPos = _myTransform.position;
                    }
                    else
                    {
                        _myTransform.position = _originalPos;
                    }
                    _canvasGroup.blocksRaycasts = true; //阻止穿透，可以再次移动
                }
            }
            else
            {
                _myReTransform.position = _originalPos;
            }
        }

        protected virtual void InvokeMethodByEndDrag()
        {
            //print(GetType() + "/InvokeMethodByEndDrag()");
        }

    }
}