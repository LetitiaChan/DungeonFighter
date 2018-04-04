using UnityEngine;

namespace Control
{
    public class MakeObjRotation : BaseControl
    {

        public float rotateSpeed = 1f;

        void Update()
        {
            transform.Rotate(Vector3.up, rotateSpeed);
        }

    }
}
