using UnityEngine;
using System.Collections;
using Kernal;
using Model;

namespace Global
{
    /// <summary>
    /// 游戏的存盘与调用
    /// </summary>
    public class SaveAndLoading : MonoBehaviour
    {
        private static SaveAndLoading _Instance;
        /*数据持久化路径*/
        private static string _FileNameByGlobalParameterData = Application.persistentDataPath + "/GlobalParaData";
        private static string _FileNameByKernalData = Application.persistentDataPath + "/KernalData";
        private static string _FileNameByExtenalData = Application.persistentDataPath + "/ExtenalData";
        private static string _FileNameByPackageData = Application.persistentDataPath + "/PackageData";

        private PlayerKernalDataProxy _playerKernalDataProxy;
        private PlayerExternalDataProxy _playerExternalDataProxy;
        private PlayerPackageDataProxy _playerPackageDataProxy;

        public static SaveAndLoading GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_SaveAndLoading").AddComponent<SaveAndLoading>();
            }
            return _Instance;
        }

        #region 存储游戏进度
        public void SaveGameProgress()
        {
            _playerKernalDataProxy = PlayerKernalDataProxy.GetInstance();
            _playerExternalDataProxy = PlayerExternalDataProxy.GetInstance();
            _playerPackageDataProxy = PlayerPackageDataProxy.GetInstance();


            StoreToXML_GlobalParaData();
            StoreToXML_KernalData();
            StoreToXML_ExtenalData();
            StoreToXML_PackageData();
        }

        private void StoreToXML_GlobalParaData()
        {
            string playerName = GlobalParaMgr.PlayerName;
            Scenes sceneName = GlobalParaMgr.NextSceneName;
            GlobalParameterData GPD = new GlobalParameterData(sceneName, playerName);
            string s = XmlOperation.GetInstance().SerializeObject(GPD, typeof(GlobalParameterData));
            if (!string.IsNullOrEmpty(_FileNameByGlobalParameterData))
            {
                XmlOperation.GetInstance().CreateXML(_FileNameByGlobalParameterData, s);
            }

            //Log.Write(GetType() + "StoreToXML_GlobalParaData()/XML path = " + _FileNameByGlobalParameterData, Log.Level.Special);
        }
        private void StoreToXML_KernalData()
        {
            float health = _playerKernalDataProxy.Health;
            float magic = _playerKernalDataProxy.Magic;
            float atack = _playerKernalDataProxy.Attack;
            float def = _playerKernalDataProxy.Defence;
            float dex = _playerKernalDataProxy.Dexterity;

            float maxHealth = _playerKernalDataProxy.MaxHealth;
            float maxMagic = _playerKernalDataProxy.MaxMagic;
            float maxATK = _playerKernalDataProxy.MaxAttack;
            float maxDEF = _playerKernalDataProxy.MaxDefence;
            float maxDEX = _playerKernalDataProxy.MaxDexterity;

            float ATKProp = _playerKernalDataProxy.AttackByProp;
            float DEFProp = _playerKernalDataProxy.DefenceByProp;
            float DEXProp = _playerKernalDataProxy.DexterityByProp;

            PlayerKernalData PKD = new PlayerKernalData(health, magic, atack, def, dex, maxHealth, maxMagic, maxATK, maxDEF, maxDEX, ATKProp, DEFProp, DEXProp);
            string s = XmlOperation.GetInstance().SerializeObject(PKD, typeof(PlayerKernalData));
            if (!string.IsNullOrEmpty(_FileNameByKernalData))
            {
                XmlOperation.GetInstance().CreateXML(_FileNameByKernalData, s);
            }

            //Log.Write(GetType() + "StoreToXML_KernalData()/XML path = " + _FileNameByKernalData, Log.Level.Special);
        }
        private void StoreToXML_ExtenalData()
        {
            int exp = _playerExternalDataProxy.Experience;
            int killnum = _playerExternalDataProxy.KillNumber;
            int level = _playerExternalDataProxy.Level;
            int gold = _playerExternalDataProxy.Gold;
            int diamond = _playerExternalDataProxy.Diamonds;

            PlayerExternalData PED = new PlayerExternalData(exp, killnum, level, gold, diamond);
            string s = XmlOperation.GetInstance().SerializeObject(PED, typeof(PlayerExternalData));
            if (!string.IsNullOrEmpty(_FileNameByExtenalData))
            {
                XmlOperation.GetInstance().CreateXML(_FileNameByExtenalData, s);
            }

            //Log.Write(GetType() + "StoreToXML_ExtenalData()/XML path = " + _FileNameByExtenalData, Log.Level.Special);
        }
        private void StoreToXML_PackageData()
        {
            int bloodBottleNum = _playerPackageDataProxy.BloodBottleNum;
            int magicBottleNum = _playerPackageDataProxy.MagicBottleNum;
            int atkNum = _playerPackageDataProxy.PropATKNum;
            int defNum = _playerPackageDataProxy.PropDEFNum;
            int dexNum = _playerPackageDataProxy.PropDEXNum;

            PlayerPackageData PPD = new PlayerPackageData(bloodBottleNum, magicBottleNum, atkNum, defNum, dexNum);
            string s = XmlOperation.GetInstance().SerializeObject(PPD, typeof(PlayerPackageData));
            if (!string.IsNullOrEmpty(_FileNameByPackageData))
            {
                XmlOperation.GetInstance().CreateXML(_FileNameByPackageData, s);
            }

            //Log.Write(GetType() + "StoreToXML_PackageData()/XML path = " + _FileNameByPackageData, Log.Level.Special);
        }
        #endregion

        #region 提取游戏进度
        public bool LoadingGame_GlobalParaData()
        {
            ReadFromXML_GlobalParaData();
            return true;
        }
        public bool LoadingGame_PlayerData()
        {
            ReadFromXML_PlayerKernalData();
            ReadFromXML_PlayerExtenalData();
            ReadFromXML_PlayerPackageData();
            return true;
        }
        private void ReadFromXML_GlobalParaData()
        {
            GlobalParameterData dpd;
            if (string.IsNullOrEmpty(_FileNameByGlobalParameterData))
            {
                Debug.LogError(GetType() + "/ReadFromXML_GlobalParaData()/_FileNameByGlobalParameterData is not find");
                return;
            }
            try
            {
                string temp = XmlOperation.GetInstance().LoadXML(_FileNameByGlobalParameterData);
                dpd = XmlOperation.GetInstance().DeserializeObject(temp, typeof(GlobalParameterData)) as GlobalParameterData;
                GlobalParaMgr.PlayerName = dpd.PlayerName;
                GlobalParaMgr.NextSceneName = Scenes.MajorCity;     //dpd.NextSceneName
                GlobalParaMgr.CurGameType = CurrentGameType.Continue;
            }
            catch
            {
                Debug.LogError(GetType() + "ReadFromXML_GlobalParaData()/读取游戏的全局参数不成功，请检查");
            }
        }
        private void ReadFromXML_PlayerKernalData()
        {
            PlayerKernalData pkd;
            if (string.IsNullOrEmpty(_FileNameByKernalData))
            {
                Debug.LogError(GetType() + "/ReadFromXML_PlayerKernalData()/_FileNameByKernalData is not find");
                return;
            }
            try
            {
                string temp = XmlOperation.GetInstance().LoadXML(_FileNameByKernalData);
                pkd = XmlOperation.GetInstance().DeserializeObject(temp, typeof(PlayerKernalData)) as PlayerKernalData;

                PlayerKernalDataProxy.GetInstance().Health = pkd.Health;
                PlayerKernalDataProxy.GetInstance().Magic = pkd.Magic;
                PlayerKernalDataProxy.GetInstance().Attack = pkd.Attack;
                PlayerKernalDataProxy.GetInstance().Defence = pkd.Defence;
                PlayerKernalDataProxy.GetInstance().Dexterity = pkd.Dexterity;

                PlayerKernalDataProxy.GetInstance().MaxHealth = pkd.MaxHealth;
                PlayerKernalDataProxy.GetInstance().MaxMagic = pkd.MaxMagic;
                PlayerKernalDataProxy.GetInstance().MaxAttack = pkd.MaxAttack;
                PlayerKernalDataProxy.GetInstance().MaxDefence = pkd.MaxDefence;
                PlayerKernalDataProxy.GetInstance().MaxDexterity = pkd.MaxDexterity;

                PlayerKernalDataProxy.GetInstance().AttackByProp = pkd.AttackByProp;
                PlayerKernalDataProxy.GetInstance().DefenceByProp = pkd.DefenceByProp;
                PlayerKernalDataProxy.GetInstance().DexterityByProp = pkd.DexterityByProp;
            }
            catch
            {
                Debug.LogError(GetType() + "ReadFromXML_PlayerKernalData()/读取游戏的玩家核心参数不成功，请检查");
            }
        }
        private void ReadFromXML_PlayerExtenalData()
        {
            PlayerExternalData ped;
            if (string.IsNullOrEmpty(_FileNameByExtenalData))
            {
                Debug.LogError(GetType() + "/ReadFromXML_PlayerExtenalData()/_FileNameByExtenalData is not find");
                return;
            }
            try
            {
                string temp = XmlOperation.GetInstance().LoadXML(_FileNameByExtenalData);
                ped = XmlOperation.GetInstance().DeserializeObject(temp, typeof(PlayerExternalData)) as PlayerExternalData;

                PlayerExternalDataProxy.GetInstance().Experience = ped.Experience;
                PlayerExternalDataProxy.GetInstance().KillNumber = ped.KillNumber;
                PlayerExternalDataProxy.GetInstance().Level = ped.Level;
                PlayerExternalDataProxy.GetInstance().Gold = ped.Gold;
                PlayerExternalDataProxy.GetInstance().Diamonds = ped.Diamonds;
            }
            catch
            {
                Debug.LogError(GetType() + "ReadFromXML_PlayerExtenalData()/读取游戏的玩家扩展参数不成功，请检查");
            }
        }
        private void ReadFromXML_PlayerPackageData()
        {
            PlayerPackageData ppd;
            if (string.IsNullOrEmpty(_FileNameByPackageData))
            {
                Debug.LogError(GetType() + "/ReadFromXML_PlayerPackageData()/_FileNameByPackageData is not find");
                return;
            }
            try
            {
                string temp = XmlOperation.GetInstance().LoadXML(_FileNameByPackageData);
                ppd = XmlOperation.GetInstance().DeserializeObject(temp, typeof(PlayerPackageData)) as PlayerPackageData;

                PlayerPackageDataProxy.GetInstance().BloodBottleNum = ppd.BloodBottleNum;
                PlayerPackageDataProxy.GetInstance().MagicBottleNum = ppd.MagicBottleNum;
                PlayerPackageDataProxy.GetInstance().PropATKNum = ppd.PropATKNum;
                PlayerPackageDataProxy.GetInstance().PropDEFNum = ppd.PropDEFNum;
                PlayerPackageDataProxy.GetInstance().PropDEXNum = ppd.PropDEXNum;
            }
            catch
            {
                Debug.LogError(GetType() + "ReadFromXML_PlayerPackageData()/读取游戏的玩家背包参数不成功，请检查");
            }
        }
        #endregion

    }
}