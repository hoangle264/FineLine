using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Demo_VisionMaster.Helpers
{
    public class CV_Clone
    {
        public static Mat ConvertBitmapToMat(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap), "Input Bitmap cannot be null.");
            }
            return BitmapConverter.ToMat(bitmap);
        }

        public static Bitmap ConvertMatToBitmap(Mat mat)
        {
            if (mat == null)
            {
                throw new ArgumentNullException(nameof(mat), "Input Mat cannot be null.");
            }
            return mat.ToBitmap();
        }
    }
}
