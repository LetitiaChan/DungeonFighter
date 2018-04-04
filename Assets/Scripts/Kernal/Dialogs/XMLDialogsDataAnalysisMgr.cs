using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.IO;

namespace Kernal
{
    public class XMLDialogsDataAnalysisMgr : MonoBehaviour
    {
        private const float TIMES_DELAY = 0.2f;
        private const string XML_ATTRIBUTE_1 = "DialogSecNum";
        private const string XML_ATTRIBUTE_2 = "DialogSecName";
        private const string XML_ATTRIBUTE_3 = "SectionIndex";
        private const string XML_ATTRIBUTE_4 = "DialogSide";
        private const string XML_ATTRIBUTE_5 = "DialogPerson";
        private const string XML_ATTRIBUTE_6 = "DialogContent";
        private const string XML_ATTRIBUTE_7 = "DialogButton";

        private const string XML_DEFINATION_HERO = "Hero";
        private const string XML_DEFINATION_NPC = "NPC";

        private static XMLDialogsDataAnalysisMgr _Instance;
        private List<DialogDataFormat> _DialogData;
        private string _XMLPath;
        private string _XMLRootNodeName;


        private XMLDialogsDataAnalysisMgr()
        {
            _DialogData = new List<DialogDataFormat>();
        }

        public static XMLDialogsDataAnalysisMgr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_XMLD").AddComponent<XMLDialogsDataAnalysisMgr>();
            }
            return _Instance;
        }

        public void SetXMLPathAndRootNodeName(string xmlPath, string xmlRootNodeName)
        {
            if (!string.IsNullOrEmpty(xmlPath) && !string.IsNullOrEmpty(xmlRootNodeName))
            {
                _XMLPath = xmlPath;
                _XMLRootNodeName = xmlRootNodeName;
            }
        }

        public List<DialogDataFormat> GetAllXMLDatas()
        {
            if (_DialogData != null && _DialogData.Count >= 1)
            {
                return _DialogData;
            }
            else
            {
                return null;
            }
        }

        IEnumerator Start()
        {
            DontDestroyOnLoad(gameObject);

            yield return new WaitForSeconds(TIMES_DELAY);//等待关于XML路径与XML根节点名称，进行附值。
            if (!string.IsNullOrEmpty(_XMLPath) && !string.IsNullOrEmpty(_XMLRootNodeName))
            {
                StartCoroutine("ReadXMLConfigByWWW");
            }
            else
            {
                Debug.LogError(GetType() + "/Start()/_StrXMLPath or _StrXMLRootNodeName is null!,please check!");
            }
        }

        IEnumerator ReadXMLConfigByWWW()
        {
            WWW www = new WWW(_XMLPath);

            while (!www.isDone)
            {
                yield return www;
            }
            if (www.isDone)
                InitXMLConfig(www, _XMLRootNodeName);
        }

        private void InitXMLConfig(WWW www, string rootNodeName)
        {
            Debug.LogWarning(www.text + "," + _XMLPath);
            if (_DialogData == null || string.IsNullOrEmpty(www.text))
            {
                Debug.LogError(GetType() + "/InitXMLConfig()/_DialogDataArray==null or rootNodeName is null!,please check!");
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(www.text);//发现这种方式，发布到Android 手机端，不能正确的输出中文。

            /* 以下4行代码，来代替上面注视掉的内容，解决正确在发布手机端解析输出中文问题 */
            StringReader stringReader = new StringReader(www.text);
            stringReader.Read();
            XmlReader reader = XmlReader.Create(stringReader);
            xmlDoc.LoadXml(stringReader.ReadToEnd());

            XmlNodeList nodes = xmlDoc.SelectSingleNode(_XMLRootNodeName).ChildNodes;
            foreach (XmlElement xe in nodes)
            {
                DialogDataFormat data = new DialogDataFormat();
                data.DialogSecNum = Convert.ToInt32(xe.GetAttribute(XML_ATTRIBUTE_1));
                data.DialogSecName = xe.GetAttribute(XML_ATTRIBUTE_2);
                data.SectionIndex = Convert.ToInt32(xe.GetAttribute(XML_ATTRIBUTE_3));
                string attr4 = xe.GetAttribute(XML_ATTRIBUTE_4);
                if (attr4 == XML_DEFINATION_HERO)
                    data.DialogSide = DialogSide.HeroSide;
                else if (attr4 == XML_DEFINATION_NPC)
                    data.DialogSide = DialogSide.NPCSide;
                else
                    data.DialogSide = DialogSide.None;
                data.DialogPerson = xe.GetAttribute(XML_ATTRIBUTE_5);
                data.DialogContent = xe.GetAttribute(XML_ATTRIBUTE_6);
                var strAttr = xe.GetAttribute(XML_ATTRIBUTE_7);
                bool temp_attr = false;
                bool.TryParse(strAttr, out temp_attr);
                data.DialogButton = temp_attr;
                _DialogData.Add(data);
            }
        }
    }
}
