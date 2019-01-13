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
    public partial class Paint : Form
    {
       Pen pen = new Pen(Color.OrangeRed, 1);
        int xAxis = 0;
        int yAxis = 0;
        Rectangle rect;
        Point lineY;
        List<object> shapeList = new List<object>();
        List<Point> listPoint = new List<Point>();
        Graphics g;
        bool isDragging = false;
        string shape;
       
        InterfaceShape shapes;

        public Paint()
        {
            InitializeComponent();
        }

        private void Paint_Load(object sender, EventArgs e)
        {
            xAxis = panelCanvas.Width / 2;
            yAxis = panelCanvas.Height / 2;
            panelCanvas.Paint += new PaintEventHandler(PanelCanvas_Paint);
        }

        private void txtCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cmd = txtCommand.Text;
                if (cmd.ToLower().Equals("help"))
                {
                    Form2 form = new Form2();
                    form.Show();
                    return;
                }
                if (!cmd.Contains(" "))
                {
                    ShowStatus("Invalid Command!!!", false);
                    return;
                }
                string initialCommand = cmd.Split(' ')[0]; //Getting starting command

                if (initialCommand.ToLower().Equals("drawto"))
                {
                    if (!cmd.Contains(","))
                    {
                        ShowStatus("Invalid command: Eg. DrawTo 500,500", false);
                        return;
                    }
                    string xyPos = cmd.Split(' ')[1];
                    xAxis = int.Parse(xyPos.Split(',')[0]);
                    yAxis = int.Parse(xyPos.Split(',')[1]);
                    ShowStatus("Position successfully set to " + xAxis + " and " + yAxis, true);
                }
                else if (initialCommand.ToLower().Equals("moveto"))
                {
                    if (!cmd.Contains(","))
                    {
                        ShowStatus("Invalid command: Eg. MoveTo 500,500", false);
                        return;
                    }
                    string xyPos = cmd.Split(' ')[1];
                    xAxis = int.Parse(xyPos.Split(',')[0]);
                    yAxis = int.Parse(xyPos.Split(',')[1]);
                    ShowStatus("Position successfully moved to " + xAxis + " and " + yAxis, true);
                }
                else if (initialCommand.ToLower().Equals("circle"))
                {
                    int radius = int.Parse(cmd.Split(' ')[1]);
                    shapes = shapeFactory.getShape("CIRCLE");
                    shapes.SetParam(xAxis, yAxis, radius, 0, pen.Color, false, "",  lineY, lineY);
                    shapeList.Add(shapes);
                    ShowStatus("Cirle has been drawn.", true);

                }
                else if (initialCommand.ToLower().Equals("rectangle"))
                {
                    if (!cmd.Contains(","))
                    {
                        ShowStatus("Invalid command: Eg. Rectangle 500,500", false);
                        return;
                    }
                    string wh = cmd.Split(' ')[1];
                    int width = int.Parse(wh.Split(',')[0]);
                    int height = int.Parse(wh.Split(',')[1]);
                    shapes = shapeFactory.getShape("RECTANGLE");
                    shapes.SetParam(xAxis, yAxis, width, height, pen.Color, false, "",  lineY, lineY);
                    shapeList.Add(shapes);
                    ShowStatus("Rectangle has been drawn.", true);

                }
                else if (initialCommand.ToLower().Equals("string"))
                {
                    string text = cmd.Split(' ')[1];
                    shapes = shapeFactory.getShape("STRING");
                    shapes.SetParam(xAxis, yAxis, 0, 0, pen.Color, false, text,  lineY, lineY);
                    shapeList.Add(shapes);
                    ShowStatus("String has been drawn.", true);
                }
                else if (initialCommand.ToLower().Equals("repeat"))
                {
                    string[] cmdList = cmd.Split(' ');
                    if (cmdList.Length != 5)
                    {
                        ShowStatus("Invalid Command: Eg. repeat 4 circle radius +10", false);
                        return;
                    }
                    int counter = int.Parse(cmdList[1]);
                    string shapeName = cmdList[2];
                    int radius = int.Parse(cmdList[4].Split('+')[1]);
                    int midXPanel = panelCanvas.Width / 2;
                    int midYPanel = panelCanvas.Height / 2;


                    int circleRadius = 50;
                    for (int i = 0; i < counter; i++)
                    {
                        xAxis = midXPanel - (circleRadius / 2);
                        yAxis = midYPanel - (circleRadius / 2);

                        if (shapeName.ToLower().Equals("circle"))
                        {
                            shapes = shapeFactory.getShape("CIRCLE");
                            shapes.SetParam(xAxis, yAxis, circleRadius, 0, pen.Color, false, "",  lineY, lineY);
                            shapeList.Add(shapes);
                        }
                        else if (shapeName.ToLower().Equals("rectangle"))
                        {
                            shapes = shapeFactory.getShape("RECTANGLE");
                            shapes.SetParam(xAxis, yAxis, circleRadius, circleRadius, pen.Color, false, "",  lineY, lineY);
                            shapeList.Add(shapes);
                        }
                        else
                        {
                            ShowStatus("Invalid Command: Shape name is invalid!!!", false);
                            return;
                        }
                        circleRadius += radius;
                    }
                    ShowStatus("Command executed successfully!!!", true);
                }
                else if(initialCommand.ToLower().Equals("line"))
                {
                    if (!cmd.Contains(","))
                    {
                        ShowStatus("Invalid command: Eg. Line x1,y1,x2,y2", false);
                        return;
                    }
                    string[] xyPos = cmd.Split(' ')[1].Split(',');
                    if(xyPos.Length < 4)
                    {
                        ShowStatus("Invalid command: Eg. Line x1,y1,x2,y2", false);
                        return;
                    }
                    xAxis = int.Parse(xyPos[0]);
                    yAxis = int.Parse(xyPos[1]);
                    int y1 = int.Parse(xyPos[2]);
                    int y2 = int.Parse(xyPos[3]);
                    lineY = new Point(y1, y2);
                    shapes = shapeFactory.getShape("LINE");
                    shapes.SetParam(xAxis, yAxis, 0, 0, pen.Color, false, "",  new Point(xAxis, yAxis), lineY);
                    shapeList.Add(shapes);
                    ShowStatus("Line has been drawn.", true);
                }
                else if (initialCommand.ToLower().Equals("polygon"))
                {
                    string tmp1 = cmd.Split('[')[1];
                    string tmp2 = tmp1.Split(']')[0];
                    string[] points = tmp2.Split(',');
                    if (!cmd.Contains(","))
                    {
                        ShowStatus("Invalid command: Eg. Polygon [500:500,200:200,240:450]", false);
                        return;
                    }                            
                    Polygon polygon = new Polygon(pen.Color, false);
                    foreach(string point in points)
                    {
                        int x = int.Parse(point.Split(':')[0]);
                        int y = int.Parse(point.Split(':')[1]);
                        polygon.addPoint(new Point(x, y));
                    }                    
                    shapeList.Add(polygon);
                    ShowStatus("Polygon has been drawn.", true);
                }
                else
                {
                    ShowStatus("Invalid Command!!!", false);
                }

                panelCanvas.Invalidate();
                panelCanvas.Refresh();
                txtCommand.Text = "";
            }
        }

        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            //Initiating graphic object if it's null
            if (g == null) g = panelCanvas.CreateGraphics();
            Font fy = new Font("Helvetica", 10, FontStyle.Bold);
            Brush br = new SolidBrush(pen.Color);

            //This if block shows the shape while dragging the mouse            
            if (isDragging)
            {
                switch (shape)
                {
                    case "CIRCLE":
                        g.DrawEllipse(pen, rect);
                        break;
                    case "RECTANGLE":
                        g.DrawRectangle(pen, rect);
                        break;
                    case "FILL RECTANGLE":
                        g.FillRectangle(br, rect);
                        break;
                    case "FILL CIRCLE":
                        g.FillEllipse(br, rect);
                        break;
                    case "POLYGON":
                        if (listPoint.Count < 2) return;
                        g.DrawPolygon(pen, listPoint.ToArray());
                        break;
                    case "FILL POLYGON":
                        if (listPoint.Count < 2) return;
                        g.FillPolygon(br, listPoint.ToArray());
                        break;
                    case "LINE":
                        g.DrawLine(pen, new Point(xAxis, yAxis), lineY);
                        break;
                }

            }
            else //This is the actual code for drawing inside the panel
            {
                if (shapeList.Count > 0) //Checking if user has drawn anything inside the panel
                {
                    foreach (object obj in shapeList) //Looping through drawing objects to display
                    {
                        if (obj.GetType() == typeof(Polygon))
                        {
                            Polygon polygon = (Polygon)obj; //Type casting obj to Polygon
                            if (polygon.getPoint().Count > 2) //Checking if user has set more than 2 points
                            {
                                if (polygon.isFilled) //Checking if it's a filled polygon
                                {
                                    Brush pbr = new SolidBrush(polygon.color);
                                    g.FillPolygon(pbr, polygon.getPoint().ToArray());
                                    pbr.Dispose();
                                }
                                else
                                {
                                    Pen pen = new Pen(polygon.color);
                                    g.DrawPolygon(pen, polygon.getPoint().ToArray());
                                    pen.Dispose();
                                }
                            }
                        }
                        else
                        {
                            shapes = (Shape)obj;
                            shapes.Draw(g);
                        }
                    }
                }
            }
        }

        //Used for showing status in statusbar
        

        private void panelCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            xAxis = e.X; //setting x-axis
            yAxis = e.Y; //setting y-axis
        }

        private void panelCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;
            int x = Math.Min(xAxis, e.X);
            int y = Math.Min(yAxis, e.Y);
            int width = Math.Max(xAxis, e.X) - Math.Min(xAxis, e.X);
            int height = Math.Max(yAxis, e.Y) - Math.Min(yAxis, e.Y);
            if(shape == "LINE") //Checking if it's a line
            lineY = e.Location; //setting y1,y2 for the line to draw
            else
            rect = new Rectangle(x, y, width, height);
            panelCanvas.Refresh(); //refreshing panel to draw temporary shapes
        }

        private void panelCanvas_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        //This is a funtion related to design
        private void ResetToolBar()
        {
            toolRectangle.BackColor = Color.DarkGray;
            toolFillRectangle.BackColor = Color.DarkGray;
            toolCircle.BackColor = Color.DarkGray;
            toolFillCircle.BackColor = Color.DarkGray;
            toolPolygon.BackColor = Color.DarkGray;
            toolFillPolygon.BackColor = Color.DarkGray;
            toolLine.BackColor = Color.DarkGray;
            listPoint.Clear();
        }

        private void toolRectangle_Click(object sender, EventArgs e)
        {
            ResetToolBar();
            toolRectangle.BackColor = Color.LightSlateGray;
            shape = "RECTANGLE";
        }

        private void toolCircle_Click(object sender, EventArgs e)
        {
            ResetToolBar();
            toolCircle.BackColor = Color.LightSlateGray;
            shape = "CIRCLE";
        }

        private void toolPolygon_Click(object sender, EventArgs e)
        {
            ResetToolBar();
            toolPolygon.BackColor = Color.LightSlateGray;
            shape = "POLYGON";
        }

        private void toolColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                toolColor.BackColor = colorDialog.Color;
                pen.Color = colorDialog.Color;
            }
        }

        private void toolFillRectangle_Click(object sender, EventArgs e)
        {
            ResetToolBar();
            toolFillRectangle.BackColor = Color.LightSlateGray;
            shape = "FILL RECTANGLE";
        }

        private void toolFillCircle_Click(object sender, EventArgs e)
        {
            ResetToolBar();
            toolFillCircle.BackColor = Color.LightSlateGray;
            shape = "FILL CIRCLE";
        }

        private void toolFillPolygon_Click(object sender, EventArgs e)
        {
            ResetToolBar();
            toolFillPolygon.BackColor = Color.LightSlateGray;
            shape = "FILL POLYGON";
        }
       

        private void toolLine_Click(object sender, EventArgs e)
        {
            ResetToolBar();
            toolLine.BackColor = Color.LightSlateGray;
            shape = "LINE";
        }

        private void panelCanvas_Paint_1(object sender, PaintEventArgs e)
        {

        }

    }
}
