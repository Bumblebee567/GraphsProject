﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        static Graph g1;
        public MainWindow()
        {
            InitializeComponent();
            Search.IsEnabled = false;
        }

        private void Wyczyść_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Clear();
            GenerateSimple.IsEnabled = true;
            GenerateRegular.IsEnabled = true;
            Degree.Text = String.Empty;
            NumOfVertcs.Text = String.Empty;
            TypeLabel.Content = String.Empty;
            RegVertcs.IsChecked = false;
            RndVertcs.IsChecked = false;
            Search.IsEnabled = false;
            TitleLabel.Content = String.Empty;
        }

        private void GrafOkreślony_Click(object sender, RoutedEventArgs e)
        {
        }

        private void GenerateSimple_Click(object sender, RoutedEventArgs e)
        {
            if (NumOfVertcs.Text == "" || Degree.Text == "")
            {
                MessageBox.Show("Nie można wygenerować grafu - brak danych", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Convert.ToInt32(NumOfVertcs.Text) <= Convert.ToInt32(Degree.Text))
            {
                MessageBox.Show("Nie można wygenerować grafu", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                NumOfVertcs.Text = String.Empty;
                Degree.Text = String.Empty;
                return;
            }
            if(RndVertcs.IsChecked == false && RegVertcs.IsChecked == false)
            {
                MessageBox.Show("Nie można wygenerować grafu - brak wybranego rozmieszczenia wierzchołków", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (RegVertcs.IsChecked == true)
            {
                TitleLabel.Content = "Graf prosty";
                GenerateRegular.IsEnabled = false;
                GenerateSimple.IsEnabled = false;
                Search.IsEnabled = true;
                g1 = Graph.GenerateVerticesGraph(Convert.ToInt32(NumOfVertcs.Text));
                g1.GenerateEdgesOfRandomGraph(g1, Convert.ToInt32(Degree.Text));
                Line[] edgesToDraw = new Line[g1.ListOfEdges.Count];
                for (int i = 0; i < g1.ListOfEdges.Count; i++)
                {
                    edgesToDraw[i] = new Line();
                    edgesToDraw[i].Stroke = System.Windows.Media.Brushes.Black;
                    edgesToDraw[i].X1 = g1.ListOfEdges[i].StartPoint.X;
                    edgesToDraw[i].Y1 = g1.ListOfEdges[i].StartPoint.Y;
                    edgesToDraw[i].X2 = g1.ListOfEdges[i].EndPoint.X;
                    edgesToDraw[i].Y2 = g1.ListOfEdges[i].EndPoint.Y;
                    edgesToDraw[i].StrokeThickness = 2;
                    Canvas1.Children.Add(edgesToDraw[i]);
                }
                g1.ColorVerticesOfGraph();
                for (int i = 0; i < g1.ListOfVertices.Count; i++)
                {
                    var p = new PointVer();
                    //if (i % 2 == 0)
                    //{
                    //    p.myEllipse.Fill = Brushes.Red;
                    //}
                    p.myEllipse.Fill = g1.ListOfVertices[i].Color;
                    p.TextBlock.Text = g1.ListOfVertices[i].Id.ToString();
                    double left = g1.ListOfVertices[i].X - (p.myEllipse.Width / 2);
                    double top = g1.ListOfVertices[i].Y - (p.myEllipse.Height / 2);
                    p.Margin = new Thickness(left, top, 0, 0);
                    TypeLabel.Content = String.Empty;
                    TypeLabel.Content = $"Liczba użytych kolorów: {g1.ListOfVertices.GroupBy(x => x.Color).Count()}";
                    Canvas1.Children.Add(p);
                }
            }
            else if (RndVertcs.IsChecked == true)
            {
                TitleLabel.Content = "Graf prosty";
                GenerateRegular.IsEnabled = false;
                GenerateSimple.IsEnabled = false;
                Search.IsEnabled = true;
                g1 = Graph.GenerateVerticesOfGraphRandomly(Convert.ToInt32(NumOfVertcs.Text));
                g1.GenerateEdgesOfRandomGraph(g1, Convert.ToInt32(Degree.Text));
                Line[] edgesToDraw = new Line[g1.ListOfEdges.Count];
                for (int i = 0; i < g1.ListOfEdges.Count; i++)
                {
                    edgesToDraw[i] = new Line();
                    edgesToDraw[i].Stroke = System.Windows.Media.Brushes.Black;
                    edgesToDraw[i].X1 = g1.ListOfEdges[i].StartPoint.X;
                    edgesToDraw[i].Y1 = g1.ListOfEdges[i].StartPoint.Y;
                    edgesToDraw[i].X2 = g1.ListOfEdges[i].EndPoint.X;
                    edgesToDraw[i].Y2 = g1.ListOfEdges[i].EndPoint.Y;
                    edgesToDraw[i].StrokeThickness = 2;
                    Canvas1.Children.Add(edgesToDraw[i]);
                }
                g1.ColorVerticesOfGraph();
                for (int i = 0; i < g1.ListOfVertices.Count; i++)
                {
                    var p = new PointVer();
                    //if (i % 2 == 0)
                    //{
                    //    p.myEllipse.Fill = Brushes.Red;
                    //}
                    p.myEllipse.Fill = g1.ListOfVertices[i].Color;
                    p.TextBlock.Text = g1.ListOfVertices[i].Id.ToString();
                    double left = g1.ListOfVertices[i].X - (p.myEllipse.Width / 2);
                    double top = g1.ListOfVertices[i].Y - (p.myEllipse.Height / 2);
                    p.Margin = new Thickness(left, top, 0, 0);
                    TypeLabel.Content = String.Empty;
                    TypeLabel.Content = $"Liczba użytych kolorów: {g1.ListOfVertices.GroupBy(x => x.Color).Count()}";
                    Canvas1.Children.Add(p);
                }
            }
        }

        private void GenerateRegular_Click(object sender, RoutedEventArgs e)
        {
            if (RndVertcs.IsChecked == false && RegVertcs.IsChecked == false)
            {
                MessageBox.Show("Nie można wygenerować grafu - brak wybranego rozmieszczenia wierzchołków", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (Convert.ToInt32(NumOfVertcs.Text) > 21)
            {
                MessageBox.Show("Nie można wygenerować grafu - zbyt duża ilość wierzchołków", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                NumOfVertcs.Text = String.Empty;
                Degree.Text = String.Empty;
                return;
            }
            else if(Convert.ToInt32(NumOfVertcs.Text)%2 == 1 && Convert.ToInt32(Degree.Text) == 1)
            {
                MessageBox.Show("Nie można wygenerować grafu - złe dane", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                NumOfVertcs.Text = String.Empty;
                Degree.Text = String.Empty;
                return;
            }
            else if(NumOfVertcs.Text == "" || Degree.Text == "")
            {
                MessageBox.Show("Nie można wygenerować grafu - brak danych", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (Convert.ToInt32(NumOfVertcs.Text) <= Convert.ToInt32(Degree.Text))
            {
                MessageBox.Show("Nie można wygenerować grafu", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                NumOfVertcs.Text = String.Empty;
                Degree.Text = String.Empty;
                return;
            }
            else if (Convert.ToInt32(NumOfVertcs.Text) < 10 && Convert.ToInt32(Degree.Text) >= 5)
            {
                MessageBox.Show("Nie można wygenerować grafu", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                NumOfVertcs.Text = String.Empty;
                Degree.Text = String.Empty;
                return;
            }
          

            if (RegVertcs.IsChecked == true)
            {
                TitleLabel.Content = "Graf regularny";
                GenerateRegular.IsEnabled = false;
                GenerateSimple.IsEnabled = false;
                Search.IsEnabled = true;
                g1 = Graph.GenerateVerticesGraph(Convert.ToInt32(NumOfVertcs.Text));
                bool isDrawn = false;
                while (isDrawn == false)
                {
                    isDrawn = g1.GenerateEdgesOfRegularGraph(Convert.ToInt32(NumOfVertcs.Text), Convert.ToInt32(Degree.Text), g1);
                }
                Line[] edgesToDraw = new Line[g1.ListOfEdges.Count];
                for (int i = 0; i < g1.ListOfEdges.Count; i++)
                {
                    edgesToDraw[i] = new Line();
                    edgesToDraw[i].Stroke = System.Windows.Media.Brushes.Black;
                    edgesToDraw[i].X1 = g1.ListOfEdges[i].StartPoint.X;
                    edgesToDraw[i].Y1 = g1.ListOfEdges[i].StartPoint.Y;
                    edgesToDraw[i].X2 = g1.ListOfEdges[i].EndPoint.X;
                    edgesToDraw[i].Y2 = g1.ListOfEdges[i].EndPoint.Y;
                    edgesToDraw[i].StrokeThickness = 2;
                    Canvas1.Children.Add(edgesToDraw[i]);
                }
                g1.ColorVerticesOfGraph();
                for (int i = 0; i < g1.ListOfVertices.Count; i++)
                {
                    var p = new PointVer();
                    //if (i % 2 == 0)
                    //{
                    //    p.myEllipse.Fill = Brushes.Red;
                    //}
                    p.myEllipse.Fill = g1.ListOfVertices[i].Color;
                    p.TextBlock.Text = g1.ListOfVertices[i].Id.ToString();
                    double left = g1.ListOfVertices[i].X - (p.myEllipse.Width / 2);
                    double top = g1.ListOfVertices[i].Y - (p.myEllipse.Height / 2);
                    p.Margin = new Thickness(left, top, 0, 0);
                    TypeLabel.Content = String.Empty;
                    TypeLabel.Content = $"Liczba użytych kolorów: {g1.ListOfVertices.GroupBy(x => x.Color).Count()}";
                    Canvas1.Children.Add(p);
                }
            }
            else if (RndVertcs.IsChecked == true)
            {
                TitleLabel.Content = "Graf regularny";
                GenerateRegular.IsEnabled = false;
                GenerateSimple.IsEnabled = false;
                Search.IsEnabled = true;
                g1 = Graph.GenerateVerticesOfGraphRandomly(Convert.ToInt32(NumOfVertcs.Text));
                bool isDrawn = false;
                while (isDrawn == false)
                {
                    isDrawn = g1.GenerateEdgesOfRegularGraph(Convert.ToInt32(NumOfVertcs.Text), Convert.ToInt32(Degree.Text), g1);
                }
                Line[] edgesToDraw = new Line[g1.ListOfEdges.Count];
                for (int i = 0; i < g1.ListOfEdges.Count; i++)
                {
                    edgesToDraw[i] = new Line();
                    edgesToDraw[i].Stroke = System.Windows.Media.Brushes.Black;
                    edgesToDraw[i].X1 = g1.ListOfEdges[i].StartPoint.X;
                    edgesToDraw[i].Y1 = g1.ListOfEdges[i].StartPoint.Y;
                    edgesToDraw[i].X2 = g1.ListOfEdges[i].EndPoint.X;
                    edgesToDraw[i].Y2 = g1.ListOfEdges[i].EndPoint.Y;
                    edgesToDraw[i].StrokeThickness = 2;
                    
                    Canvas1.Children.Add(edgesToDraw[i]);
                }
                g1.ColorVerticesOfGraph();
                for (int i = 0; i < g1.ListOfVertices.Count; i++)
                {
                    var p = new PointVer();
                    //if (i % 2 == 0)
                    //{
                    //    p.myEllipse.Fill = Brushes.Red;
                    //}
                    p.myEllipse.Fill = g1.ListOfVertices[i].Color;
                    p.TextBlock.Text = g1.ListOfVertices[i].Id.ToString();
                    double left = g1.ListOfVertices[i].X - (p.myEllipse.Width / 2);
                    double top = g1.ListOfVertices[i].Y - (p.myEllipse.Height / 2);
                    p.Margin = new Thickness(left, top, 0, 0);
                    TypeLabel.Content = String.Empty;
                    TypeLabel.Content = $"Liczba użytych kolorów: {g1.ListOfVertices.GroupBy(x => x.Color).Count()}";
                    Canvas1.Children.Add(p);
                }
            }
        
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var matrix = g1.GenerateAdjacencyMatrix();
            var sb = new StringBuilder();
            sb.AppendLine("Graph generator v1.0 - macierz sąsiedztwa:");
            sb.AppendLine();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sb.Append(matrix[i, j] + " ");
                }
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine("Ciąg przeszukania grafu wszerz: ");
            sb.AppendLine();
            var a = Graph.BFS(matrix, g1);
            for (int i = 0; i < a.Count; i++)
            {
                sb.Append(a[i]+" ");
            }
            File.WriteAllText("wyniki.txt", string.Empty);
            File.AppendAllText("wyniki.txt", sb.ToString());
            Process.Start("wyniki.txt");
        }
    }
}
