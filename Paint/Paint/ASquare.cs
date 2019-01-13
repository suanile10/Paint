using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
   public class ASquare : InterfaceShape
    {
        int x, y;
        public ASquare()
        {

        }

        public void SetParam(int x, int y, int length, int width)
        {
            this.x = length;
            this.y = width;
        }
        public void Draw(Graphics g)
        {
           // Console.WriteLine(this.x.ToString);
            Rectangle s = new Rectangle(40,40,40,40); // create objet of Rectangle
            Pen myPen = new Pen(Color.Black, 5); // Create object of pen
            g.DrawRectangle(myPen,s);
        }
    }
}
