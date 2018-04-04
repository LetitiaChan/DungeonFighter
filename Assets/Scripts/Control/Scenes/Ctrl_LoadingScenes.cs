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
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
            StartCoroutine("ScenesPreProgressing");
            StartCoroutine("HandleGC");
        }

        IEnumerator ScenesPreProgressing()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
            switch (GlobalParaMgr.NextSceneName)
            {
                case Scenes.TestScene:
                    break;
                case Scenes.StartScene:
                    break;
                case Scenes.LogonScene:
                    break;
                case Scenes.LevelOne:
                    //DebugConsole.Log("XML解析 - 1111111111111111111111111");
                    StartCoroutine("ScenesPreProgressing_LevelOne");
                    break;
                case Scenes.LevelTwo:
                    break;
                case Scenes.BaseScene:
                    break;
                default:
                    break;
            }
        }

        IEnumerator ScenesPreProgressing_LevelOne()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
            //DebugConsole.Log("XML解析 - 2222222222222222222222222");

            List<DialogDataFormat> dialogsDataArray = XMLDialogsDataAnalysisMgr.GetInstance().GetAllXMLDatas();
            //DebugConsole.Log("XML解析 - 3333333333333333333333333    " + "dialogsDataArray 存在？ " + (dialogsDataArray == null ? "False" : "True"));
            //DebugConsole.Log(GetType() + " 对话数据量：" + dialogsDataArray.Count);
            bool result = DialogDataMgr.GetInstance().LoadAllDialogData(dialogsDataArray);
            if (!result)
            {
                DebugConsole.Log(GetType() + "/ScenesPreProgressing_LevelOne()/‘对话数据管理器’加载数据失败");
            }
        }

        IEnumerator HandleGC()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
            Resources.UnloadUnusedAssets();
            System.GC.Collect();  //强制垃圾收集
        }
    }
}


