using UnityEngine;
using System.Collections;
using Global;
using Kernal;
using Model;

namespace Control
{
    /// <summary>
    /// Title: 控制层：第一关卡场景控制处理
    /// Description:
    ///     1： 负责第一关卡敌人的动态加载。
    ///     2： 敌人的“多出生点”设计
    /// </summary>
    public class Ctrl_LevelOneScenes : BaseControl
    {
        public static Ctrl_LevelOneScenes Instance;
        public AudioClip acBackground;
        public Transform[] TraSpawnEnemyPos;
        public GameObject goWarriorPrefab_Green;
        public GameObject goWarriorPrefab_Red;


        void Awake()
        {
            Instance = this;
            PlayerExternalData.evePlayerExtenalData += LevelUp;
        }

        IEnumerator Start()
        {
            AudioManager.AudioBackgroundVolumn = 0.3f;
            AudioManager.AudioEffectVolumn = 1;
            AudioManager.PlayBackground(acBackground);

            StartCoroutine(SpawnEnemey(2));
            yield return new WaitForSeconds(2f);
            StartCoroutine(SpawnEnemey(2));
            yield return new WaitForSeconds(2f);
            StartCoroutine(SpawnEnemey(4));
            yield return new WaitForSeconds(4f);
            StartCoroutine(SpawnEnemey(2));
            yield return new WaitForSeconds(3f);
            StartCoroutine(SpawnEnemey(5));
            yield return new WaitForSeconds(5f);
            StartCoroutine(SpawnEnemey(3));
        }

        /// <summary>
        /// 敌人的出生(加入对象缓冲池技术)
        /// </summary>
        IEnumerator SpawnEnemey(int createEnemysNum)
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
            for (int i = 1; i <= createEnemysNum; i++)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);

                var enemySpawnPosition = GetRandomEnemySpawnPosition(TraSpawnEnemyPos);

                GameObject goEnemyClone = null;
                var randomNum = UnityHelper.GetInstance().GetRandomNumByRange(1, 2);
                if (randomNum == 1)
                {
                    goWarriorPrefab_Green.transform.position = enemySpawnPosition.transform.position;
                    goEnemyClone = PoolManager.PoolsArray["_Enemys"].GetGameObjectByPool(goWarriorPrefab_Green, goWarriorPrefab_Green.transform.position, Quaternion.identity);
                }
                else
                {
                    goWarriorPrefab_Red.transform.position = enemySpawnPosition.transform.position;
                    goEnemyClone = PoolManager.PoolsArray["_Enemys"].GetGameObjectByPool(goWarriorPrefab_Red, goWarriorPrefab_Red.transform.position, Quaternion.identity);
                }
                EnemySpawnParticalEffect(goEnemyClone);
            }
        }

        private void EnemySpawnParticalEffect(GameObject enemyWarror)
        {
            if (enemyWarror)
            {
                StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
                    "ParticleProps/EnemyDisplay", true,
                    enemyWarror.transform.position + transform.TransformDirection(0F, 2.1f, -1.1f), transform.rotation,
                    enemyWarror.transform, "EnemyDisplayEffect", 0));
            }
        }

        /// <summary>
        /// 敌人的出生（暂时不用）
        /// </summary>
        //IEnumerator SpawnEnemey(int createEnemysNum)
        //{
        //    yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
        //    for (int i = 1; i <= createEnemysNum; i++)
        //    {
        //        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
        //        //动态调用资源（敌人“预设”）
        //        //GameObject goEnemyClone = ResourcesMgr.GetInstance().LoadAsset("Prefabs/Enemys/skeleton_warrior_green", true);
        //        GameObject goEnemyClone = ResourcesMgr.GetInstance().LoadAsset(GetRandomEnemyType(), true);

        //        //定义克隆体随机出现位置
        //        Transform  TranEnemySpawnPosition=GetRandomEnemySpawnPosition();
        //        //克隆的位置
        //        goEnemyClone.transform.position = new Vector3(TranEnemySpawnPosition.transform.position.x, TranEnemySpawnPosition.transform.position.y, TranEnemySpawnPosition.transform.position.z);
        //        //克隆在层级视图中的位置
        //        goEnemyClone.transform.parent = TraSpawnEnemysPosition_1.transform.parent;
        //        //克隆敌人出现特效
        //        EnemySpawnParticalEffect(goEnemyClone);
        //    }
        //}


        public void EnterNextScenes()
        {
            base.EnterNextScenes(Scenes.MajorCity);
        }
    }
}
