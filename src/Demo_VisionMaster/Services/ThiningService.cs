using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XImgProc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionDesigner;

namespace Demo_VisionMaster.Services
{
    public class ThiningService
    {
        public double ResultLenght { get; set; }
        public System.Drawing.Bitmap Run(CMvdImage inputImage)
        {
            try
            {

                var image = LoadImage(inputImage);
                Mat OriginImage = image;
                double targetLeagth = 1800;
                var contour = FindSkelerion(image);
                Cv2.DrawContours(image, contour, -1, Scalar.Green, 1);
                var z = contour[0].Select(pt => new OpenCvSharp.Point(pt.X, pt.Y)).ToArray();
                z = z.OrderBy(pt => pt.X).ToArray();
                int idxMaxX = z.Length - 1;
                int idxMinX = 0;
                var pointMaxX = z[idxMaxX];
                var pointMinX = z[idxMinX];

                var contourArr = contour[0];
                var (contourRot, angel, origin) = RotateContour(contourArr, pointMinX, pointMaxX);
           

                var (left, right, top, bottom) = FindExtremePoints(contourRot.Select(pt => new OpenCvSharp.Point((int)Math.Round(pt.X), (int)Math.Round(pt.Y))).ToArray());
                double maxDistance = DistancePointToPoint(left, right);
                var pointMaxY = FindCurvePeak(new OpenCvSharp.Point((int)Math.Round(top.X), (int)Math.Round(top.Y)), new OpenCvSharp.Point((int)Math.Round(bottom.X), (int)Math.Round(bottom.Y)));

                Cv2.CvtColor(OriginImage, OriginImage, ColorConversionCodes.GRAY2BGR);
                Cv2.Circle(OriginImage, InvertRotatePoint(left, angel, origin).ToPoint(), 8, new Scalar(0, 0, 255), -1);
                Cv2.Circle(OriginImage, InvertRotatePoint(right, angel, origin).ToPoint(), 8, new Scalar(0, 255, 0), -1);
                Cv2.Circle(OriginImage, InvertRotatePoint(pointMaxY, angel, origin).ToPoint(), 8, new Scalar(255, 0, 0), -1);
             
               

                var (idx1, idx2) = FindParallelChordRotated(contourRot, targetLeagth, 1, 2, 500);

                if (idx1 != null)
                {

                    var pt1Rot = contourRot[idx1.Value];
                    var pt2Rot = contourRot[idx2.Value];
                    var pt1 = InvertRotatePoint(pt1Rot, angel, origin).ToPoint();
                    var pt2 = InvertRotatePoint(pt2Rot, angel, origin).ToPoint();
                    var p = InvertRotatePoint(pointMaxY, angel, origin).ToPoint();
                    var (start, end, lengthPix) = CreatePerpendicularFromPoint(p, pt1, pt2);
                    //check Farthest
                    //Helpers.CheckMaxDistancePoint checkMaxDistancePoint = new Helpers.CheckMaxDistancePoint();
                    //var (checkDistance, checkFarthestpoint) = checkMaxDistancePoint.MaxDistanceFromPointsToLine(contourArr, pointMinX, pointMaxX);
                    //Console.WriteLine("Angle:" + angel);
                    //Console.WriteLine("Distance" + lengthPix.ToString());
                    //Console.WriteLine(pointMinX.ToString()+"/"+ pointMaxX.ToString());  
                    //Console.WriteLine(pt1.ToString()+"/"+pt2.ToString());
                    //Console.WriteLine("Check Farthest:"+ checkFarthestpoint.ToString());
                    //Console.WriteLine("MaxY"+ p);
                    //Console.WriteLine("Hinh chieu:" + end);
                    //Cv2.Circle(OriginImage, checkFarthestpoint, 4, new Scalar(173, 255, 47), -1);
                    //Console.WriteLine("Check Distance" + checkDistance.ToString());
                    //Cv2.Circle(OriginImage, pointMinX, 4, new Scalar(200, 100, 100), -1);
                    //Cv2.Circle(OriginImage,pointMaxX, 4, new Scalar(100, 200, 200), -1);
                    // Check Farthest
                    Cv2.Line(OriginImage, start, end, new Scalar(255, 0, 255), 1);
                    ResultLenght = lengthPix;
                    Cv2.Line(OriginImage, pt1, pt2, new Scalar(0, 255, 0), 1);
                    Cv2.Line(OriginImage, new OpenCvSharp.Point(pointMaxX.X, pointMaxX.Y), new OpenCvSharp.Point(pointMinX.X, pointMinX.Y), new Scalar(255, 100, 0), 1);
                    var imageFinal = OriginImage.ToBitmap();
                    return imageFinal;
                }
                else
                {
                    Cv2.Circle(OriginImage, new OpenCvSharp.Point(pointMaxX.X, pointMaxX.Y), 4, new Scalar(0, 255, 0), -1);
                    Cv2.Circle(OriginImage, new OpenCvSharp.Point(pointMinX.X, pointMinX.Y), 4, new Scalar(0, 255, 0), -1);
                    Cv2.Line(OriginImage, new OpenCvSharp.Point(pointMaxX.X, pointMaxX.Y), new OpenCvSharp.Point(pointMinX.X, pointMinX.Y), new Scalar(0, 0, 255), 1);
                    Cv2.Resize(OriginImage, OriginImage, new OpenCvSharp.Size(1200, 700));
                    ResultLenght = 0;
                    var imageFinal = OriginImage.ToBitmap();
                    return imageFinal;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + "\r\n" + ex.StackTrace);
                return null;
            }
        }
        public Mat LoadImage(string Path)
        {
            return Cv2.ImRead(Path, ImreadModes.Grayscale);
        }
        public Mat LoadImage(CMvdImage image)
        {
            System.Drawing.Bitmap bitmap = null;
            bitmap = image.GetBitmap();
            return Helpers.CV_Clone.ConvertBitmapToMat(bitmap);

        }
        public Point[][] FindSkelerion(Mat img)
        {
            Mat ocv = new Mat();
            Cv2.Threshold(img, ocv, 200, 250, ThresholdTypes.Binary);
            CvXImgProc.Thinning(img, ocv, ThinningTypes.ZHANGSUEN);
            Point[][] contours;
            HierarchyIndex[] hierarchies;
            Cv2.FindContours(ocv, out contours, out hierarchies, RetrievalModes.External, ContourApproximationModes.ApproxNone);
            return contours;
        }

        public double DistancePointToPoint(Point2f pt1, Point2f pt2)
        {
            return Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X) + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y));
        }

        public Point FindCurvePeak(Point pt1, Point pt2)
        {
            return Math.Abs(pt1.Y) > Math.Abs(pt2.Y) ? pt1 : pt2;
        }

        public (Point2f[] ContourRot, double angle, Point2f A) RotateContour(Point[] contour, Point2f A, Point2f B)
        {
            var v = new Point2f(B.X - A.X, B.Y - A.Y);
            double angle = -Math.Atan2(v.Y, v.X);
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            //Console.WriteLine(cos.ToString()+ sin.ToString());
            Point2f[] contourRot = contour.Select(pt =>
            {
                var x = (pt.X - A.X);
                var y = (pt.Y - A.Y);
                return new Point2f
                (
                    x * cos - y * sin,
                    x * sin + y * cos
                );
            }).ToArray();
            return (contourRot, angle, A);
        }

        public Point2f InvertRotatePoint(Point2f pt, double angle, Point2f origin)
        {
            float cos = (float)Math.Cos(-angle);
            float sin = (float)Math.Sin(-angle);
            float x = pt.X * cos - pt.Y * sin + origin.X;
            float y = pt.X * sin + pt.Y * cos + origin.Y;
            return new Point2f(x, y);
        }

        public (int?, int?) FindParallelChordRotated(Point2f[] contourRot, double TargetLeagth, double yTolerance = 1, double leagthToLerance = 2, int maxGroupSize = 500)
        {
            try
            {
                var yVals = contourRot.Select(pt => (int)Math.Round(pt.Y / yTolerance)).ToArray();
                var yGroup = new Dictionary<int, List<int>>();
                for (int i = 0; i < yVals.Length; i++)
                {
                    if (!yGroup.ContainsKey(yVals[i]))
                        yGroup[yVals[i]] = new List<int>();
                    yGroup[yVals[i]].Add(i);
                }
                foreach (var indices in yGroup.Values)
                {
                    if (indices.Count < 2)
                        continue;
                    var localIndices = indices;
                    if (indices.Count > maxGroupSize)
                    {

                        var xs = indices.Select(i => contourRot[i].X).ToArray();
                        var sort = xs.Select((val, idx) => (val, idx)).OrderBy(i => i.val).Select(t => t.idx).ToList();
                        localIndices = sort.Take(10).Select(i => indices[i]).Concat(sort.Skip(sort.Count - 10).Select(i => indices[i])).ToList();
                    }
                    var LocalXs = localIndices.Select(i => contourRot[i].X).ToArray();
                    for (int i = 0; i < LocalXs.Length; i++)
                    {
                        for (int j = i + 1; j < LocalXs.Length; j++)
                        {
                            if (Math.Abs(Math.Abs(LocalXs[i] - LocalXs[j]) - TargetLeagth) < leagthToLerance)
                            {
                                return (localIndices[i], localIndices[j]);
                            }
                        }
                    }
                }
                return (null, null);
            }
            catch (Exception)
            {
                return (null, null);
            }

        }

        public (Point2f left, Point2f right, Point2f top, Point2f bottom) FindExtremePoints(Point[] Contour)
        {
            var pts = Contour.Select(pt => new Point2f(pt.X, pt.Y)).ToArray();
            var leftmost = pts.OrderBy(pt => pt.X).First();
            var rightmost = pts.OrderBy(pt => pt.X).Last();
            var topmost = pts.OrderBy(pt => pt.Y).First();
            var bottommost = pts.OrderBy(pt => pt.Y).Last();
            return (leftmost, rightmost, topmost, bottommost);

        }

        public (OpenCvSharp.Point start, OpenCvSharp.Point end, double lengthPixels) CreatePerpendicularFromPoint(OpenCvSharp.Point p, OpenCvSharp.Point pt1, OpenCvSharp.Point pt2)
        {
            // Vector đoạn pt1 → pt2
            double dx = pt2.X - pt1.X;
            double dy = pt2.Y - pt1.Y;

            double lengthSquared = dx * dx + dy * dy;
            if (lengthSquared == 0)
                throw new ArgumentException("Hai điểm đầu vào trùng nhau, không xác định được đường thẳng.");

            // Vector pt1 → p
            double px = p.X - pt1.X;
            double py = p.Y - pt1.Y;

            // Tính hệ số t để tìm hình chiếu vuông góc trên đoạn pt1 → pt2
            double t = (px * dx + py * dy) / lengthSquared;

            // Tìm điểm hình chiếu (điểm trên đoạn thẳng gần nhất với p)
            var proj = new OpenCvSharp.Point(
                (int)(pt1.X + t * dx),
                (int)(pt1.Y + t * dy)
            );

            double distance = Math.Sqrt(Math.Pow(proj.X - p.X, 2) + Math.Pow(proj.Y - p.Y, 2));
            return (p, proj, distance);

        }
    }
}
