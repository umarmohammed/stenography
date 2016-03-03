//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Drawing;
//using System.Collections;
//using System.Windows.Media.Imaging;
//using System.Runtime.InteropServices;

//namespace Stenography.Image_Tools
//{
//    enum ImageOrientation
//    {
//        Landscape,
//        Portrait,
//        Square
//    }


//    static class ImageUtil
//    {
//        // TODO: move the resizing code to a seperate class
//        //1 Crops the targetImageBitmap so that it has the same aspect ratio defined by SourceImageSize.
//        //2 Resizes targetImageBitmap to scale * sourceImageSize
//        public static Bitmap CropAndResizeBitmap(Size sourceImageSize, Bitmap targetImageBitmap, double scale)
//        {
//            Size targetImageSize = targetImageBitmap.Size;
//            if (imageNeedsCropping(sourceImageSize, targetImageSize))
//            { 
//                targetImageBitmap = cropImage(sourceImageSize, targetImageBitmap);
//            }

//            return resizeBitmap(targetImageBitmap, scale);
//        }

//        // Convert extension method convert a BitArray to an integer
//        public static int ToInt(this BitArray bitArray)
//        {

//            if (bitArray.Length > 32)
//                throw new ArgumentException("Argument length shall be at most 32 bits.");

//            int[] array = new int[1];
//            bitArray.CopyTo(array, 0);
//            return array[0];

//        }

//        [DllImport("gdi32")]
//        static extern int DeleteObject(IntPtr o);

//        // Convert a System.Drawing.Bitmap to BitmapSource so that it can be 
//        // used as a source for a WPF Image
//        public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
//        {
//            IntPtr ip = source.GetHbitmap();
//            BitmapSource bs = null;
//            try
//            {
//                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
//                   IntPtr.Zero, System.Windows.Int32Rect.Empty,
//                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
//            }
//            finally
//            {
//                DeleteObject(ip);
//            }

//            return bs;
//        }

//        private static Bitmap resizeBitmap(Bitmap sourceBMP, double scale)
//        {
//            int width = Convert.ToInt32(sourceBMP.Size.Width * scale);
//            int height = Convert.ToInt32(sourceBMP.Size.Height * scale);
//            Bitmap result = new Bitmap(width, height);
//            using (Graphics g = Graphics.FromImage(result))
//                g.DrawImage(sourceBMP, 0, 0, width, height);
//            return result;
//        }

//        private static Bitmap cropAtRect(this Bitmap b, Rectangle r)
//        {
//            Bitmap nb = new Bitmap(r.Width, r.Height);
//            Graphics g = Graphics.FromImage(nb);
//            g.DrawImage(b, -r.X, -r.Y);
//            return nb;
//        }

//        private static ImageOrientation getOrientation(Size imageSize)
//        {
//            if (imageSize.Width > imageSize.Height)
//            {
//                return ImageOrientation.Landscape;
//            }

//            if (imageSize.Width == imageSize.Height)
//            {
//                return ImageOrientation.Square;
//            }

//            return ImageOrientation.Portrait;
//        }

//        private static double getAspectRatio(Size imageSize)
//        {
//            return (double)imageSize.Width / imageSize.Height;
//        }

//        private static Bitmap cropImage(Size sourceImageSize, Bitmap targetImageBitmap)
//        {
//            Size targetImageSize = targetImageBitmap.Size;
//            if (getOrientation(sourceImageSize) != getOrientation(targetImageSize))
//            {
//                throw new Exception("Image Orienatations must be the same");
//            }

//            if (isImageLarger(targetImageSize, sourceImageSize))
//            {
//                return targetImageBitmap.cropAtRect(new Rectangle(Point.Empty, sourceImageSize));
//            }

//            double sourceAspectRatio = getAspectRatio(sourceImageSize);
//            double targetAspectRatio = getAspectRatio(targetImageSize);

//            if (targetAspectRatio > sourceAspectRatio)
//            {
//                // TODO: Handle cropping width
//                // crop width
//                // check ratio
//                // while ratio not within tolerance
//                // crop height
//                // crop width
//                // check ratio ratio
//                return targetImageBitmap;
//            }

//            // TODO: handle cropping height
//            // crop height
//            // check ratio
//            // while ratio not within tolerance
//            // crop width
//            // crop height
//            // check ratio
//            return targetImageBitmap;
//        }

//        private static bool isImageLarger(Size image1Size, Size image2Size)
//        {
//            return image1Size.Height >= image2Size.Height && image1Size.Width >= image2Size.Width;
//        }

//        private static bool imageNeedsCropping(Size sourceImageSize, Size targetImageSize)
//        {
//            if (getOrientation(sourceImageSize) == ImageOrientation.Square && getOrientation(targetImageSize) == ImageOrientation.Square)
//            {
//                return false;
//            }

//            return sourceImageSize != targetImageSize;
//        }
//    }
//}
