using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace Demo_VisionMaster.Helpers
{
    public class CalibrateHelper
    {
        public static bool FindChessBoard(Mat image, Size patternSize, out Point2f[] conner) 
        {
            return Cv2.FindChessboardCorners(image, patternSize, out conner);
        }
        public static void DrawChessBoard(Mat image, Size patternSize,  Point2f[] conner)
        {
            Cv2.DrawChessboardCorners(image, patternSize, conner, true);
        }

    }
}
