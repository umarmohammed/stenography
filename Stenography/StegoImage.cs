using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using Stenography.Image_Tools;

namespace Stenography
{
    class StegoImage
    {
        private Bitmap visibleImage;
        private Bitmap hiddenImage;
        private Bitmap stegImage;
        
        public StegoImage(string visibleImageFilename, string hiddenImageFilename)
        {
            visibleImage = new Bitmap(visibleImageFilename);
            hiddenImage = new Bitmap(hiddenImageFilename);
        } 

        public void createStegImage()
        {
            hiddenImage = ImageUtil.CropAndResizeBitmap(visibleImage.Size, hiddenImage, 0.25);
            stegImage = new Bitmap(hiddenImage.Width, hiddenImage.Height);
            for (int x = 0; x < stegImage.Width; ++x)
            {
                for (int y = 0; y < stegImage.Height; ++y)
                {
                    // TODO: Do stenography algorithm
                    stegImage.SetPixel(x, y, hiddenImage.GetPixel(x, y));
                   // Byte[] rVisible = { visibleImage.GetPixel(x, y).R };
                   // BitArray visiblePixelArrayR = new BitArray(rVisible);
                    //visibleImage.Size
                    
                }
            }
        }

        public void saveStegImage(string filename)
        {
            if (stegImage != null)
            {
                stegImage.Save(filename);
            }
            
        }
    }
}
