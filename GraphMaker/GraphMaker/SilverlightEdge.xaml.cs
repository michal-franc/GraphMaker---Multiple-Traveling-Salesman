using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GraphMaker.Objects;

namespace GraphMaker
{
    public partial class SilverlightEdge : UserControl
    {
        private int _edgeNumber;

        public int EdgeNumber
        {
            get
            {
                return _edgeNumber;
            }
            set
            {
                _edgeNumber = value;
                lblEdgeNumber.Content = value.ToString();
            }
        }

        public SilverlightEdge(int index,Point p)
        {
            InitializeComponent();
            EdgeNumber = index;
            Position = p;
            this.SetValue(Canvas.ZIndexProperty, 1);
        }

        private Point _position;
        public Edge Edge;

        public Point Position 
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                this.SetValue(Canvas.LeftProperty, value.X);
                this.SetValue(Canvas.TopProperty, value.Y);
            }
        }

    }
}
