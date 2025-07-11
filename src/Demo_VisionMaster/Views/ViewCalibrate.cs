using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System.Windows;
using Size = OpenCvSharp.Size;
using Demo_VisionMaster.Helpers;
using Demo_VisionMaster.Services;
using System.IO;

namespace Demo_VisionMaster.Views
{
    public partial class ViewCalibrate : Form
    {
        public ViewCalibrate()
        {
            InitializeComponent();
            listView1.View = View.List;
            txtFolderPath.Text = Properties.Settings.Default.FolderPathConfig;
            loadFile(txtFolderPath.Text);
        }
        public static ViewCalibrate _Instance = null;
        public static ViewCalibrate Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ViewCalibrate();
                }
                return _Instance;
            }
        }

        private void btnStreaming_Click(object sender, EventArgs e)
        {
            StartStreaming();


        }
        VideoCapture capture;
        Mat frame = new Mat();
        Timer timer = new Timer();
        string showCalibFile = "";

        private void StartStreaming()
        {
            capture = new VideoCapture(0);
            timer.Interval = 30;
            timer.Tick += (s, e) =>
            {
                capture.Read(frame);
                // if (!frame.Empty())
                // pictureBox1.Image = BitmapConverter.ToBitmap(frame);
            };
            timer.Start();
        }
        private void TriggerImage()
        {
            if (frame.Empty()) return;
            string path = $"captured_images/img_{DateTime.Now.Ticks}.jpg";
            Cv2.ImWrite(path, frame);
            // pictureBox1.Image = BitmapConverter.ToBitmap(frame.Clone());
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự không hợp lệ
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự không hợp lệ
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự không hợp lệ
            }
        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Chọn thư mục cần lấy đường dẫn";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    txtFolderPath.Text = selectedPath;
                    loadFile(selectedPath);
                    //save information
                    Properties.Settings.Default.FolderPathConfig = selectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                int width = Convert.ToInt16(txtWidth.Text);
                int height = Convert.ToInt16(txtHeight.Text);
                int size = Convert.ToInt16(txtSize.Text);
                string[] files = Directory.GetFiles(txtFolderPath.Text, "*.bmp");
                var list = files.ToList();
                CalibrateService calibrateService = new CalibrateService();
                calibrateService.CalibrateFromImage(list, new Size(width, height), size);
                txtCalibrateResult.Text=calibrateService.Ratio.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnStopStreaming_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void btnTrigger_Click(object sender, EventArgs e)
        {
            TriggerImage();
            loadFile(txtFolderPath.Text);
        }
        private void loadFile(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                // Xóa danh sách cũ
                listView1.Items.Clear();

                // Lấy danh sách file

                string[] files = Directory.GetFiles(folderPath, "*.*")
                                 .Where(f => f.EndsWith(".bmp") || f.EndsWith(".jpg") || f.EndsWith(".jpeg") || f.EndsWith(".png"))
                                 .ToArray();
                txtNumberImange.Text = files.Count().ToString();

                Array.Sort(files);
                foreach (string file in files)
                {
                    // Chỉ lấy tên file, không lấy đường dẫn đầy đủ
                    string fileName = Path.GetFileName(file);
                    listView1.Items.Add(fileName);
                }
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string filePath = listView1.SelectedItems[0].Text;
                try
                {
                    showCalibFile = filePath;
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Không thể mở ảnh: " + ex.Message);
                }
            }
        }

        private void btnShowImage_Click(object sender, EventArgs e)
        {
            if (showCalibFile != "")
            {
                try
                {
                    string imgPath = txtFolderPath.Text + "\\" + showCalibFile;
                    Mat img = Cv2.ImRead(imgPath, ImreadModes.Grayscale);
                    Cv2.ImShow("Image", img);
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
