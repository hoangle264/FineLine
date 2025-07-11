using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_VisionMaster.Controls;
using Demo_VisionMaster.Model;
using VisionDesigner;
using VMControls.RenderInterface;

namespace Demo_VisionMaster.Services
{
    public class CropService
    {
        public CropService() { }
        private Rectangle _rect;
        public void Init(Rectangle roi)
        {
            _rect = roi;
        }
        public void Init()
        {
            string path = Properties.Settings.Default.FileCofigRoiRectang;
            if (path != "")
            {
                ROI_Information roi = AppCoreBackend.Ins.roiRepo.Load<ROI_Information>(path);
                _rect.X = roi.X;
                _rect.Y = roi.Y;
                _rect.Width = roi.Width;
                _rect.Height = roi.Height;
            }
            else
            {
                _rect.X = 0;
                _rect.Y = 0;
                _rect.Width = 0;
                _rect.Height = 0;
            }
            var t = _rect;
        }
        public CMvdImage Run(CMvdImage inputImage)
        {
            try
            {

                Bitmap image = inputImage.GetBitmap();
                Rectangle cropArea = Rectangle.Intersect(_rect, new Rectangle(System.Drawing.Point.Empty, image.Size));
                Bitmap cropped = image.Clone(cropArea, image.PixelFormat);
                CMvdImage result = new CMvdImage();
                result.InitImage(cropped);
                return result;
            }
            catch
            {
                return null;
            }
        }

    }
}
