using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using OpenCvSharp;


namespace Demo_VisionMaster.Helpers
{
    internal class CheckMaxDistancePoint
    {
        public (double MaxDistance, Point FarthestPoint) MaxDistanceFromPointsToLine(Point[] points, Point start, Point end)
        {
            if (points == null || points.Length == 0)
            {
                throw new ArgumentException("Points array cannot be empty");
            }

            double maxDistance = 0;
            Point farthestPoint = new Point();

            foreach (var point in points)
            {
                double distance = DistanceFromPointToLineSegment(point, start, end);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    farthestPoint = point;
                }
            }

            return (maxDistance, farthestPoint);
        }
        public double DistanceFromPointToLineSegment(Point point, Point start, Point end)
        {
            // Vector from line start to end
            double lineVectorX = end.X - start.X;
            double lineVectorY = end.Y - start.Y;

            // Vector from line start to point
            double pointVectorX = point.X - start.X;
            double pointVectorY = point.Y - start.Y;

            // Length of line segment squared
            double lineLengthSq = lineVectorX * lineVectorX + lineVectorY * lineVectorY;

            // Handle case where line segment is actually a point
            if (Math.Abs(lineLengthSq) < 1e-10) // Using epsilon for floating-point comparison
            {
                return Distance(point, start);
            }

            // Calculate projection scalar (dot product / lineLengthSq)
            double t = (pointVectorX * lineVectorX + pointVectorY * lineVectorY) / lineLengthSq;

            if (t < 0) // Projection falls before line segment
            {
                return Distance(point, start);
            }

            if (t > 1) // Projection falls after line segment
            {
                return Distance(point, end);
            }

            // Projection falls on line segment - calculate perpendicular distance
            Point projection = new Point(
                start.X + t * lineVectorX,
                start.Y + t * lineVectorY
            );

            return Distance(point, projection);
        }

        /// <summary>
        /// Calculates Euclidean distance between two points
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>The distance between the points</returns>
        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
