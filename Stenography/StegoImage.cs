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

        public enum Channel
        {
            R,
            G,
            B
        };
        
        public StegoImage(string visibleImageFilename, string hiddenImageFilename)
        {
            visibleImage = new Bitmap(visibleImageFilename);
            hiddenImage = new Bitmap(hiddenImageFilename);
        } 

        public void CreateStegImage()
        {
            hiddenImage = ImageUtil.CropAndResizeBitmap(visibleImage.Size, hiddenImage, 0.5);
            stegImage = new Bitmap(visibleImage.Width, visibleImage.Height);
         
            // TODO: Move all this code into a common Stenography Algorithm class
            PixelMapper pixelMapper = new PixelMapper(visibleImage.Size, hiddenImage.Size);

            foreach (PixelMap pm in pixelMapper.GetHiddenToVisiblePixelMapEnumerator())
            {
                setBits(stegImage, visibleImage, hiddenImage, pm);
            }
        }

        private void setBits(Bitmap stegImage, Bitmap VisibleImage, Bitmap hiddenImage, PixelMap pm)
        {
            foreach(PixelBitsMap pbm in pm.VisibleImagePoints)
            {
                BitArray r = copyChannel(VisibleImage, hiddenImage, pm.HiddenImagePoint, pbm, Channel.R);
                BitArray g = copyChannel(VisibleImage, hiddenImage, pm.HiddenImagePoint, pbm, Channel.G);
                BitArray b = copyChannel(VisibleImage, hiddenImage, pm.HiddenImagePoint, pbm, Channel.B);

                Color pixelColor = Color.FromArgb(r.ToInt(), g.ToInt(), b.ToInt());

                stegImage.SetPixel(pbm.Point.X, pbm.Point.Y, pixelColor);
            }
        }

        private BitArray copyChannel(Bitmap visibleImage, Bitmap hiddenImage, Point hiddenImagePoint, PixelBitsMap pbm, Channel c)
        {
            BitArray visibleChannel = getPixelBitArray(visibleImage, pbm.Point.X, pbm.Point.Y, c);
            BitArray hiddenChannel = getPixelBitArray(hiddenImage, hiddenImagePoint.X, hiddenImagePoint.Y, c);

            visibleChannel[0] = hiddenChannel[pbm.SourceBit1];
            visibleChannel[1] = hiddenChannel[pbm.SourceBit2];

            return visibleChannel;
        }

        private BitArray getPixelBitArray(Bitmap image, int x, int y, Channel c)
        {
            Color pixel = image.GetPixel(x, y);
            byte pixelChannel = 0;
            switch (c)
            {
                case Channel.R:
                    pixelChannel = pixel.R;
                    break;
                case Channel.G:
                    pixelChannel = pixel.G;
                    break;
                case Channel.B:
                    pixelChannel = pixel.B;
                    break;
                default:
                    pixelChannel = 0;
                    break;
            }

            byte[] tmp = { pixelChannel };

            return new BitArray(tmp);
        }

        public void SaveStegImage(string filename)
        {
            if (stegImage != null)
            {
                stegImage.Save(filename);
            }
            
        }
    }
}
