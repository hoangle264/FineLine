using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using Size = OpenCvSharp.Size;

namespace Demo_VisionMaster.Services
{
    public class CalibrateService
    {
        public CalibrateService() { }

        private readonly List<Mat> _objPoint = new List<Mat>();
        private readonly List<Mat> _imgPoint = new List<Mat>();
        private Size _imgSize;
        private Mat _camMatrix;
        private Mat _dist;
        public float Ratio { get; set; }

        public void CalibrateFromImage(List<string> CalibImage, Size PatternSize, float squareSize) 
        {
            var objp = new List<Point3f>();
            for (int i = 0; i < PatternSize.Height; i++) 
            {
                for (int j = 0; j < PatternSize.Width; j++)
                {
                    objp.Add(new Point3f(j * squareSize, i * squareSize, 0));
                }
            }
            foreach (var Path in CalibImage) 
            {
                var img  = Cv2.ImRead(Path , ImreadModes.Color);
                Cv2.CvtColor(img, img, ColorConversionCodes.BGR2GRAY);
                _imgSize = img.Size();
                if (Helpers.CalibrateHelper.FindChessBoard(img, PatternSize, out Point2f[] corner)) 
                {
                    Cv2.CornerSubPix(img, corner, new Size(11,11), new Size(-1,-1), new TermCriteria(CriteriaTypes.Eps | CriteriaTypes.Count , 30 ,0.1));
                    _objPoint.Add(InputArray.Create(objp).GetMat());
                    _imgPoint.Add(InputArray.Create(corner).GetMat());

                    Cv2.DrawChessboardCorners(img, PatternSize, corner, true);
                    
                }
                else 
                {
                    continue;
                }
            }
            _camMatrix = new Mat();
            _dist = new Mat();
            Cv2.CalibrateCamera(_objPoint, _imgPoint, _imgSize, _camMatrix, _dist, out _, out _);
            Ratio = ComputeMillimeterPerPixel(squareSize);
           // Console.WriteLine($"Tỉ lệ mm/pixel: {ratio:F4}");

        }
        public Mat GetCameraMatrix() => _camMatrix;
        public Mat GetDist() => _dist;
        public float ComputeMillimeterPerPixel(float squareSize)
        {
            if (_imgPoint == null || _imgPoint.Count == 0)
                throw new Exception("Chưa có dữ liệu điểm ảnh để tính toán mm/pixel");

            // Lấy danh sách điểm ảnh (corners) từ ảnh đầu tiên đã hiệu chuẩn
            var imgPts = _imgPoint[0]; // Mat dạng N x 1, type = CV_32FC2
            Point2f[] points;
            imgPts.GetArray(out points); // ✅ This is the correct syntax

            if (points.Length < 2)
                throw new Exception("Không đủ điểm để tính khoảng cách pixel");

            // Tính khoảng cách pixel giữa 2 góc kề nhau trên bàn cờ
            float dx = points[1].X - points[0].X;
            float dy = points[1].Y - points[0].Y;
            float pixelDistance = (float)Math.Sqrt(dx * dx + dy * dy);

            // Tỉ lệ mm/pixel
            float mmPerPixel = squareSize / pixelDistance;
            return mmPerPixel;
        }

    }
}
