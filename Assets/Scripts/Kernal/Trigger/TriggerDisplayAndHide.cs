using UnityEngine;

namespace Kernal
{
    /// <summary>
    /// 触发游戏对象显示与隐藏，即手工版的“遮挡剔除”
    /// </summary>
    public class TriggerDisplayAndHide : MonoBehaviour
    {
        public string TagNameByHero = "Player";
        public string TagNameByDisplayObject = "TagNameDisplayName";
        public string TagNameByHideObject = "TagNameHideName";

        private GameObject[] GoDisplayObjectArray;
        private GameObject[] GoHideObjectArray;


        void Start()
        {
            GoDisplayObjectArray = GameObject.FindGameObjectsWithTag(TagNameByDisplayObject);
            GoHideObjectArray = GameObject.FindGameObjectsWithTag(TagNameByHideObject);
        }

        void OnTriggerEnter(Collider con)
        {
            if (con.gameObject.tag == TagNameByHero)
            {
                foreach (GameObject goItem in GoDisplayObjectArray)
                {
                    goItem.SetActive(true);
                }
            }
        }

        void OnTriggerExit(Collider con)
        {
            if (con.gameObject.tag == TagNameByHero)
            {
                foreach (GameObject goItem in GoHideObjectArray)
                {
                    goItem.SetActive(false);
                }
            }
        }
    }
}