﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace Stenography.Image_Tools
{
    enum ImageOrientation
    {
        Landscape,
        Portrait,
        Square
    }


    static class ImageUtil
    {
        public static Bitmap CropAtRect(this Bitmap b, Rectangle r)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(nb);
            g.DrawImage(b, -r.X, -r.Y);
            return nb;
        }

        //1 Crops the targetImageBitmap so that it has the same aspect ratio defined by SourceImageSize.
        //2 Resizes targetImageBitmap to scale * sourceImageSize
        public static Bitmap CropAndResizeBitmap(Size sourceImageSize, Bitmap targetImageBitmap, double scale)
        {
            Size targetImageSize = targetImageBitmap.Size;
            if (imageNeedsCropping(sourceImageSize, targetImageSize))
            { 
                targetImageBitmap = cropImage(sourceImageSize, targetImageBitmap);
            }

            return ResizeBitmap(targetImageBitmap, scale);
        }

        private static Bitmap ResizeBitmap(Bitmap sourceBMP, double scale)
        {
            int width = Convert.ToInt32(sourceBMP.Size.Width * scale);
            int height = Convert.ToInt32(sourceBMP.Size.Height * scale);
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
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

            if (isImageLarger(targetImageSize, sourceImageSize))
            {
                return targetImageBitmap.CropAtRect(new Rectangle(Point.Empty, sourceImageSize));
            }

            double sourceAspectRatio = getAspectRatio(sourceImageSize);
            double targetAspectRatio = getAspectRatio(targetImageSize);

            if (targetAspectRatio > sourceAspectRatio)
            {
                // TODO: Handle cropping width
                // crop width
                // check ratio
                // while ratio not within tolerance
                // crop height
                // crop width
                // check ratio ratio
                return targetImageBitmap;
            }

            // TODO: handle cropping height
            // crop height
            // check ratio
            // while ratio not within tolerance
            // crop width
            // crop height
            // check ratio
            return targetImageBitmap;
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

        public static int toInt(this BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }
    }
}