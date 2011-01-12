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
        public List<SilverlightEdge> Edges { get; set; }
        public Point ClusterCenter { get; set; }
        public SimulatedAnnealing  Annealing {get;set;}

        private Color _color;

        public Color Color
        {
            set
            {
                _color = value;
                foreach (SilverlightEdge edge in Edges)
                {
                    edge.Color = value;
                }
            }
            get
            {
                return _color;
            }
        }

        public Cluster(Point center)
        {
            Edges = new List<SilverlightEdge>();
            ClusterCenter = center;
            Annealing = new SimulatedAnnealing();
        }
    }
}
