using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class ARectangle : InterfaceShape
    {
        public void draw(Graphics g)
        {
            Console.WriteLine("Inside Rectangle::draw() method.");
            Rectangle r1 = new Rectangle(50, 50, 50, 50);
            Pen blackpen = new Pen(Color.Black, 5);
            g.DrawRectangle(blackpen, r1);
        }
    }
}
