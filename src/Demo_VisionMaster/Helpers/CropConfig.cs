using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Demo_VisionMaster.Helpers
{
    /// <summary>
    /// A serializable version of System.Drawing.Rectangle for JSON serialization.
    /// </summary>
    public class SerializableRectangle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public SerializableRectangle() { }

        public SerializableRectangle(Rectangle rect)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }
    }

    /// <summary>
    /// Represents the application's configuration, including last image path and current ROI.
    /// </summary>
    public class AppConfig
    {
        public string LastImagePath { get; set; }
        public SerializableRectangle CurrentSelectionRect { get; set; }

        public AppConfig()
        {
            LastImagePath = string.Empty;
            CurrentSelectionRect = new SerializableRectangle(Rectangle.Empty);
        }
    }
}
