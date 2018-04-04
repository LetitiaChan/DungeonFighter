using UnityEngine;
using System.Collections.Generic;

namespace Global
{
    public class ConvertEnumToStr
    {
        private static ConvertEnumToStr _instance;
        private Dictionary<Scenes, string> _scenesLib;


        private ConvertEnumToStr()
        {
            _scenesLib = new Dictionary<Scenes, string>();
            _scenesLib.Add(Scenes.StartScene, "1_StartScene");
            _scenesLib.Add(Scenes.LogonScene, "2_LogonScene");
            _scenesLib.Add(Scenes.LoadingScene, "0_LoadingScene");
            _scenesLib.Add(Scenes.LevelOne, "3_LevelOne");
            _scenesLib.Add(Scenes.MajorCity, "4_MajorScene");
            _scenesLib.Add(Scenes.LevelTwo, "5_LevelTwo");
            _scenesLib.Add(Scenes.TestScene, "100_TestScene");
        }

        public static ConvertEnumToStr GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConvertEnumToStr();
            }
            return _instance;
        }

        public string GetStrByEnumScenes(Scenes scene)
        {
            if (_scenesLib != null && _scenesLib.Count >= 1)
            {
                return _scenesLib[scene];
            }
            else
            {
                Debug.LogWarning(GetType() + "/GetStrByEnumScenes()/ _DicScenesEnumLib.count<=0 !, please check!");
                return null;
            }
        }

    }
}
