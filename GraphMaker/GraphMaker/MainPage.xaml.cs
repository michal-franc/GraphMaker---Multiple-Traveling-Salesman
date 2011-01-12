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
    public partial class MainPage : UserControl
    {

        public int NrOfSalesmans
        {
            get
            {
                return int.Parse(txtBoxIloscK.Text);
            }
        }

        private List<Edge> _edges = new List<Edge>();

        List<SilverlightEdge> _slEdges = new List<SilverlightEdge>();

        private int edgeCounter = 0;
        SilverlightEdge _edge1;
        SilverlightEdge _edge2;
        private bool _oneSelected=false;

        SimulatedAnnealing annealingForOne;

        private List<Cluster> Clusters; 

       public MainPage()
        {
            InitializeComponent();
        }

       private void AddNewEdge(Point p,int index)
       {
           SilverlightEdge edge = new SilverlightEdge(index,p);
           edge.MouseLeftButtonDown += new MouseButtonEventHandler(edge_MouseLeftButtonDown);
           this.LayoutRoot.Children.Add(edge);

           Edge newEdge = new Edge(index);
           edge.Edge = newEdge;

           _edges.Add(newEdge);
           _slEdges.Add(edge);
       }

       private void AddNewVertice(SilverlightEdge edge1, SilverlightEdge edge2)
       {
           if (!Vertice.CheckIfVerticeExist(edge1.Edge, edge2.Edge))
           {
               SilverlightVertice vertice = new SilverlightVertice(edge1.Position, edge2.Position);
               vertice.Line.MouseLeftButtonDown += new MouseButtonEventHandler(line_MouseLeftButtonDown);

               edge1.Edge.AddVertice(new Vertice(edge2.Edge));
               edge2.Edge.AddVertice(new Vertice(edge1.Edge));
               this.LayoutRoot.Children.Add(vertice.Line);
           }
       }

        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((ModifierKeys.Control & Keyboard.Modifiers) != 0) 
            {
                edgeCounter++;
                UIElement element = sender as UIElement;
                Point p = e.GetPosition(element);

                AddNewEdge(p, edgeCounter);
            }
        }

        void edge_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SilverlightEdge edge = sender as SilverlightEdge;

            if ((ModifierKeys.Alt & Keyboard.Modifiers) != 0)
            {
                this.LayoutRoot.Children.Remove(edge);
                ReorderEdges(edge.Edge.Number);
            }
            else
            {
                if (!_oneSelected)
                {
                    _oneSelected = true;
                    _edge1 = edge;
                }
                else
                {
                    _oneSelected = false;
                    _edge2 = edge;
                    AddNewVertice(_edge1, _edge2);
                }
            }
        }

        private void ReorderEdges(int reorderNumber)
        {
            foreach (SilverlightEdge edge in this.LayoutRoot.Children)
            {
                if (edge.Edge.Number > reorderNumber)
                {
                    edge.Edge.Number -= 1;
                    edge.EdgeNumber  -= 1;

                }
            }
            if (edgeCounter > 0)
            {
                edgeCounter--;
            }
        }

        void line_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Line line = sender as Line;

            if ((ModifierKeys.Alt & Keyboard.Modifiers) != 0)
            {
                this.LayoutRoot.Children.Remove(line);
            }
            else
            {
                TextBox textBox = new TextBox();
                textBox.Tag = line.Tag;
                textBox.Width = 30;
                textBox.Height = 20;

                double x = line.X1 - line.X2;
                if (x > 0)
                    x = x/2 + line.X2;
                else
                    x = -x/2 + line.X1;

                double y = line.Y1 - line.Y2;

                if (y > 0)
                    y = y/2 + line.Y2;
                else
                    y = -y/2 + line.Y1;

                textBox.SetValue(Canvas.LeftProperty, x);
                textBox.SetValue(Canvas.TopProperty, y);

                textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);

                this.LayoutRoot.Children.Add(textBox);
            }
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox box = sender as TextBox;

            if (e.Key == Key.Enter)
            {
                Label lbl = new Label();
                lbl.Content = box.Text;
                 SilverlightVertice vertice  = box.Tag as SilverlightVertice;

                 if (vertice != null)
                 {
                     vertice.Weight = int.Parse(box.Text);
                 }

                lbl.Foreground = new SolidColorBrush(Color.FromArgb((byte)255,(byte)255,(byte)0,(byte)0));
                lbl.FontSize = 9;
                lbl.Width = 20;
                lbl.Height = 20;
                lbl.SetValue(Canvas.LeftProperty, (double)box.GetValue(Canvas.LeftProperty)+5);
                lbl.SetValue(Canvas.TopProperty, (double)box.GetValue(Canvas.TopProperty) + 5);
                lbl.SetValue(Canvas.ZIndexProperty, 2);
                this.LayoutRoot.Children.Remove(box);
                this.LayoutRoot.Children.Add(lbl);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            edgeCounter = 0;
            this.LayoutRoot.Children.Clear();
        }

        private void btnGenerateRandom_Click(object sender, RoutedEventArgs e)
        {
            //1. Create random generator
            Random rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 100; i++)
            {
                edgeCounter++;
                double x = rand.NextDouble() * 800;
                double y = rand.NextDouble() * 500;
                Point location = new Point(x, y);
                AddNewEdge(location, edgeCounter);
                //2. Create edge in random location in x y.
            }

        }

        private void btnCalcDist_Click(object sender, RoutedEventArgs e)
        {
            if (NrOfSalesmans <= 1)
            {
                foreach (SilverlightEdge edge in _slEdges)
                {
                    foreach (SilverlightEdge edge1 in _slEdges)
                    {
                        edge.Distances.Add(edge.Position.CalculateDistance(edge1.Position));
                    }
                }
                annealingForOne = new SimulatedAnnealing();
                List<int> order = annealingForOne.CalculateInit(_slEdges);
                DrawLines(order);
            }
            else
            {
                Clusters = CreateAreas(NrOfSalesmans);

                foreach (Cluster clust in Clusters)
               {
                   foreach (SilverlightEdge edge in clust.Edges)
                   {
                       foreach (SilverlightEdge edge1 in clust.Edges)
                       {
                           edge.Distances.Add(edge.Position.CalculateDistance(edge1.Position));
                       }
                   }
                   clust.Color = Common.ColorPallete.GetColor();

                   List<int> order = clust.Annealing.CalculateInit(clust.Edges);
                   DrawLines(order,clust);
               }

            }

        }



        private void ClearLines()
        {
            List<Line> lines = new List<Line>();
            foreach (UIElement element in this.LayoutRoot.Children)
            {
                Line line = element as Line;
                if (line !=null)
                {
                    lines.Add(line);
                }
            }

            foreach (UIElement l in lines)
            {
                this.LayoutRoot.Children.Remove(l);
            }
        }

        private void DrawLines(List<int> order)
        {
            for(int i=0;i<order.Count-1;i++)
            {
                SilverlightVertice vertice = new SilverlightVertice(_slEdges[order[i]].Position, _slEdges[order[i + 1]].Position);
                vertice.Color = _slEdges[order[i]].Color;
                this.LayoutRoot.Children.Add(vertice.Line);
            }

            SilverlightVertice vertice1 = new SilverlightVertice(_slEdges[order[order.Count - 1]].Position, _slEdges[order[0]].Position);
            vertice1.Color = _slEdges[order[order.Count - 1]].Color;
            this.LayoutRoot.Children.Add(vertice1.Line);
        }

        private void DrawLines(List<int> order,Cluster cluster)
        {
            for (int i = 0; i < order.Count - 1; i++)
            {
                SilverlightVertice vertice = new SilverlightVertice(cluster.Edges[order[i]].Position, cluster.Edges[order[i + 1]].Position);
                vertice.Color = cluster.Color;
                this.LayoutRoot.Children.Add(vertice.Line);
            }

            SilverlightVertice vertice1 = new SilverlightVertice(cluster.Edges[order[order.Count - 1]].Position, cluster.Edges[order[0]].Position);
            vertice1.Color = cluster.Color;
            this.LayoutRoot.Children.Add(vertice1.Line);
        }


        private List<Cluster> CreateAreas(int nrOfClusters)
        {

            KMeanClustering clusteringAlgorithm = new KMeanClustering();

            List<Cluster> clusters = clusteringAlgorithm.CreateClusters(_slEdges, nrOfClusters);
            int counter =0;
            foreach(Cluster clust in clusters)
            {
                counter++;
                Label lbl =new Label();
                lbl.Foreground = new SolidColorBrush(Color.FromArgb(255,0,255,0));
                lbl.FontSize = 15;
                lbl.Width = 20;
                lbl.Height = 20;
                lbl.Content = counter.ToString();
                lbl.SetValue(Canvas.LeftProperty, clust.ClusterCenter.X);
                lbl.SetValue(Canvas.TopProperty, clust.ClusterCenter.Y);
                lbl.SetValue(Canvas.ZIndexProperty, 2);
                this.LayoutRoot.Children.Add(lbl);
            }

            return clusters;
        }

        private void btnBest_Click(object sender, RoutedEventArgs e)
        {
            ClearLines();
            if (NrOfSalesmans <= 1)
            {
                List<int> order = annealingForOne.Calculate();
                DrawLines(order);
            }
            else
            {
                foreach (Cluster clust in Clusters)
                {
                    List<int> order = clust.Annealing.Calculate();
                    DrawLines(order, clust);
                }

            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ClearLines();
            if (NrOfSalesmans <= 1)
            {
                List<int> order = annealingForOne.Calculate(100,false);
                DrawLines(order);
            }
            else
            {
                foreach (Cluster clust in Clusters)
                {
                    List<int> order = clust.Annealing.Calculate(100,false);
                    DrawLines(order, clust);
                }

            }

        }
    }
}
