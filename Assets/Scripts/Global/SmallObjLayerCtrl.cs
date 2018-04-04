using UnityEngine;

namespace Global
{
    /// <summary>
    /// 层消隐技术：小物件远距离消隐，近距离显示
    /// </summary>
    public class SmallObjLayerCtrl : MonoBehaviour
    {
        public int disappearDistance = 10;

        private float[] distanceArray = new float[32]; //Unity最多支持32层

        void Start()
        {
            distanceArray[8] = disappearDistance;// 8：Layer_SmallObj 定义在Index=8
            Camera.main.layerCullDistances = distanceArray;
        }

    }
}
