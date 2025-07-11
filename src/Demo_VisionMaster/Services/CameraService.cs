using Demo_VisionMaster.Services;
using Demo_VisionMaster.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using System.Windows.Media.Media3D;
using VisionDesigner;
using VisionDesigner.Camera;
using VisionDesigner.OCR;
using static Demo_VisionMaster.Events.PlcEvent;

namespace Demo_VisionMaster.Services
{
    public class CameraService
    {
        Helpers.CameraHelper camera = new Helpers.CameraHelper();
        List<string> ListDevice = new List<string>();
        List<double> ListLenghtData;
        List<Bitmap> ListImageData;
        public CropService cropService = new CropService();    
        public EnhancementService enhancementService = new EnhancementService();
        public SurfaceService surfaceService = new SurfaceService();
        public BlodService blodService = new BlodService();
        public ThiningService thiningService= new ThiningService();

        public Bitmap Images;
        public double Lenghts;


        public void init()
        {
            ListDevice.Add("U3V: RoboLink1 (00F14940837)");
            ListLenghtData = new List<double>();
            ListImageData = new List<Bitmap>();
            camera.CreateDicCam();
            var dic = camera.ListDevice();
            //var Tool = camera.CamTool();

            if (dic.Count == 0 || ListDevice == null) return;

            //camera.MutipleConnectCam(ListDevice, dic);
        }

        public async void RunService() 
        {
            cropService.Init();
            enhancementService.Init();
            surfaceService.Init();
            blodService.Init();
            string sBlod = Properties.Settings.Default.FilePathBoldAnalys;
            string sEnhence = Properties.Settings.Default.FilePathEnhancement;
            string sSurface = Properties.Settings.Default.FilePathSurface;
            enhancementService.Import(sEnhence);
            surfaceService.Import(sSurface);
            blodService.Import(sBlod);
            try
            {
                camera.MutipleReadImage();
                var ListImage = camera.Listimages();
                foreach (var item in ListImage)
                {
                    MainImageProcessing(item);
                    ListLenghtData.Add(Lenghts);
                    ListImageData.Add(Images);
                }
                ViewHome.Instance.UpdateMainPicture(Images);
            }
            catch (Exception ex)
            {
                ViewHome.Instance.UpdateAppStatus(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
       


        public CCameraTool CCTool { get; set; }
        public Bitmap Bitmaps { get; set; }
        public double Line { get; set; }
        public bool Status = false;

        public bool InitUC(string CameraName)
        {
            cropService.Init();
            enhancementService.Init();
            surfaceService.Init();
            blodService.Init();

            string sBlod = Properties.Settings.Default.FilePathBoldAnalys;
            string sEnhence = Properties.Settings.Default.FilePathEnhancement;
            string sSurface = Properties.Settings.Default.FilePathSurface;

            enhancementService.Import(sEnhence);
            surfaceService.Import(sSurface);
            blodService.Import(sBlod);

            camera.CreateDicCam();
            var dic = camera.ListDevice();

            if (dic.Count == 0 || ListDevice == null) return false;
            CCTool = camera.SingleConnectCam(CameraName, dic);
            if (CCTool != null) 
            {
                return true;
            }
            else return false;
          
        }



        public void RunServiceUC()
        {
            try
            {
                var Image = camera.SingleReadImage(CCTool);
                MainImageProcessing(Image);
                Line = Lenghts;
                Bitmaps = Images;
                ViewHome.Instance.UpdateMainPicture(Images);
            }
            catch (Exception ex)
            {
                ViewHome.Instance.UpdateAppStatus(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public bool StopServiceUC(string CameraName)
        {
            var dic = camera.ListDevice();
            if (dic.Count == 0 || ListDevice == null) return false;
            // Pass the correct CCameraTool instance to disconnect
            camera.SingleDisconnectCamera(CCTool);
            return false;
        }

        public void MainImageProcessing(CMvdImage Image) 
        {
            var crop = Crop(Image);
            var enhence = Enhance(Image);
            var surface=Surface(enhence);
            var blod=Blod(surface);
            Images = surface.GetBitmap();
           // FindLiner(blod, ref Images , ref Lenghts);
        }



        public CMvdImage Crop(CMvdImage image) 
        {
            return cropService.Run(image);
        }


        public CMvdImage Enhance(CMvdImage image) 
        {
            return enhancementService.Run(image);
        }


        public CMvdImage Surface(CMvdImage image) 
        {
            return surfaceService.Run(image);
        }


        public CMvdImage Blod(CMvdImage image) 
        {
            return blodService.Run(image);  
        }


        public void FindLiner(CMvdImage image, ref Bitmap Image, ref double Lenght ) 
        {
             Image = thiningService.Run(image);
             Lenght = thiningService.ResultLenght;
        }
    }
}
