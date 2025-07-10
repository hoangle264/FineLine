using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionDesigner;
using VisionDesigner.ImageEnhance;
using VisionDesigner.PreproMask;

namespace Demo_VisionMaster.Services
{
    public class EnhancementService
    {
        public CMvdImage Enchancement_Image { get; set; }
        public string filePath { get; set; }
        private const float MVD_FLOAT_EPS = 0.0001f; // 浮点计算误差
        private CImageEnhanceTool m_stImageEnhanceToolObj = null;
        private CPreproMaskTool m_stPreproMaskToolObj = null;
        private CMvdImage m_stInputImage = null;
        private CMvdShape m_stROIShape = null;
        List<CMvdShape> m_lMaskShapes = new List<CMvdShape>();
        public EnhancementService()
        {
           

        }
        public void Init()
        {
            try
            {
                m_stImageEnhanceToolObj = new CImageEnhanceTool();
                m_stPreproMaskToolObj = new CPreproMaskTool();
                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    m_stImageEnhanceToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        m_stImageEnhanceToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }

            }
            catch (MvdException ex)
            {
                Console.WriteLine("Fail to initialize the tool. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Fail with error " + ex.Message + "\r\n");
            }
        }

        public void Import(string FileXml)
        {
            FileStream fileStr = null;
            try
            {
                if (File.Exists(FileXml))
                {
                    fileStr = new FileStream(FileXml, FileMode.Open, FileAccess.Read);
                    Int64 nFileLen = fileStr.Length;
                    byte[] fileBytes = new byte[nFileLen];
                    uint nReadLen = Convert.ToUInt32(fileStr.Read(fileBytes, 0, fileBytes.Length));
                    fileStr.Close();
                    fileStr.Dispose();
                    m_stImageEnhanceToolObj.LoadConfiguration(fileBytes, nReadLen);
                }
            }
            catch (MvdException ex)
            {
                Console.WriteLine("Fail to import xml file. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Fail to import xml file with error ' " + ex.Message + " '\r\n");
            }
        }

        public CMvdImage Run(string ImageFile)
        {
            try
            {
                m_stInputImage = new CMvdImage();
                m_stInputImage.InitImage(ImageFile);
                if ((null == m_stImageEnhanceToolObj) || (null == m_stInputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_TOOL, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }
                m_stImageEnhanceToolObj.InputImage = m_stInputImage;

                bool bUseMaskFlag = false; // 是否启用掩膜
                if (m_lMaskShapes.Count > 0)
                {
                    bUseMaskFlag = true;
                }
                else if (null != m_stROIShape)
                {
                    if (MVD_SHAPE_TYPE.MvdShapeRectangle != m_stROIShape.ShapeType)
                    {
                        bUseMaskFlag = true;
                    }
                    else
                    {
                        var rectROI = m_stROIShape as CMvdRectangleF;
                        if (Math.Abs(rectROI.Angle) > MVD_FLOAT_EPS)
                        {
                            bUseMaskFlag = true;
                        }
                    }
                }

                if (true == bUseMaskFlag)
                {
                    m_stPreproMaskToolObj.RegionListEx.Clear();
                    m_stPreproMaskToolObj.InputImage = m_stInputImage;
                    if (null != m_stROIShape)
                    {
                        m_stPreproMaskToolObj.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(m_stROIShape, MVD_REGION_TYPE.MvdRegionTypeRoi));
                    }

                    foreach (var item in m_lMaskShapes)
                    {
                        m_stPreproMaskToolObj.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(item, MVD_REGION_TYPE.MvdRegionTypeMask));
                    }
                    m_stPreproMaskToolObj.Run();
                    m_stImageEnhanceToolObj.RegionImage = m_stPreproMaskToolObj.OutputImage;
                }
                else
                {
                    m_stImageEnhanceToolObj.RegionImage = null;
                    m_stImageEnhanceToolObj.ROI = m_stROIShape;
                }
                m_stImageEnhanceToolObj.Run();
                return m_stImageEnhanceToolObj.Result.OutputImage;
            }
            catch (MvdException ex)
            {
                Console.WriteLine("An error occurred while running the tool. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n");
                return null;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("An error occurred while running the tool with ' " + ex.Message + " '\r\n");
                return null;
            }
        }

        public CMvdImage Run(CMvdImage StreamingImage)
        {
            try
            {
                m_stInputImage = StreamingImage;
                if ((null == m_stImageEnhanceToolObj) || (null == m_stInputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_TOOL, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }
                m_stImageEnhanceToolObj.InputImage = m_stInputImage;

                bool bUseMaskFlag = false; // 是否启用掩膜
                if (m_lMaskShapes.Count > 0)
                {
                    bUseMaskFlag = true;
                }
                else if (null != m_stROIShape)
                {
                    if (MVD_SHAPE_TYPE.MvdShapeRectangle != m_stROIShape.ShapeType)
                    {
                        bUseMaskFlag = true;
                    }
                    else
                    {
                        var rectROI = m_stROIShape as CMvdRectangleF;
                        if (Math.Abs(rectROI.Angle) > MVD_FLOAT_EPS)
                        {
                            bUseMaskFlag = true;
                        }
                    }
                }

                if (true == bUseMaskFlag)
                {
                    m_stPreproMaskToolObj.RegionListEx.Clear();
                    m_stPreproMaskToolObj.InputImage = m_stInputImage;
                    if (null != m_stROIShape)
                    {
                        m_stPreproMaskToolObj.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(m_stROIShape, MVD_REGION_TYPE.MvdRegionTypeRoi));
                    }

                    foreach (var item in m_lMaskShapes)
                    {
                        m_stPreproMaskToolObj.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(item, MVD_REGION_TYPE.MvdRegionTypeMask));
                    }
                    m_stPreproMaskToolObj.Run();
                    m_stImageEnhanceToolObj.RegionImage = m_stPreproMaskToolObj.OutputImage;
                }
                else
                {
                    m_stImageEnhanceToolObj.RegionImage = null;
                    m_stImageEnhanceToolObj.ROI = m_stROIShape;
                }
                m_stImageEnhanceToolObj.Run();
                return m_stImageEnhanceToolObj.Result.OutputImage;

            }
            catch (MvdException ex)
            {
                Console.WriteLine("An error occurred while running the tool. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n");
                return null;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("An error occurred while running the tool with ' " + ex.Message + " '\r\n");
                return null;
            }
        }
    }
}
