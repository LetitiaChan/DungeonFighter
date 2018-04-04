using Global;

namespace Control
{
    public class Ctrl_HeroAttackInputByET : BaseControl
    {
        //#if UNITY_ANDROID || UNITY_IPHONE

        public static Ctrl_HeroAttackInputByET Instance;
        public static event del_PlayerControlWithStr evePlayerControl;

        void Awake()
        {
            Instance = this;
        }

        public void ResponseATKByNormal()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_NORMAL);
            }
        }
        public void ResponseATKByMagicA()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_A);
            }
        }
        public void ResponseATKByMagicB()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_B);
            }
        }
        public void ResponseATKByMagicC()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_C);
            }
        }
        public void ResponseATKByMagicD()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_D);
            }
        }

        //#endif
    }
}