using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public interface InterfaceShape
    {
        void Draw(Graphics g);
        void SetParameter(int x, int y, int width, int height, Color color, bool isFilled, string text, Point pointX, Point pointY);
    }
}
