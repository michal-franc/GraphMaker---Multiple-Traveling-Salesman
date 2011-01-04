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

        public List<Cluster> CreateClusters(List<SilverlightEdge> edges, int nrOfClusters)
        {
            List<Cluster> returnCluster = new List<Cluster>();

            CalculateBoundaries(edges);
            CalculateRadius();
            returnCluster = InitialClusters(edges, nrOfClusters);
            while (RecalculateClusters(edges, ref returnCluster) == false)
            {

            }


            SilverlightEdge centralEdge = GetCentralEdge(edges);

            foreach (Cluster c in returnCluster)
            {
                if(!c.Edges.Contains(centralEdge))
                    c.Edges.Add(centralEdge);
            }
            return returnCluster;
        }

        private bool RecalculateClusters(List<SilverlightEdge> edges,ref List<Cluster> returnCluster)
        {
            bool more = false;

            foreach (Cluster cluster in returnCluster)
            {
                double averagedX = 0.0;
                double averagedY = 0.0;

                foreach (SilverlightEdge edge in cluster.Edges)
                {
                    averagedX += edge.Position.X;
                    averagedY += edge.Position.Y;
                }

                averagedX = averagedX / cluster.Edges.Count;
                averagedY = averagedY / cluster.Edges.Count;

                Point newPoint = new Point(averagedX,averagedY);

                if (newPoint.CalculateDistance(cluster.ClusterCenter) > 0.1)
                {
                    cluster.ClusterCenter = new Point(averagedX, averagedY);
                    more = true;
                }
            }

            foreach (Cluster cluster in returnCluster)
            {
                cluster.Edges.Clear();
            }

            foreach (SilverlightEdge edge in edges)
            {
                double distance = double.MaxValue;
                Cluster bestCluster = null;
                foreach (Cluster cluster in returnCluster)
                {
                    double newDistance = edge.Position.CalculateDistance(cluster.ClusterCenter);
                    if (newDistance < distance)
                    {
                        bestCluster = cluster;
                        distance = newDistance;
                    }
                }

                if (bestCluster != null)
                {
                    bestCluster.Edges.Add(edge);
                }
            }

            return more;
        }

        private List<Cluster> InitialClusters(List<SilverlightEdge> edges, int nrOfClusters)
        {
            List<Cluster> returnCluster = new List<Cluster>();
            double deegreStep = 360.0 / nrOfClusters;

            for (int i = 0; i < nrOfClusters; i++)
            {

                double step = deegreStep / 2 + i * deegreStep;

                double newX = CenterPoint.X + Radius * (Math.Cos(step * 0.0174532925));
                double newY = CenterPoint.Y - Radius * (Math.Sin(step * 0.0174532925));

                returnCluster.Add(new Cluster(new Point(newX, newY)));
            }

            foreach (SilverlightEdge edge in edges)
            {
                double distance = double.MaxValue;
                Cluster bestCluster = null;
                foreach (Cluster cluster in returnCluster)
                {
                    double newDistance = edge.Position.CalculateDistance(cluster.ClusterCenter);
                    if (newDistance < distance)
                    {
                        bestCluster = cluster;
                        distance = newDistance;
                    }
                }

                if (bestCluster != null)
                {
                    bestCluster.Edges.Add(edge);
                }
            }

            return returnCluster;
        }

        private void CalculateBoundaries(List<SilverlightEdge> edges)
        {
            MaxX = edges.Max(x => x.Position.X);
            MinX = edges.Min(x => x.Position.X);
            LengthX = MaxX - MinX;
            MaxY = edges.Max(x => x.Position.Y);
            MinY = edges.Min(x => x.Position.Y);
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

        private SilverlightEdge GetCentralEdge(List<SilverlightEdge> edges)
        {
            SilverlightEdge bestEdge = null;

            double distance = double.MaxValue;

            foreach (SilverlightEdge edge in edges)
            {
                double newDistance = edge.Position.CalculateDistance(CenterPoint);
                if (newDistance < distance)
                {
                    bestEdge = edge;
                    distance = newDistance;
                }
            }


            return bestEdge;
        }

    }
}
