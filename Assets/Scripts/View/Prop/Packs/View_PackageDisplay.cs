using UnityEngine;
using UnityEngine.UI;
using Kernal;
using Global;
using Model;

namespace View
{
    public class View_PackageDisplay : MonoBehaviour
    {
        public GameObject goPropBloodBottle;
        public GameObject goPropMagicBottle;
        public GameObject goPropATK;
        public GameObject goPropDEF;
        public GameObject goPropDEX;

        public Text txtPropBloodBottleNum;
        public Text txtPropMagicBottleNum;


        void Awake()
        {
            PlayerPackageData.evePlayerPackageData += DisplayProp;
        }

        public void DisplayProp(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("BloodBottleNum"))
            {
                if (goPropBloodBottle && txtPropBloodBottleNum)
                {
                    if (System.Convert.ToInt32(kv.Value) >= 1)
                    {
                        goPropBloodBottle.SetActive(true);
                        txtPropBloodBottleNum.text = kv.Value.ToString();
                    }
                }
            }
            else if (kv.Key.Equals("MagicBottleNum"))
            {
                if (goPropMagicBottle && txtPropMagicBottleNum)
                {
                    if (System.Convert.ToInt32(kv.Value) >= 1)
                    {
                        goPropMagicBottle.SetActive(true);
                        txtPropMagicBottleNum.text = kv.Value.ToString();
                    }
                }
            }
            else if (kv.Key.Equals("PropATKNum"))
            {
                if (goPropATK && System.Convert.ToInt32(kv.Value) >= 1)
                {
                    goPropATK.SetActive(true);
                }
            }
            else if (kv.Key.Equals("PropDEFNum"))
            {
                if (goPropDEF && System.Convert.ToInt32(kv.Value) >= 1)
                {
                    goPropDEF.SetActive(true);
                }
            }
            else if (kv.Key.Equals("PropDEXNum"))
            {
                if (goPropDEX && System.Convert.ToInt32(kv.Value) >= 1)
                {
                    goPropDEX.SetActive(true);
                }
            }
        }

    }
}