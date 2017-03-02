/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:公共层： 枚举类型转换字符串
 *
 *	Description:
 *		1.（单例模式的应用）
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
using UnityEngine;

namespace Global
{
    public class ConvertEnumToStr
    {
        private static ConvertEnumToStr _Instance;                             //本类实例
        private Dictionary<EnumScenes, string> _DicScenesEnumLib;              //枚举场景类型集合

        /// <summary>
        /// 构造函数
        /// </summary>
        private ConvertEnumToStr()
        {
            _DicScenesEnumLib = new Dictionary<EnumScenes, string>();
            _DicScenesEnumLib.Add(EnumScenes.ScenesStart, "1_StartScene");
            _DicScenesEnumLib.Add(EnumScenes.ScenesLoading, "LoadingScene");
            _DicScenesEnumLib.Add(EnumScenes.ScenesLogin, "2_LoginScene");
            _DicScenesEnumLib.Add(EnumScenes.ScenesLevelOne, "3_LevelOneScene");
            //_DicScenesEnumLib.Add(ScenesEnum.TestScenes, "100_TestDialogsScenes");
        }

        /// <summary>
        /// 得到实例
        /// </summary>
        /// <returns></returns>
        public static ConvertEnumToStr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new ConvertEnumToStr();
            }
            return _Instance;
        }

        /// <summary>
        /// 得到字符串形式的场景名称
        /// </summary>
        /// <param name="scene">枚举类型的场景名称</param>
        /// <returns></returns>
        public string GetStrByEnumScenes(EnumScenes scene)
        {
            if (_DicScenesEnumLib != null && _DicScenesEnumLib.Count >= 1)
            {
                return _DicScenesEnumLib[scene];
            }
            else
            {
                Debug.LogWarning(GetType() + "/GetStrByEnumScenes(),check SceneName");
                return null;
            }
        }
    }
}