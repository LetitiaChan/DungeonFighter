/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:全局参数
 *
 *	Description:
 *		1.定义全局枚举
 *		2.定义全局委托
 *		3.定义全局常量
 *		4.定义系统Tag(标签)
 *
 *	Date:2017.02.20
 *
 *	Version:
 *		1.0
 *
 *	Author:chenx
 *
*/
using System.Collections;
using System.Collections.Generic;

namespace Global
{
    public class GlobalParameter
    {
        /// <summary>
        /// EasyTouch插件定义摇杆名称
        /// </summary>
        public const string JOYSTICK_NAME = "Herojoystick";

        /// <summary>
        /// 输入管理器定义_攻击名称_普通攻击
        /// </summary>
        public const string INPUT_MGR_ATTACKNAME_NORMAL = "NormalAttack";

        /// <summary>
        /// 输入管理器定义_攻击名称_大招A
        /// </summary>
        public const string INPUT_MGR_ATTACKNAME_MAGICTRICK_A = "MagicTrickA";

        /// <summary>
        /// 输入管理器定义_攻击名称_大招B
        /// </summary>
        public const string INPUT_MGR_ATTACKNAME_MAGICTRICK_B = "MagicTrickB";

        /// <summary>
        /// 输入管理器定义_攻击名称_大招C
        /// </summary>
        public const string INPUT_MGR_ATTACKNAME_MAGICTRICK_C = "MagicTrickC";

        /// <summary>
        /// 输入管理器定义_攻击名称_大招D
        /// </summary>
        public const string INPUT_MGR_ATTACKNAME_MAGICTRICK_D = "MagicTrickD";

        //间隔时间
        public const float INTERVAL_TIME_0DOT02 = 0.02F;
        public const float INTERVAL_TIME_0DOT1 = 0.1F;
        public const float INTERVAL_TIME_0DOT2 = 0.2F;
        public const float INTERVAL_TIME_0DOT3 = 0.3F;
        public const float INTERVAL_TIME_0DOT5 = 0.5F;
        public const float INTERVAL_TIME_1 = 1F;
        public const float INTERVAL_TIME_1DOT5 = 1.5F;
        public const float INTERVAL_TIME_2 = 2F;
        public const float INTERVAL_TIME_2DOT5 = 2.5F;
        public const float INTERVAL_TIME_3 = 3F;
    }

    /// <summary>
    /// 项目Tag（标签）定义
    /// </summary>
    public class Tag
    {
        public static string Tag_Enemys = "Tag_Enemys";
        public static string Player = "Player";
    }

    #region 项目的枚举类型
    /// <summary>
    /// 场景名称
    /// </summary>
    public enum EnumScenes
    {
        ScenesStart,
        ScenesLogin,
        ScenesLoading,
        ScenesLevelOne,
        ScenesLevelTwo,
    }
    /// <summary>
    /// 英雄类型
    /// </summary>
    public enum EnumHeroType
    {
        SwordHero,  //少年剑侠
        MagicHero,  //魔法师
        Other
    }

    /// <summary>
    /// 主角的动作状态
    /// </summary>
    public enum HeroActionState
    {
        None,
        Idle,
        Runing,
        NormalAttack,
        MagicTrickA,
        MagicTrickB,
        MagicTrickC,
        MagicTrickD
    }

    /// <summary>
    /// 普通攻击,连招
    /// </summary>
    public enum NormalATKComboState
    {
        NormalATK1,
        NormalATK2,
        NormalATK3,
    }
    /// <summary>
    /// 等级名称
    /// </summary>
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
    /// <summary>
    /// 简单敌人状态
    /// </summary>
    public enum EnemyState
    {
        Idle,                                                                  //休闲
        Walking,                                                               //行走
        Attack,                                                                //攻击
        Hurt,                                                                  //受伤
        Dead                                                                   //死亡
    }
    #endregion

    #region 项目的委托类型
    /// <summary>
    /// 委托：主角控制
    /// </summary>
    /// <param name="controlType">控制类型</param>
    public delegate void del_PlayerControlWithStr(string controlType);

    /// <summary>
    /// 委托：玩家核心模型数值
    /// </summary>
    /// <param name="kv"></param>
    public delegate void del_PlayerKernalModel(KeyValuesUpdate kv);

    /// <summary>
    /// 键值更新类
    /// </summary>
    public class KeyValuesUpdate
    {
        private string _Key;        //键
        private object _Value;      //值

        public string Key
        {
            get
            {
                return _Key;
            }
        }

        public object Value
        {
            get
            {
                return _Value;
            }
        }

        public KeyValuesUpdate(string key, object value)
        {
            _Key = key;
            _Value = value;
        }
    }

    #endregion
}