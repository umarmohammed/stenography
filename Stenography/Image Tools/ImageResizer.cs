using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stenography.Image_Tools
{
    enum ImageOrientation
    {
        Landscape,
        Portrait,
        Square
    }

    static class ImageResizer
    {
        //1 Crops the targetImageBitmap so that it has the same aspect ratio defined by SourceImageSize.
        //2 Resizes targetImageBitmap to scale * sourceImageSize
        public static Bitmap CropAndResizeBitmap(Size sourceImageSize, Bitmap targetImageBitmap, double scale)
        {
            Size targetImageSize = targetImageBitmap.Size;
            if (imageNeedsCropping(sourceImageSize, targetImageSize))
            {
                targetImageBitmap = cropImage(sourceImageSize, targetImageBitmap);
            }

            return resizeBitmap(sourceImageSize, targetImageBitmap, scale);
        }

        private static Bitmap resizeBitmap(Size sourceImageSize, Bitmap sourceBMP, double scale)
        {
            int width = Convert.ToInt32(sourceImageSize.Width * scale);
            int height = Convert.ToInt32(sourceImageSize.Height * scale);
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        private static Bitmap cropAtRect(this Bitmap b, Rectangle r)
        {
            Bitmap nb = new Bitmap(b);
            return nb.Clone(r, nb.PixelFormat);
        }

        private static ImageOrientation getOrientation(Size imageSize)
        {
            if (imageSize.Width > imageSize.Height)
            {
                return ImageOrientation.Landscape;
            }

            if (imageSize.Width == imageSize.Height)
            {
                return ImageOrientation.Square;
            }

            return ImageOrientation.Portrait;
        }

        private static double getAspectRatio(Size imageSize)
        {
            return (double)imageSize.Width / imageSize.Height;
        }

        private static Bitmap cropImage(Size sourceImageSize, Bitmap targetImageBitmap)
        {
            Size targetImageSize = targetImageBitmap.Size;
            if (getOrientation(sourceImageSize) != getOrientation(targetImageSize))
            {
                throw new Exception("Image Orienatations must be the same");
            }

            return targetImageBitmap.cropAtRect(getCropRectangle(sourceImageSize, targetImageSize));
        }

        private static Rectangle getCropRectangle(Size sourceImageSize, Size targetImageSize)
        {
            double sourceAspectRatio = getAspectRatio(sourceImageSize);
            double targetAspectRatio = getAspectRatio(targetImageSize);

            int rectWidth  = targetImageSize.Width;
            int rectHeight = targetImageSize.Height;
            if (targetAspectRatio > sourceAspectRatio)
            {
                rectWidth = Convert.ToInt32(targetImageSize.Height * sourceAspectRatio);
            }
            if (targetAspectRatio < sourceAspectRatio)
            {
                rectHeight = Convert.ToInt32(targetImageSize.Width / sourceAspectRatio);
            }

            return new Rectangle(0, 0, rectWidth, rectHeight);
        }

        private static bool isImageLarger(Size image1Size, Size image2Size)
        {
            return image1Size.Height >= image2Size.Height && image1Size.Width >= image2Size.Width;
        }

        private static bool imageNeedsCropping(Size sourceImageSize, Size targetImageSize)
        {
            if (getOrientation(sourceImageSize) == ImageOrientation.Square && getOrientation(targetImageSize) == ImageOrientation.Square)
            {
                return false;
            }

            return sourceImageSize != targetImageSize;
        }
    }
}
