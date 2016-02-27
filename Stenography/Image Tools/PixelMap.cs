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
        public Point point { get; set; }
        public int sourceBit1 { get; set; }
        public int sourceBit2 { get; set; }

        public PixelBitsMap(Point point, int sourceBit1, int sourceBit2)
        {
            this.point = point;
            this.sourceBit1 = sourceBit1;
            this.sourceBit2 = sourceBit2;
        }
    }

    class PixelMap
    {
        public Point hiddenImagePoint { get; set; }
        public PixelBitsMap[] visibleImagePoints { get; set; }

        public PixelMap(int hiddenImagePointX, int hiddenImagePointY)
        {
            hiddenImagePoint       = new Point(hiddenImagePointX, hiddenImagePointY);

            Point topLeftPoint     = new Point(hiddenImagePoint.X * 2, hiddenImagePoint.Y * 2);

            Point topRightPoint    = topLeftPoint + new Size(0, 1);
            Point bottomLeftPoint  = topLeftPoint + new Size(1, 0);
            Point bottomRightPoint = topLeftPoint + new Size(1, 1);

            visibleImagePoints = new PixelBitsMap[4] 
            {
                new PixelBitsMap(topLeftPoint,     0, 1),
                new PixelBitsMap(topRightPoint,    2, 3),
                new PixelBitsMap(bottomLeftPoint,  4, 5),
                new PixelBitsMap(bottomRightPoint, 6, 7)
            }; 
        }
    }
}
