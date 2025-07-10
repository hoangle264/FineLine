using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_VisionMaster.ParseXML;
using VisionDesigner.BlobFind;
using VisionDesigner;
using System.IO;
using System.Drawing;
using OpenCvSharp;
using Demo_VisionMaster.Views;

namespace Demo_VisionMaster.Z_TestClass
{
    public class TestBlodFunction
    {
        public TestBlodFunction() { }
        private CBlobFindTool m_stBlobFindToolObj = null;
        private CMvdImage m_stImage = null;
        List<CMvdShape> m_lMaskShapes = new List<CMvdShape>();
        private CMvdShape m_stROIShape = null;

        public void Init() 
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
                Console.WriteLine("Fail to initialize the tool. ErrorCode: 0x" + ex.ErrorCode.ToString("X") + "\r\n");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Fail with error " + ex.Message + "\r\n");
            }
        }

        public void Import(string filePath) 
        {
            try
            {
                FileStream fileStr = null;
                if (File.Exists(filePath))
                {
                    fileStr = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    Int64 nFileLen = fileStr.Length;
                    byte[] fileBytes = new byte[nFileLen];
                    uint nReadLen = Convert.ToUInt32(fileStr.Read(fileBytes, 0, fileBytes.Length));
                    m_stBlobFindToolObj.LoadConfiguration(fileBytes, nReadLen);
                }

            }
            catch (MvdException ex)
            {
                Console.WriteLine(ex.ErrorCode.ToString("X")+"\r\n");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        public void Run(string ImageFile) 
        {
            try
            {
                m_stImage = new CMvdImage();
                m_stImage.InitImage(ImageFile);
                if ((null == m_stBlobFindToolObj) || (null == this.m_stImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }
                m_stBlobFindToolObj.InputImage = m_stImage;
                m_stBlobFindToolObj.BasicParam.ShowBlobImageStatus = true;
                if (null == m_stROIShape)
                {
                    m_stBlobFindToolObj.ROI = new VisionDesigner.CMvdRectangleF(m_stImage.Width / 2, m_stImage.Height / 2, m_stImage.Width, m_stImage.Height);
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

                var BlobOutline = new CMvdPointSetF();
                BlobOutline.BorderColor = new MVD_COLOR(0, 255, 0, 255);
                foreach (var item in m_stBlobFindToolObj.Result.BlobInfo)
                {
                    CMvdRectangleF blobBox = new CMvdRectangleF(item.BoxInfo.CenterX, item.BoxInfo.CenterY, item.BoxInfo.Width, item.BoxInfo.Height);
                    blobBox.Angle = item.BoxInfo.Angle;
                    blobBox.BorderColor = new MVD_COLOR(255, 0, 0, 255);

                    foreach (var Item in item.ContourPoints)
                    {
                        BlobOutline.AddPoint(Item.sX, Item.sY);
                    }
                }
                if (0 != BlobOutline.PointsList.Count)
                {
                    //m_stBlobOutline = BlobOutline;
                }
                var m_stBinaryImage = m_stBlobFindToolObj.Result.BinaryImage;
                var m_stBlobImage = m_stBlobFindToolObj.Result.BlobImage;
                Bitmap bitmap = null;
                bitmap = m_stBlobImage.GetBitmap();
                var image = Helpers.CV_Clone.ConvertBitmapToMat(bitmap);
                m_stBlobImage.SaveImage("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");
                ViewImage.Instance.updatePbResultPic(bitmap);
                //Cv2.ImShow("Result Image",image);
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

        public void Run(Bitmap StreamImage)
        {
            try
            {
                m_stImage = new CMvdImage();
                m_stImage.InitImage(StreamImage);
                if ((null == m_stBlobFindToolObj) || (null == this.m_stImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }
                m_stBlobFindToolObj.InputImage = m_stImage;
                m_stBlobFindToolObj.BasicParam.ShowBlobImageStatus = true;
                if (null == m_stROIShape)
                {
                    m_stBlobFindToolObj.ROI = new VisionDesigner.CMvdRectangleF(m_stImage.Width / 2, m_stImage.Height / 2, m_stImage.Width, m_stImage.Height);
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

                var BlobOutline = new CMvdPointSetF();
                BlobOutline.BorderColor = new MVD_COLOR(0, 255, 0, 255);
                foreach (var item in m_stBlobFindToolObj.Result.BlobInfo)
                {
                    CMvdRectangleF blobBox = new CMvdRectangleF(item.BoxInfo.CenterX, item.BoxInfo.CenterY, item.BoxInfo.Width, item.BoxInfo.Height);
                    blobBox.Angle = item.BoxInfo.Angle;
                    blobBox.BorderColor = new MVD_COLOR(255, 0, 0, 255);

                    foreach (var Item in item.ContourPoints)
                    {
                        BlobOutline.AddPoint(Item.sX, Item.sY);
                    }
                }
                if (0 != BlobOutline.PointsList.Count)
                {
                    //m_stBlobOutline = BlobOutline;
                }
                var m_stBinaryImage = m_stBlobFindToolObj.Result.BinaryImage;
                var m_stBlobImage = m_stBlobFindToolObj.Result.BlobImage;
                Bitmap bitmap = null;
                bitmap = m_stBlobImage.GetBitmap();
                var image = Helpers.CV_Clone.ConvertBitmapToMat(bitmap);
                m_stBlobImage.SaveImage("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");
                ViewImage.Instance.updatePbResultPic(bitmap);
                //Cv2.ImShow("Result Image",image);
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
    }
}
