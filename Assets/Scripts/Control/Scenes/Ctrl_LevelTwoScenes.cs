using UnityEngine;
using System;
using System.Collections;
using Kernal;
using Global;
using Model;

namespace Control
{
    public class Ctrl_LevelTwoScenes : BaseControl
    {
        public static Ctrl_LevelTwoScenes Instance;
        public static del_FBEndNotify eveFB_End;

        public AudioClip BackgroundMusic;
        public AudioClip BackgroundMusic_Fight;
        public string[] TagNameByHideObject;
        public GameObject goArcher;
        public GameObject goMage;
        public GameObject goKing;
        public GameObject goWarrior;
        public GameObject goParticleWall;
        public GameObject goBossBruce;

        public Transform[] TraSpawnEnemyPos_AreaA_Archer;
        public Transform[] TraSpawnEnemyPos_AreaA_Mage;
        public Transform[] TraSpawnEnemyPos_AreaB_Archer;
        public Transform[] TraSpawnEnemyPos_AreaB_King;
        public Transform[] TraSpawnEnemyPos_AreaB_Warrior;
        public Transform[] TraSpawnEnemyPos_AreaC_King;
        //区域D （Boss核心战斗区域）
        public Transform[] TraSpawnEnemyPos_AreaD_Boss;
        public Transform[] TraSpawnEnemyPos_AreaD_Archer;
        public Transform[] TraSpawnEnemyPos_AreaD_Mage;
        public Transform[] TraSpawnEnemyPos_AreaD_Warrior;
        public Transform[] TraSpawnEnemyPos_AreaD_King;


        private bool IsSingleSpawn_AreaA = true;
        private bool IsSingleSpawn_AreaB = true;
        private bool IsSingleSpawn_AreaC = true;
        private bool IsSingleSpawn_AreaD = true;
        private bool IsSingleSpawn_AreaE = true;

        //副本数据
        [HideInInspector]
        public bool IsBossBruceDead = false;
        public int FBLevel = 0;
        public int KillNum_king = 0;
        public int KillNum_archer = 0;
        public int KillNum_mage = 0;
        public int KillNum_warrior = 0;
        public int AwardNum_coin = 500;
        public int AwardNum_exp = 1000;

        private DateTime _timeStart;

        void Awake()
        {
            Instance = this;
            TriggerCommonEvent.eveCommonTrigger += SpawnEnemy;
            PlayerExternalData.evePlayerExtenalData += LevelUp;
        }

        private void SpawnEnemy(CommonTriggerType CTT)
        {
            switch (CTT)
            {
                case CommonTriggerType.None:
                    break;
                case CommonTriggerType.Enumy1_Dialog:
                    if (IsSingleSpawn_AreaA)
                    {
                        IsSingleSpawn_AreaA = false;
                        SpawnEnemy_Area_A();
                    }
                    break;
                case CommonTriggerType.Enumy2_Dialog:
                    if (IsSingleSpawn_AreaB)
                    {
                        IsSingleSpawn_AreaB = false;
                        SpawnEnemy_Area_B();
                    }
                    break;
                case CommonTriggerType.Enumy3_Dialog:
                    if (IsSingleSpawn_AreaC)
                    {
                        IsSingleSpawn_AreaC = false;
                        SpawnEnemy_Area_C();
                    }
                    break;
                case CommonTriggerType.Enumy4_Dialog:
                    if (IsSingleSpawn_AreaD)
                    {
                        IsSingleSpawn_AreaD = false;
                        EnterFightArea();
                    }
                    break;
                case CommonTriggerType.Enumy5_Dialog:
                    if (IsSingleSpawn_AreaE)
                    {
                        IsSingleSpawn_AreaE = false;
                        StartCoroutine("SpawnEnemy_Area_BossBruce");
                    }
                    break;
                default:
                    break;
            }
        }

        void Start()
        {
            ResetPlayer();

            AudioManager.AudioBackgroundVolumn = 0.4f;
            AudioManager.AudioEffectVolumn = 0.5f;
            AudioManager.PlayBackground(BackgroundMusic);
            StartCoroutine("HideUnActiveArea");//默认隐藏场景中的非活动区域
            goParticleWall.SetActive(false);
        }

        private void ResetPlayer()
        {
            var HeroStartPos = GameObject.Find("_Environment/HeroStartPos");
            var player = GameObject.FindGameObjectWithTag(Tag.Player);
            if (HeroStartPos && player)
            {
                player.transform.position = HeroStartPos.transform.position;
            }
        }

        private IEnumerator HideUnActiveArea()
        {
            yield return new WaitForEndOfFrame();

            foreach (string tagNameHideObj in TagNameByHideObject)
            {
                GameObject[] goHideObjArray = GameObject.FindGameObjectsWithTag(tagNameHideObj);
                foreach (GameObject item in goHideObjArray)
                {
                    item.SetActive(false);
                }
            }
        }

        #region 产生敌人
        private void SpawnEnemy_Area_A()
        {
            if (goArcher && goMage)
            {
                StartCoroutine(SpawnEnemey(goArcher, 1, TraSpawnEnemyPos_AreaA_Archer));
                StartCoroutine(SpawnEnemey(goMage, 2, TraSpawnEnemyPos_AreaA_Mage));
            }
        }
        private void SpawnEnemy_Area_B()
        {
            if (goArcher && goMage)
            {
                StartCoroutine(SpawnEnemey(goArcher, 2, TraSpawnEnemyPos_AreaB_Archer));
                StartCoroutine(SpawnEnemey(goWarrior, 2, TraSpawnEnemyPos_AreaB_Warrior));
                StartCoroutine(SpawnEnemey(goKing, 1, TraSpawnEnemyPos_AreaB_King));
            }
        }
        private void SpawnEnemy_Area_C()
        {
            if (goKing)
            {
                StartCoroutine(SpawnEnemey(goKing, 3, TraSpawnEnemyPos_AreaC_King, true));
            }
        }
        private void EnterFightArea()
        {
            #region for Test
            //Ctrl_HeroProperty.Instance.AddLevel();
            //Ctrl_HeroProperty.Instance.AddLevel();
            //Ctrl_HeroProperty.Instance.AddLevel();
            //Ctrl_HeroProperty.Instance.AddLevel();
            //Ctrl_HeroProperty.Instance.AddLevel();
            //Ctrl_HeroProperty.Instance.DecreaseHealthValues(1000f);
            //PlayerPackageDataProxy.GetInstance().IncreaseBloodBottleNum(5);
            #endregion


            GlobalParaMgr.IsBossFightingScene = true;
            ResetFBData();
            goParticleWall.SetActive(true);
            ClearEnemyInScene();

            AudioManager.AudioBackgroundVolumn = 0.6f;
            AudioManager.AudioEffectVolumn = 0.8f;
            AudioManager.PlayBackground(BackgroundMusic_Fight);

            GameObject goComboLabel = ResourcesMgr.GetInstance().LoadAsset("Prefabs/UI/ComboCountLabel", false);
            goComboLabel.transform.parent = GameObject.FindGameObjectWithTag(Tag.Tag_UIPlayerInfo).transform;
            goComboLabel.GetComponent<UnityEngine.UI.Text>().rectTransform.anchoredPosition = new Vector2(-216, 46);
        }

        IEnumerator SpawnEnemy_Area_BossBruce()
        {
            _timeStart = DateTime.Now;
            if (goBossBruce)
            {
                StartCoroutine(SpawnEnemey(goBossBruce, 1, TraSpawnEnemyPos_AreaD_Boss, false));
            }

            //循环刷小怪
            while (IsBossBruceDead == false)
            {
                yield return new WaitForSeconds(5f);
                if (GetEnemyNumber() <= 3)
                {
                    yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
                    if (IsBossBruceDead == false)
                    {
                        StartCoroutine(SpawnEnemey(goArcher, 2, TraSpawnEnemyPos_AreaD_Archer, true));
                        StartCoroutine(SpawnEnemey(goMage, 2, TraSpawnEnemyPos_AreaD_Mage, true));
                        StartCoroutine(SpawnEnemey(goWarrior, 1, TraSpawnEnemyPos_AreaD_Warrior, true));
                        StartCoroutine(SpawnEnemey(goKing, 1, TraSpawnEnemyPos_AreaD_King, true));
                    }
                }
            }
        }

        private int GetEnemyNumber()
        {
            GameObject[] goEnemy = GameObject.FindGameObjectsWithTag(Tag.Tag_Enemys);
            if (goEnemy != null)
                return goEnemy.Length;
            else
                return 0;
        }

        private void ClearEnemyInScene()
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag(Tag.Tag_Enemys);
            foreach (var item in enemys)
            {
                item.GetComponent<Ctrl_BaseEnemyProperty>().ClearEnemy();
            }
        }
        #endregion

        private void ResetFBData()
        {
            IsBossBruceDead = false;
            FBLevel = 0;
            KillNum_king = 0;
            KillNum_archer = 0;
            KillNum_mage = 0;
            KillNum_warrior = 0;
        }

        public void BattleEnd()
        {
            IsBossBruceDead = true;
            ClearEnemyInScene();

            //根据时长评级
            TimeSpan dur = DateTime.Now.Subtract(_timeStart).Duration();
            if (dur.TotalSeconds < 20f)
                FBLevel = 0;
            else if (dur.TotalSeconds < 60f)
                FBLevel = 1;
            else
                FBLevel = 2;
            //print("FB时长 " + dur.TotalSeconds + "s, 等级" + FBLevel.ToString());
            StartCoroutine("DisplayFBConclusion");
        }

        IEnumerator DisplayFBConclusion()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_5);
            if (eveFB_End != null)
                eveFB_End();
        }
    }
}