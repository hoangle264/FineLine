using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo_VisionMaster.Views;
using VisionDesigner;
using VM.Core;
using VM.PlatformSDKCS;
using static Demo_VisionMaster.Enums.AppControlEnum;

namespace Demo_VisionMaster
{
    public partial class MainView : Form
    {
        #region Single Turn
        public static MainView _Instance = null;
        public static MainView Instance 
        {
            get 
            {
                if (_Instance == null) 
                {
                    _Instance = new MainView();
                }
                return _Instance;
            }
        }
        #endregion
        #region Call Child Form
        private Form CurrentForm;
        public eAppMuduleSupport OpenChildForm(eAppMuduleSupport module, Form childForm) 
        {
            bool Is_same_form = false;
            if (this.pnMain.Tag != null) 
            {
                if (this.pnMain.Tag is Tuple<eAppMuduleSupport,Form>) 
                {
                    Tuple<eAppMuduleSupport, Form> tuple = (Tuple<eAppMuduleSupport, Form>)(this.pnMain.Tag);
                    if(tuple.Item1 == module) 
                    {
                        Is_same_form = true;
                    }
                }
            }
            if (Is_same_form == false)
            {
                if (CurrentForm != null)
                {
                    CurrentForm.Visible = false;
                }
                this.pnMain.Tag = Tuple.Create(module, childForm);
                CurrentForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                childForm.BringToFront();
                this.pnMain.Controls.Add(childForm);
                childForm.Show();
                return module;
            }
            else 
            {
                //Exception skip
                return eAppMuduleSupport.None;
            }
        }
        #endregion

        private eAppMuduleSupport currentPage = eAppMuduleSupport.None;
        public MainView()
        {
            InitializeComponent();
        }

        private void btnExits_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
        private void MainView_Load(object sender, EventArgs e)
        {
            currentPage = OpenChildForm(eAppMuduleSupport.Home, ViewHome.Instance);
            pnClickHome.BackColor = Color.FromArgb(71, 139, 230);
            btnHome.BackColor = Color.FromArgb(43, 50, 59);
            btnSetting.BackColor = Color.FromArgb(33, 40, 48);
            btnCalib.BackColor = Color.FromArgb(33, 40, 48);
            btnCamera.BackColor = Color.FromArgb(33, 40, 48);

            btnHome.ForeColor = Color.FromArgb(60, 147, 227);
            btnCamera.ForeColor = Color.FromArgb(255, 255, 255);
            btnCalib.ForeColor = Color.FromArgb(255, 255, 255);
            btnSetting.ForeColor = Color.FromArgb(255, 255, 255);

            pnClickCamera.BackColor = Color.FromArgb(33, 40, 48);
            pnClickCalib.BackColor = Color.FromArgb(33, 40, 48);
            pnClickImage.BackColor = Color.FromArgb(33, 40, 48);
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            currentPage = OpenChildForm(eAppMuduleSupport.Home, ViewHome.Instance);
            pnClickHome.BackColor = Color.FromArgb(71, 139, 230);
            btnHome.BackColor = Color.FromArgb(43, 50, 59);
            btnSetting.BackColor = Color.FromArgb(33, 40, 48);
            btnCalib.BackColor = Color.FromArgb(33, 40, 48);
            btnCamera.BackColor = Color.FromArgb(33, 40, 48);

            btnHome.ForeColor = Color.FromArgb(60, 147, 227);
            btnCamera.ForeColor = Color.FromArgb(255,255,255);
            btnCalib.ForeColor = Color.FromArgb(255, 255, 255);
            btnSetting.ForeColor = Color.FromArgb(255, 255, 255);

            pnClickCamera.BackColor = Color.FromArgb(33, 40, 48);
            pnClickCalib.BackColor = Color.FromArgb(33, 40, 48);
            pnClickImage.BackColor = Color.FromArgb(33, 40, 48);

        }
        private async void btnSetting_Click(object sender, EventArgs e)
        {
            currentPage = OpenChildForm(eAppMuduleSupport.Setting, ViewImage.Instance);
            await ViewImage.Instance.AutoLoadding();
            pnClickImage.BackColor = Color.FromArgb(71, 139, 230);
            btnSetting.BackColor = Color.FromArgb(43, 50, 59);
            btnHome.BackColor = Color.FromArgb(33, 40, 48);
            btnCalib.BackColor = Color.FromArgb(33, 40, 48);
            btnCamera.BackColor = Color.FromArgb(33, 40, 48);

            btnSetting.ForeColor = Color.FromArgb(60, 147, 227);
            btnCamera.ForeColor = Color.FromArgb(255, 255, 255);
            btnCalib.ForeColor = Color.FromArgb(255, 255, 255);
            btnHome.ForeColor = Color.FromArgb(255, 255, 255);

            pnClickHome.BackColor = Color.FromArgb(33, 40, 48);
            pnClickCamera.BackColor = Color.FromArgb(33, 40, 48);
            pnClickCalib.BackColor = Color.FromArgb(33, 40, 48);

        }
        private void btnCalib_Click(object sender, EventArgs e)
        {
            currentPage = OpenChildForm(eAppMuduleSupport.Calibrate, ViewCalibrate.Instance);
            pnClickCalib.BackColor = Color.FromArgb(71, 139, 230);
            btnCalib.BackColor = Color.FromArgb(43, 50, 59);
            btnSetting.BackColor = Color.FromArgb(33, 40, 48);
            btnHome.BackColor = Color.FromArgb(33, 40, 48);
            btnCamera.BackColor = Color.FromArgb(33, 40, 48);

            btnCalib.ForeColor = Color.FromArgb(60, 147, 227);
            btnCamera.ForeColor = Color.FromArgb(255, 255, 255);
            btnSetting.ForeColor = Color.FromArgb(255, 255, 255);
            btnHome.ForeColor = Color.FromArgb(255, 255, 255);

            pnClickHome.BackColor = Color.FromArgb(33, 40, 48);
            pnClickCamera.BackColor = Color.FromArgb(33, 40, 48);
            pnClickImage.BackColor = Color.FromArgb(33, 40, 48);

        }
        private void btnCamera_Click(object sender, EventArgs e)
        {
            currentPage = OpenChildForm(eAppMuduleSupport.Camera, ViewCamera.Instance);
            pnClickCamera.BackColor = Color.FromArgb(71, 139, 230);
            btnCamera.BackColor = Color.FromArgb(43, 50, 59);
            btnSetting.BackColor = Color.FromArgb(33, 40, 48);
            btnCalib.BackColor = Color.FromArgb(33, 40, 48);
            btnHome.BackColor = Color.FromArgb(33, 40, 48);

            btnCamera.ForeColor = Color.FromArgb(60, 147, 227);
            btnSetting.ForeColor = Color.FromArgb(255, 255, 255);
            btnCalib.ForeColor = Color.FromArgb(255, 255, 255);
            btnHome.ForeColor = Color.FromArgb(255, 255, 255);

            pnClickHome.BackColor = Color.FromArgb(33, 40, 48);
            pnClickCalib.BackColor = Color.FromArgb(33, 40, 48);
            pnClickImage.BackColor = Color.FromArgb(33, 40, 48);

        }

        #region layout UI
        //----------------------------------Home--------------------------------------------
        private void btnHome_MouseEnter(object sender, EventArgs e)
        {
            pnClickHome.BackColor = Color.FromArgb(71, 139, 230);
        }

        private void btnHome_MouseLeave(object sender, EventArgs e)
        {
            if (currentPage != eAppMuduleSupport.Home) 
            {
                pnClickHome.BackColor = Color.FromArgb(33, 40, 48);
            }
        }
        //----------------------------------Camera--------------------------------------------
        private void btnCamera_MouseEnter(object sender, EventArgs e)
        {
            pnClickCamera.BackColor = Color.FromArgb(71, 139, 230);
        }

        private void btnCamera_MouseLeave(object sender, EventArgs e)
        {
            if (currentPage != eAppMuduleSupport.Camera)
            {
                pnClickCamera.BackColor = Color.FromArgb(33, 40, 48);
            }
        }
        //---------------------------------Calib---------------------------------------------
        private void btnCalib_MouseEnter(object sender, EventArgs e)
        {
            pnClickCalib.BackColor = Color.FromArgb(71, 139, 230);
        }
        private void btnCalib_MouseLeave(object sender, EventArgs e)
        {
            if (currentPage != eAppMuduleSupport.Calibrate)
            {
                pnClickCalib.BackColor = Color.FromArgb(33, 40, 48);
            }
        }
        //-----------------------------------Image-------------------------------------------
        private void btnSetting_MouseEnter(object sender, EventArgs e)
        {
            pnClickImage.BackColor = Color.FromArgb(71, 139, 230);
        }

        private void btnSetting_MouseLeave(object sender, EventArgs e)
        {
            if (currentPage != eAppMuduleSupport.Setting)
            {
                pnClickImage.BackColor = Color.FromArgb(33, 40, 48);
            }
        }

        #endregion


    }
}
