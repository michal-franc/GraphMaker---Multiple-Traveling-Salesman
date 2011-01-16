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
using GraphMaker.TFSAlgorithm;

namespace GraphMaker
{
    public partial class SilverlightEdge : UserControl
    {
        private List<SilverlightVertice> _vertices = new List<SilverlightVertice>();

        public Color Color
        {
            set
            {
                this.Ellipse.Stroke = new SolidColorBrush(value);
                //foreach (SilverlightVertice vert in _vertices)
                //{
                //    vert.Color = Color;
                //}
            }
            get
            {
                return ((SolidColorBrush)this.Ellipse.Stroke).Color;
            }
        }
      
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
            this.Color = Common.ColorPallete.DefaultColor;
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
                PolarCords = new PolarCord(value);
            }
        }

        public PolarCord PolarCords { get; set; }

    }
}
