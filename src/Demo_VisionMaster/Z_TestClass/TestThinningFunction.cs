using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.XImgProc;

namespace Demo_VisionMaster.Z_TestClass
{
    public class TestThinningFunction
    {
        public Mat LoadImage(string Path) 
        {
            return Cv2.ImRead(Path, ImreadModes.Grayscale);
        }

        public Point[][] FindSkelerion(Mat img) 
        {
            Mat ocv = new Mat();
            Cv2.Threshold(img, ocv, 200, 250, ThresholdTypes.Binary);
            CvXImgProc.Thinning(img, ocv,ThinningTypes.ZHANGSUEN);
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

        public (Point2f[] ContourRot, double angle, Point2f A) RotateContour(Point[] contour, Point2f A, Point2f B ) 
        {
            var v = new Point2f(B.X - A.X, B.Y -A.Y);
            double angle = -Math.Atan2(v.Y, v.X);
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

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
