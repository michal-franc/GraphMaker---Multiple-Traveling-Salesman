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
    public class KMeanClustering
    {
        public double MaxX { get; set;}
        public double MinX { get; set;}
        public double LengthX { get; set; }

        public double MaxY { get; set; }
        public double MinY { get; set; }
        public double LengthY { get; set; }

        public Point CenterPoint { get; set; }


        public double Radius {get;set;}
        
        //∙Place K points into the space represented by the objects that are being clustered. These points represent
        //initial group centroids.
        //∙Assign each object to the group that has the closest centroid.
        //∙When all objects have been assigned, recalculate the positions of the K centroids.
        //∙Repeat Steps 2 and 3 until the centroids no longer move. This produces a separation of the objects into
        //groups from which the metric to be minimized can be calculated.
        //Table 1 and Fig. 2 show the results, after applying �-means clustering

        public List<Cluster> CreateClusters(List<Point> points, int nrOfClusters)
        {

            CalculateBoundaries(points);
            CalculateRadius();

            double deegreStep = 360.0 / nrOfClusters;


            List<Cluster> returnCluster = new List<Cluster>();

            for (int i = 0; i < nrOfClusters; i++)
            {

                double step = deegreStep/2 + i * deegreStep;

                double newX = CenterPoint.X + Radius * (Math.Cos(step * 0.0174532925));
                double newY = CenterPoint.Y - Radius * (Math.Sin(step * 0.0174532925));

                returnCluster.Add(new Cluster(new Point(newX,newY)));
            }

            return returnCluster;

        }

        private void CalculateBoundaries(List<Point> points)
        {
            MaxX = points.Max(x => x.X);
            MinX = points.Min(x => x.X);
            LengthX = MaxX - MinX;
            MaxY = points.Max(x => x.Y);
            MinY = points.Min(x => x.Y);
            LengthY = MaxY-MinY;

            CenterPoint = new Point((MinX + MaxX) / 2, (MaxY + MinY) / 2);
        }

        private void CalculateRadius()
        {
            if (LengthX > LengthY)
            {
                Radius = MaxY - CenterPoint.Y;
            }
            else
            {
                Radius = MaxX - CenterPoint.X;
            }
        }

    }
}
