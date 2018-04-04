using UnityEngine;

using Control;

namespace View
{
    public class View_StartScenes : MonoBehaviour
    {
        public void ClickNewGame()
        {
            Ctrl_StartScenes.Instance.ClickNewGame();
        }

        public void ClickGameContinue()
        {
            Ctrl_StartScenes.Instance.ClickGameContinue();
        }
    }
}
