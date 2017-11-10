using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    class Graph
    {
        static Random rnd = new Random();
        public int NumberOfVertices { get; set; }
        public List<Vertex> ListOfVertices = new List<Vertex>();
        public List<Edge> ListOfEdges = new List<Edge>();
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in ListOfVertices)
            {
                sb.AppendLine("Id:" + item.Id + " X: " + item.X + " Y: " + item.Y);
            }
            return sb.ToString();
        }
        public static Graph GenerateGraph(int numOfVertices)
        {
            var graph = new Graph();
            graph.NumberOfVertices = numOfVertices;
            double x, y;
            for (int i = 0; i < numOfVertices; i++)
            {
                x = 310 + 250 * Math.Cos(2 * Math.PI * i / numOfVertices);
                y = 285 + 250 * Math.Sin(2 * Math.PI * i / numOfVertices);
                graph.ListOfVertices.Add(new Vertex(x, y, i));
            }
            return graph;
        }
        static int counter = 0;
        public int[,] GenerateMatrix(int degree)
        {
            int[,] matrix = new int[NumberOfVertices, NumberOfVertices];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (counter >= degree)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        if (i == j)
                        {
                            matrix[i, j] = 0;
                        }
                        else
                        {
                            matrix[i, j] = 1;
                            counter += 1;
                        }
                    }
                }
                counter = 0;
            }
            return matrix;
        }
        public int[,] GenerateAdciedenceMatrix(int degree)
        {
            var middlepoint = NumberOfVertices / 2;
            int[,] matrix = new int[NumberOfVertices, NumberOfVertices];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = i; j < matrix.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = 0;
                    }
                    else
                    {
                        if (j == (middlepoint + i) - 1 || j == (middlepoint + i) + 1)
                        {
                            matrix[i, j] = 1;
                        }
                        else
                        {
                            matrix[i, j] = 0;
                        }
                    }
                }
            }
            for (int i = 0; i < NumberOfVertices; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    matrix[i, j] = matrix[j, i];
                }
            }
            return matrix;
        }
        public int[,] GenerateAjacenceMatrixBeta(int degree)
        {
            int[,] matrix = new int[NumberOfVertices, NumberOfVertices];
            int randomIndex = 0;
            //wypełnienie macierzy zerami
            for (int i = 0; i < NumberOfVertices; i++)
            {
                for (int j = 0; j < NumberOfVertices; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = 0; i < NumberOfVertices; i++)
            {
                for (int c = 0; c < degree; c++)
                {
                    randomIndex = rnd.Next(0, NumberOfVertices - 1);
                    if (randomIndex != i)
                        matrix[i, randomIndex] = 1;
                    else
                        c--;
                }
            }
            return matrix;
        }
        
        
        public void GenerateEdges(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        ListOfEdges.Add(new Edge { StartPoint = ListOfVertices[i], EndPoint = ListOfVertices[j] });
                    }
                }
            }
        }
        public void GenerateEdgesOfRandomGraph(Graph graph, int degree)
        {
            int counter = 0;
            int remainingEdges = 0;
            List<Vertex> verticesToConnect = new List<Vertex>(); //lista potencjalnych wierzchołków do połączenia
            for (int i = 0; i < graph.ListOfVertices.Count; i++)
            {
                if (i == 0)
                {
                    //z wierzchołka 0 tworzy liczbę krawędzi taką jak stopień grafu
                    for (int j = 0; j < degree; j++)
                    {
                        graph.ListOfEdges.Add(new Edge
                        {
                            StartPoint = graph.ListOfVertices[i],
                            EndPoint = graph.ListOfVertices[rnd.Next(i + 1, graph.ListOfVertices.Count - 1)]
                        });
                    }
                }
                else
                {
                    //sprawdza ile krawędzi zawiera w sobie dany wierzchołek
                    counter = CountNumberOfEdgesCointaingVertex(graph, graph.ListOfVertices[i]);
                    if (counter >= degree)
                        continue;
                    else if (counter < degree)
                    {
                        //wyznacza ile krawędzi wychodzących/wchodzących do/z wierzchołka brakuje by miał stopień degree-1
                        remainingEdges = (degree) - counter;
                        //pętla tworzy listę potencjalnych wierzchołków, do których może być poprowadzona krawędź z wierzchołka
                        for (int k = 0; k < graph.ListOfVertices.Count; k++)
                        {
                            if (graph.ListOfVertices[k] != graph.ListOfVertices[i] && CountNumberOfEdgesCointaingVertex(graph, graph.ListOfVertices[k]) < degree)
                            {
                                verticesToConnect.Add(graph.ListOfVertices[k]);
                            }
                        }
                        //mamy dodane wierzchołki, do których możemy proprawdzić krawędzie z obecnego wierzchołka
                        for (int j = 0; j < remainingEdges; j++)
                        {
                            if (j >= verticesToConnect.Count)
                                break;
                            graph.ListOfEdges.Add(new Edge { StartPoint = graph.ListOfVertices[i], EndPoint = verticesToConnect[j] });
                        }
                        verticesToConnect.Clear();
                    }
                }
            }
        }
        public int CountNumberOfEdgesCointaingVertex(Graph graph, Vertex vertex)
        {
            return graph.ListOfEdges.Where(x => x.StartPoint == vertex || x.EndPoint == vertex).Count();
        }
    }
}
