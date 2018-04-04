using UnityEngine;
using System.Collections;
using Kernal;
using Global;

namespace Control
{
    public class Ctrl_Panel_Task : BaseControl
    {
        public static Ctrl_Panel_Task Instance;

        void Awake()
        {
            Instance = this;
        }

        public void EnterLevelTwo()
        {
            base.EnterNextScenes(Scenes.LevelTwo);
        }

    }
}
