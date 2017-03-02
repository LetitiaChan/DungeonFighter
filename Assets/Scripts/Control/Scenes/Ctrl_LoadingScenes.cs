/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层: 场景异步加载，后台逻辑处理 
 *
 *	Description:
 *		1.
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
using System.Collections;
using System.Collections.Generic;

using Kernal;
using Global;

namespace Control
{
    public class Ctrl_LoadingScenes : BaseControl
    {

        IEnumerator Start()
        {
            //Log.Write(GetType() + "/Start()");
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
            //关卡预处理逻辑
            StartCoroutine("ScenesPreProgressing");
            //垃圾收集
            StartCoroutine("HandleGC");
        }

        //关卡预处理逻辑
        IEnumerator ScenesPreProgressing()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
            switch (GlobalParaMgr.NextSceneName)
            {
                case EnumScenes.ScenesStart:
                    break;
                case EnumScenes.ScenesLogin:
                    break;
                case EnumScenes.ScenesLoading:
                    break;
                case EnumScenes.ScenesLevelOne:
                    //StartCoroutine("ScenesPreProgressing_LevelOne");
                    break;
                case EnumScenes.ScenesLevelTwo:
                    break;
                default:
                    break;
            }
        }

        ///// <summary>
        ///// 预处理_第一关卡
        ///// </summary>
        ///// <returns></returns>
        //IEnumerator ScenesPreProgressing_LevelOne()
        //{
        //    yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
        //    //参数赋值。
        //    XMLDialogsDataAnalysisMgr.GetInstance().SetXMLPathAndRootNodeName(KernalParameter.GetDialogConfigXMLPath(), KernalParameter.GetDialogConfigXMLRootNodeName());
        //    yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT3);
        //    //得到XML中所有的数据
        //    List<DialogDataFormat> liDialogsDataArray = XMLDialogsDataAnalysisMgr.GetInstance().GetAllxmlDataArray();
        //    //测试给“对话数据管理器”加载数据
        //    bool booResult = DialogDataMgr.GetInstance().LoadAllDialgData(liDialogsDataArray);
        //    if (!booResult)
        //    {
        //        Log.Write(GetType() + "/Start()/‘对话数据管理器’加载数据失败", Log.Level.High);
        //    }
        //}

        /// <summary>
        ///垃圾资源收集
        /// </summary>
        /// <returns></returns>
        IEnumerator HandleGC()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
            //卸载无用的资源
            Resources.UnloadUnusedAssets();
            //强制垃圾收集
            System.GC.Collect();
        }

    }//Class_end
}


