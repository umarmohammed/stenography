﻿using System;
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

namespace Stenography
{
    /// <summary>
    /// Interaction logic for DisplayImageWindow.xaml
    /// </summary>
    public partial class DisplayImageWindow : Window
    {
        // TODO: Arrange elements properly in the grid
        public DisplayImageWindow()
        {
            InitializeComponent();
        }

        private void loadImageButton_Click(object sender, RoutedEventArgs e)
        {
            Util.LoadImage(this.stegImage);
            hiddenImage.Source = stegImage.Source;

            HiddenImageExtractor hie = new HiddenImageExtractor((this.stegImage.Source as BitmapImage).UriSource.OriginalString);
            // TODO: make this code asynchronous
            System.Drawing.Bitmap b = hie.getHiddenImage();
            hiddenImage.Source = ImageUtil.loadBitmap(b);
        }
    }
}