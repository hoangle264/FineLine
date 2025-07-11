﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo_VisionMaster.Helpers;
using Demo_VisionMaster.Z_TestClass;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Demo_VisionMaster.Views
{
    public partial class SettingForm : Form
    {
        public static SettingForm _Instance = null;
        public static SettingForm Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SettingForm();
                }
                return _Instance;
            }
        }
        public SettingForm()
        {
            InitializeComponent();
        }

        public void updatePbResultPic(Bitmap Image)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    updatePbResultPic(Image);
                }));
                return;
            }
            pbResultPic.Image = Image;    
        }

        private void btnEnhancement_Click(object sender, EventArgs e)
        {
            EnhancementConfigView view = new EnhancementConfigView();
            view.ShowDialog();
        }

        private void btnSurface_Click(object sender, EventArgs e)
        {
            SurfaceConfigView view = new SurfaceConfigView();
            view.ShowDialog();
        }

        private void btnBlod_Click(object sender, EventArgs e)
        {
            BlodConfigView view = new BlodConfigView();
            view.ShowDialog();
        }

        private void btnEnhan_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TestEnhanFuction test = new TestEnhanFuction();
            test.Init();
            test.Import("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\EnhancementConfig.xml");
            test.Run("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");
            stopwatch.Stop();
            var t = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(t.ToString());
        }

        private void btnSur_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TestSurfaceFucntion test = new TestSurfaceFucntion();
            test.Init();
            test.Import("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\SurfaceConfig.xml");
            test.Run("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");
            stopwatch.Stop();
            var t = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(t.ToString());
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TestBlodFunction test = new TestBlodFunction();
            test.Init();
            test.Import("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\BlodConfig.xml");
            test.Run("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");
            stopwatch.Stop();
            var t = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(t.ToString());
        }

        private void btnCropImage_Click(object sender, EventArgs e)
        {
            CropImageView view = new CropImageView();
            view.ShowDialog();
        }

        private void btnTestCrop_Click(object sender, EventArgs e)
        {
            string imageToProcessPath = @"C:\Users\Dell\Desktop\VisionParket\VisionParket\Image\Pic_20.bmp";
            string configFilePath = @"C:\Users\Dell\Desktop\VisionParket\VisionParket\Image\ImageCropperConfig.json";
            string outputImagePath = "C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp";
            string outputImagePath1 = "C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp1.bmp";
            if (!File.Exists(imageToProcessPath))
            {
                return;
            }
            if (!File.Exists(configFilePath))
            {
                return;
            }
            Mat croppedImage = null;
            try
            {
                croppedImage = ImageCropperHelper.CropImageWithConfig(imageToProcessPath, configFilePath);
                if (croppedImage != null)
                {
                    Cv2.ImWrite(outputImagePath, croppedImage);
                    Cv2.ImWrite(outputImagePath1, croppedImage);
                    var image = croppedImage.ToBitmap();
                    updatePbResultPic(image);
                    ResaveImage(outputImagePath, outputImagePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi không mong muốn trong quá trình cắt: {ex.Message}");
            }
            finally 
            {
               //todo
            }
          
        }
        private void ResaveImage(string inputPath, string outputPath)
        {
            Mat image = Cv2.ImRead(inputPath, ImreadModes.Grayscale);
            if (image.Empty())
            {
                Console.WriteLine("Không thể đọc ảnh từ: " + inputPath);
                return;
            }
            Cv2.ImWrite(outputPath, image);
            Console.WriteLine("Đã lưu ảnh thành công tại: " + outputPath);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Z_TestClass.TestThinningFunction test = new TestThinningFunction();
                var image = test.LoadImage("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\Temp.bmp");
                double targetLeagth = 1600;
                var contour = test.FindSkelerion(image);
                Cv2.DrawContours(image, contour, -1, Scalar.Green, 1);
                var z = contour[0].Select(pt => new OpenCvSharp.Point(pt.X, pt.Y)).ToArray();
                z = z.OrderBy(pt => pt.X).ToArray();
                int idxMaxX = 0;
                int idxMinX = z.Length - 1;
                var pointMaxX = z[idxMaxX];
                var pointMinX = z[idxMinX];

                var contourArr = contour[0];
                var (contourRot, angel, origin) = test.RotateContour(contourArr, pointMinX, pointMaxX);

                var (left, right, top, bottom) = test.FindExtremePoints(contourRot.Select(pt => new OpenCvSharp.Point((int)Math.Round(pt.X), (int)Math.Round(pt.Y))).ToArray());
                double maxDistance = test.DistancePointToPoint(left, right);
                var pointMaxY = test.FindCurvePeak(new OpenCvSharp.Point((int)Math.Round(top.X), (int)Math.Round(top.Y)), new OpenCvSharp.Point((int)Math.Round(bottom.X), (int)Math.Round(bottom.Y)));

                var OriginImage = test.LoadImage(@"C:\Users\Dell\Desktop\VisionParket\VisionParket\Image\Temp1.bmp");
                Cv2.CvtColor(OriginImage, OriginImage, ColorConversionCodes.GRAY2BGR); 
                Cv2.Circle(OriginImage, test.InvertRotatePoint(left, angel, origin).ToPoint(), 8, new Scalar(0, 0, 255), -1);
                Cv2.Circle(OriginImage, test.InvertRotatePoint(right, angel, origin).ToPoint(), 8, new Scalar(0, 255, 0), -1);
                Cv2.Circle(OriginImage, test.InvertRotatePoint(pointMaxY, angel, origin).ToPoint(), 8, new Scalar(255, 0, 0), -1);

                var (idx1, idx2) = test.FindParallelChordRotated(contourRot, targetLeagth, 2, 2, 500);

                if (idx1 != null)
                {
                    var pt1Rot = contourRot[idx1.Value];
                    var pt2Rot = contourRot[idx2.Value];
                    var pt1 = test.InvertRotatePoint(pt1Rot, angel, origin).ToPoint();
                    var pt2 = test.InvertRotatePoint(pt2Rot, angel, origin).ToPoint();

                    var p = test.InvertRotatePoint(pointMaxY, angel, origin).ToPoint();
                    var (start, end, lengthPix) = test.CreatePerpendicularFromPoint(p, pt1, pt2);

                    Cv2.Line(OriginImage, start, end, new Scalar(255, 0, 255), 1);
                    Console.WriteLine($"Chiều dài đoạn vuông góc là: {lengthPix:F2} pixels");

                    Cv2.Line(OriginImage, pt1, pt2, new Scalar(0, 255, 0), 1);
                    Cv2.Line(OriginImage, new OpenCvSharp.Point(pointMaxX.X, pointMaxX.Y), new OpenCvSharp.Point(pointMinX.X, pointMinX.Y), new Scalar(255, 100, 0), 1);
                    Cv2.Resize(OriginImage, OriginImage, new OpenCvSharp.Size(1200,700));
                    //Cv2.ImShow("OK", OriginImage);
                    var imageFinal = OriginImage.ToBitmap();
                    updatePbResultPic(imageFinal);
                    //Cv2.DestroyAllWindows();
                }
                else
                {
                    Cv2.Circle(OriginImage, new OpenCvSharp.Point(pointMaxX.X, pointMaxX.Y), 4, new Scalar(0, 255, 0), -1);
                    Cv2.Circle(OriginImage, new OpenCvSharp.Point(pointMinX.X, pointMinX.Y), 4, new Scalar(0, 255, 0), -1);
                    Cv2.Line(OriginImage, new OpenCvSharp.Point(pointMaxX.X, pointMaxX.Y), new OpenCvSharp.Point(pointMinX.X, pointMinX.Y), new Scalar(0, 0, 255), 1);
                    Cv2.Resize(OriginImage, OriginImage, new OpenCvSharp.Size(1200, 700));
                    //Cv2.ImShow("OK", OriginImage);
                    var imageFinal = OriginImage.ToBitmap();
                    updatePbResultPic(imageFinal);
                    //Cv2.DestroyAllWindows();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);  
            }
           
        }

        private void pbResultPic_Click(object sender, EventArgs e)
        {
            if (pbResultPic.Image != null)
            {
                ShowImageInFullScreen(pbResultPic.Image);
            }
        }
        private void ShowImageInFullScreen(Image image)
        {
            Form form = new Form();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.WindowState = FormWindowState.Maximized;
            form.BackColor = Color.Black;

            PictureBox pb = new PictureBox();
            pb.Image = image;
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.Dock = DockStyle.Fill;
            pb.BackColor = Color.Black;
            pb.Cursor = Cursors.Hand;
            pb.Click += (s, e) => form.Close(); // Click lần nữa để đóng

            form.Controls.Add(pb);
            form.ShowDialog();
        }
    }
}
