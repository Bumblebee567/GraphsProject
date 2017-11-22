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
        }

        private void Wyczyść_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Clear();
        }

        private void GrafOkreślony_Click(object sender, RoutedEventArgs e)
        {
        }

        private void GenerateSimple_Click(object sender, RoutedEventArgs e)
        {
            if (RegVertcs.IsChecked == true)
            {
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
                for (int i = 0; i < g1.ListOfVertices.Count; i++)
                {
                    var p = new PointVer();
                    if (i % 2 == 0)
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
            else if (RndVertcs.IsChecked == true)
            {
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
                for (int i = 0; i < g1.ListOfVertices.Count; i++)
                {
                    var p = new PointVer();
                    if (i % 2 == 0)
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

        private void GenerateRegular_Click(object sender, RoutedEventArgs e)
        {
            if (RegVertcs.IsChecked == true)
            {
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
                for (int i = 0; i < g1.ListOfVertices.Count; i++)
                {
                    var p = new PointVer();
                    if (i % 2 == 0)
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
            else if (RndVertcs.IsChecked == true)
            {
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
                for (int i = 0; i < g1.ListOfVertices.Count; i++)
                {
                    var p = new PointVer();
                    if (i % 2 == 0)
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

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var matrix = g1.GenerateAdjacencyMatrix();
            var a = Graph.BFS(matrix, g1);
            var sb = new StringBuilder();
            for (int i = 0; i < a.Count; i++)
            {
                sb.Append(a[i]);
            }
            File.AppendAllText("wyniki.txt", sb.ToString());
            Process.Start("C:/Users/Admin/Source/Repos/GraphsProject3/WpfApp1/bin/Debug/test.txt");
        }
    }
}
