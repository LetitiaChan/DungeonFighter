using UnityEngine;

namespace Kernal
{
    public class KernalParameter
    {
        //暂时不用
#if UNITY_STANDALONE_WIN
        //系统配置信息_日志路径
        internal static readonly string SystemConfigInfo_LogPath = "file://" + Application.dataPath + "/StreamingAssets/SystemConfigInfo.xml";
        //系统配置信息_日志根节点名称
        internal static readonly string SystemConfigInfo_LogRootNodeName = "SystemConfigInfo";

        //对话系统XML路径
        internal static readonly string DialogsXMLConfig_XmlPath = "file://" + Application.dataPath + "/StreamingAssets/SystemDialogsInfo.xml";
        //对话系统XML根节点名称
        internal static readonly string DialogsXMLConfig_XmlRootNodeName = "Dialogs_CN";

#elif UNITY_ANDROID
        //系统配置信息_日志路径
        internal static readonly string SystemConfigInfo_LogPath = Application.dataPath + "!/Assets/SystemConfigInfo.xml";
        //系统配置信息_日志根节点名称
        internal static readonly string SystemConfigInfo_LogRootNodeName = "SystemConfigInfo";

        //对话系统XML路径
        internal static readonly string DialogsXMLConfig_XmlPath = Application.dataPath + "!/Assets/SystemDialogsInfo.xml";
        //对话系统XML根节点名称
        internal static readonly string DialogsXMLConfig_XmlRootNodeName = "Dialogs_CN";
#elif UNITY_IPHONE
        //系统配置信息_日志路径
        internal static readonly string SystemConfigInfo_LogPath = Application.dataPath + "/Raw/SystemConfigInfo.xml";
        //系统配置信息_日志根节点名称
        internal static readonly string SystemConfigInfo_LogRootNodeName = "SystemConfigInfo";

        //对话系统XML路径
        internal static readonly string DialogsXMLConfig_XmlPath = Application.dataPath + "/Raw/SystemDialogsInfo.xml";
        //对话系统XML根节点名称
        internal static readonly string DialogsXMLConfig_XmlRootNodeName = "Dialogs_CN";
#endif

        public static string GetLogPath()
        {
            string logPath = null;
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                logPath = Application.streamingAssetsPath + "/SystemConfigInfo.xml";
            }
            else
            {
                logPath = "file://" + Application.streamingAssetsPath + "/SystemConfigInfo.xml";
            }
            return logPath;
        }

        public static string GetLogRootNodeName()
        {
            string XMLRootNodeName = null;

            XMLRootNodeName = "SystemConfigInfo";
            return XMLRootNodeName;
        }

        public static string GetDialogConfigXMLPath()
        {
            string dialogConfigXMLPath = null;
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                dialogConfigXMLPath = Application.streamingAssetsPath + "/SystemDialogsInfo.xml";
            }
            else
            {
                dialogConfigXMLPath = "file://" + Application.streamingAssetsPath + "/SystemDialogsInfo.xml";
            }
            return dialogConfigXMLPath;
        }

        public static string GetDialogConfigXMLRootNodeName()
        {
            string dialogRootNodeName = null;

            dialogRootNodeName = "Dialogs_CN";
            return dialogRootNodeName;
        }

    }
}
