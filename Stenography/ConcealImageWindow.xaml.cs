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

        private async void saveStegoImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.visibleImage.Source == null || this.hiddenImage.Source == null)
            {
                return;
            }

            try
            {
                SaveFileDialog fd = new SaveFileDialog();
                // TODO: handle other file formats
                fd.Filter = "bmp|*.bmp";
                bool? result = fd.ShowDialog();

                if (result == true)
                {
                    if (isExistingFilename(fd.FileName))
                    {
                        throw new InvalidOperationException("Cannot save to hidden or visible image filename");
                    }
                    await saveStegImage(fd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async Task saveStegImage(string filename)
        {
            this.progressLabel.Content = "Please wait creating Image ...";
            var visibleImageFilename = this.visibleImage.GetImageFilename();
            var hiddenImageFilename = this.hiddenImage.GetImageFilename();
            System.Drawing.Bitmap stegBitmap = null;
            await Task.Run(() =>
            {
                try
                {
                    stegBitmap = StenographyAlgorithm.EmbedImage(visibleImageFilename, hiddenImageFilename);
                    stegBitmap.Save(filename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            if (stegBitmap == null)
            {
                this.progressLabel.Content = "Error";
                return;
            }

            this.progressLabel.Content = "Image saved";
            
        }

        private bool isExistingFilename(string filename)
        {
            return filename == (this.visibleImage.Source as BitmapImage).UriSource.OriginalString || filename == (this.hiddenImage.Source as BitmapImage).UriSource.OriginalString;
        }
    }
}
