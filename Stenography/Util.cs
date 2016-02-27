using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

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
    }
}
