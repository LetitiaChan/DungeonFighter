using Global;

namespace Model
{
    /// <summary>
    /// 玩家核心数值类
    /// Description: 
    ///     1> 作用：存取数据/XML对象持久化/用事件实现观察者模式[即自动更新视图层]
    /// </summary>
    public class PlayerKernalData
    {
        public static event del_PlayerKernalModel evePlayerKernal;

        private float _health;
        private float _magic;
        private float _attack;
        private float _defence;
        private float _dexterity;

        private float _maxHealth;
        private float _maxMagic;
        private float _maxAttack;
        private float _maxDefence;
        private float _maxDexterity;

        private float _attackByProp;
        private float _defenceByProp;
        private float _dexterityByProp;

        #region 属性
        public float Health
        {
            get { return _health; }
            set
            {
                _health = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Health", Health);
                    evePlayerKernal(kv);
                }
            }
        }
        public float Magic
        {
            get { return _magic; }
            set
            {
                _magic = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Magic", Magic);
                    evePlayerKernal(kv);
                }
            }
        }
        public float Attack
        {
            get { return _attack; }
            set
            {
                _attack = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Attack", Attack);
                    evePlayerKernal(kv);
                }
            }
        }
        public float Defence
        {
            get { return _defence; }
            set
            {
                _defence = value;
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Defence", Defence);
                    evePlayerKernal(kv);
                }
            }
        }
        public float Dexterity
        {
            get { return _dexterity; }
            set
            {
                _dexterity = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Dexterity", Dexterity);
                    evePlayerKernal(kv);
                }
            }
        }


        public float MaxHealth
        {
            get { return _maxHealth; }
            set
            {
                _maxHealth = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxHealth", MaxHealth);
                    evePlayerKernal(kv);
                }
            }
        }
        public float MaxMagic
        {
            get { return _maxMagic; }
            set
            {
                _maxMagic = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxMagic", MaxMagic);
                    evePlayerKernal(kv);
                }
            }
        }
        public float MaxAttack
        {
            get { return _maxAttack; }
            set
            {
                _maxAttack = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxAttack", MaxAttack);
                    evePlayerKernal(kv);
                }
            }
        }
        public float MaxDefence
        {
            get { return _maxDefence; }
            set
            {
                _maxDefence = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxDefence", MaxDefence);
                    evePlayerKernal(kv);
                }
            }
        }
        public float MaxDexterity
        {
            get { return _maxDexterity; }
            set
            {
                _maxDexterity = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxDexterity", MaxDexterity);
                    evePlayerKernal(kv);
                }
            }
        }

        public float AttackByProp
        {
            get
            {
                return _attackByProp;
            }
            set
            {
                _attackByProp = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("AttackByProp", AttackByProp);
                    evePlayerKernal(kv);
                }
            }
        }
        public float DefenceByProp
        {
            get
            {
                return _defenceByProp;
            }
            set
            {
                _defenceByProp = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("DefenceByProp", DefenceByProp);
                    evePlayerKernal(kv);
                }
            }
        }
        public float DexterityByProp
        {
            get
            {
                return _dexterityByProp;
            }

            set
            {
                _dexterityByProp = value;

                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("DexterityByProp", DexterityByProp);
                    evePlayerKernal(kv);
                }
            }
        }
        #endregion

        public PlayerKernalData() { }

        public PlayerKernalData(float health, float magic, float attack, float defence, float dexterity,
            float maxHealth, float maxMagic, float maxAttack, float maxDefence, float maxDexterity,
            float attackByProp, float defenceByProp, float dexterityByProp)
        {

            this._health = health;
            this._magic = magic;
            this._attack = attack;
            this._defence = defence;
            this._dexterity = dexterity;

            this._maxHealth = maxHealth;
            this._maxMagic = maxMagic;
            this._maxAttack = maxAttack;
            this._maxDefence = maxDefence;
            this._maxDexterity = maxDexterity;

            this._attackByProp = attackByProp;
            this._defenceByProp = defenceByProp;
            this._dexterityByProp = dexterityByProp;
        }
    }
}