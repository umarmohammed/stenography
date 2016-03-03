using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Collections;
using System.Runtime.InteropServices;

namespace Stenography
{
    static class Util
    {
        public static void LoadImage(Image image)
        {
            OpenFileDialog fd = new OpenFileDialog();
            bool? result = fd.ShowDialog();

            if (result == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(fd.FileName));
                image.Source = bitmapImage;
            }
        }

        // Convert extension method convert a BitArray to an integer
        public static int ToInt(this BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];
        }

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        // Convert a System.Drawing.Bitmap to BitmapSource so that it can be 
        // used as a source for a WPF Image
        public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
        {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try
            {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, System.Windows.Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(ip);
            }

            return bs;
        }
    }
}
