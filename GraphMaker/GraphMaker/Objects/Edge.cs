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

namespace GraphMaker.Objects
{
    public class Edge
    {

        public int Number
        {
            get;
            set;

        }
        public IList<Vertice> Vertices
        {
            get { return _vertices; }
        }

        private IList<Vertice> _vertices = new List<Vertice>();



        public Edge(int number)
        {
            Number = number;
        }

        
        public void AddVertice(Vertice vertice)
        {
            vertice.EdgeFirst = this;
            Vertices.Add(vertice);
        }


        public Edge(IList<Vertice> vertices)
        {
            _vertices = vertices;
        }

        public Vertice GetBestVertice()
        {
            return null;
        }

        public IList<Edge>  GetNeighbours()
        {
            return null;
        }

        public bool IsNeighbour(Edge edge)
        {
            return false;
        }
    }
}
