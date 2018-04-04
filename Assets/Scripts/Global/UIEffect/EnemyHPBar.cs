using UnityEngine;
using UnityEngine.UI;
using Kernal;
using Control;

namespace Global
{
    public class EnemyHPBar : MonoBehaviour
    {
        public float HPPrefabLength = 1f;
        public float HPPrefabHeight = 1f;
        private float _HPCurrent;
        private float _HPMax;

        private GameObject _TargetEnemyObj;
        private Camera _WorldCamera;
        private Camera _UICamera;
        private Slider _UISlider;


        public void SetTargetEnemy(GameObject goEnemy)
        {
            _TargetEnemyObj = goEnemy;
        }

        void Start()
        {
            transform.localScale = new Vector3(HPPrefabLength, HPPrefabHeight, 0);
            _UISlider = GetComponent<Slider>();
            _WorldCamera = Camera.main.gameObject.GetComponent<Camera>();
            _UICamera = GameObject.FindGameObjectWithTag(Tag.Tag_UICamera).GetComponent<Camera>();
            if (_TargetEnemyObj == null)
            {
                Debug.LogError(GetType() + "Start()/_TargetEnemyObj = null!");
                return;
            }
        }

        void Update()
        {
            try
            {
                if (Time.frameCount % 2 == 0)
                {
                    _HPCurrent = _TargetEnemyObj.GetComponent<Ctrl_BaseEnemyProperty>().CurrentHealth;
                    _HPMax = _TargetEnemyObj.GetComponent<Ctrl_BaseEnemyProperty>().maxHealth;
                    _UISlider.value = _HPCurrent / _HPMax;
                    transform.localScale = new Vector3(HPPrefabLength, HPPrefabHeight, 0);
                    if (_HPCurrent <= _HPMax * 0.05)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
            catch (System.Exception)
            {

            }
        }

        void LateUpdate()
        {
            if (_TargetEnemyObj != null)
            {
                if (Time.frameCount % 2 == 0)
                {
                    //三维坐标系与UI坐标系转换
                    Vector3 pos = _WorldCamera.WorldToScreenPoint(_TargetEnemyObj.transform.position);
                    pos = _UICamera.ScreenToWorldPoint(pos);
                    transform.position = new Vector3(pos.x, pos.y + 2f, 0);
                }
            }
        }
    }
}