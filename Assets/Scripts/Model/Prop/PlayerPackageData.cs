using UnityEngine;
using System.Collections;
using Kernal;
using Global;

namespace Model
{
    public class PlayerPackageData
    {
        public static del_PlayerKernalModel evePlayerPackageData;

        private int _bloodBottleNum;
        private int _magicBottleNum;
        private int _propATKNum;
        private int _propDEFNum;
        private int _propDEXNum;

        public int BloodBottleNum
        {
            get { return _bloodBottleNum; }
            set
            {
                _bloodBottleNum = value;
                if (evePlayerPackageData != null)
                {
                    //Log.Write(GetType() + "/BloodBottleNum", Log.Level.Special);
                    KeyValuesUpdate kv = new KeyValuesUpdate("BloodBottleNum", BloodBottleNum);
                    evePlayerPackageData(kv);
                }
            }
        }

        public int MagicBottleNum
        {
            get { return _magicBottleNum; }
            set
            {
                _magicBottleNum = value;
                if (evePlayerPackageData != null)
                {
                    //Log.Write(GetType() + "/MagicBottleNum", Log.Level.Special);
                    KeyValuesUpdate kv = new KeyValuesUpdate("MagicBottleNum", MagicBottleNum);
                    evePlayerPackageData(kv);
                }
            }
        }

        public int PropATKNum
        {
            get { return _propATKNum; }
            set
            {
                _propATKNum = value;
                if (evePlayerPackageData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("PropATKNum", PropATKNum);
                    evePlayerPackageData(kv);
                }
            }
        }

        public int PropDEFNum
        {
            get { return _propDEFNum; }
            set
            {
                _propDEFNum = value;
                if (evePlayerPackageData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("PropDEFNum", PropDEFNum);
                    evePlayerPackageData(kv);
                }
            }
        }

        public int PropDEXNum
        {
            get { return _propDEXNum; }
            set
            {
                _propDEXNum = value;
                if (evePlayerPackageData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("PropDEXNum", PropDEXNum);
                    evePlayerPackageData(kv);
                }
            }
        }

        //定义私有的构造函数
        private PlayerPackageData()
        {
        }

        public PlayerPackageData(int bloodBottleNum, int magicBottleNum, int ATKNum, int DEFNum, int DEXNum)
        {
            this._bloodBottleNum = bloodBottleNum;
            this._magicBottleNum = magicBottleNum;
            this._propATKNum = ATKNum;
            this._propDEFNum = DEFNum;
            this._propDEXNum = DEXNum;
        }
    }
}