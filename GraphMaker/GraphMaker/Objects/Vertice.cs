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
    public class Vertice
    {
        public int Value
        {
            get;
            set;
        }

        #region Private

        private Edge _edgeFirst;
        private Edge _edgeSecond;

        
        #endregion

        public int Weight { get; set; }

        public Edge EdgeFirst { get { return _edgeFirst; } set { _edgeFirst = value; } }
        public Edge EdgeSecond { get { return _edgeSecond; } set { _edgeSecond = value; } }


        public Vertice(Edge edgeFirst,Edge edgeSecond) 
            : this(edgeSecond)
        {
            _edgeFirst = edgeFirst;
        }

        public Vertice(Edge edgeSecond)
        {
            _edgeSecond = edgeSecond;
        }

        public static bool CheckIfVerticeExist(Edge edge1,Edge edge2)
        {
            foreach (Vertice v in edge1.Vertices)
            {
                if (v.EdgeSecond == edge2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
