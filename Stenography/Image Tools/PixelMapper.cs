using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Stenography.Image_Tools
{
    class PixelMapper 
    {
        private PixelMap[] pixelMaps;
        private Size hiddenImageSize;
       
        public PixelMapper(Size visibleImageSize, Size hiddenImageSize)
        {
            this.hiddenImageSize = hiddenImageSize;
            pixelMaps = new PixelMap[hiddenImageSize.Height * hiddenImageSize.Width];
            mapPixels();
        }

        private void mapPixels()
        {
            int pixelMapIndex = 0;
            for (int x = 0; x <  hiddenImageSize.Width; x++)
            {
                for (int y = 0; y < hiddenImageSize.Height; y++)
                {
                    pixelMaps[pixelMapIndex++] = new PixelMap(x, y);
                }
            }
        }

        public IEnumerable<PixelMap> getHiddenToVisiblePixelMapEnumerator()
        {
            for (int i = 0; i < pixelMaps.Count(); i++)
            {
                yield return pixelMaps[i];
            }
        }

    }
}
