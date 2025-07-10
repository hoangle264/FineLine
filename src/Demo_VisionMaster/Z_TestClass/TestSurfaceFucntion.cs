using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_VisionMaster.ParseXML;
using VisionDesigner.PreproMask;
using VisionDesigner.SurfaceDefectFilter;
using VisionDesigner;
using System.IO;
using OpenCvSharp;
using System.Drawing;
using Demo_VisionMaster.Views;

namespace Demo_VisionMaster.Z_TestClass
{
    public class TestSurfaceFucntion
    {
        public TestSurfaceFucntion() { }
        private CSurfaceDefectFilterTool _SurfaceDefectFilterTool = null;
        private CPreproMaskTool _MaskTool = null;
        private CMvdImage _InputImage = null;
        private CMvdShape _ROI = null;
        private List<CMvdShape> _MaskShapeList = new List<CMvdShape>();
        public void Init() 
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
                string filePath = FileXml;
                if (File.Exists(filePath))
                {
                    fileStr = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    Int64 nFileLen = fileStr.Length;
                    byte[] fileBytes = new byte[nFileLen];
                    uint nReadLen = Convert.ToUInt32(fileStr.Read(fileBytes, 0, fileBytes.Length));
                    fileStr.Close();
                    fileStr.Dispose();
                    _SurfaceDefectFilterTool.LoadConfiguration(fileBytes, nReadLen);
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

        public void Run(string ImageFile) 
        {
            try
            {
                _InputImage = new CMvdImage();
                _InputImage.InitImage(ImageFile);
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
                _SurfaceDefectFilterTool.Run();
                var outputImage = _SurfaceDefectFilterTool.Result.FilterImage;
                outputImage.SaveImage("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");
                Bitmap bitmap = null;
                bitmap = outputImage.GetBitmap();
                var image = Helpers.CV_Clone.ConvertBitmapToMat(bitmap);
                ViewImage.Instance.updatePbResultPic(bitmap);
                //Cv2.ImShow("Result Image", image);
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

        public void Run(Bitmap StreamingImage)
        {
            try
            {
                _InputImage = new CMvdImage();
                _InputImage.InitImage(StreamingImage);
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
                _SurfaceDefectFilterTool.Run();
                var outputImage = _SurfaceDefectFilterTool.Result.FilterImage;
                outputImage.SaveImage("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");
                Bitmap bitmap = null;
                bitmap = outputImage.GetBitmap();
                var image = Helpers.CV_Clone.ConvertBitmapToMat(bitmap);
                ViewImage.Instance.updatePbResultPic(bitmap);
                //Cv2.ImShow("Result Image", image);
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
    }
}
