using UnityEngine;
using Kernal;

namespace View
{
    public class GuideMoving : MonoBehaviour
    {
        public GameObject goMovingGoal;

        void Start()
        {
            iTween.MoveTo(this.gameObject,
                iTween.Hash("position", goMovingGoal.transform.position,
                            "time", 1f,
                            "looptype", iTween.LoopType.loop)
                );
        }

    }
}
