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
using Stenography.Stenography_Algorithm;

namespace Stenography
{
    /// <summary>
    /// Interaction logic for ConcealImageWindow.xaml
    /// </summary>
    public partial class ConcealImageWindow : Window
    {
        public ConcealImageWindow()
        {
            InitializeComponent();
        }

        private void loadVisibleImageButton_Click(object sender, RoutedEventArgs e)
        {
            Util.LoadImage(this.visibleImage);
        }

        private void loadHiddenImageButton_Click(object sender, RoutedEventArgs e)
        {
            Util.LoadImage(this.hiddenImage);
        }

        private void saveStegoImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.visibleImage.Source == null || this.hiddenImage.Source == null)
            {
                return;
            }

            // TODO: make this code asynchronous
            try
            {
                SaveFileDialog fd = new SaveFileDialog();
                fd.Filter = "jpg|*.jpg|bmp|*.bmp|png|*.png";
                bool? result = fd.ShowDialog();

                if (result == true)
                {
                    if (isExistingFilename(fd.FileName))
                    {
                        throw new InvalidOperationException("Cannot save to hidden or visible image filename");
                    }
                    System.Drawing.Bitmap stegBitmap = StenographyAlgorithm.EmbedImage((this.visibleImage.Source as BitmapImage).UriSource.OriginalString, (this.hiddenImage.Source as BitmapImage).UriSource.OriginalString);
                    // TODO: save image format according to file extension
                    stegBitmap.Save(fd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private bool isExistingFilename(string filename)
        {
            return filename == (this.visibleImage.Source as BitmapImage).UriSource.OriginalString || filename == (this.hiddenImage.Source as BitmapImage).UriSource.OriginalString;
        }
    }
}
