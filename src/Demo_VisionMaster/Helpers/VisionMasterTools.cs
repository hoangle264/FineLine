using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisionDesigner;
using VisionDesigner.ImageEnhance;
using VisionDesigner.PreproMask;
using MvdXmlParse;
using VisionDesigner.Legacy.Controls;
using System.Diagnostics;
using VisionDesigner.SurfaceDefectFilter;
using VisionDesigner.BlobFind;
using MvRenderActivexLib;


namespace Demo_VisionMaster.Helpers
{
    
    public partial class VisionMasterTools
    {
        // Enhancement tool
        private const float MVD_FLOAT_EPS = 0.0001f; // 浮点计算误差
        private CImageEnhanceTool m_stImageEnhanceToolObj = null;
        private CPreproMaskTool m_stPreproMaskToolObj = null;
        private CMvdImage m_stInputImage = null;
        private CMvdShape m_stROIShape = null;
        List<VisionDesigner.CMvdShape> m_lMaskShapes = new List<VisionDesigner.CMvdShape>();
        // Surface tool
        private Stopwatch _StopWatch = new Stopwatch();
        private CSurfaceDefectFilterTool _SurfaceDefectFilterTool = null;
        private CPreproMaskTool _MaskTool = null;
        private CMvdImage _InputImage = null;
        private CMvdShape _ROI = null;
        private List<CMvdShape> _MaskShapeList = new List<CMvdShape>();
        // Blod tool
        private CBlobFindTool m_stBlobFindToolObj = null;

        private CMvdImage m_stBinaryImage = null;
        private CMvdImage m_stBlobImage = null;
        List<CMvdRectangleF> m_lBlobBoxRender1 = new List<CMvdRectangleF>();
        List<CMvdRectangleF> m_lBlobBoxRender2 = new List<CMvdRectangleF>();
        private CMvdShape m_stBlobOutline = null;
        public void InputsImage() 
        {
            System.Windows.Forms.OpenFileDialog fileDlg = null;
            try
            {
                fileDlg = new System.Windows.Forms.OpenFileDialog();
                fileDlg.Filter = @"Picture|*.bmp";
                fileDlg.RestoreDirectory = true;

                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (null == m_stInputImage)
                    {
                        m_stInputImage = new CMvdImage();
                    }
                    m_stInputImage.InitImage(fileDlg.FileName);
                    //MVD_ERROR_CODE nErrorCode = MVD_ERROR_CODE.MVD_OK;
                   
                }
                fileDlg.Dispose();
            }
            catch (MvdException ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
            finally
            {
                if (null != fileDlg)
                {
                    fileDlg.Dispose();
                }
            }
        }
        public void Init_Image_Enhancement() 
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
        public void Image_Enhancement() 
        {
            try
            {
                if ((null == m_stImageEnhanceToolObj) || (null == m_stInputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_TOOL, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }
                m_stImageEnhanceToolObj.InputImage = m_stInputImage;

                bool bUseMaskFlag = false; 
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
               
                CMvdImage stOutputImage = m_stImageEnhanceToolObj.Result.OutputImage;
               // MVD_ERROR_CODE nErrorCode = MVD_ERROR_CODE.MVD_OK;
               
            }
            catch (MvdException ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
        }
        
        public void Init_Image_Surface_Defect_Filter()
        {

            try
            {
                _SurfaceDefectFilterTool = new CSurfaceDefectFilterTool();

                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    _SurfaceDefectFilterTool.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        _SurfaceDefectFilterTool.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                

                _MaskTool = new CPreproMaskTool();

               
            }
            catch (MvdException ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
        }
        public void Image_Surface_Defect_Filter() 
        {
            try
            {
                if ((null == _SurfaceDefectFilterTool) || (null == _InputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_TOOL, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }

                _SurfaceDefectFilterTool.InputImage = _InputImage;
                _SurfaceDefectFilterTool.InputROI = _ROI;

                _SurfaceDefectFilterTool.RegionImage = null;
                _SurfaceDefectFilterTool.RegionImageAssistant.ROIList.Clear();

                if (_MaskShapeList.Count > 0)
                {
                    _MaskTool.RegionListEx.Clear();
                    _MaskTool.InputImage = _InputImage;
                    if (null != _ROI)
                    {
                        _MaskTool.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(_ROI, MVD_REGION_TYPE.MvdRegionTypeRoi));
                        _SurfaceDefectFilterTool.RegionImageAssistant.ROIList.Add(_ROI);
                    }
                    foreach (var shape in _MaskShapeList)
                    {
                        _MaskTool.RegionListEx.Add(new Tuple<CMvdShape, MVD_REGION_TYPE>(shape, MVD_REGION_TYPE.MvdRegionTypeMask));
                    }
                    _MaskTool.Run();
                    _SurfaceDefectFilterTool.RegionImage = _MaskTool.OutputImage;
                }

                float[] weight = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
                weight[0] = Convert.ToSingle(5);
                weight[1] = Convert.ToSingle(5);
                weight[2] = Convert.ToSingle(5);
                weight[3] = Convert.ToSingle(5);
                weight[4] = Convert.ToSingle(5);
                weight[5] = Convert.ToSingle(5);
                _SurfaceDefectFilterTool.BasicParam.WeightData = weight;
                _SurfaceDefectFilterTool.BasicParam.Offset = Convert.ToInt32(5);

                _StopWatch.Reset();
                _StopWatch.Start();
                _SurfaceDefectFilterTool.Run();
                _StopWatch.Stop();


                var outputImage = _SurfaceDefectFilterTool.Result.FilterImage;
                //MVD_ERROR_CODE nErrorCode = MVD_ERROR_CODE.MVD_OK;

            }
            catch (MvdException ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
        }
        public void Init_Image_Blod_Analysis()
        {
            try
            {
                m_stBlobFindToolObj = new CBlobFindTool();
                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    m_stBlobFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        m_stBlobFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }
               
            }
            catch (MvdException ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
        }
        public void Image_Blod_Analysis() 
        {
            try
            {
                if ((null == m_stBlobFindToolObj) || (null == m_stInputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }

                m_stBlobFindToolObj.InputImage = m_stInputImage;
                int ImageTypebinary = 0;
                int ImageTypeBlod = 1;
                if (ImageTypebinary == ImageTypeBlod)
                {
                    m_stBlobFindToolObj.BasicParam.ShowBlobImageStatus = true;
                    m_stBlobFindToolObj.BasicParam.ShowBinaryImageStatus = false;
                }
                else
                {
                    m_stBlobFindToolObj.BasicParam.ShowBlobImageStatus = false;
                    m_stBlobFindToolObj.BasicParam.ShowBinaryImageStatus = true;
                }
                if (null == m_stROIShape)
                {
                    m_stBlobFindToolObj.ROI = new VisionDesigner.CMvdRectangleF(m_stInputImage.Width / 2, m_stInputImage.Height / 2, m_stInputImage.Width, m_stInputImage.Height);
                }
                else
                {
                    m_stBlobFindToolObj.ROI = m_stROIShape;
                }

                m_stBlobFindToolObj.ClearMasks();
                foreach (var item in m_lMaskShapes)
                {
                    m_stBlobFindToolObj.AddMask(item);
                }

                m_stBlobFindToolObj.Run();
             

                if (0 != m_lBlobBoxRender1.Count)
                {
                    m_lBlobBoxRender1.Clear();
                }

                if (null != m_stBlobOutline) 
                { 
                    m_stBlobOutline = null;
                }

               

                var BlobOutline = new CMvdPointSetF();
                BlobOutline.BorderColor = new MVD_COLOR(0, 255, 0, 255);
                foreach (var item in m_stBlobFindToolObj.Result.BlobInfo)
                {
                    CMvdRectangleF blobBox = new CMvdRectangleF(item.BoxInfo.CenterX, item.BoxInfo.CenterY, item.BoxInfo.Width, item.BoxInfo.Height);
                    blobBox.Angle = item.BoxInfo.Angle;
                    blobBox.BorderColor = new MVD_COLOR(255, 0, 0, 255);
                    //mvdRenderActivex1.MVD_AddMvdShape(blobBox);
                    m_lBlobBoxRender1.Add(blobBox);

                  

                    foreach (var Item in item.ContourPoints)
                    {
                        BlobOutline.AddPoint(Item.sX, Item.sY);
                    }

                }
                if (0 != BlobOutline.PointsList.Count)
                {
                    m_stBlobOutline = BlobOutline;
                }

                m_stBinaryImage = m_stBlobFindToolObj.Result.BinaryImage;
                m_stBlobImage = m_stBlobFindToolObj.Result.BlobImage;

            }
            catch (MvdException ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
        }

    }
}
