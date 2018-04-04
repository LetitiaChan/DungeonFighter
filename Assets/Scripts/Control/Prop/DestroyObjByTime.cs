namespace Control
{
    public class DestroyObjByTime : BaseControl
    {
        public float destroyTime = 2f;

        void Start()
        {
            Destroy(this.gameObject, destroyTime);
        }
    }
}
