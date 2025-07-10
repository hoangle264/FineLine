using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Demo_VisionMaster.ParseXML
{
    public class CMvdXmlParseTool_Blod
    {
        private List<CMvdNodeInteger> m_listIntValue = null;
        private List<CMvdNodeEnumeration> m_listEnumValue = null;
        private List<CMvdNodeFloat> m_listFloatValue = null;
        private List<CMvdNodeBoolean> m_listBooleanValue = null;

        public CMvdXmlParseTool_Blod(Byte[] bufXml, uint nXmlLen)
        {
            m_listIntValue = new List<CMvdNodeInteger>();
            m_listEnumValue = new List<CMvdNodeEnumeration>();
            m_listFloatValue = new List<CMvdNodeFloat>();
            m_listBooleanValue = new List<CMvdNodeBoolean>();
            UpdateXmlBuf(bufXml, nXmlLen);
        }

        public List<CMvdNodeInteger> IntValueList
        {
            get
            {
                return m_listIntValue;
            }
        }

        public List<CMvdNodeEnumeration> EnumValueList
        {
            get
            {
                return m_listEnumValue;
            }
        }

        public List<CMvdNodeFloat> FloatValueList
        {
            get
            {
                return m_listFloatValue;
            }
        }

        public List<CMvdNodeBoolean> BooleanValueList
        {
            get
            {
                return m_listBooleanValue;
            }
        }

        public void UpdateXmlBuf(Byte[] bufXml, uint nXmlLen)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;    //ºöÂÔÎÄµµÀïÃæµÄ×¢ÊÍ
            XmlReader reader = XmlReader.Create(new MemoryStream(bufXml, 0, (int)nXmlLen), settings);
            xmlDoc.Load(reader);
            reader.Close();
            m_listIntValue.Clear();
            m_listEnumValue.Clear();
            m_listFloatValue.Clear();
            m_listBooleanValue.Clear();
            XmlNode xnCategory = xmlDoc.SelectSingleNode("AlgorithmRoot").SelectSingleNode("Category");
            foreach (XmlNode xn in xnCategory)
            {
                switch (xn.Name)
                {
                    case "Integer":
                        {
                            CMvdNodeInteger NodeInt = new CMvdNodeInteger();
                            NodeInt.Name = ((XmlElement)xn).GetAttribute("Name");
                            NodeInt.Description = xn.SelectSingleNode("Description").InnerText;
                            NodeInt.DisplayName = xn.SelectSingleNode("DisplayName").InnerText;
                            NodeInt.Visibility = xn.SelectSingleNode("Visibility").InnerText;
                            NodeInt.AccessMode = xn.SelectSingleNode("AccessMode").InnerText;
                            NodeInt.AlgorithmIndex = IntStringToInt(xn.SelectSingleNode("AlgorithmIndex").InnerText);
                            NodeInt.CurValue = IntStringToInt(xn.SelectSingleNode("CurValue").InnerText);
                            NodeInt.DefaultValue = IntStringToInt(xn.SelectSingleNode("DefaultValue").InnerText);
                            NodeInt.MinValue = IntStringToInt(xn.SelectSingleNode("MinValue").InnerText);
                            NodeInt.MaxValue = IntStringToInt(xn.SelectSingleNode("MaxValue").InnerText);
                            NodeInt.IncValue = IntStringToInt(xn.SelectSingleNode("IncValue").InnerText);
                            m_listIntValue.Add(NodeInt);
                        }
                        break;
                    case "Enumeration":
                        {
                            CMvdNodeEnumeration NodeEnum = new CMvdNodeEnumeration();
                            NodeEnum.Name = ((XmlElement)xn).GetAttribute("Name");
                            NodeEnum.Description = xn.SelectSingleNode("Description").InnerText;
                            NodeEnum.DisplayName = xn.SelectSingleNode("DisplayName").InnerText;
                            NodeEnum.Visibility = xn.SelectSingleNode("Visibility").InnerText;
                            NodeEnum.AccessMode = xn.SelectSingleNode("AccessMode").InnerText;
                            NodeEnum.AlgorithmIndex = IntStringToInt(xn.SelectSingleNode("AlgorithmIndex").InnerText);
                            int nCurValue = IntStringToInt(xn.SelectSingleNode("CurValue").InnerText);
                            int nDefaultValue = IntStringToInt(xn.SelectSingleNode("DefaultValue").InnerText);
                            XmlNodeList xnlEnumEntry = xn.SelectNodes("EnumEntry");
                            List<CMvdNodeEnumEntry> clistNodeEnumEntry = new List<CMvdNodeEnumEntry>();
                            foreach (XmlNode xnEnumEntry in xnlEnumEntry)
                            {
                                CMvdNodeEnumEntry cNodeEnumEntry = new CMvdNodeEnumEntry();
                                cNodeEnumEntry.Name = ((XmlElement)xnEnumEntry).GetAttribute("Name");
                                cNodeEnumEntry.Description = xnEnumEntry.SelectSingleNode("Description").InnerText;
                                cNodeEnumEntry.DisplayName = xnEnumEntry.SelectSingleNode("DisplayName").InnerText;
                                cNodeEnumEntry.Value = IntStringToInt(xnEnumEntry.SelectSingleNode("Value").InnerText);
                                clistNodeEnumEntry.Add(cNodeEnumEntry);
                                if (nCurValue == cNodeEnumEntry.Value)
                                {
                                    NodeEnum.CurValue = cNodeEnumEntry;
                                }
                                if (nDefaultValue == cNodeEnumEntry.Value)
                                {
                                    NodeEnum.DefaultValue = cNodeEnumEntry;
                                }
                            }
                            NodeEnum.EnumRange = clistNodeEnumEntry;
                            m_listEnumValue.Add(NodeEnum);
                        }
                        break;
                    case "Float":
                        {
                            CMvdNodeFloat NodeFloat = new CMvdNodeFloat();
                            NodeFloat.Name = ((XmlElement)xn).GetAttribute("Name");
                            NodeFloat.Description = xn.SelectSingleNode("Description").InnerText;
                            NodeFloat.DisplayName = xn.SelectSingleNode("DisplayName").InnerText;
                            NodeFloat.Visibility = xn.SelectSingleNode("Visibility").InnerText;
                            NodeFloat.AccessMode = xn.SelectSingleNode("AccessMode").InnerText;
                            NodeFloat.AlgorithmIndex = IntStringToInt(xn.SelectSingleNode("AlgorithmIndex").InnerText);
                            NodeFloat.CurValue = System.Convert.ToSingle(xn.SelectSingleNode("CurValue").InnerText);
                            NodeFloat.DefaultValue = System.Convert.ToSingle(xn.SelectSingleNode("DefaultValue").InnerText);
                            NodeFloat.MinValue = System.Convert.ToSingle(xn.SelectSingleNode("MinValue").InnerText);
                            NodeFloat.MaxValue = System.Convert.ToSingle(xn.SelectSingleNode("MaxValue").InnerText);
                            NodeFloat.IncValue = System.Convert.ToSingle(xn.SelectSingleNode("IncValue").InnerText);
                            m_listFloatValue.Add(NodeFloat);
                        }
                        break;
                    case "Boolean":
                        {
                            CMvdNodeBoolean NodeBoolean = new CMvdNodeBoolean();
                            NodeBoolean.Name = ((XmlElement)xn).GetAttribute("Name");
                            NodeBoolean.Description = xn.SelectSingleNode("Description").InnerText;
                            NodeBoolean.DisplayName = xn.SelectSingleNode("DisplayName").InnerText;
                            NodeBoolean.Visibility = xn.SelectSingleNode("Visibility").InnerText;
                            NodeBoolean.AccessMode = xn.SelectSingleNode("AccessMode").InnerText;
                            NodeBoolean.AlgorithmIndex = IntStringToInt(xn.SelectSingleNode("AlgorithmIndex").InnerText);
                            NodeBoolean.CurValue = xn.SelectSingleNode("CurValue").InnerText.Equals("true", StringComparison.OrdinalIgnoreCase) == true ? true : false;
                            NodeBoolean.DefaultValue = xn.SelectSingleNode("DefaultValue").InnerText.Equals("true", StringComparison.OrdinalIgnoreCase) == true ? true : false;
                            m_listBooleanValue.Add(NodeBoolean);
                        }
                        break;
                    default:
                        {
                            throw new VisionDesigner.MvdException(VisionDesigner.MVD_MODULE_TYPE.MVD_MODUL_APP
                                                                , VisionDesigner.MVD_ERROR_CODE.MVD_E_SUPPORT
                                                                , "Algorithm type not support!");
                        }
                }
            }
        }

        public void ClearXmlBuf()
        {
            m_listIntValue.Clear();
            m_listEnumValue.Clear();
            m_listFloatValue.Clear();
            m_listBooleanValue.Clear();
        }

        private int IntStringToInt(string strIntString)
        {
            if (strIntString.Contains("0x") || strIntString.Contains("0X"))
            {
                return Convert.ToInt32(strIntString, 16);
            }
            else
            {
                return Convert.ToInt32(strIntString, 10);
            }
        }
    }
}
