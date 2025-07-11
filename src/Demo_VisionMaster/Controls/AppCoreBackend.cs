using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using IoTClient.Clients.Modbus;
using Demo_VisionMaster.Repostory;
using Demo_VisionMaster.Model;
using OpenCvSharp;
using Demo_VisionMaster.Services;
using static Demo_VisionMaster.Events.PlcEvent;
using Demo_VisionMaster.Views;

namespace Demo_VisionMaster.Controls
{
    public partial class AppCoreBackend
    {
        #region Base app to call
        private static AppCoreBackend _ins = new AppCoreBackend();
        public static AppCoreBackend Ins 
        {
            get 
            {
                return _ins == null ? _ins = new AppCoreBackend() : _ins;
            }
        }
        public AppCoreBackend()
        {
            var process = Process.GetProcessesByName($"{Assembly.GetEntryAssembly().GetName().Name}");
            if(process.Length > 1) 
            {
                process[1].Kill();
            }
        }
        #endregion


      
        public ROI_Information roiInfo { get; set; }
        public Generic roiRepo = new Generic();

        PlcService plc = new PlcService();
        CameraService cameraService = new CameraService();

        public void Initialize()
        {
            LoadDefaultConfig();
            plc.InitPLC();
            StartUI();

        }
        private void LoadDefaultConfig()
        {
            cameraService.init();
        }
        public double handlHardTrigger()
        {
            cameraService.RunService();
            var Value = cameraService.Lenghts;
            return Value;
        }
        public double handlHardTrigger(int index) 
        {
            ViewHome.Instance.EnableTrigger(index);
            return ViewHome.Instance.Line;
        }
        public void InitPlc() 
        {
            plc.RunProcess();
        }
        public void DeinitPlc() 
        {
            plc.Deinit();
        }
        

        
    }
}
