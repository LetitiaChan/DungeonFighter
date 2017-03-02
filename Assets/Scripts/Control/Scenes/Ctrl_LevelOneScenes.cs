/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：第一关卡场景控制处理
 *
 *	Description:
 *		1.负责第一关卡敌人的动态加载。
 *		1.敌人的“多出生点”设计
 *
 *	Date:
 *
 *	Version:
 *		1.0
 *
 *	Author:chenx
 *
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Global;
using Kernal;
using Model;

namespace Control
{
    public class Ctrl_LevelOneScenes : BaseControl
    {
        public AudioClip AcBackground;                                         //背景音乐音频剪辑
        public Transform TraSpawnEnemysPosition_1;                             //敌人出现的位置
        public Transform TraSpawnEnemysPosition_2;
        public Transform TraSpawnEnemysPosition_3;
        public Transform TraSpawnEnemysPosition_4;
        public Transform TraSpawnEnemysPosition_5;
        public Transform TraSpawnEnemysPosition_6;
        public Transform TraSpawnEnemysPosition_7;
        public Transform TraSpawnEnemysPosition_8;
        public Transform TraSpawnEnemysPosition_9;
        public Transform TraSpawnEnemysPosition_10;

        public bool IsSingleTime = true;                                       //单次开关
        //对象缓冲池，复杂对象（敌人预设）
        public GameObject goWarriorPrefab_Green;                             //简单敌人



        void Awake()
        {
            //事件注册（等级提升[升级]）
            PlayerExternalData.evePlayerExtenalData += LevelUp;
        }

        IEnumerator Start()
        {
            //背景音乐播放
            AudioManager.SetAudioBackgroundVolumns(0.3F);
            AudioManager.SetAudioEffectVolumns(1F);
            AudioManager.PlayBackground(AcBackground);
            //敌人的动态加载(按照时间进行加载)
            StartCoroutine(SpawnEnemey(2));
            yield return new WaitForSeconds(3F);
            StartCoroutine(SpawnEnemey(2));
            yield return new WaitForSeconds(2F);
            StartCoroutine(SpawnEnemey(4));
            yield return new WaitForSeconds(5F);
            StartCoroutine(SpawnEnemey(2));
            yield return new WaitForSeconds(3F);
            StartCoroutine(SpawnEnemey(5));
            yield return new WaitForSeconds(6F);
            StartCoroutine(SpawnEnemey(3));
        }

        ///// <summary>
        ///// 敌人的出生（暂时不用）
        ///// </summary>
        ///// <param name="createEnemysNum">敌人数量</param>
        ///// <returns></returns>
        //IEnumerator SpawnEnemey(int createEnemysNum)
        //{
        //    yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
        //    for (int i = 1; i <= createEnemysNum; i++)
        //    {
        //        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
        //        //动态调用资源（敌人“预设”）
        //        GameObject goEnemyClone = ResourcesMgr.GetInstance().LoadAsset(GetRandomEnemyType(), true);

        //        //定义克隆体随机出现位置
        //        Transform TranEnemySpawnPosition = GetRandomEnemySpawnPosition();
        //        //克隆的位置
        //        goEnemyClone.transform.position = new Vector3(TranEnemySpawnPosition.transform.position.x, TranEnemySpawnPosition.transform.position.y, TranEnemySpawnPosition.transform.position.z);
        //        //克隆在层级视图中的位置
        //        goEnemyClone.transform.parent = TraSpawnEnemysPosition_1.transform.parent;
        //        //克隆敌人出现特效
        //        EnemySpawnParticalEffect(goEnemyClone);
        //    }
        //}

        /// <summary>
        /// 敌人的出生(加入对象缓冲池技术)
        /// </summary>
        /// <param name="createEnemysNum">敌人数量</param>
        /// <returns></returns>
        IEnumerator SpawnEnemey(int createEnemysNum)
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
            for (int i = 1; i <= createEnemysNum; i++)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);

                //定义克隆体随机出现位置
                Transform TranEnemySpawnPosition = GetRandomEnemySpawnPosition();
                //克隆的位置
                goWarriorPrefab_Green.transform.position = new Vector3(TranEnemySpawnPosition.transform.position.x, TranEnemySpawnPosition.transform.position.y, TranEnemySpawnPosition.transform.position.z);
                //在“对象缓冲池”中激活指定的对象
                PoolManager.PoolsArray["_Enemys"].GetGameObjectByPool(goWarriorPrefab_Green, goWarriorPrefab_Green.transform.position, Quaternion.identity);

                //克隆敌人出现特效
                //EnemySpawnParticalEffect(goEnemyClone);
            }
        }

        /// <summary>
        /// 得到敌人的多出生点
        /// </summary>
        /// <returns></returns>
        public Transform GetRandomEnemySpawnPosition()
        {
            Transform TranReturnEnemyPosition;                                 //返回的敌人位置

            int intRandomNum = UnityHelper.GetInstance().GetRandomNum(1, 10);
            if (intRandomNum == 1)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_1;
            }
            else if (intRandomNum == 2)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_2;
            }
            else if (intRandomNum == 3)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_3;
            }
            else if (intRandomNum == 4)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_4;
            }
            else if (intRandomNum == 5)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_5;
            }
            else if (intRandomNum == 6)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_6;
            }
            else if (intRandomNum == 7)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_7;
            }
            else if (intRandomNum == 8)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_8;
            }
            else if (intRandomNum == 9)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_9;
            }
            else if (intRandomNum == 10)
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_10;
            }
            else
            {
                TranReturnEnemyPosition = TraSpawnEnemysPosition_1;
            }

            return TranReturnEnemyPosition;
        }

        /// <summary>
        /// 得到敌人的种类（预设）路径
        /// </summary>
        /// <returns></returns>
        public string GetRandomEnemyType()
        {
            string strEnemyTypePath = "Prefabs/Enemys/skeleton_king_green";  //返回敌人种类路径

            int intRandomNum = UnityHelper.GetInstance().GetRandomNum(1, 2);
            if (intRandomNum == 1)
            {
                strEnemyTypePath = "Prefabs/Enemys/skeleton_king_green";
            }
            else if (intRandomNum == 2)
            {
                strEnemyTypePath = "Prefabs/Enemys/skeleton_king_purple";
            }
            else
            {
                strEnemyTypePath = "Prefabs/Enemys/skeleton_king_green";
            }

            return strEnemyTypePath;
        }

        /// <summary>
        /// 敌人出现粒子特效
        /// </summary>
        /// <param name="EnemyWarror"></param>
        private void EnemySpawnParticalEffect(GameObject EnemyWarror)
        {
            StartCoroutine(LoadParticalEffectPublicMethod(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/EnemyDisplay", true, EnemyWarror.transform.position + transform.TransformDirection(0F, 2F, 0F),
                EnemyWarror.transform, "EnemyDisplayEffect", 0));
        }

        /// <summary>
        /// 主角升级
        /// </summary>
        /// <param name="kv"></param>
        private void LevelUp(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Level"))
            {
                if (IsSingleTime)
                {
                    IsSingleTime = false;
                }
                else
                {
                    HeroLevelUp();
                }
            }
        }

        //主角升级
        private void HeroLevelUp()
        {
            //提取升级粒子预设
            GameObject HeroLevelUp = ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Hero_LvUp", true);
            //音效
            AudioManager.PlayAudioEffectA("LevelUp");
        }


    }
}
