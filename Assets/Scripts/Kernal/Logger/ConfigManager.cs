using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;

namespace Kernal
{
    public class ConfigManager : IConfigManger
    {
        private static Dictionary<string, string> _AppSetting;


        public ConfigManager(string logPath, string xmlRootNodeName)
        {
            _AppSetting = new Dictionary<string, string>();
            InitAndAnalysisXML(logPath, xmlRootNodeName);
        }

        private void InitAndAnalysisXML(string logPath, string xmlRootNodeName)
        {
            if (string.IsNullOrEmpty(logPath) || string.IsNullOrEmpty(xmlRootNodeName))
            {
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(logPath);

            using (StringReader strRdr = new StringReader(xmlDoc.InnerXml))
            {
                using (XmlReader rdr = XmlReader.Create(strRdr))
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine("rdr.NodeType = " + rdr.NodeType);
                        //XML读写器从指定根节点开始读写
                        if (rdr.IsStartElement() && rdr.LocalName == xmlRootNodeName)
                        {
                            using (XmlReader xmlReaderItem = rdr.ReadSubtree())
                            {
                                while (xmlReaderItem.Read())
                                {
                                    if (xmlReaderItem.NodeType == XmlNodeType.Element)
                                    {
                                        string strNode = xmlReaderItem.Name;
                                        xmlReaderItem.Read();
                                        if (xmlReaderItem.NodeType == XmlNodeType.Text)
                                        {
                                            _AppSetting[strNode] = xmlReaderItem.Value;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public Dictionary<string, string> AppSetting
        {
            get { return _AppSetting; }
        }

        public int GetAppSettingMaxNumber()
        {
            if (_AppSetting != null && _AppSetting.Count >= 1)
            {
                return _AppSetting.Count;
            }
            else
            {
                return 0;
            }
        }

    }
}
