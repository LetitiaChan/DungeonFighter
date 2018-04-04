using UnityEngine.EventSystems;
using Kernal;
using Model;

namespace View
{
    public class ATK_PackItems : BasePackages, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public string targetGridName = "Img_ATK";
        public float addHeroMaxATK = 20;

        void Start()
        {
            base.strMoveToTargetGuidName = targetGridName;
            base.RunInstanceByChildClass();
        }

        protected override void InvokeMethodByEndDrag()
        {
            //print(GetType() + "/InvokeMethodByEndDrag()");
            PlayerKernalDataProxy.GetInstance().IncreaseMaxATK(addHeroMaxATK);
            PlayerKernalDataProxy.GetInstance().UpdateATKValues();
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