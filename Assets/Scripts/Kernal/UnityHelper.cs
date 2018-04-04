using UnityEngine;

namespace Kernal
{
    public class UnityHelper
    {
        private static UnityHelper _instance = null;
        private float _accumulatedTime;


        private UnityHelper()
        { }

        public static UnityHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UnityHelper();
            }
            return _instance;
        }


        /// <summary>
        /// 间隔指定时间段，返回布尔数值
        /// </summary>
        /// <param name="smallIntervalTime">指定的时间段间隔（0.1-3F 秒之间）</param>
        /// <returns>
        /// true: 表示指定时间段到了。
        /// </returns>
        public bool isTimeOutSmall(float smallIntervalTime)
        {
            _accumulatedTime += Time.deltaTime;
            if (_accumulatedTime >= smallIntervalTime)
            {
                _accumulatedTime = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void FaceToGoal(Transform self, Transform goal, float rotatSpeed)
        {
            //self.transform.LookAt(_TraNearestEnemy); //LookAt角度有问题，采用注视旋转
            Vector3 rotatForward = new Vector3(goal.position.x, 0, goal.position.z) -
                                    new Vector3(self.position.x, 0, self.position.z);
            self.rotation = Quaternion.Slerp(self.rotation, Quaternion.LookRotation(rotatForward), rotatSpeed);
        }

        public int GetRandomNumByRange(int minNum, int maxNum)
        {
            int randomNumResult = 0;

            if (minNum == maxNum)
                randomNumResult = minNum;
            else
                randomNumResult = Random.Range(minNum, maxNum + 1);

            return randomNumResult;
        }

    }
}
