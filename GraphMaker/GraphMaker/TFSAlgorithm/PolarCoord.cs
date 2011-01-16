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

namespace GraphMaker.TFSAlgorithm
{
    public class PolarCord
    {
        public double R { get; set; }
        public double TAU { get; set; }

        private Point _point;

        public void Center(Point center)
        {

            double X = _point.X-center.X;
            double Y = _point.Y - center.Y;

            if (X > 0)
            {
                TAU = Math.Atan2(Y,X);
            }
            else if (X < 0 && Y >= 0)
            {
                TAU = Math.Atan2(Y, X) +180;
            }
            else if (X < 0 && Y < 0)
            {
                TAU = Math.Atan2(Y, X)-180;
            }
            else if (X == 0 && Y > 0)
            {
                TAU = 90;
            }
            else if(X==0 && Y<0)
            {
                TAU = -90;
            }
            else if(X==0 && Y==0)
            {
                TAU = 0;
            }

            R = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

        }

        public PolarCord(Point p)
        {
            _point = p;
            R = Math.Sqrt(Math.Pow(p.X, 2)+Math.Pow(p.Y, 2));
            TAU = Math.Atan2(p.Y,p.X);
        }
    }
}
