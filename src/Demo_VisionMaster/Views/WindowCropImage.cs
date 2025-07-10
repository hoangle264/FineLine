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
using Demo_VisionMaster.Controls;
using Demo_VisionMaster.Helpers;
using Demo_VisionMaster.Model;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using VisionDesigner;

namespace Demo_VisionMaster.Views
{
    public partial class WindowCropImage : Form
    {
        private System.Drawing.Point startPoint;
        private Rectangle selectionRect;
        private bool isDragging = false;
        public WindowCropImage()
        {
            InitializeComponent();
            pictureBoxOriginal.AllowDrop = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = e.Location;
            selectionRect = new Rectangle(startPoint, new System.Drawing.Size(0, 0));
            pictureBoxOriginal.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {   
                int width = e.Location.X - startPoint.X;
                int height = e.Location.Y - startPoint.Y;
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
            Rectangle transformSize = transformRectange(selectionRect);
            Rectangle cropArea = Rectangle.Intersect(transformSize, new Rectangle(System.Drawing.Point.Empty, src.Size));
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

        private void btnExport_Click(object sender, EventArgs e)
        {

            Rectangle rectangle=transformRectange(selectionRect);
            ROI_Information roi=new ROI_Information
            {
                X = rectangle.X,
                Y = rectangle.Y,
                Width = rectangle.Width,
                Height = rectangle.Height
            };


            System.Windows.Forms.SaveFileDialog fileDlg = null;
            FileStream fileStr = null;
            try
            {
                fileDlg = new System.Windows.Forms.SaveFileDialog();
                fileDlg.Filter = @"json Files(*.json)|*.json";
                fileDlg.RestoreDirectory = true;
                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filePath = fileDlg.FileName;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    /* Save parameters in local file as XML. */
                    AppCoreBackend.Ins.roiInfo = roi;
                    AppCoreBackend.Ins.roiRepo.Save(filePath, roi);
                    Properties.Settings.Default.FileCofigRoiRectang = filePath;
                    Properties.Settings.Default.Save();
                    fileStr.Dispose();
                }
                fileDlg.Dispose();
            }

            catch (System.Exception ex)
            {
               
            }
            finally
            {
                if (null != fileDlg)
                {
                    fileDlg.Dispose();
                }
                if (null != fileStr)
                {
                    fileStr.Dispose();
                }
            }

           
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
          
            ROI_Information roi =AppCoreBackend.Ins.roiRepo.Load("C:\\Users\\Dell\\Desktop\\VisionParket\\VisionParket\\Image\\roi_info.json");
            selectionRect.X = roi.X;
            selectionRect.Y = roi.Y;
            selectionRect.Width = roi.Width;
            selectionRect.Height = roi.Height;
        }
        private Rectangle transformRectange(Rectangle rect)
        {
            System.Drawing.Point startPoint = TranslateZoomMousePosition(new System.Drawing.Point(selectionRect.X, selectionRect.Y));
            System.Drawing.Point endPoint = TranslateZoomMousePosition(new System.Drawing.Point(selectionRect.X + selectionRect.Width, selectionRect.Y + selectionRect.Height));
            int transformWidth = endPoint.X - startPoint.X;
            int transformHeight = endPoint.Y - startPoint.Y;
            return new Rectangle(startPoint.X, startPoint.Y, transformWidth, transformHeight);
        }
        private System.Drawing.Point TranslateZoomMousePosition(System.Drawing.Point coordinates)
        {
            if (pictureBoxOriginal.Image == null) return coordinates;

            // Lấy kích thước thực của ảnh và PictureBox
            var imageSize = pictureBoxOriginal.Image.Size;
            var pbSize = pictureBoxOriginal.ClientSize;

            // Tính tỉ lệ scale
            float ratioWidth = (float)pbSize.Width / imageSize.Width;
            float ratioHeight = (float)pbSize.Height / imageSize.Height;
            float ratio = Math.Min(ratioWidth, ratioHeight);

            // Tính kích thước ảnh đã scale
            int scaledWidth = (int)(imageSize.Width * ratio);
            int scaledHeight = (int)(imageSize.Height * ratio);

            // Tính offset (viền đen)
            int offsetX = (pbSize.Width - scaledWidth) / 2;
            int offsetY = (pbSize.Height - scaledHeight) / 2;

            // Trừ offset và chia cho tỉ lệ để ra tọa độ trên ảnh gốc
            int x = (int)((coordinates.X - offsetX) / ratio);
            int y = (int)((coordinates.Y - offsetY) / ratio);

            // Clamp lại trong vùng ảnh
            x = Math.Max(0, Math.Min(imageSize.Width - 1, x));
            y = Math.Max(0, Math.Min(imageSize.Height - 1, y));

            return new System.Drawing.Point(x, y);
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog fileDlg = null;
            try
            {
                fileDlg = new System.Windows.Forms.OpenFileDialog();
                fileDlg.Filter = @"image(*.bmp;*.jpeg;*.jpg;*.png)|*.bmp;*.jpeg;*.jpg;*.png||";
                fileDlg.RestoreDirectory = true;

                if (fileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if(fileDlg.FileName!="")
                    pictureBoxOriginal.Image=Image.FromFile(fileDlg.FileName);
                }
                fileDlg.Dispose();

            }
            catch (MvdException ex)
            {

            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                if (null != fileDlg)
                {
                    fileDlg.Dispose();
                }
            }
        }
    }
    
}
