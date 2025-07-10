using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Demo_VisionMaster.Views;
using VisionDesigner;
using VisionDesigner.Camera;
using Demo_VisionMaster.Services;
using System.Drawing;
using static Demo_VisionMaster.Events.PlcEvent;
using System.Web.Management;

namespace Demo_VisionMaster.Services
{
    public class CameraService
    {
        Helpers.CameraHelper camera = new Helpers.CameraHelper();
        List<string> ListDevice = new List<string>();
        List<double> LenghtData;
        List<Bitmap> ImageData;
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
            LenghtData = new List<double>();
            ImageData = new List<Bitmap>();
            camera.CreateDicCam();
            var dic = camera.ListDevice();
            //var Tool = camera.CamTool();

            if (dic.Count == 0 || ListDevice == null) return;

            camera.MutipleConnectCam(ListDevice, dic);
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
                    await MainImageProcessing(item);
                    LenghtData.Add(Lenghts);
                    ImageData.Add(Images);
                }
                ViewHome.Instance.UpdateMainPicture(Images);
            }
            catch (Exception ex)
            {
                ViewHome.Instance.UpdateAppStatus(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
        public async Task MainImageProcessing(CMvdImage Image) 
        {
            var crop = Crop(Image);
            var enhence = Enhance(Image);
            var surface=Surface(enhence);
            var blod=Blod(surface);
            FindLiner(blod, ref Images , ref Lenghts);
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
