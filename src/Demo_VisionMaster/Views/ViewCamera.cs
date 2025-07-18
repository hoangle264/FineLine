﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionDesigner;
using VisionDesigner.Camera;

namespace Demo_VisionMaster.Views
{
    public partial class ViewCamera : Form
    {
        #region Single Turn
        public static ViewCamera _Instance = null;
        public static ViewCamera Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ViewCamera();
                }
                return _Instance;
            }
        }
        #endregion
        private string _keyCamera1 = "CAMERA_NAME_1";
        private string _keyCamera2 = "CAMERA_NAME_2";
        private string _keyCamera3 = "CAMERA_NAME_3";
        private string _keyCamera4 = "CAMERA_NAME_4";
        private string _keyPLCIP = "PLC_IP";
        private string _keyPort = "PLC_PORT";
        private string _KeyStartAddress = "MOSBUS_SERVER_ADDRESS";
        public ViewCamera()
        {
            InitializeComponent();
            txtNameCam1.Text = System.Environment.GetEnvironmentVariable(_keyCamera1);
            txtNameCam2.Text = System.Environment.GetEnvironmentVariable(_keyCamera2);
            txtNameCam3.Text = System.Environment.GetEnvironmentVariable(_keyCamera3);
            txtNameCam4.Text = System.Environment.GetEnvironmentVariable(_keyCamera4);
            txtPLCaddress.Text = System.Environment.GetEnvironmentVariable(_keyPLCIP);
            txtPLCPort.Text = System.Environment.GetEnvironmentVariable(_keyPort);
            txtPLCStartAddress.Text = System.Environment.GetEnvironmentVariable(_KeyStartAddress);
            _CameraTool = mvdCameraEdit1.GetSubject();
            _CameraTool.StartGrabbed += new EventHandler(this.StartDisplay);
            _CameraTool.StopGrabbing += new EventHandler(this.StopDisplay);
        }
        private CCameraTool _CameraTool = null;
        private Thread _hDisplayThread = null;
        private bool _bGrabbing = false;
        delegate void SetLabelTextDelegate(string text);
        private void mvdCameraEdit1_Load(object sender, EventArgs e)
        {

        }
        private void StartDisplay(object sender, EventArgs e)
        {
            if (null == _hDisplayThread)
            {
                _hDisplayThread = new Thread(DisplayThreadProcess);
                _hDisplayThread.Start();
            }
            _bGrabbing = true;
        }
        private void StopDisplay(object sender, EventArgs e)
        {
            try
            {
                _bGrabbing = false;
                if (null != _hDisplayThread)
                {
                    _hDisplayThread.Abort();
                    _hDisplayThread = null;
                }
                SetLabelText("");
                mvdRenderActivex1.ClearImages();
                mvdRenderActivex1.Display();
            }
            catch (Exception ex)
            { }
        }
        private void DisplayThreadProcess()
        {
            CMvdImage cFrameImage = new CMvdImage();
            bool bPixelFormatCheck = true;    // 是否需要检查像素格式
            bool bTimeout = false;    // 是否超时等待中
            while (_bGrabbing)
            {
                try
                {
                    _CameraTool.CameraGrabResult.GetOneFrameTimeout(ref cFrameImage);
                    if ((VisionDesigner.MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08 == cFrameImage.PixelFormat)
                        || (VisionDesigner.MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == cFrameImage.PixelFormat))
                    {
                        mvdRenderActivex1.LoadImageFromObject(cFrameImage);
                        mvdRenderActivex1.Display();
                        SetLabelText("");
                    }
                    else if (bPixelFormatCheck || bTimeout)
                    {
                        SetLabelText("Only MONO8 and RGB24_C3 are supported to display.");
                    }
                    bPixelFormatCheck = false;
                    bTimeout = false;
                }
                catch (MvdException ex)
                {
                    if (ex.ErrorCode != MVD_ERROR_CODE.MVD_CAM_E_NODATA)
                    {
                        SetLabelText("Get frame failed with 0x" + ex.ErrorCode.ToString("X"));
                        break;
                    }

                    if (false == bTimeout)
                    {
                        SetLabelText("No image was captured during timeout.");    // 超时未取到图，如触发模式未触发
                        bTimeout = true;
                    }
                }
                catch (Exception ex)
                {
                    break;
                }
            }
        }
        private void SetLabelText(string text)
        {
            //if (this.lbDisplayMsg.InvokeRequired)
            //{
            //    while (!this.lbDisplayMsg.IsHandleCreated)
            //    {
            //        // 窗体关闭时异常处理
            //        if (this.lbDisplayMsg.Disposing || this.lbDisplayMsg.IsDisposed)
            //        {
            //            return;
            //        }
            //    }
            //    this.lbDisplayMsg.Invoke(new SetLabelTextDelegate(SetLabelText), new object[] { text });
            //}
            //else
            //{
            //    this.lbDisplayMsg.Text = text;
            //}
        }
        private void CameraControlForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _CameraTool.CloseDevice();
                _bGrabbing = false;
                if (null != _hDisplayThread)
                {
                    _hDisplayThread.Join();
                }
            }
            catch (Exception ex)
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnPLCconnect_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable(_keyPLCIP, txtPLCaddress.Text);
            System.Environment.SetEnvironmentVariable(_keyPort, txtPLCPort.Text);
            System.Environment.SetEnvironmentVariable(_KeyStartAddress, txtPLCStartAddress.Text);
        }

        private void btnC1Saveinfo_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable(_keyCamera1, txtNameCam1.Text);
        }

        private void btnC2Saveinfo_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable(_keyCamera2, txtNameCam2.Text);
        }

        private void btnC3Saveinfo_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable(_keyCamera3, txtNameCam3.Text);
        }

        private void btnC4Saveinfo_Click(object sender, EventArgs e)
        {
            System.Environment.SetEnvironmentVariable(_keyCamera4, txtNameCam4.Text);
        }

        private void mvdCameraEdit1_Load_1(object sender, EventArgs e)
        {

        }
    }

}
