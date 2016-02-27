using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stenography.Image_Tools;
using System.Collections;

namespace Stenography
{
    class HiddenImageExtractor
    {
        private Bitmap stegImage;


        public HiddenImageExtractor(string stegImageFilename)
        {
            stegImage = new Bitmap(stegImageFilename);
        }

        public Bitmap getHiddenImage()
        {
            Bitmap hiddenImage = new Bitmap(stegImage.Size.Width / 2, stegImage.Size.Height / 2);

            // TODO: move this code into a common StenographyAlgorithm class
            PixelMapper pixelMapper = new PixelMapper(stegImage.Size, hiddenImage.Size);

            foreach (PixelMap pm in pixelMapper.getHiddenToVisiblePixelMapEnumerator())
            {
                BitArray r = extractChannel(stegImage, hiddenImage, pm, StegoImage.Channel.R);
                BitArray g = extractChannel(stegImage, hiddenImage, pm, StegoImage.Channel.G);
                BitArray b = extractChannel(stegImage, hiddenImage, pm, StegoImage.Channel.B);

                Color pixelColor = Color.FromArgb(r.toInt(), g.toInt(), b.toInt());

                hiddenImage.SetPixel(pm.hiddenImagePoint.X, pm.hiddenImagePoint.Y, pixelColor);
            }

            return hiddenImage;
        }

        private BitArray extractChannel(Bitmap stegImage, Bitmap hiddenImage, PixelMap pm, StegoImage.Channel c)
        {
            BitArray hiddenChannel = new BitArray(8);

            foreach(PixelBitsMap pbm in pm.visibleImagePoints)
            {
                BitArray stegChannel = getPixelBitArray(stegImage, pbm.point.X, pbm.point.Y, c);
                hiddenChannel[pbm.sourceBit1] = stegChannel[0];
                hiddenChannel[pbm.sourceBit2] = stegChannel[1];
            }

            return hiddenChannel;
        }

        private BitArray getPixelBitArray(Bitmap image, int x, int y, StegoImage.Channel c)
        {
            Color pixel = image.GetPixel(x, y);
            byte pixelChannel = 0;
            switch (c)
            {
                case StegoImage.Channel.R:
                    pixelChannel = pixel.R;
                    break;
                case StegoImage.Channel.G:
                    pixelChannel = pixel.G;
                    break;
                case StegoImage.Channel.B:
                    pixelChannel = pixel.B;
                    break;
                default:
                    pixelChannel = 0;
                    break;
            }

            byte[] tmp = { pixelChannel };

            return new BitArray(tmp);
        }
    }
}
