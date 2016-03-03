using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Stenography.Image_Tools
{
    struct PixelBitsMap
    {
        public Point Point { get; set; }
        public int SourceBit1 { get; set; }
        public int SourceBit2 { get; set; }

        public PixelBitsMap(Point point, int sourceBit1, int sourceBit2)
        {
            this.Point = point;
            this.SourceBit1 = sourceBit1;
            this.SourceBit2 = sourceBit2;
        }
    }

    class PixelMap
    {
        public Point HiddenImagePoint { get; set; }
        public PixelBitsMap[] VisibleImagePoints { get; set; }

        public PixelMap(int hiddenImagePointX, int hiddenImagePointY)
        {
            HiddenImagePoint       = new Point(hiddenImagePointX, hiddenImagePointY);

            Point topLeftPoint     = new Point(HiddenImagePoint.X * 2, HiddenImagePoint.Y * 2);

            Point topRightPoint    = topLeftPoint + new Size(0, 1);
            Point bottomLeftPoint  = topLeftPoint + new Size(1, 0);
            Point bottomRightPoint = topLeftPoint + new Size(1, 1);

            VisibleImagePoints = new PixelBitsMap[4] 
            {
                new PixelBitsMap(topLeftPoint,     0, 1),
                new PixelBitsMap(topRightPoint,    2, 3),
                new PixelBitsMap(bottomLeftPoint,  4, 5),
                new PixelBitsMap(bottomRightPoint, 6, 7)
            }; 
        }
    }
}
