/*
 *      1> 定义整个项目的枚举类型
 *      2> 定义整个项目的委托
 *      3> 定义整个项目的系统常量
 *      4> 定义系统所有的Tag （标签）
 */
namespace Global
{

    /* 定义项目系统常量  */
    public class GlobalParameter
    {
        public const string JOYSTICK_NAME = "HeroJoystick";     //EasyTouch 插件定义摇杆名称


        public const string INPUT_MGR_ATTACKNAME_NORMAL = "NormalAttack";
        public const string INPUT_MGR_ATTACKNAME_MAGICTRICK_A = "MagicTrickA";
        public const string INPUT_MGR_ATTACKNAME_MAGICTRICK_B = "MagicTrickB";
        public const string INPUT_MGR_ATTACKNAME_MAGICTRICK_C = "MagicTrickC";
        public const string INPUT_MGR_ATTACKNAME_MAGICTRICK_D = "MagicTrickD";


        public const float INTERVAL_TIME_0DOT02 = 0.02f;
        public const float INTERVAL_TIME_0DOT1 = 0.1f;
        public const float INTERVAL_TIME_0DOT2 = 0.2f;
        public const float INTERVAL_TIME_0DOT3 = 0.3f;
        public const float INTERVAL_TIME_0DOT4 = 0.4f;
        public const float INTERVAL_TIME_0DOT5 = 0.5f;
        public const float INTERVAL_TIME_1 = 1f;
        public const float INTERVAL_TIME_1DOT5 = 1.5f;
        public const float INTERVAL_TIME_2 = 2f;
        public const float INTERVAL_TIME_2DOT5 = 2.5f;
        public const float INTERVAL_TIME_3 = 3f;
        public const float INTERVAL_TIME_4 = 4f;
        public const float INTERVAL_TIME_5 = 5f;
        public const float INTERVAL_TIME_10 = 10f;
    }

    /*  项目Tag (标签)定义 */
    public class Tag
    {
        public static string Player = "Player";
        public static string Tag_Enemys = "Tag_Enemys";
        public static string Tag_MajorCity_Up = "Tag_MajorCity_Up";
        public static string Tag_MajorCity_Down = "Tag_MajorCity_Down";
        public static string Tag_PackItems = "Tag_PackItems";
        public static string Tag_UICamera = "Tag_UICamera";
        public static string Tag_UIPlayerInfo = "Tag_UIPlayerInfo";


    }

    #region 项目的枚举类型
    public enum CurrentGameType
    {
        None,
        NewGame,
        Continue
    }

    public enum Scenes
    {
        TestScene,
        StartScene,
        LoadingScene,
        LogonScene,
        MajorCity,
        LevelOne,
        LevelTwo,
        BaseScene
    }

    public enum PlayerType
    {
        SwordHero,
        MagicHero,
        Other
    }

    public enum HeroActionState
    {
        None,
        Idle,
        Runing,
        Hurt,
        Dead,
        NormalAttack,
        MagicTrickA,
        MagicTrickB,
        MagicTrickC,
        MagicTrickD
    }

    public enum NormalATKComboState
    {
        NormalATK1,
        NormalATK2,
        NormalATK3,
    }

    public enum LevelName
    {
        Level_0 = 0,
        Level_1 = 1,
        Level_2 = 2,
        Level_3 = 3,
        Level_4 = 4,
        Level_5 = 5,
        Level_6 = 6,
        Level_7 = 7,
        Level_8 = 8,
        Level_9 = 9,
        Level_10 = 10
    }

    public enum EnemyState
    {
        Idle,
        Walking,
        Attack,
        Hurt,
        Dead
    }

    #endregion


    #region  项目的委托类型

    /// <summary>
    /// 委托： 主角控制
    /// </summary>
    /// <param name="controlType">控制的类型</param>
    public delegate void del_PlayerControlWithStr(string controlType);

    /// <summary>
    /// 委托： 玩家核心模型数值
    /// </summary>
    /// <param name="kv"></param>
    public delegate void del_PlayerKernalModel(KeyValuesUpdate kv);

    public delegate void del_EnemyAttrUpdate(KeyValuesUpdate kv);

    public delegate void del_FBEndNotify();

    /// <summary>
    /// 键值更新
    /// </summary>
    public class KeyValuesUpdate
    {
        private string _key;
        private object _value;

        public string Key
        {
            get { return _key; }
        }
        public object Value
        {
            get { return _value; }
        }

        public KeyValuesUpdate(string key, object values)
        {
            _key = key;
            _value = values;
        }
    }

    #endregion

}