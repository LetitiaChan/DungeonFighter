using Global;

namespace Model
{
    /// <summary>
    /// 玩家扩展数值类
    /// </summary>
    public class PlayerExternalData
    {
        public static event del_PlayerKernalModel evePlayerExtenalData;

        private int _experience;
        private int _killNumber;
        private int _level;
        private int _gold;
        private int _diamonds;

        #region 属性
        public int Experience
        {
            get
            {
                return _experience;
            }

            set
            {
                _experience = value;

                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Experience", Experience);
                    evePlayerExtenalData(kv);
                }
            }
        }
        public int KillNumber
        {
            get
            {
                return _killNumber;
            }

            set
            {
                _killNumber = value;

                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("KillNumber", KillNumber);
                    evePlayerExtenalData(kv);
                }
            }
        }
        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                var old = _level;
                _level = value;

                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Level", Level);
                    evePlayerExtenalData(kv);
                    if (old < _level)
                    {
                        KeyValuesUpdate kvup = new KeyValuesUpdate("LevelUp", Level);
                        evePlayerExtenalData(kvup);
                    }
                }
            }
        }
        public int Gold
        {
            get
            {
                return _gold;
            }

            set
            {
                _gold = value;

                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Gold", Gold);
                    evePlayerExtenalData(kv);
                }
            }
        }
        public int Diamonds
        {
            get
            {
                return _diamonds;
            }

            set
            {
                _diamonds = value;

                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Diamonds", Diamonds);
                    evePlayerExtenalData(kv);
                }
            }
        }
        #endregion

        public PlayerExternalData() { }

        public PlayerExternalData(int exp, int killNumber, int level, int gold, int diamonds)
        {
            this._experience = exp;
            this._killNumber = killNumber;
            this._level = level;
            this._gold = gold;
            this._diamonds = diamonds;
        }
    }
}