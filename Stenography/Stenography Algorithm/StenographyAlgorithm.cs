using Stenography.Image_Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stenography.Stenography_Algorithm
{
    class StenographyAlgorithm
    {
        public enum Channel
        {
            R,
            G,
            B
        };

        public static Bitmap ExtractHiddenImage(string stegImageFilename)
        {
            Bitmap stegImage = new Bitmap(stegImageFilename);
            Bitmap hiddenImage = new Bitmap(stegImage.Size.Width / 2, stegImage.Size.Height / 2);

            PixelMapper pixelMapper = new PixelMapper(stegImage.Size, hiddenImage.Size);

            foreach (PixelMap pm in pixelMapper.GetHiddenToVisiblePixelMapEnumerator())
            {
                BitArray r = extractChannel(stegImage, hiddenImage, pm, Channel.R);
                BitArray g = extractChannel(stegImage, hiddenImage, pm, Channel.G);
                BitArray b = extractChannel(stegImage, hiddenImage, pm, Channel.B);

                Color pixelColor = Color.FromArgb(r.ToInt(), g.ToInt(), b.ToInt());

                hiddenImage.SetPixel(pm.HiddenImagePoint.X, pm.HiddenImagePoint.Y, pixelColor);
            }

            return hiddenImage;
        }

        public static Bitmap EmbedImage(string visibleImageFilename, string hiddenImageFilename)
        {
            Bitmap visibleImage = new Bitmap(visibleImageFilename);
            Bitmap hiddenImage = new Bitmap(hiddenImageFilename);

            // TODO: don't need to resize to a half if hidden image is less than a half
            hiddenImage = ImageResizer.CropAndResizeBitmap(visibleImage.Size, hiddenImage, 0.5);
            Bitmap stegImage = new Bitmap(visibleImage.Width, visibleImage.Height);

            PixelMapper pixelMapper = new PixelMapper(visibleImage.Size, hiddenImage.Size);

            foreach (PixelMap pm in pixelMapper.GetHiddenToVisiblePixelMapEnumerator())
            {
                foreach (PixelBitsMap pbm in pm.VisibleImagePoints)
                {
                    BitArray r = copyChannel(visibleImage, hiddenImage, pm.HiddenImagePoint, pbm, Channel.R);
                    BitArray g = copyChannel(visibleImage, hiddenImage, pm.HiddenImagePoint, pbm, Channel.G);
                    BitArray b = copyChannel(visibleImage, hiddenImage, pm.HiddenImagePoint, pbm, Channel.B);

                    Color pixelColor = Color.FromArgb(r.ToInt(), g.ToInt(), b.ToInt());

                    stegImage.SetPixel(pbm.Point.X, pbm.Point.Y, pixelColor);
                }
            }

            return stegImage;
        }

        private static BitArray copyChannel(Bitmap visibleImage, Bitmap hiddenImage, Point hiddenImagePoint, PixelBitsMap pbm, Channel c)
        {
            BitArray visibleChannel = getPixelBitArray(visibleImage, pbm.Point.X, pbm.Point.Y, c);
            BitArray hiddenChannel = getPixelBitArray(hiddenImage, hiddenImagePoint.X, hiddenImagePoint.Y, c);

            visibleChannel[0] = hiddenChannel[pbm.SourceBit1];
            visibleChannel[1] = hiddenChannel[pbm.SourceBit2];

            return visibleChannel;
        }

        private static BitArray extractChannel(Bitmap stegImage, Bitmap hiddenImage, PixelMap pm, Channel c)
        {
            BitArray hiddenChannel = new BitArray(8);

            foreach (PixelBitsMap pbm in pm.VisibleImagePoints)
            {
                BitArray stegChannel = getPixelBitArray(stegImage, pbm.Point.X, pbm.Point.Y, c);
                hiddenChannel[pbm.SourceBit1] = stegChannel[0];
                hiddenChannel[pbm.SourceBit2] = stegChannel[1];
            }

            return hiddenChannel;
        }

        private static BitArray getPixelBitArray(Bitmap image, int x, int y, Channel c)
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
    }
}
