using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class draw : Form
    {
        String syntax;
        public draw(string command)
        {
            InitializeComponent();
            this.syntax = command;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            ShapeFactory shapefactory = new ShapeFactory();
            InterfaceShape shape = shapefactory.getShape(this.syntax);
            shape.draw(g);
        }
    }
}
