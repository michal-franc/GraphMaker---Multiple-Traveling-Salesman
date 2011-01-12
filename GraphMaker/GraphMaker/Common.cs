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

namespace GraphMaker
{
    public static class Common
    {
        public static double CalculateDistance(this Point p1,Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

    public static class ColorPallete
    {
        private static int counter = -1;
        private static Color [] _colors = new Color[12]
        {
            Color.FromArgb(255,255,0,0),
            Color.FromArgb(255,150,0,0),
            Color.FromArgb(255,0,0,0),
            Color.FromArgb(255,0,255,0),
            Color.FromArgb(255,0,150,0),
            Color.FromArgb(255,0,50,0),
            Color.FromArgb(255,0,0,255),
            Color.FromArgb(255,0,0,150),
            Color.FromArgb(255,0,0,50),
            Color.FromArgb(255,150,150,0),
            Color.FromArgb(255,50,50,0),
            Color.FromArgb(255,0,150,150)
        };

        public static Color DefaultColor
        {
            get
            {
                return Color.FromArgb(255,0,0,255);
            }
        }

        public static Color GetColor()
        {
            counter++;
            counter = (counter % _colors.Length);  
            return _colors[counter];
        }
    }
    }
}
