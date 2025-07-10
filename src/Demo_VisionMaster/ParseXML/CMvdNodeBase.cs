using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_VisionMaster.ParseXML
{
    public class CMvdNodeBase
    {
        string m_strName = string.Empty;
        string m_strDescription = string.Empty;
        string m_strDisplayName = string.Empty;
        string m_strVisibility = string.Empty;
        string m_strAccessMode = string.Empty;
        int m_nAlgorithmIndex = 0;

        public string Name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        public string Description
        {
            get
            {
                return m_strDescription;
            }
            set
            {
                m_strDescription = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return m_strDisplayName;
            }
            set
            {
                m_strDisplayName = value;
            }
        }

        public string Visibility
        {
            get
            {
                return m_strVisibility;
            }
            set
            {
                m_strVisibility = value;
            }
        }

        public string AccessMode
        {
            get
            {
                return m_strAccessMode;
            }
            set
            {
                m_strAccessMode = value;
            }
        }

        public int AlgorithmIndex
        {
            get
            {
                return m_nAlgorithmIndex;
            }
            set
            {
                m_nAlgorithmIndex = value;
            }
        }
    }
    public class CMvdNodeInteger : CMvdNodeBase
    {
        int m_nCurValue = 0;
        int m_nDefaultValue = 0;
        int m_nMinValue = 0;
        int m_nMaxValue = 0;
        int m_nIncValue = 0;

        public int CurValue
        {
            get
            {
                return m_nCurValue;
            }
            set
            {
                m_nCurValue = value;
            }
        }

        public int DefaultValue
        {
            get
            {
                return m_nDefaultValue;
            }
            set
            {
                m_nDefaultValue = value;
            }
        }

        public int MinValue
        {
            get
            {
                return m_nMinValue;
            }
            set
            {
                m_nMinValue = value;
            }
        }

        public int MaxValue
        {
            get
            {
                return m_nMaxValue;
            }
            set
            {
                m_nMaxValue = value;
            }
        }

        public int IncValue
        {
            get
            {
                return m_nIncValue;
            }
            set
            {
                m_nIncValue = value;
            }
        }
    }

    public class CMvdNodeEnumEntry
    {
        string m_strName = string.Empty;
        string m_strDescription = string.Empty;
        string m_strDisplayName = string.Empty;
        bool m_bIsImplemented = false;
        int m_nValue = 0;

        public string Name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        public string Description
        {
            get
            {
                return m_strDescription;
            }
            set
            {
                m_strDescription = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return m_strDisplayName;
            }
            set
            {
                m_strDisplayName = value;
            }
        }

        public bool IsImplemented
        {
            get
            {
                return m_bIsImplemented;
            }
            set
            {
                m_bIsImplemented = value;
            }
        }

        public int Value
        {
            get
            {
                return m_nValue;
            }
            set
            {
                m_nValue = value;
            }
        }
    }

    public class CMvdNodeFloat : CMvdNodeBase
    {
        float m_fCurValue = 0;
        float m_fDefaultValue = 0;
        float m_fMinValue = 0;
        float m_fMaxValue = 0;
        float m_fIncValue = 0;

        public float CurValue
        {
            get
            {
                return m_fCurValue;
            }
            set
            {
                m_fCurValue = value;
            }
        }

        public float DefaultValue
        {
            get
            {
                return m_fDefaultValue;
            }
            set
            {
                m_fDefaultValue = value;
            }
        }

        public float MinValue
        {
            get
            {
                return m_fMinValue;
            }
            set
            {
                m_fMinValue = value;
            }
        }

        public float MaxValue
        {
            get
            {
                return m_fMaxValue;
            }
            set
            {
                m_fMaxValue = value;
            }
        }

        public float IncValue
        {
            get
            {
                return m_fIncValue;
            }
            set
            {
                m_fIncValue = value;
            }
        }
    }

    public class CMvdNodeBoolean : CMvdNodeBase
    {
        bool m_bDefaultValue = false;
        bool m_bCurValue = false;

        public bool CurValue
        {
            get
            {
                return m_bCurValue;
            }
            set
            {
                m_bCurValue = value;
            }
        }

        public bool DefaultValue
        {
            get
            {
                return m_bDefaultValue;
            }
            set
            {
                m_bDefaultValue = value;
            }
        }
    }
    public class CMvdNodeEnumeration : CMvdNodeBase
    {
        CMvdNodeEnumEntry m_eCurValue = new CMvdNodeEnumEntry();
        CMvdNodeEnumEntry m_eDefaultValue = new CMvdNodeEnumEntry();
        List<CMvdNodeEnumEntry> m_listEnumEntry = new List<CMvdNodeEnumEntry>();

        public CMvdNodeEnumEntry CurValue
        {
            get
            {
                return m_eCurValue;
            }
            set
            {
                m_eCurValue = value;
            }
        }

        public CMvdNodeEnumEntry DefaultValue
        {
            get
            {
                return m_eDefaultValue;
            }
            set
            {
                m_eDefaultValue = value;
            }
        }

        public List<CMvdNodeEnumEntry> EnumRange
        {
            get
            {
                return m_listEnumEntry;
            }
            set
            {
                m_listEnumEntry = value;
            }
        }
    }
}
