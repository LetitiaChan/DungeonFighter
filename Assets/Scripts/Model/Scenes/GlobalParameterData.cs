using UnityEngine;
using System.Collections;
using Kernal;
using Global;

namespace Model
{
    /// <summary>
    /// for 对象持久化
    /// </summary>
    public class GlobalParameterData
    {
        private static Scenes _NextSceneName;
        private string _PlayerName;

        public Scenes NextSceneName
        {
            get { return _NextSceneName; }
            set { _NextSceneName = value; }
        }

        public string PlayerName
        {
            get { return _PlayerName; }
            set { _PlayerName = value; }
        }

        //无参构造函数
        private GlobalParameterData() { }
        public GlobalParameterData(Scenes sceneName, string playerName)
        {
            _NextSceneName = sceneName;
            _PlayerName = playerName;
        }
    }
}