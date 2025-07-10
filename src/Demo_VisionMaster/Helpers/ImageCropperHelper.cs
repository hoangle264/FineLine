using System;
using System.Drawing;
using System.IO;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Newtonsoft.Json;

namespace Demo_VisionMaster.Helpers
{
    /// <summary>
    /// Helper class containing image processing and configuration management logic.
    /// </summary>
    public static class ImageCropperHelper
    {
        /// <summary>
        /// Loads an image from the specified path using OpenCVSharp.
        /// </summary>
        /// <param name="imagePath">The full path to the image file.</param>
        /// <returns>A Mat object containing the image, or null if loading fails.</returns>
        public static Mat LoadImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath)) return null;
            try { return Cv2.ImRead(imagePath, ImreadModes.Color); }
            catch (Exception ex) { Console.WriteLine($"Error loading image from {imagePath}: {ex.Message}"); return null; }
        }

        /// <summary>
        /// Crops a region of interest (ROI) from an original image.
        /// </summary>
        /// <param name="originalMat">The original image as an OpenCVSharp Mat object.</param>
        /// <param name="roiRect">The ROI rectangle in original image pixel coordinates.</param>
        /// <returns>A new Mat object with the cropped image, or null if cropping fails or ROI is invalid.</returns>
        public static Mat CropImage(Mat originalMat, Rectangle roiRect)
        {
            if (originalMat == null || originalMat.Empty() || roiRect.Width <= 0 || roiRect.Height <= 0) return null;

            // Basic boundary check for ROI within the original image dimensions
            if (roiRect.X < 0 || roiRect.Y < 0 ||
                roiRect.X + roiRect.Width > originalMat.Width ||
                roiRect.Y + roiRect.Height > originalMat.Height)
            {
                Console.WriteLine("ROI is out of bounds for the original image.");
                return null;
            }

            try { return new Mat(originalMat, new OpenCvSharp.Rect(roiRect.X, roiRect.Y, roiRect.Width, roiRect.Height)); }
            catch (Exception ex) { Console.WriteLine($"Error cropping image: {ex.Message}"); return null; }
        }

        /// <summary>
        /// Loads application configuration from a JSON file.
        /// </summary>
        /// <param name="configPath">The full path to the JSON config file.</param>
        /// <returns>The loaded AppConfig object, or null if loading fails.</returns>
        public static AppConfig LoadConfig(string configPath)
        {
            if (string.IsNullOrEmpty(configPath) || !File.Exists(configPath)) return null;
            try { return JsonConvert.DeserializeObject<AppConfig>(File.ReadAllText(configPath)); }
            catch (Exception ex) { Console.WriteLine($"Error loading config from {configPath}: {ex.Message}"); return null; }
        }

        /// <summary>
        /// Saves application configuration to a JSON file.
        /// </summary>
        /// <param name="config">The AppConfig object to save.</param>
        /// <param name="configPath">The full path to the JSON config file.</param>
        /// <returns>True if saving is successful, False otherwise.</returns>
        public static bool SaveConfig(AppConfig config, string configPath)
        {
            if (config == null || string.IsNullOrEmpty(configPath)) return false;
            try
            {
                string directory = Path.GetDirectoryName(configPath);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
                return true;
            }
            catch (Exception ex) { Console.WriteLine($"Error saving config to {configPath}: {ex.Message}"); return false; }
        }

        /// <summary>
        /// Converts a Rectangle from PictureBox display coordinates to original Mat image pixel coordinates.
        /// Accounts for PictureBoxSizeMode.Zoom.
        /// </summary>
        /// <param name="pbRect">The rectangle in PictureBox client coordinates.</param>
        /// <param name="originalMat">The original image Mat object.</param>
        /// <param name="pictureBoxSize">The ClientSize of the PictureBox.</param>
        /// <returns>A Rectangle representing the ROI in original Mat pixel coordinates.</returns>
        public static Rectangle ConvertPictureBoxToMatCoords(Rectangle pbRect, Mat originalMat, System.Drawing.Size pictureBoxSize)
        {
            if (originalMat == null || originalMat.Empty() || pbRect.Width <= 0 || pbRect.Height <= 0 || pictureBoxSize.Width <= 0 || pictureBoxSize.Height <= 0)
                return Rectangle.Empty;

            float originalWidth = originalMat.Width, originalHeight = originalMat.Height;
            float displayWidth = pictureBoxSize.Width, displayHeight = pictureBoxSize.Height;

            int imageX = 0, imageY = 0;
            int actualImageWidthInPb = (int)displayWidth, actualImageHeightInPb = (int)displayHeight;

            float imageRatio = originalWidth / originalHeight;
            float pbRatio = displayWidth / displayHeight;

            if (pbRatio > imageRatio) // PictureBox wider than image
            {
                actualImageHeightInPb = (int)displayHeight;
                actualImageWidthInPb = (int)(actualImageHeightInPb * imageRatio);
                imageX = (int)((displayWidth - actualImageWidthInPb) / 2);
            }
            else // PictureBox taller than image
            {
                actualImageWidthInPb = (int)displayWidth;
                actualImageHeightInPb = (int)(actualImageWidthInPb / imageRatio);
                imageY = (int)((displayHeight - actualImageHeightInPb) / 2);
            }

            int cropX = (int)((pbRect.X - imageX) * (originalWidth / actualImageWidthInPb));
            int cropY = (int)((pbRect.Y - imageY) * (originalHeight / actualImageHeightInPb));
            int cropWidth = (int)(pbRect.Width * (originalWidth / actualImageWidthInPb));
            int cropHeight = (int)(pbRect.Height * (originalHeight / actualImageHeightInPb));

            // Ensure coordinates are within image bounds
            cropX = Math.Max(0, cropX); cropY = Math.Max(0, cropY);
            cropWidth = Math.Min(originalMat.Width - cropX, cropWidth);
            cropHeight = Math.Min(originalMat.Height - cropY, cropHeight);

            return new Rectangle(cropX, cropY, cropWidth, cropHeight);
        }

        /// <summary>
        /// Converts a Rectangle from original Mat image pixel coordinates to PictureBox display coordinates.
        /// Accounts for PictureBoxSizeMode.Zoom.
        /// </summary>
        /// <param name="matRect">The rectangle in original Mat pixel coordinates.</param>
        /// <param name="originalMat">The original image Mat object.</param>
        /// <param name="pictureBoxSize">The ClientSize of the PictureBox.</param>
        /// <returns>A Rectangle representing the ROI in PictureBox client coordinates.</returns>
        public static Rectangle ConvertMatToPictureBoxCoords(Rectangle matRect, Mat originalMat, System.Drawing.Size pictureBoxSize)
        {
            if (originalMat == null || originalMat.Empty() || matRect.Width <= 0 || matRect.Height <= 0 || pictureBoxSize.Width <= 0 || pictureBoxSize.Height <= 0)
                return Rectangle.Empty;

            float originalWidth = originalMat.Width, originalHeight = originalMat.Height;
            float displayWidth = pictureBoxSize.Width, displayHeight = pictureBoxSize.Height;

            int imageX = 0, imageY = 0;
            int actualImageWidthInPb = (int)displayWidth, actualImageHeightInPb = (int)displayHeight;

            float imageRatio = originalWidth / originalHeight;
            float pbRatio = displayWidth / displayHeight;

            if (pbRatio > imageRatio)
            {
                actualImageHeightInPb = (int)displayHeight;
                actualImageWidthInPb = (int)(actualImageHeightInPb * imageRatio);
                imageX = (int)((displayWidth - actualImageWidthInPb) / 2);
            }
            else
            {
                actualImageWidthInPb = (int)displayWidth;
                actualImageHeightInPb = (int)(actualImageWidthInPb / imageRatio);
                imageY = (int)((displayHeight - actualImageHeightInPb) / 2);
            }

            int pbX = (int)(matRect.X * (actualImageWidthInPb / originalWidth)) + imageX;
            int pbY = (int)(matRect.Y * (actualImageHeightInPb / originalHeight)) + imageY;
            int pbWidth = (int)(matRect.Width * (actualImageWidthInPb / originalWidth));
            int pbHeight = (int)(matRect.Height * (actualImageHeightInPb / originalHeight));

            pbX = Math.Max(0, pbX); pbY = Math.Max(0, pbY);
            pbWidth = Math.Min(pictureBoxSize.Width - pbX, pbWidth);
            pbHeight = Math.Min(pictureBoxSize.Height - pbY, pbHeight);

            return new Rectangle(pbX, pbY, pbWidth, pbHeight);
        }

        /// <summary>
        /// Loads an image, applies the ROI from a specified config file, and returns the cropped image.
        /// This method is designed for external processes to utilize the cropping logic.
        /// </summary>
        /// <param name="imagePathToCrop">The full path to the image file that needs to be cropped.</param>
        /// <param name="configFilePath">The full path to the configuration JSON file containing the ROI.</param>
        /// <returns>A Mat object containing the cropped image. Returns null if any step fails (image load, config load, crop).</returns>
        public static Mat CropImageWithConfig(string imagePathToCrop, string configFilePath)
        {
            if (string.IsNullOrEmpty(imagePathToCrop) || !File.Exists(imagePathToCrop))
            {
                Console.WriteLine($"Error: Image path '{imagePathToCrop}' is invalid or does not exist.");
                return null;
            }

            if (string.IsNullOrEmpty(configFilePath) || !File.Exists(configFilePath))
            {
                Console.WriteLine($"Error: Config file path '{configFilePath}' is invalid or does not exist.");
                return null;
            }

            // 1. Tải ảnh gốc
            Mat originalMat = null;
            try
            {
                originalMat = LoadImage(imagePathToCrop);
                if (originalMat == null)
                {
                    Console.WriteLine($"Error: Could not load image from '{imagePathToCrop}'.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception loading image '{imagePathToCrop}': {ex.Message}");
                originalMat?.Dispose();
                return null;
            }

            // 2. Tải cấu hình
            AppConfig config = null;
            try
            {
                config = LoadConfig(configFilePath);
                if (config == null)
                {
                    Console.WriteLine($"Error: Could not load configuration from '{configFilePath}'.");
                    originalMat.Dispose();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception loading config '{configFilePath}': {ex.Message}");
                originalMat.Dispose();
                return null;
            }


            // 3. Lấy ROI từ cấu hình
            Rectangle roiRect = config.CurrentSelectionRect?.ToRectangle() ?? Rectangle.Empty;

            if (roiRect.IsEmpty || roiRect.Width <= 0 || roiRect.Height <= 0)
            {
                Console.WriteLine("Error: ROI from config is empty or invalid. Cannot crop.");
                originalMat.Dispose();
                return null;
            }

            // 4. Cắt ảnh
            Mat croppedMat = null;
            try
            {
                croppedMat = CropImage(originalMat, roiRect);
                if (croppedMat == null)
                {
                    Console.WriteLine($"Error: Could not crop image using ROI {roiRect} from config.");
                    return null; // Cropped Mat might be null if ROI is invalid/out of bounds
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception cropping image: {ex.Message}");
                croppedMat?.Dispose(); // Dispose if partially created
                return null;
            }
            finally
            {
                originalMat.Dispose(); // Luôn giải phóng ảnh gốc sau khi cắt xong
            }

            return croppedMat;
        }
    }
}
