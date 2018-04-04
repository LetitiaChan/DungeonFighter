using UnityEngine;
using Global;

public class View_TestDialog : MonoBehaviour
{
    private int _curSectionIndex = 0;


    public void ShowNextDialog()
    {
        ++_curSectionIndex;
        DialogUIMgr._instance.DisplayNextDialog(DialogType.SingleDialog, _curSectionIndex);
    }


    public void test()
    {
        Vector3 mousePos = Vector3.up;
        //2D转向的Demo 
        Vector3 targetDir = mousePos - transform.position;//计算到目标点的方向
        float dotValue = Vector3.Dot(transform.right, targetDir.normalized);
        float angle = Mathf.Acos(dotValue) * Mathf.Rad2Deg;//计算夹角
        if (!float.IsNaN(angle))
        {
            transform.RotateAround(transform.position, Vector3.forward, angle);//转向目标点
        }

    }

}
