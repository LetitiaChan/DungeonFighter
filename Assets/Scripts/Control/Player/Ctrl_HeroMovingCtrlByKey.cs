using UnityEngine;
using Global;
using Kernal;

namespace Control
{
    public class Ctrl_HeroMovingCtrlByKey : BaseControl
    {
        //#if UNITY_STANDALONE_WIN || UNITY_EDITOR
#if UNITY_STANDALONE_WIN
        public float heroMovingSpeed = 5f;

        private CharacterController _cc;
        private float _gravity = 1f;

        void Start()
        {
            _cc = GetComponent<CharacterController>();
        }

        void Update()
        {
            //if(Time.frameCount % 3 == 0){ } 提高脚本效率,每3帧执行一次
            ControlMoving();
        }

        void ControlMoving()
        {
            float movingXPos = Input.GetAxis("Horizontal");//从-1到1偏移量
            float movingYPos = Input.GetAxis("Vertical");

            if (movingXPos != 0 || movingYPos != 0)
            {
                /*大招时锁定方向*/
                if (!Ctrl_HeroAnimationCtrl.Instance.IsHeroActionMagicTrick())
                {
                    //设置角色的朝向（朝向当前坐标+摇杆偏移量）
                    transform.LookAt(new Vector3(transform.position.x - movingXPos, transform.position.y, transform.position.z - movingYPos));
                }
                Vector3 movement = transform.forward * Time.deltaTime * heroMovingSpeed;
                movement.y -= _gravity;
                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle ||
                    Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Runing)
                {
                    _cc.Move(movement);

                    if (UnityHelper.GetInstance().isTimeOutSmall(0.3F))//0.2
                    {
                        Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Runing);
                    }
                }
            }
            //else
            //{
            //    //if (UnityHelper.GetInstance().GetSmallTime(0.2F))
            //    //{
            //    //    Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Idle);
            //    //}
            //}
        }

#endif

    }
}