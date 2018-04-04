using UnityEngine;

namespace Kernal
{
    /// <summary>
    /// 相机跟随脚本（固定角度）
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        #region Unity Inspector Fields
        public float distance = 6.0f;                                           // The distance in the x-z plane to the target
        public float height = 8.0f;                                             // the height we want the camera to be above the target
        public float heightDamping = 4.0f;
        public float rotationDamping = 0.0f;
        #endregion
        public Transform target = null;                                         // The target we are following

        private float _timer = 2.0f;
        private GameObject _player;


        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            if (_player == null)
            {
                _player = ResourcesMgr.GetInstance().LoadAsset("Prefabs/Player/SwordAnimatorHero", false);
            }
            target = _player.transform;
        }

        void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                target = _player.transform;
                _timer = 2.0f;
            }
        }

        void LateUpdate()
        {
            if (!target) return;

            var wantedRotationAngle = target.eulerAngles.y;
            var wantedHeight = target.position.y + height;
            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);// Convert the angle into a rotation
            // Set the position of the camera on the x-z plane to: distance meters behind the target
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);// Set the height of the camera

            transform.LookAt(target);
        }
    }
}