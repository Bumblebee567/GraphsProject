using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//KOLOROWANIE WIERZCHOŁKÓW ALGORYTM WYBRAC, PRZESZUKIWANIE MACIERZY W GŁĄB

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            //var myLine = new Line();
            //myLine.Stroke = System.Windows.Media.Brushes.Brown;
            //myLine.X1 = 1;
            //myLine.X2 = 50;
            //myLine.Y1 = 1;
            //myLine.Y2 = 50;
            //myLine.StrokeThickness = 10;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            //Canvas1.Children.Add(myLine);
            //var myElipse = new Ellipse();
            //myElipse.StrokeThickness = 2;
            //myElipse.Stroke = Brushes.Black;
            //myElipse.Width = 10;
            //myElipse.Height = 10;
            //Canvas.SetLeft(myElipse, Canvas1.Width/2);
            //Canvas.SetTop(myElipse, Canvas1.Height/2);

            //Canvas1.Children.Add(myElipse);
        }

        private void Wyczyść_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Clear();
        }

        private void GrafOkreślony_Click(object sender, RoutedEventArgs e)
        {
            var g1 = Graph.GenerateGraph(Convert.ToInt32(NumOfVertcs.Text));
            //var matrix = g1.GenerateAdciedenceMatrix(int.Parse(Degree.Text));
            ////var matrix = g1.GenerateMatrix(int.Parse(Degree.Text));
            //g1.GenerateEdges(matrix);
            g1.GenerateEdgesOfRandomGraph(g1, Convert.ToInt32(Degree.Text));
            Line[] edgesToDraw = new Line[g1.ListOfEdges.Count];
            //edgesToDraw[0] = new Line();
            //edgesToDraw[0].Stroke = System.Windows.Media.Brushes.Brown;
            //edgesToDraw[0].X1 = g1.ListOfEdges[0].StartPoint.X;
            //edgesToDraw[0].Y1 = g1.ListOfEdges[0].StartPoint.Y;
            //edgesToDraw[0].X2 = g1.ListOfEdges[0].EndPoint.X;
            //edgesToDraw[0].Y2 = g1.ListOfEdges[0].EndPoint.Y;
            //edgesToDraw[0].StrokeThickness = 3;
            //Canvas1.Children.Add(edgesToDraw[0]);

            //var myLine = new Line();
            //myLine.Stroke = System.Windows.Media.Brushes.Brown;
            //myLine.X1 = g1.ListOfEdges[0].StartPoint.X;
            //myLine.X2 = g1.ListOfEdges[0].EndPoint.X;
            //myLine.Y1 = g1.ListOfEdges[0].StartPoint.Y;
            //myLine.Y2 = g1.ListOfEdges[0].EndPoint.Y;
            //myLine.StrokeThickness = 10;
            //myLine.VerticalAlignment = VerticalAlignment.Center;
            //Canvas1.Children.Add(myLine);

            for (int i = 0; i < g1.ListOfEdges.Count; i++)
            {
                edgesToDraw[i] = new Line();
                edgesToDraw[i].Stroke = System.Windows.Media.Brushes.Blue;
                edgesToDraw[i].X1 = g1.ListOfEdges[i].StartPoint.X;
                edgesToDraw[i].Y1 = g1.ListOfEdges[i].StartPoint.Y;
                edgesToDraw[i].X2 = g1.ListOfEdges[i].EndPoint.X;
                edgesToDraw[i].Y2 = g1.ListOfEdges[i].EndPoint.Y;
                edgesToDraw[i].StrokeThickness = 2;
                Canvas1.Children.Add(edgesToDraw[i]);

                //PLIK Z WYNIKAMI PREZSZUKIWANIA - POTWIERDZA SPOSÓB
                //REFERAT  Z LITERATURY ANGLOJĘZYCZNEJ - JAKIŚ ALGORYTM NP. KOLOROWANIA - NAJWAŻNIEJSZE RZECZY - NA 14 GRUDNIA
            }
            for (int i = 0; i < g1.ListOfVertices.Count; i++)
            {
                var p = new PointVer();
                if(i%2 == 0)
                {
                    p.myEllipse.Fill = Brushes.Red;
                }
                p.TextBlock.Text = i.ToString();
                double left = g1.ListOfVertices[i].X - (p.myEllipse.Width / 2);
                double top = g1.ListOfVertices[i].Y - (p.myEllipse.Height / 2);
                p.Margin = new Thickness(left, top, 0, 0);
                Canvas1.Children.Add(p);
            }
        }

    }
}
