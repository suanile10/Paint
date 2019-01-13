using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    [Serializable]
    public class ACircle : InterfaceShape
    {
        public int x { get; set; }
        public int y { get; set; }
        public int radius { get; set; }
        public bool isFilled { get; set; }
        public Color color { get; set; }

        public void Draw(Graphics g)
        {
            if (isFilled)
            {
                Brush br = new SolidBrush(color);
                g.FillEllipse(br, new Rectangle(x, y, radius, radius));
                br.Dispose();
            }
            else
            {
                Pen pen = new Pen(color, 1);
                g.DrawEllipse(pen, new Rectangle(x, y, radius, radius));
                pen.Dispose();
            }
        }

        public void SetParameter(int x, int y, int width, int height, Color color, bool isFilled, string text, Point pointX, Point pointY)
        {
            this.x = x;
            this.y = y;
            this.radius = width;
            this.color = color;
            this.isFilled = isFilled;
        }
    }

}
}
