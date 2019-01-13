using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class ACircle : InterfaceShape
    {
        public void draw(Graphics g)
        {
            
            Pen blackpen = new Pen(Color.Black, 5);
        //    g.DrawEllipse(blackpen, 50,50);
        }
    }
}
