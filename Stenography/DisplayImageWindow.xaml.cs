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
using Stenography.Image_Tools;
using Stenography.Stenography_Algorithm;
    
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

        private async void loadImageButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Investigate GDI error
            Util.LoadImage(this.stegImage);

            if (this.stegImage.Source == null)
            {
                return;
            }
            
            try
            {
                await extractImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task extractImage()
        {
            this.progressLabel.Content = "Please wait extracting image ...";
            string stegImageFilename = this.stegImage.GetImageFilename();
            System.Drawing.Bitmap hiddenBitmap = null;
            await Task.Run(() =>
            {
                try
                {
                    hiddenBitmap = StenographyAlgorithm.ExtractHiddenImage(stegImageFilename);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            if (hiddenBitmap == null)
            {
                this.progressLabel.Content = "Error";
                return;
            }
           
            hiddenImage.Source = Util.LoadBitmap(hiddenBitmap);
            this.progressLabel.Content = "Image extracted";
        }
    }
}
