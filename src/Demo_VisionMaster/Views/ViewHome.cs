using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Demo_VisionMaster.Controls;
using Demo_VisionMaster.Services;
using IoTClient.Clients.Modbus;
using VisionDesigner.Camera;
using static Demo_VisionMaster.Enums.LogEnum;

namespace Demo_VisionMaster.Views
{
    public partial class ViewHome : Form
    {
        #region Single Turn
        public static ViewHome _Instance = null;
        public static ViewHome Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ViewHome();
                }
                return _Instance;
            }
        }
        #endregion
        public ViewHome()
        {
            InitializeComponent();
            
        }
        Helpers.CameraHelper camera = new Helpers.CameraHelper();
        private void ViewHome_Load(object sender, EventArgs e)
        {
            btnStopApp.Enabled = false;
            var cleanupService = new LogCleanupService();
            var Status_Cam_1 = CameraDisplay1.EnableInit("CAMERA_NAME_1");
            UpdateStatusCam1(Status_Cam_1);

            var Status_Cam_2 = CameraDisplay2.EnableInit("CAMERA_NAME_2");
            UpdateStatusCam2(Status_Cam_2);

            var Status_Cam_3 = CameraDisplay3.EnableInit("CAMERA_NAME_3");
            UpdateStatusCam3(Status_Cam_3);

            var Status_Cam_4 = CameraDisplay4.EnableInit("CAMERA_NAME_4");
            UpdateStatusCam4(Status_Cam_4);

            cleanupService.CleanOldLogs();
        }

        public void UpdateStatusCam1(bool status) 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateStatusCam1(status);
                }));
                return;
            }
            CameraDisplay1.Connection = status;
        }
        public void UpdateStatusCam2(bool status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateStatusCam2(status);
                }));
                return;
            }
            CameraDisplay2.Connection = status;
        }
        public void UpdateStatusCam3(bool status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateStatusCam3(status);
                }));
                return;
            }
            CameraDisplay3.Connection = status;
        }
        public void UpdateStatusCam4(bool status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateStatusCam4(status);
                }));
                return;
            }
            CameraDisplay4.Connection = status;
        }
        public double Line { get; set; }

        public void EnableTrigger(int Index) 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    EnableTrigger(Index);
                }));
                return;
            }
            switch (Index)
            {
                case 0:
                    Line = CameraDisplay1.Start();
                    return;
                case 1:
                    Line = CameraDisplay2.Start();
                    return;
                case 2:
                    Line = CameraDisplay3.Start();
                    return;
                case 3:
                    Line = CameraDisplay4.Start();
                    return;
                default:
                    break;
            }
        }

        public void UpdateAppStatus(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateAppStatus(status);
                }));
                return;
            }
           // txtAppStatus.Text = status;
        }
        public void UpdateListDevice(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateAppStatus(status);
                }));
                return;
            }
           // cbListDevice.Items.Add(status);
        }
        public void RefectListDevice()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    RefectListDevice();
                }));
                return;
            }
          //  cbListDevice.Items.Clear();
        }
        public void DefautListDevice()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    DefautListDevice();
                }));
                return;
            }
          //  cbListDevice.SelectedIndex = 0;
        }
        public void UpdateNotification(string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateNotification(status);
                }));
                return;
            }
          //  txtAppStatus.Text += status;
        }
        public void UpdateMainPicture(Bitmap image)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateMainPicture(image);
                }));
                return;
            }
         // pbMain.Image = image;

        }
        private void btnTrigger_Click(object sender, EventArgs e)
        {
            camera.GetListDevice();
        }

        private async void btnInitApp_Click_1(object sender, EventArgs e)
        {
            try
            {
                AppCoreBackend.Ins.InitPlc();
                LoggerService.Info("Run main process!");
                btnTrigger.Enabled = false;
                btnInitApp.Enabled = false;
                btnStopApp.Enabled = true;
                btnRefest.Enabled = false;
            }
            catch (Exception ex)
            {
                UpdateAppStatus(ex.Message + "\r\n" + ex.StackTrace);
                LoggerService.Error(ex.ToString());
            }
        }

        private void btnStopApp_Click_1(object sender, EventArgs e)
        {
            AppCoreBackend.Ins.DeinitPlc();
            LoggerService.Info("Stop main process!");
            btnTrigger.Enabled = true;
            btnInitApp.Enabled = true;
            btnStopApp.Enabled = false;
            btnRefest.Enabled = true;
        }

        private void btnTrigger_Click_1(object sender, EventArgs e)
        {
            camera.GetListDevice();
        }

        private void btnRefest_Click(object sender, EventArgs e)
        {
            try
            {
                AppCoreBackend.Ins.DeinitPlc();

                var Status_Cam_1 = CameraDisplay1.Stop("CAMERA_NAME_1");
                var Status_Cam_2 = CameraDisplay2.Stop("CAMERA_NAME_2");
                var Status_Cam_3 = CameraDisplay3.Stop("CAMERA_NAME_3");
                var Status_Cam_4 = CameraDisplay4.Stop("CAMERA_NAME_4");
                UpdateStatusCam1(Status_Cam_1);
                UpdateStatusCam2(Status_Cam_2);
                UpdateStatusCam3(Status_Cam_3);
                UpdateStatusCam4(Status_Cam_4);

                var Status_Cam_11 = CameraDisplay1.EnableInit("CAMERA_NAME_1");
                var Status_Cam_21 = CameraDisplay2.EnableInit("CAMERA_NAME_2");
                var Status_Cam_31 = CameraDisplay3.EnableInit("CAMERA_NAME_3");
                var Status_Cam_41 = CameraDisplay4.EnableInit("CAMERA_NAME_4");
                UpdateStatusCam1(Status_Cam_11);
                UpdateStatusCam2(Status_Cam_21);
                UpdateStatusCam3(Status_Cam_31);
                UpdateStatusCam4(Status_Cam_41);
            }
            catch(Exception ex)
            {
                return;
            }

            


        }
    }
}
