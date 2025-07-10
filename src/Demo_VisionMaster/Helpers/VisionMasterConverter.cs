using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VisionDesigner;
using VisionDesigner.Controls;
using OpenCvSharp;



namespace Demo_VisionMaster.Helpers
{
    public class VisionMasterConverter
    {
        [DllImport("kernel32.dll")]
        public static  extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

        public static  unsafe void ConvertMat2MVDImage(Mat mat, ref CMvdImage cMvdImg)
        {
            // 参数合法性判断
            if (null == mat || null == cMvdImg)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_HANDLE);
            }
            // 像素格式判断
            if (MatType.CV_8UC1 != mat.Type() && MatType.CV_8UC3 != mat.Type())
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_SUPPORT);
            }

            uint imgWidth = (uint)mat.Size().Width;// 图片的真实宽度
            uint imgHeight = (uint)mat.Size().Height;// 图片的真实高度
            int nChannelNum = mat.Channels();
            byte[] bMvdImgData = null;

            // 根据传入的mat图像初始化MVDImage
            if (mat.Type() == MatType.CV_8UC1)
            {
                cMvdImg.InitImage(imgWidth, imgHeight, MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
            }
            else if (mat.Type() == MatType.CV_8UC3)
            {
                cMvdImg.InitImage(imgWidth, imgHeight, MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3);
            }

            // 图像数据拷贝
            bMvdImgData = cMvdImg.GetImageData().stDataChannel[0].arrDataBytes;
            if (mat.IsContinuous())
            {
                Marshal.Copy(mat.Ptr(0), bMvdImgData, 0, (int)cMvdImg.GetImageData().stDataChannel[0].nLen);
            }
            else //避免mat图有裁剪等操作导致图像数据不连续问题
            {
                IntPtr pDstPosPtr = IntPtr.Zero;
                fixed (void* pMVDImgData = bMvdImgData)
                {
                    IntPtr MVDImgDataOrignPtr = new IntPtr(pMVDImgData);
                    for (int i = 0; i < imgHeight; i++)    // 逐行拷贝
                    {
                        pDstPosPtr = new IntPtr(MVDImgDataOrignPtr.ToInt64() + Convert.ToInt64(i * imgWidth * nChannelNum));
                        IntPtr pdata = mat.Ptr(i);
                        CopyMemory(pDstPosPtr, pdata, Convert.ToInt32(imgWidth * nChannelNum));
                    }
                }
            }

            if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == cMvdImg.PixelFormat)
            {
                // Mat为BGRBGR...存储，MVDImage为RGBRGB...存储，需要调整
                byte bTemp;
                for (int i = 0; i < imgWidth * imgHeight; i++)
                {
                    bTemp = bMvdImgData[3 * i];
                    bMvdImgData[3 * i] = bMvdImgData[3 * i + 2];
                    bMvdImgData[3 * i + 2] = bTemp;
                }
            }
        }

        public static void ConvertMVDImage2Mat(CMvdImage mvdImage, ref Mat mat)
        {
            // 参数合法性判断
            if (null == mat || null == mvdImage)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_HANDLE);
            }

            // 像素格式判断
            if (mvdImage.PixelFormat != MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08 && mvdImage.PixelFormat != MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_SUPPORT);
            }

            int imgWidth = (int)mvdImage.Width;
            int imgHeight = (int)mvdImage.Height;

            // 根据传入的MVDImage类型初始化Mat
            if (mvdImage.PixelFormat == MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08)
            {
                mat.Create(imgHeight, imgWidth, MatType.CV_8UC1);
                Marshal.Copy(mvdImage.GetImageData(0).arrDataBytes, 0, mat.Ptr(0), (int)mvdImage.GetImageData(0).nLen);
            }
            else if (mvdImage.PixelFormat == MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3)
            {
                mat.Create(imgHeight, imgWidth, MatType.CV_8UC3);
                // 先备份MVD图像数据，保证不改变源图像数据
                byte[] bMvdImgDataTemp = new byte[mvdImage.GetImageData(0).nLen];
                Array.Copy(mvdImage.GetImageData(0).arrDataBytes, bMvdImgDataTemp, bMvdImgDataTemp.Length);

                // Mat为BGRBGR...存储，MVD为RGBRGB...存储，需要调整
                byte bTemp;
                for (int i = 0; i < imgWidth * imgHeight; i++)
                {
                    bTemp = bMvdImgDataTemp[3 * i];
                    bMvdImgDataTemp[3 * i] = bMvdImgDataTemp[3 * i + 2];
                    bMvdImgDataTemp[3 * i + 2] = bTemp;
                }
                // 将数据拷贝至Mat图像
                Marshal.Copy(bMvdImgDataTemp, 0, mat.Ptr(0), bMvdImgDataTemp.Length);
            }

        }

        public static void ConvertBitmap2MVDImage(Bitmap cBitmapImg, ref CMvdImage cMvdImg)
        {
            // 参数合法性判断
            if (null == cBitmapImg || null == cMvdImg)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_PARAMETER_ILLEGAL);
            }

            // 判断像素格式
            if (PixelFormat.Format8bppIndexed != cBitmapImg.PixelFormat && PixelFormat.Format24bppRgb != cBitmapImg.PixelFormat)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_SUPPORT);
            }

            Int32 nImageWidth = cBitmapImg.Width;
            Int32 nImageHeight = cBitmapImg.Height;
            Int32 nChannelNum = 0;
            BitmapData bitmapData = null;

            try
            {
                // 获取图像信息
                if (PixelFormat.Format8bppIndexed == cBitmapImg.PixelFormat) // 灰度图
                {
                    bitmapData = cBitmapImg.LockBits(new Rectangle(0, 0, nImageWidth, nImageHeight)
                                                                    , ImageLockMode.ReadOnly
                                                                    , PixelFormat.Format8bppIndexed);
                    cMvdImg.InitImage(Convert.ToUInt32(nImageWidth), Convert.ToUInt32(nImageHeight), MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                    nChannelNum = 1;
                }
                else if (PixelFormat.Format24bppRgb == cBitmapImg.PixelFormat) // 彩色图
                {
                    bitmapData = cBitmapImg.LockBits(new Rectangle(0, 0, nImageWidth, nImageHeight)
                                                                , ImageLockMode.ReadOnly
                                                                , PixelFormat.Format24bppRgb);
                    cMvdImg.InitImage(Convert.ToUInt32(nImageWidth), Convert.ToUInt32(nImageHeight), MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3);
                    nChannelNum = 3;
                }

                // 考虑图像是否4字节对齐，bitmap要求4字节对齐，而mvdimage不要求对齐
                if (0 == nImageWidth % 4) // 4字节对齐时，直接拷贝
                {
                    Marshal.Copy(bitmapData.Scan0, cMvdImg.GetImageData().stDataChannel[0].arrDataBytes, 0, nImageWidth * nImageHeight * nChannelNum);
                }
                else // 按步长逐行拷贝
                {
                    // 每行实际占用字节数
                    Int32 nRowPixelByteNum = nImageWidth * nChannelNum + 4 - (nImageWidth * nChannelNum % 4);
                    // 每行首字节首地址
                    IntPtr bitmapDataRowPos = IntPtr.Zero;
                    for (int i = 0; i < nImageHeight; i++)
                    {
                        // 获取每行第一个像素值的首地址
                        bitmapDataRowPos = new IntPtr(bitmapData.Scan0.ToInt64() + nRowPixelByteNum * i);
                        Marshal.Copy(bitmapDataRowPos, cMvdImg.GetImageData().stDataChannel[0].arrDataBytes, i * nImageWidth * nChannelNum, nImageWidth * nChannelNum);
                    }
                }

                // bitmap彩色图按BGR存储，而MVDimg按RGB存储，改变存储顺序
                // 交换R和B
                if (PixelFormat.Format24bppRgb == cBitmapImg.PixelFormat)
                {
                    byte bTemp;
                    byte[] bMvdImgData = cMvdImg.GetImageData().stDataChannel[0].arrDataBytes;
                    for (int i = 0; i < nImageWidth * nImageHeight; i++)
                    {
                        bTemp = bMvdImgData[3 * i];
                        bMvdImgData[3 * i] = bMvdImgData[3 * i + 2];
                        bMvdImgData[3 * i + 2] = bTemp;
                    }
                }
            }
            finally
            {
                cBitmapImg.UnlockBits(bitmapData);
            }
        }

        public static void ConvertMVDImage2Bitmap(CMvdImage cMvdImg, ref Bitmap cBitmapImg)
        {
            // 参数合法性判断
            if (null == cMvdImg)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_PARAMETER_ILLEGAL);
            }

            // 判断像素格式
            if (MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08 != cMvdImg.PixelFormat && MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 != cMvdImg.PixelFormat)
            {
                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_SUPPORT);
            }

            Int32 nImageWidth = Convert.ToInt32(cMvdImg.Width);
            Int32 nImageHeight = Convert.ToInt32(cMvdImg.Height);
            Int32 nChannelNum = 0;
            BitmapData bitmapData = null;
            byte[] bBitmapDataTemp = null;
            try
            {
                // 获取图像信息
                if (MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08 == cMvdImg.PixelFormat) // 灰度图
                {
                    cBitmapImg = new Bitmap(nImageWidth, nImageHeight, PixelFormat.Format8bppIndexed);

                    // 灰度图需指定调色板
                    ColorPalette colorPalette = cBitmapImg.Palette;
                    for (int j = 0; j < 256; j++)
                    {
                        colorPalette.Entries[j] = Color.FromArgb(j, j, j);
                    }
                    cBitmapImg.Palette = colorPalette;

                    bitmapData = cBitmapImg.LockBits(new Rectangle(0, 0, nImageWidth, nImageHeight)
                                                                    , ImageLockMode.WriteOnly
                                                                    , PixelFormat.Format8bppIndexed);

                    // 灰度图不做深拷贝
                    bBitmapDataTemp = cMvdImg.GetImageData().stDataChannel[0].arrDataBytes;
                    nChannelNum = 1;
                }
                else if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == cMvdImg.PixelFormat) // 彩色图
                {
                    cBitmapImg = new Bitmap(nImageWidth, nImageHeight, PixelFormat.Format24bppRgb);
                    bitmapData = cBitmapImg.LockBits(new Rectangle(0, 0, nImageWidth, nImageHeight)
                                                                , ImageLockMode.WriteOnly
                                                                , PixelFormat.Format24bppRgb);
                    // 彩色图做深拷贝
                    bBitmapDataTemp = new byte[cMvdImg.GetImageData().stDataChannel[0].nLen];
                    Array.Copy(cMvdImg.GetImageData().stDataChannel[0].arrDataBytes, bBitmapDataTemp, bBitmapDataTemp.Length);
                    nChannelNum = 3;
                }

                // bitmap彩色图按BGR存储，而MVDimg按RGB存储，改变存储顺序
                // 交换R和B
                if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == cMvdImg.PixelFormat)
                {
                    byte bTemp;
                    for (int i = 0; i < nImageWidth * nImageHeight; i++)
                    {
                        bTemp = bBitmapDataTemp[3 * i];
                        bBitmapDataTemp[3 * i] = bBitmapDataTemp[3 * i + 2];
                        bBitmapDataTemp[3 * i + 2] = bTemp;
                    }
                }

                // 考虑图像是否4字节对齐，bitmap要求4字节对齐，而mvdimage不要求对齐
                if (0 == nImageWidth % 4) // 4字节对齐时，直接拷贝
                {
                    Marshal.Copy(bBitmapDataTemp, 0, bitmapData.Scan0, nImageWidth * nImageHeight * nChannelNum);
                }
                else // 按步长逐行拷贝
                {
                    // 每行实际占用字节数
                    Int32 nRowPixelByteNum = nImageWidth * nChannelNum + 4 - (nImageWidth * nChannelNum % 4);
                    // 每行首字节首地址
                    IntPtr bitmapDataRowPos = IntPtr.Zero;
                    for (int i = 0; i < nImageHeight; i++)
                    {
                        // 获取每行第一个像素值的首地址
                        bitmapDataRowPos = new IntPtr(bitmapData.Scan0.ToInt64() + nRowPixelByteNum * i);
                        Marshal.Copy(bBitmapDataTemp, i * nImageWidth * nChannelNum, bitmapDataRowPos, nImageWidth * nChannelNum);
                    }
                }

                cBitmapImg.UnlockBits(bitmapData);
            }
            catch (MvdException ex)
            {
                if (null != cBitmapImg)
                {
                    cBitmapImg.UnlockBits(bitmapData);
                    cBitmapImg.Dispose();
                    cBitmapImg = null;
                }
                throw ex;
            }
            catch (System.Exception ex)
            {
                if (null != cBitmapImg)
                {
                    cBitmapImg.UnlockBits(bitmapData);
                    cBitmapImg.Dispose();
                    cBitmapImg = null;
                }
                throw ex;
            }
        }

    }
}
