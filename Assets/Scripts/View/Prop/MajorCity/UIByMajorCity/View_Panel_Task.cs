using UnityEngine;
using Kernal;
using Global;
using Control;

namespace View
{
    public class View_Panel_Task : MonoBehaviour
    {

        public void EnterLevelTwo()
        {
            Ctrl_Panel_Task.Instance.EnterLevelTwo();
        }
    }
}