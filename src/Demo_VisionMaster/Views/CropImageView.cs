using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo_VisionMaster.Helpers;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Demo_VisionMaster.Views
{
    public partial class CropImageView : Form
    {
        private System.Drawing.Point startPoint;
        private Rectangle selectionRect;
        private bool isDragging = false;
        public CropImageView()
        {
            InitializeComponent();
            pictureBoxOriginal.AllowDrop = true;
            pictureBoxOriginal.MouseDown += pictureBox1_MouseDown;
            pictureBoxOriginal.MouseMove += pictureBox1_MouseMove;
            pictureBoxOriginal.MouseUp += pictureBox1_MouseUp;
            pictureBoxOriginal.Paint += pictureBox1_Paint;
            pictureBoxOriginal.DragEnter += pictureBoxOriginal_DragEnter;
            pictureBoxOriginal.DragDrop += pictureBoxOriginal_DragDrop;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = e.Location;
            selectionRect = new Rectangle(e.Location, new System.Drawing.Size(0, 0));
            pictureBoxOriginal.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int width = e.X - startPoint.X;
                int height = e.Y - startPoint.Y;
                selectionRect = new Rectangle(startPoint.X, startPoint.Y, width, height);
                pictureBoxOriginal.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            CropSelectedArea();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (isDragging && selectionRect.Width > 0 && selectionRect.Height > 0)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, selectionRect);
                }
            }
        }
        private void pictureBoxOriginal_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void pictureBoxOriginal_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
                pictureBoxOriginal.Image = Image.FromFile(files[0]);
        }
        private void CropSelectedArea()
        {
            if (pictureBoxOriginal.Image == null || selectionRect.Width <= 0 || selectionRect.Height <= 0)
                return;

            Bitmap src = new Bitmap(pictureBoxOriginal.Image);
            Rectangle cropArea = Rectangle.Intersect(selectionRect, new Rectangle(System.Drawing.Point.Empty, src.Size));
            Bitmap cropped = src.Clone(cropArea, src.PixelFormat);

            pictureBoxCropped.Image = cropped;

            // Lưu ảnh
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "BMP Image|*.bmp|PNG Image|*.png|JPEG Image|*.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format = ImageFormat.Bmp;
                if (sfd.FileName.EndsWith(".png")) format = ImageFormat.Png;
                else if (sfd.FileName.EndsWith(".jpg")) format = ImageFormat.Jpeg;

                cropped.Save(sfd.FileName, format);
                MessageBox.Show("Đã lưu ảnh thành công!");
                ResaveImage(sfd.FileName, sfd.FileName);
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
    }
    
}
