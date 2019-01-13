using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    class ShapeFactory
    {
        public InterfaceShape getShape(String shapeType)
        {
            if (shapeType == null)
            {
                return null;
            }
            
            else if (shapeType == "rectangle")
            {
                return new ARectangle();

            }
            

            return null;
        }
    }
}
