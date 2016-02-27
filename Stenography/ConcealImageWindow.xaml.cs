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
    /// Interaction logic for ConcealImageWindow.xaml
    /// </summary>
    public partial class ConcealImageWindow : Window
    {
        // TODO: Arrange elements properly in the grid.
        public ConcealImageWindow()
        {
            InitializeComponent();
        }

        private void loadVisibleImageButton_Click(object sender, RoutedEventArgs e)
        {
            loadImage(this.visibleImage);
        }

        private void loadHiddenImageButton_Click(object sender, RoutedEventArgs e)
        {
            loadImage(this.hiddenImage);
        }

        // TODO: tidy up use Util.LoadImage
        private void loadImage(Image image)
        {
            OpenFileDialog fd = new OpenFileDialog();
            bool? result = fd.ShowDialog();

            if (result == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(fd.FileName));
                image.Source = bitmapImage;
            }
        }

        private void saveStegoImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.visibleImage.Source == null || this.hiddenImage.Source == null)
            {
                return;
            }

            // TODO: make this code asynchronous
            StegoImage stegImage = new StegoImage((this.visibleImage.Source as BitmapImage).UriSource.OriginalString, (this.hiddenImage.Source as BitmapImage).UriSource.OriginalString);
            SaveFileDialog fd = new SaveFileDialog();
            bool? result = fd.ShowDialog();

            if (result == true)
            {
                stegImage.createStegImage();
                stegImage.saveStegImage(fd.FileName);
            }
        }
    }
}
