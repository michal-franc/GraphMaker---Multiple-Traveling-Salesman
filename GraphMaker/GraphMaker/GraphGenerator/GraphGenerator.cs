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
using GraphMaker.Objects;
using System.Collections.Generic;

namespace GraphMaker.GraphGenerator
{
    public static class GraphGenerator
    {
        public static IList<Edge> Edges { get; set; }

        public static void GenerateGraph()
        {
            throw new NotImplementedException();
        }

        public static void CreateNewGraph(IList<Edge> edges)
        {
            GraphGenerator.Edges = edges;
        }

        public static void FindBestRoute(IRouteMaker routeMaker)
        {
            throw new NotImplementedException();
        }
    }
}
