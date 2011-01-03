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
using System.Collections.Generic;

namespace GraphMaker.TFSAlgorithm
{
    public class Cluster
    {
        public List<Point> Points { get; set; }
        public Point ClusterCenter { get; set; }

        public Cluster(Point center)
        {
            Points = new List<Point>();
            ClusterCenter = center;
        }
    }
}
