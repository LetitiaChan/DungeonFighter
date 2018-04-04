using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Global;
using Kernal;

namespace Control
{
    public class BaseControl : MonoBehaviour
    {
        protected void EnterNextScenes(Global.Scenes scenesEnumName)
        {
            GlobalParaMgr.NextSceneName = scenesEnumName;
            SceneManager.LoadScene(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(Global.Scenes.LoadingScene));
        }

        protected IEnumerator LoadParticalEffect(float internalTime, string particalEffectPath, bool IsUseCatch,
                   Vector3 pos, Quaternion qua, Transform parent = null, string audioClipName = null, float destroyTime = 0)
        {
            yield return new WaitForSeconds(internalTime);

            GameObject goParticalPrefab = ResourcesMgr.GetInstance().LoadAsset(particalEffectPath, IsUseCatch);
            goParticalPrefab.transform.position = pos;
            goParticalPrefab.transform.rotation = qua;
            if (parent != null)
                goParticalPrefab.transform.parent = parent;
            if (!string.IsNullOrEmpty(audioClipName))
                AudioManager.PlayAudioEffectA(audioClipName);
            if (destroyTime > 0)
                Destroy(goParticalPrefab, destroyTime);
        }

        protected IEnumerator LoadParticalEffect_UsePool(float internalTime, GameObject goParticalEffect,
            Vector3 pos, Quaternion qua, Transform parent = null, string audioClipName = null)
        {
            yield return new WaitForSeconds(internalTime);

            var goClone = PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goParticalEffect, goParticalEffect.transform.position, Quaternion.identity);
            if (goClone)
            {
                goClone.transform.position = pos;
                goClone.transform.rotation = qua;
                if (parent)
                    goClone.transform.parent = parent;
                if (!string.IsNullOrEmpty(audioClipName))
                    AudioManager.PlayAudioEffectA(audioClipName);
            }
        }

        protected IEnumerator LoadParticalEffectInPool_MoveUpLabel(float internalTime, GameObject goParticalEffect,
            Vector3 pos, Quaternion qua, GameObject goTarget, int displayNum, Transform parent = null, string audioClipName = null)
        {
            yield return new WaitForSeconds(internalTime);

            var goClone = PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goParticalEffect, goParticalEffect.transform.position, Quaternion.identity);
            if (goClone)
            {
                goClone.GetComponent<MoveUpLabel>().SetTarget(goTarget);
                goClone.GetComponent<MoveUpLabel>().SetReduceHPNumber(displayNum);
                goClone.transform.position = pos;
                goClone.transform.rotation = qua;
                if (parent)
                    goClone.transform.parent = parent;
                if (!string.IsNullOrEmpty(audioClipName))
                    AudioManager.PlayAudioEffectB(audioClipName);
            }
        }

        protected IEnumerator LoadParticalEffectInPool_BossHurtLabel(float internalTime, GameObject goParticalEffect,
            Vector3 pos, Quaternion qua, Vector3 scale, int displayNum, Transform parent = null, string audioClipName = null)
        {
            yield return new WaitForSeconds(internalTime);

            var goClone = PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goParticalEffect, goParticalEffect.transform.position, Quaternion.identity);
            if (goClone)
            {
                goClone.GetComponent<BossHurtLabel>().SetReduceHPNumber(displayNum);

                if (parent)
                    goClone.transform.parent = parent;
                goClone.transform.localPosition = pos;
                goClone.transform.rotation = qua;
                goClone.transform.localScale = scale;
                if (!string.IsNullOrEmpty(audioClipName))
                    AudioManager.PlayAudioEffectB(audioClipName);
            }
        }

        /// <summary>
        /// 敌人的出生(加入对象缓冲池技术)
        /// </summary>
        protected IEnumerator SpawnEnemey(GameObject enemyPrefab, int createEnemysNum, Transform[] enemySpawnPos, bool isCreateHPBar = false)
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
            for (int i = 1; i <= createEnemysNum; i++)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);

                //定义克隆体随机出现位置
                var enemySpawnPosition = GetRandomEnemySpawnPosition(enemySpawnPos);

                enemyPrefab.transform.position = enemySpawnPosition.transform.position;
                var goEnemyClone = PoolManager.PoolsArray["_Enemys"].GetGameObjectByPool(enemyPrefab, enemyPrefab.transform.position, Quaternion.identity);

                //添加敌人血条
                if (isCreateHPBar)
                {
                    GameObject goHPBar = ResourcesMgr.GetInstance().LoadAsset("Prefabs/UI/EnemyHPBar", true);
                    goHPBar.transform.parent = GameObject.FindGameObjectWithTag(Tag.Tag_UIPlayerInfo).transform;
                    goHPBar.GetComponent<EnemyHPBar>().SetTargetEnemy(goEnemyClone);
                }
            }
        }

        public Transform GetRandomEnemySpawnPosition(Transform[] enemyCreatePos)
        {
            var randomNum = UnityHelper.GetInstance().GetRandomNumByRange(0, enemyCreatePos.Length - 1);
            return enemyCreatePos[randomNum];
        }


        protected void LevelUp(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("LevelUp"))
            {
                HeroLevelUp();
            }
        }

        private void HeroLevelUp()
        {
            string audioClip_LevelUp = "LevelUp";
            GameObject HeroLevelUp = ResourcesMgr.GetInstance().LoadAsset("ParticleProps/Hero_LvUp", true);
            AudioManager.PlayAudioEffectA(audioClip_LevelUp);
        }
    }
}
