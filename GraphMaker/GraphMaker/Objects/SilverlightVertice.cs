using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GraphMaker.Objects
{
    public class SilverlightVertice
    {

        public SilverlightVertice(Point p1,Point p2)
        {
            Line = new Line();
            Line.SetValue(Canvas.ZIndexProperty, 0);
            Line.Stroke = new SolidColorBrush() { Color = Color.FromArgb((byte)255, (byte)0, (byte)0, (byte)255) };

            Line.X1 = p1.X + 10;
            Line.Y1 = p1.Y + 10;

            Line.X2 = p2.X + 10;
            Line.Y2 = p2.Y + 10;

            Line.StrokeThickness = 2;
        }

        public Vertice Vertice
        {
            get;
            set;
        }

        public Line Line
        {
            get;
            set;
        }

        public Label Label
        {
            get;
            set;
        }
    }
}
