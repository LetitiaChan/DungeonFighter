using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;

namespace View
{
    public class GuideMgr : MonoBehaviour
    {

        private List<IGuideTrigger> _guideTrigger = new List<IGuideTrigger>();

        IEnumerator Start()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT2);

            IGuideTrigger iTri_1 = TriggerDialogs.Instance;
            IGuideTrigger iTri_2 = TriggerOperET.Instance;
            IGuideTrigger iTri_3 = TriggerOperVirtualKey.Instance;
            _guideTrigger.Add(iTri_1);
            _guideTrigger.Add(iTri_2);
            _guideTrigger.Add(iTri_3);
            StartCoroutine("CheckGuideState");
        }

        IEnumerator CheckGuideState()
        {
            //Log.Write(GetType() + "/CheckGuideState()");
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT2);
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
                for (int i = 0; i < _guideTrigger.Count; i++)
                {
                    IGuideTrigger iTri = _guideTrigger[i];
                    if (iTri.CheckCondition())
                    {
                        if (iTri.RunOperation())
                        {
                            //Log.Write(GetType() + "/CheckGuideState()/id = " + i + " done,to be removed");
                            _guideTrigger.Remove(iTri);
                        }
                    }
                }
            }
        }
    }
}
