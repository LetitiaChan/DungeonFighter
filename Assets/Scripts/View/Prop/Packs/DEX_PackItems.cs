using UnityEngine.EventSystems;
using Kernal;
using Model;

namespace View
{
    public class DEX_PackItems : BasePackages, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public string targetGridName = "Img_DEX";
        public float addHeroMaxDEX = 10;

        void Start()
        {
            base.strMoveToTargetGuidName = targetGridName;
            base.RunInstanceByChildClass();
        }

        protected override void InvokeMethodByEndDrag()
        {
            //print(GetType() + "/InvokeMethodByEndDrag()");
            PlayerKernalDataProxy.GetInstance().IncreaseMaxDEX(addHeroMaxDEX);
            PlayerKernalDataProxy.GetInstance().UpdateDEXValues();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            base.Base_OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            base.Base_OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            base.Base_OnEndDrag(eventData);
        }
    }
}