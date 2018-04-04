using UnityEngine;
using UnityEngine.UI;
using Kernal;

namespace Global
{
    public enum DialogType
    {
        None,
        DoubleDialog, //双人会话
        SingleDialog  //单人会话
    }

    public class DialogUIMgr : MonoBehaviour
    {
        public static DialogUIMgr _instance;

        #region Unity Inspector Fields
        public GameObject goHero;
        public GameObject goNPC_Left;
        public GameObject goNPC_Right;
        public GameObject goSingleDialogArea;
        public GameObject goDoubleDialogArea;

        public Text txtPersonName;
        public Text txtDoubleDialogContent;
        public Text txtSingleDialogContent;
        public Button btnDialogOK;

        [Tooltip("(0下标表示彩色精灵，1下标表示黑白精灵)")]
        public Sprite[] SprHero;
        public Sprite[] SprNPC_Right;
        #endregion


        void Awake()
        {
            _instance = this;
        }

        public bool DisplayNextDialog(DialogType diaType, int dialogSectionNum)
        {
            bool isDialogEnd = false;

            DialogDataFormat dia = DialogDataMgr.GetInstance().GetNextDialogRecord(dialogSectionNum);
            if (dia != null && dia.DialogSide != DialogSide.None)
            {
                DisplayDialogContent(diaType, dia);
            }
            else
                isDialogEnd = true;

            return isDialogEnd;
        }

        private void DisplayDialogContent(DialogType diaType, DialogDataFormat dia)
        {
            ChangeDialogUI(diaType, dia.DialogSide);
            btnDialogOK.gameObject.SetActive(dia.DialogButton);

            switch (diaType)
            {
                case DialogType.SingleDialog:
                    txtSingleDialogContent.text = dia.DialogContent;
                    break;
                case DialogType.DoubleDialog:
                    if (!string.IsNullOrEmpty(dia.DialogPerson) && !string.IsNullOrEmpty(dia.DialogContent))
                    {
                        txtPersonName.text = dia.DialogSide == DialogSide.HeroSide ? GlobalParaMgr.PlayerName : dia.DialogPerson;
                        txtDoubleDialogContent.text = dia.DialogContent;
                    }
                    break;
            }
        }

        private void ChangeDialogUI(DialogType diaType, DialogSide diaSide)
        {
            bool isDoubleDialog = diaType == DialogType.DoubleDialog;
            bool isSingleDialog = diaType == DialogType.SingleDialog;

            goNPC_Left.SetActive(isSingleDialog);
            goSingleDialogArea.SetActive(isSingleDialog);

            goHero.SetActive(isDoubleDialog);
            goNPC_Right.SetActive(isDoubleDialog);
            goDoubleDialogArea.SetActive(isDoubleDialog);


            if (isDoubleDialog)
            {
                bool isHeroSide = diaSide == DialogSide.HeroSide;
                bool isNPCSide = diaSide == DialogSide.NPCSide;
                int activateIndex = 0;
                int inactivateIndex = 1;    //(0下标表示彩色精灵，1下标表示黑白精灵)
                goHero.GetComponent<Image>().overrideSprite = SprHero[isHeroSide ? activateIndex : inactivateIndex];
                goNPC_Right.GetComponent<Image>().overrideSprite = SprNPC_Right[isHeroSide ? inactivateIndex : activateIndex];
            }
        }

    }
}