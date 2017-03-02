/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层 - 父类控制层
 *
 *	Description:
 *		1.控制层脚本中公共的部分，在本脚本继承
 *
 *	Date:2017.02.20
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

namespace Control
{
    public class BaseControl : MonoBehaviour
    {

        /// <summary>
        /// 进入下一个场景
        /// </summary>
        /// <param name="sceneName">场景（枚举）名称</param>
        protected void EnterNextScene(EnumScenes sceneName)
        {
            GlobalParaMgr.NextSceneName = sceneName;
            Application.LoadLevel(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParaMgr.NextSceneName));
        }

        /// <summary>
        /// 公共方法：攻击敌人
        /// </summary>
        /// <param name="attackArea">攻击范围</param>
        /// <param name="attackPowerMultiple">攻击力度（倍率）</param>
        /// <param name="isDirection">攻击是否有方向性</param>
        protected void AttackEnemy(List<GameObject> lisEnemys, Transform traNearestEnemy, float attackArea, int attackPowerMultiple, bool isDirection = true)
        {
            //参数检查
            if (lisEnemys == null || lisEnemys.Count <= 0)
            {
                traNearestEnemy = null;
                return;
            }

            foreach (GameObject enemyItem in lisEnemys)
            {
                if (enemyItem && enemyItem.GetComponent<Ctrl_BaseEnemyProperty>())
                {
                    if (enemyItem.GetComponent<Ctrl_BaseEnemyProperty>().CurrentState != EnemyState.Dead)
                    {
                        //敌我距离
                        float floDistance = Vector3.Distance(this.gameObject.transform.position, enemyItem.transform.position);
                        //攻击具有方向性
                        if (isDirection)
                        {
                            //定义“主角与敌人” 的方向
                            Vector3 dir = (enemyItem.transform.position - this.gameObject.transform.position).normalized;
                            //定义“主角与敌人”的夹角(用向量的“点乘”进行计算)
                            float floDirection = Vector3.Dot(dir, this.gameObject.transform.forward);
                            //如果主角与敌人在同一个方向，且在有效攻击范围内，则对敌人做伤害处理
                            if (floDirection > 0 && floDistance <= attackArea)
                            {
                                enemyItem.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetCurrentATK() * attackPowerMultiple, SendMessageOptions.DontRequireReceiver);
                            }
                        }
                        //攻击无方向性
                        else
                        {
                            if (floDistance <= attackArea)
                            {
                                enemyItem.SendMessage("OnHurt", Ctrl_HeroProperty.Instance.GetCurrentATK() * attackPowerMultiple, SendMessageOptions.DontRequireReceiver);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 粒子特效加载公共方法
        /// </summary>
        /// <param name="internalTime"></param>
        /// <param name="strParticalEffectPath">粒子特效路径</param>
        /// <param name="IsUseCache">是否使用缓存</param>
        /// <param name="particalEffectPosition">粒子特效方位</param>
        /// <param name="tranParent"></param>
        /// <param name="strAudioEffect"></param>
        /// <param name="destroyTime"></param>
        /// <returns></returns>
        protected IEnumerator LoadParticalEffectPublicMethod(float internalTime, string strParticalEffectPath, bool IsUseCache,
                   Vector3 particalEffectPosition, Transform tranParent, string strAudioEffect = null, float destroyTime = 0)
        {
            //间隔时间
            yield return new WaitForSeconds(internalTime);
            //提取的粒子预设
            GameObject goParticalPrefab = ResourcesMgr.GetInstance().LoadAsset(strParticalEffectPath, IsUseCache);
            //粒子预设的位置            
            goParticalPrefab.transform.position = particalEffectPosition;
            //父子对象
            if (tranParent != null)
            {
                goParticalPrefab.transform.parent = tranParent;
            }
            //特效音频
            if (!string.IsNullOrEmpty(strAudioEffect))
            {
                AudioManager.PlayAudioEffectA(strAudioEffect);
            }
            //销毁时间
            if (destroyTime > 0)
            {
                Destroy(goParticalPrefab, destroyTime);
            }
        }
    }
}