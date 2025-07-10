using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            cleanupService.CleanOldLogs();
        }

        private void btnInitApp_Click(object sender, EventArgs e)
        {
            try
            {
                AppCoreBackend.Ins.InitPlc();
                LoggerService.Info("Run main process!");
                btnTrigger.Enabled = false;
                btnInitApp.Enabled = false;
                btnStopApp.Enabled = true;
                //groupBox1.Enabled = false;

            }
            catch (Exception ex)
            {
                UpdateAppStatus(ex.Message + "\r\n" + ex.StackTrace);
                LoggerService.Error(ex.ToString());
            }
         
        }
        private void btnStopApp_Click(object sender, EventArgs e)
        {
            AppCoreBackend.Ins.DeinitPlc();
            LoggerService.Info("Stop main process!");
            btnTrigger.Enabled = true;
            btnInitApp.Enabled = true;
            btnStopApp.Enabled = false;
            //groupBox1.Enabled = true;
        }
        public void UpdatePlcStatus(bool status) 
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdatePlcStatus(status);
                }));
                return;
            }
            if (status) 
            {
               // pnPlcStatus.BackColor = Color.LimeGreen; 
            }
            else 
            {
               // pnPlcStatus.BackColor= Color.Red;
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
            //camera.SelectDevice(1);
            //camera.openCamera();
            //var image = camera.GetImage();
            //var bitmap = image.GetBitmap();
            //pbMain.Image = bitmap;
        }

        private void btnInitApp_Click_1(object sender, EventArgs e)
        {
            try
            {
                AppCoreBackend.Ins.InitPlc();
                LoggerService.Info("Run main process!");
                btnTrigger.Enabled = false;
                btnInitApp.Enabled = false;
                btnStopApp.Enabled = true;
                //groupBox1.Enabled = false;

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
            //groupBox1.Enabled = true;
        }

        private void btnTrigger_Click_1(object sender, EventArgs e)
        {
            camera.GetListDevice();
        }
    }
}
