using UnityEngine;
using Kernal;
using Global;

namespace View
{
    public class View_PlayerInfoResponse_MajorCity : MonoBehaviour
    {
        public static View_PlayerInfoResponse_MajorCity Instance;
        public GameObject panelSkill;
        public GameObject panelTask;
        public GameObject panelShop;
        public GameObject panelBag;

        void Awake()
        {
            Instance = this;
        }

        public void DisplayPlayerRole()
        {
            View_PlayerInfoResponse.Instance.DisplayPlayerRoles();
        }
        public void HidePlayerRole()
        {
            View_PlayerInfoResponse.Instance.HidePlayerRoles();
        }

        public void DisplayPlayerSkill()
        {
            if (panelSkill != null)
            {
                BeforeOpenWindow(panelSkill);
            }
            panelSkill.SetActive(true);
        }
        public void HidePlayerSkill()
        {
            if (panelSkill != null)
            {
                BeforeCloseWindow();
            }
            panelSkill.SetActive(false);
        }

        public void DisplayPlayerTask()
        {
            if (panelTask != null)
            {
                BeforeOpenWindow(panelTask);
            }
            panelTask.SetActive(true);
        }
        public void HidePlayerTask()
        {
            if (panelTask != null)
            {
                BeforeCloseWindow();
            }
            panelTask.SetActive(false);
        }

        public void DisplayPlayerBag()
        {
            if (panelBag != null)
            {
                BeforeOpenWindow(panelBag);
            }
            panelBag.SetActive(true);
        }
        public void HidePlayerBag()
        {
            if (panelBag != null)
            {
                BeforeCloseWindow();
            }
            panelBag.SetActive(false);
        }

        public void DisplayPlayerShop()
        {
            if (panelShop != null)
            {
                BeforeOpenWindow(panelShop);
            }
            panelShop.SetActive(true);
        }
        public void HidePlayerShop()
        {
            if (panelShop != null)
            {
                BeforeCloseWindow();
            }
            panelShop.SetActive(false);
        }


        private void BeforeOpenWindow(GameObject needDisplayPanel)
        {
            View_PlayerInfoResponse.Instance.HideET();
            GetComponent<UIMaskMgr>().SetMaskWindow(needDisplayPanel);
        }
        private void BeforeCloseWindow()
        {
            View_PlayerInfoResponse.Instance.DisplayET();
            GetComponent<UIMaskMgr>().CancleMaskWindow();
        }
    }
}