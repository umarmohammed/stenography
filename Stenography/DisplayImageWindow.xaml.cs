using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Stenography
{
    /// <summary>
    /// Interaction logic for DisplayImageWindow.xaml
    /// </summary>
    public partial class DisplayImageWindow : Window
    {
        public DisplayImageWindow()
        {
            InitializeComponent();
        }

        private void loadImageButton_Click(object sender, RoutedEventArgs e)
        {
            loadImage(this.stegImage);
        }

        private void loadImage(Image image)
        {
            OpenFileDialog fd = new OpenFileDialog();
            bool? result = fd.ShowDialog();

            if (result == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(fd.FileName));
                image.Source = bitmapImage;
            }

            HiddenImageExtractor hie = new HiddenImageExtractor((this.stegImage.Source as BitmapImage).UriSource.OriginalString);
            System.Drawing.Bitmap b = hie.getHiddenImage();

            b.Save(@"C:\Users\umar\Pictures\wedding\hidden.jpg");
        }
    }
}
