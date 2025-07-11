using Demo_VisionMaster.Services;
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

namespace Demo_VisionMaster.UserControls
{
    public partial class CameraDisplayControl : UserControl
    {
        private Bitmap _image;

        public Bitmap Image
        {
            get { return _image; }
            set { _image = value; }
        }
        private double _lenght;

        public double Lenght
        {
            get { return _lenght; }
            set { _lenght = value; }
        }
        private bool _result;

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        private string _nameCamera;

        public string NameCamera
        {
            get { return _nameCamera; }
            set
            {
                _nameCamera = value;
                lbNameCamera.Text = _nameCamera;
            }
        }
        private bool _isconnect;

        public bool Connection
        {
            get { return _isconnect; }
            set
            {
                _isconnect = value;
                pbIsconnect.Image= drawLamp(_isconnect);
            }
        }
        private Bitmap drawLamp(bool connect)
        {
            int w = pbIsconnect.Width;
            int h = pbIsconnect.Height;
            int r=Math.Min(w, h);
            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                if (connect)
                    g.FillEllipse(new SolidBrush(Color.LimeGreen),0 , 0, r, r);
                else
                    g.FillEllipse(new SolidBrush(Color.Red), 0, 0, r, r);

            }
            return bmp;
        }

        public CameraDisplayControl()
        {
            InitializeComponent();
        }
        public void UpdateCameraControl(Bitmap image, double lenght, bool result)
        {
            _image = image;
            _lenght = lenght;
            _result = result;
            pbShowImage.Image = image;
            lbLenght.Text = lenght.ToString();
            if (result)
            {
                lbResult.Text = "OK";
                lbResult.ForeColor = Color.Green;
            }
            else
            {
                lbResult.Text = "NG";
                lbResult.ForeColor = Color.Red;
            }

        }
        CameraService service = new CameraService();
        private void pbIsconnect_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool EnableInit(string KeyName) 
        {
            string CameraName = System.Environment.GetEnvironmentVariable(KeyName);
            var Status = service.InitUC(CameraName);
            return Status;
        }
        public bool Stop(string KeyName) 
        {
            string CameraName = System.Environment.GetEnvironmentVariable(KeyName);
            var Status = service.StopServiceUC(CameraName);
            return Status;
        }
        public double Start() 
        {
            service.RunServiceUC();
            UpdateCameraControl(service.Bitmaps,service.Line,service.Status);
            return service.Line;
        }
    }
}
