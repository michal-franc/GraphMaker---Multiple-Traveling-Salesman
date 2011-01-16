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
using System.Linq;
using System.Collections.Generic;

namespace GraphMaker.TFSAlgorithm
{
    public static class ShrinkWrap
    {
        public static List<int> CreateInitialorder(List<SilverlightEdge> edges)
        {
            var sortedEdges = from e in edges orderby e.PolarCords.TAU, e.PolarCords.R descending select e;

            return (from e in sortedEdges select e.EdgeNumber).ToList();

        }
    }
}
