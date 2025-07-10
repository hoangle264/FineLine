using Demo_VisionMaster.Model;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace Demo_VisionMaster.Views
{
    public partial class CropTestImage : Form
    {
        private Rectangle cropRect = new Rectangle(50, 50, 200, 150);
        private const int HANDLE_SIZE = 8;

        private enum DragHandle
        {
            None,
            Move,
            Rotate,
            TopLeft, Top, TopRight,
            Right, BottomRight, Bottom, BottomLeft,
            Left
        }

        private DragHandle currentHandle = DragHandle.None;
        private Dictionary<DragHandle, Rectangle> handleRects = new Dictionary<DragHandle, Rectangle>();
        private Point dragOffset;
        private Bitmap loadedBitmap;
        private string currentImagePath = "";

        public CropTestImage()
        {
            InitializeComponent();

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var kv in handleRects)
            {
                if (kv.Value.Contains(e.Location))
                {
                    currentHandle = kv.Key;
                    dragOffset = e.Location;
                    return;
                }
            }

            if (cropRect.Contains(e.Location))
            {
                currentHandle = DragHandle.Move;
                dragOffset = new Point(e.X - cropRect.X, e.Y - cropRect.Y);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentHandle == DragHandle.None) return;

            if (currentHandle == DragHandle.Move)
            {
                cropRect.X = e.X - dragOffset.X;
                cropRect.Y = e.Y - dragOffset.Y;
                pictureBox1.Invalidate();
                return;
            }

            if (currentHandle == DragHandle.Rotate)
            {
                // TODO: Nâng cao xử lý xoay nếu cần
                return;
            }

            // Resize:
            int dx = e.X - dragOffset.X;
            int dy = e.Y - dragOffset.Y;

            switch (currentHandle)
            {
                case DragHandle.TopLeft:
                    cropRect.X += dx; cropRect.Y += dy;
                    cropRect.Width -= dx; cropRect.Height -= dy;
                    break;
                case DragHandle.Top:
                    cropRect.Y += dy;
                    cropRect.Height -= dy;
                    break;
                case DragHandle.TopRight:
                    cropRect.Y += dy;
                    cropRect.Height -= dy;
                    cropRect.Width += dx;
                    break;
                case DragHandle.Right:
                    cropRect.Width += dx;
                    break;
                case DragHandle.BottomRight:
                    cropRect.Width += dx;
                    cropRect.Height += dy;
                    break;
                case DragHandle.Bottom:
                    cropRect.Height += dy;
                    break;
                case DragHandle.BottomLeft:
                    cropRect.X += dx;
                    cropRect.Width -= dx;
                    cropRect.Height += dy;
                    break;
                case DragHandle.Left:
                    cropRect.X += dx;
                    cropRect.Width -= dx;
                    break;
            }

            // Đảm bảo width/height > 0
            if (cropRect.Width < 10) cropRect.Width = 10;
            if (cropRect.Height < 10) cropRect.Height = 10;

            dragOffset = e.Location;
            pictureBox1.Invalidate();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            currentHandle = DragHandle.None;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (loadedBitmap == null) return;
            if (cropRect == Rectangle.Empty) return;

            using (Pen pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, cropRect);
            }

            handleRects.Clear();
            Point center = new Point(cropRect.X + cropRect.Width / 2, cropRect.Y + cropRect.Height / 2);

            void AddHandle(DragHandle handle, int x, int y)
            {
                handleRects[handle] = new Rectangle(x - HANDLE_SIZE / 2, y - HANDLE_SIZE / 2, HANDLE_SIZE, HANDLE_SIZE);
            }

            AddHandle(DragHandle.TopLeft, cropRect.Left, cropRect.Top);
            AddHandle(DragHandle.Top, center.X, cropRect.Top);
            AddHandle(DragHandle.TopRight, cropRect.Right, cropRect.Top);
            AddHandle(DragHandle.Right, cropRect.Right, center.Y);
            AddHandle(DragHandle.BottomRight, cropRect.Right, cropRect.Bottom);
            AddHandle(DragHandle.Bottom, center.X, cropRect.Bottom);
            AddHandle(DragHandle.BottomLeft, cropRect.Left, cropRect.Bottom);
            AddHandle(DragHandle.Left, cropRect.Left, center.Y);

            Point rotatePoint = new Point(center.X, cropRect.Top - 30);
            AddHandle(DragHandle.Rotate, rotatePoint.X, rotatePoint.Y);

            foreach (var rect in handleRects.Values)
            {
                e.Graphics.FillRectangle(Brushes.Blue, rect);
            }

            e.Graphics.DrawLine(Pens.Gray, center, rotatePoint);
        }

        

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    currentImagePath = ofd.FileName;
                    loadedBitmap = new Bitmap(currentImagePath);
                    pictureBox1.Image = loadedBitmap;
                }
            }
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            if (loadedBitmap == null || cropRect == Rectangle.Empty)
            {
                MessageBox.Show("Chưa có ảnh hoặc vùng crop.");
                return;
            }

            using (Mat src = Cv2.ImRead(currentImagePath))
            {
                float scaleX = (float)src.Width / pictureBox1.Width;
                float scaleY = (float)src.Height / pictureBox1.Height;

                Rect roi = new Rect(
                    (int)(cropRect.X * scaleX),
                    (int)(cropRect.Y * scaleY),
                    (int)(cropRect.Width * scaleX),
                    (int)(cropRect.Height * scaleY)
                );

                roi = roi.Intersect(new Rect(0, 0, src.Width, src.Height));

                Mat cropped = new Mat(src, roi);

                pictureBox2.Image = cropped.ToBitmap();
                //using (SaveFileDialog sfd = new SaveFileDialog())
                //{
                //    sfd.Filter = "Bitmap Image|*.bmp";
                //    if (sfd.ShowDialog() == DialogResult.OK)
                //    {
                //        Cv2.ImWrite(sfd.FileName, cropped);
                      
                //        MessageBox.Show("Đã crop và lưu ảnh.");
                //    }
                //}
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (!File.Exists("config.json"))
            {
                MessageBox.Show("Không tìm thấy config.json");
                return;
            }

            string json = File.ReadAllText("config.json");
            var config = JsonConvert.DeserializeObject<ROI_Information>(json);
            cropRect = new Rectangle(config.X, config.Y, config.Width, config.Height);
            pictureBox1.Invalidate();

            MessageBox.Show("Đã tải vùng crop từ config.json");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (cropRect == Rectangle.Empty)
            {
                MessageBox.Show("Vùng crop rỗng.");
                return;
            }

            var config = new ROI_Information
            {
                X = cropRect.X,
                Y = cropRect.Y,
                Width = cropRect.Width,
                Height = cropRect.Height
            };

            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText("config.json", json);
            MessageBox.Show("Đã lưu config.json");
        }
    }
}
