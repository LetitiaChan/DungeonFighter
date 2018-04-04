using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Global;

namespace View
{
    public class View_LoadingScenes : MonoBehaviour
    {
        public Slider loadingBar;
        private float _loadingBarValue = 0;
        private AsyncOperation _asyOper;
        private float _speed = 0.01f;

        IEnumerator Start()
        {
            #region 测试XML解析
            //Log.ClearLogFileAndBufferAllDate();
            //XMLDialogsDataAnalysisMgr.GetInstance().SetXMLPathAndRootNodeName(KernalParameter.GetDialogConfigXMLPath(), KernalParameter.GetDialogConfigXMLRootNodeName());
            //yield return new WaitForSeconds(0.4F);
            //List<DialogDataFormat> liDialogsDataArray = XMLDialogsDataAnalysisMgr.GetInstance().GetAllXMLDatas();
            //bool booResult = DialogDataMgr.GetInstance().LoadAllDialogData(liDialogsDataArray);
            //if (!booResult)
            //{
            //    Log.Write(GetType() + "/Start()/‘对话数据管理器’加载数据失败");
            //}
            //GlobalParaMgr.NextSceneName = Scenes.MajorCity;  //调试进入指定的关卡（第1关卡）
            //yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            #endregion

            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            StartCoroutine("LoadingScenesProgress");
        }

        IEnumerator LoadingScenesProgress()
        {
            _asyOper = SceneManager.LoadSceneAsync(ConvertEnumToStr.GetInstance().GetStrByEnumScenes(GlobalParaMgr.NextSceneName));
            //_loadingBarValue = _asyOper.progress;
            //_asyOper.allowSceneActivation = false;
            yield return _asyOper;
        }

        void Update()
        {
            if (_loadingBarValue < 0.98f)
            {
                _loadingBarValue += _speed;
                loadingBar.value = _loadingBarValue;
            }
            //print(_loadingBarValue + ", real = " + (_asyOper != null ? _asyOper.progress : 0));
        }
    }
}
