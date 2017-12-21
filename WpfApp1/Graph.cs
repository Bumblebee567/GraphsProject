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
        public static Graph GenerateVerticesGraph(int numOfVertices)
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
        public static Graph GenerateVerticesOfGraphRandomly(int numOfVertices)
        {
            var graph = new Graph();
            graph.NumberOfVertices = numOfVertices;
            double x, y;
            for (int i = 0; i < numOfVertices; i++)
            {
                x = rnd.Next(30, 589);
                y = rnd.Next(30, 541);
                graph.ListOfVertices.Add(new Vertex(x, y, i));
            }
            return graph;
        }
        //static int counter = 0;
        //public int[,] GenerateMatrix(int degree)
        //{
        //    int[,] matrix = new int[NumberOfVertices, NumberOfVertices];
        //    for (int i = 0; i < matrix.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < matrix.GetLength(1); j++)
        //        {
        //            if (counter >= degree)
        //            {
        //                matrix[i, j] = 0;
        //            }
        //            else
        //            {
        //                if (i == j)
        //                {
        //                    matrix[i, j] = 0;
        //                }
        //                else
        //                {
        //                    matrix[i, j] = 1;
        //                    counter += 1;
        //                }
        //            }
        //        }
        //        counter = 0;
        //    }
        //    return matrix;
        //}
        //public int[,] GenerateAdciedenceMatrix(int degree)
        //{
        //    var middlepoint = NumberOfVertices / 2;
        //    int[,] matrix = new int[NumberOfVertices, NumberOfVertices];
        //    for (int i = 0; i < matrix.GetLength(0); i++)
        //    {
        //        for (int j = i; j < matrix.GetLength(1); j++)
        //        {
        //            if (i == j)
        //            {
        //                matrix[i, j] = 0;
        //            }
        //            else
        //            {
        //                if (j == (middlepoint + i) - 1 || j == (middlepoint + i) + 1)
        //                {
        //                    matrix[i, j] = 1;
        //                }
        //                else
        //                {
        //                    matrix[i, j] = 0;
        //                }
        //            }
        //        }
        //    }
        //    for (int i = 0; i < NumberOfVertices; i++)
        //    {
        //        for (int j = 0; j < i; j++)
        //        {
        //            matrix[i, j] = matrix[j, i];
        //        }
        //    }
        //    return matrix;
        //}
        //public int[,] GenerateAjacenceMatrixBeta(int degree)
        //{
        //    int[,] matrix = new int[NumberOfVertices, NumberOfVertices];
        //    int randomIndex = 0;
        //    //wypełnienie macierzy zerami
        //    for (int i = 0; i < NumberOfVertices; i++)
        //    {
        //        for (int j = 0; j < NumberOfVertices; j++)
        //        {
        //            matrix[i, j] = 0;
        //        }
        //    }
        //    for (int i = 0; i < NumberOfVertices; i++)
        //    {
        //        for (int c = 0; c < degree; c++)
        //        {
        //            randomIndex = rnd.Next(0, NumberOfVertices - 1);
        //            if (randomIndex != i)
        //                matrix[i, randomIndex] = 1;
        //            else
        //                c--;
        //        }
        //    }
        //    return matrix;
        //}


        //public void GenerateEdgesFromMatrix(int[,] matrix)
        //{
        //    for (int i = 0; i < matrix.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < matrix.GetLength(1); j++)
        //        {
        //            if (matrix[i, j] == 1)
        //            {
        //                ListOfEdges.Add(new Edge { StartPoint = ListOfVertices[i], EndPoint = ListOfVertices[j] });
        //            }
        //        }
        //    }
        //}
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
                            //graph.ListOfVertices[i].ConnectedVertices.Add(verticesToConnect[j]);
                            //graph.ListOfVertices[j].ConnectedVertices.Add(verticesToConnect[i]);
                        }
                        verticesToConnect.Clear();
                    }
                }
            }
            for (int i = 0; i < ListOfEdges.Count; i++)
            {
                ListOfEdges[i].StartPoint.ConnectedVertices.Add(ListOfEdges[i].EndPoint);
                ListOfEdges[i].EndPoint.ConnectedVertices.Add(ListOfEdges[i].StartPoint);
            }
        }
        public int CountNumberOfEdgesCointaingVertex(Graph graph, Vertex vertex)
        {
            return graph.ListOfEdges.Where(x => x.StartPoint == vertex || x.EndPoint == vertex).Count();
        }

        static int index;
        static int pointToConnect;
        static IEnumerable<int> range;
        static bool checker = false;
        HashSet<int> excludeBucketIndex = new HashSet<int>();
        public bool GenerateEdgesOfRegularGraph(int numOfVertices, int degree, Graph g)
        {
            bool checkStop;
            bool checkFree;
            int numberOfEdges = (numOfVertices * degree) / 2;
            range = Enumerable.Range(0, degree);
            Bucket[] pointsContainers = new Bucket[numOfVertices];
            for (int i = 0; i < pointsContainers.Length; i++)
            {
                pointsContainers[i] = new Bucket();
                pointsContainers[i].Id = i;
            }

            for (int i = 0; i < pointsContainers.Length; i++)
            {
                for (int j = 0; j < degree; j++)
                {
                    pointsContainers[i].Points.Add(new PointInBucket((i * degree) + j));
                }
            }
            //kontenery z punktami zrobione
            foreach (var container in pointsContainers)
            {
                for (int i = 0; i < degree; i++)
                {
                    if (container.Points[i].IsConnected == true)
                    {
                        continue;
                    }
                    else
                    {
                        while (checker != true)
                        {
                            checkFree = CheckFreeVertices(pointsContainers, degree);
                            if (checkFree == true && _CheckIfConnectionFromBucketIsPossible(container, pointsContainers, degree) == true)
                            {
                            }
                            else
                            {
                                if (pointsContainers.Any(x => x.ConnectedBuckets.Count < degree))
                                {
                                    checker = true;
                                    return false;
                                }
                            }
                            excludeBucketIndex.Add(Array.IndexOf(pointsContainers, container));
                            index = rnd.Next(0, numOfVertices); //index losowego koszyka, różnego od obecnego
                            if (index == container.Id)
                            {
                                checker = false;
                            }
                            else if (pointsContainers[index].Points.All(x => x.IsConnected == true))
                            {
                                //Console.WriteLine("wszystkie punkty w wylosowanym kontenerze są już połączone");
                            }
                            else
                            {
                                if (pointsContainers[index].ConnectedBuckets.Contains(container))
                                {
                                    checker = false;
                                }
                                else
                                {
                                    checker = true;
                                }
                            }
                        }
                        checker = false;
                        while (checker != true)
                        {
                            pointToConnect = rnd.Next(pointsContainers[index].Points.Count);
                            if (pointsContainers[index].Points[pointToConnect].IsConnected == true)
                            {
                                checker = false;
                            }
                            else
                            {
                                container.Points[i].IsConnected = true;
                                pointsContainers[index].Points[pointToConnect].IsConnected = true;
                                if (pointsContainers[index].Points[pointToConnect].ConnectedPoint != null)
                                {
                                    checker = false;
                                }
                                else
                                {
                                    checker = true;
                                }
                            }
                        }
                        checker = false;
                        container.Points[i].ConnectedPoint = pointsContainers[index].Points[pointToConnect];
                        pointsContainers[index].Points[pointToConnect] = container.Points[i].ConnectedPoint;
                        container.ConnectedBuckets.Add(pointsContainers[index]);
                        pointsContainers[index].ConnectedBuckets.Add(container);
                    }
                }
            }

            foreach (var container in pointsContainers)
            {
                for (int i = 0; i < container.ConnectedBuckets.Count; i++)
                {
                    ListOfEdges.Add(new Edge
                    {
                        StartPoint = ListOfVertices[Array.IndexOf(pointsContainers, container)],
                        EndPoint = ListOfVertices[Array.IndexOf(pointsContainers, container.ConnectedBuckets[i])]
                    });
                    var bucketToDelete = container.ConnectedBuckets[i].ConnectedBuckets.Where(x => x.Id == container.Id).First();
                    container.ConnectedBuckets[i].ConnectedBuckets.Remove(bucketToDelete);
                    ListOfVertices[Array.IndexOf(pointsContainers, container)].ConnectedVertices.Add(ListOfVertices[Array.IndexOf(pointsContainers, container.ConnectedBuckets[i])]);
                    ListOfVertices[Array.IndexOf(pointsContainers, container.ConnectedBuckets[i])].ConnectedVertices.Add(ListOfVertices[Array.IndexOf(pointsContainers, container)]);

                }
            }
            return true;
        }
        private static bool _CheckIfConnectionFromBucketIsPossible(Bucket checkedBucket, Bucket[] bucketCollection, int degree)
        {
            var counter = 0;
            foreach (var bucket in bucketCollection)
            {
                if (bucket.ConnectedBuckets.Count == degree || bucket.ConnectedBuckets.Contains(checkedBucket))
                {
                    counter++;
                }
            }
            if (counter == bucketCollection.Count() - 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool CheckFreeVertices(Bucket[] bucketCollection, int degree)
        {
            int counter = 0;
            int degreeCounter = 0;
            bool checker = false;
            List<Bucket> bucketsWithoutMaximalDegree = new List<Bucket>();
            foreach (var bucket in bucketCollection)
            {
                if (bucket.ConnectedBuckets.Count == degree)
                {
                    counter++;
                }
                else
                {
                    degreeCounter++;
                    bucketsWithoutMaximalDegree.Add(bucket);
                }
            }
            if (bucketsWithoutMaximalDegree.Count == 2)
            {
                if (bucketsWithoutMaximalDegree[0].ConnectedBuckets.Contains(bucketsWithoutMaximalDegree[1]) && bucketsWithoutMaximalDegree[1].ConnectedBuckets.Contains(bucketsWithoutMaximalDegree[0]))
                {
                    checker = false;
                    return false;
                }
            }
            if (counter == bucketCollection.Length - 1)
            {
                return false;
            }
            else if (counter == degree - 2 && checker == true)
            {
                bucketsWithoutMaximalDegree.Clear();
                return false;
            }
            else
            {
                bucketsWithoutMaximalDegree.Clear();
                return true;
            }
        }
        public int[,] GenerateAdjacencyMatrix()
        {
            int[,] matrix = new int[NumberOfVertices, NumberOfVertices];
            for (int i = 0; i < NumberOfVertices; i++)
            {
                for (int j = 0; j < NumberOfVertices; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            for (int i = 0; i < NumberOfVertices; i++)
            {
                for (int j = 0; j < ListOfVertices[i].ConnectedVertices.Count; j++)
                {
                    matrix[i, ListOfVertices[i].ConnectedVertices[j].Id] = 1;
                    matrix[ListOfVertices[i].ConnectedVertices[j].Id, i] = 1;
                }
            }
            return matrix;
        }
        public static List<int> BFS(int[,] adjacencyMatrix, Graph g)
        {
            Stack<int> stack = new Stack<int>();
            List<int> results = new List<int>();
            stack.Push(g.ListOfVertices[0].Id);
            bool[] visitedVertices = new bool[adjacencyMatrix.Length];
            while (stack.Count != 0)
            {
                int takenVertex = stack.Pop();
                if (visitedVertices[takenVertex] == false)
                {
                    visitedVertices[takenVertex] = true;
                    for (int i = adjacencyMatrix.GetLength(0) - 1; i >= 0; i--)
                    {
                        if (adjacencyMatrix[takenVertex, i] != 0)
                        {
                            stack.Push(i);
                        }
                    }
                    results.Add(takenVertex);
                }
            }
            return results;
        }
        public void ColorVerticesOfGraph()
        {
            List<Brush> colorsToRemove = new List<Brush>();
            List<Brush> tableAfterRemoving = new List<Brush>();
            List<int> indexesToRemove = new List<int>();
            var table = Utiles.GenerateRandomBrushes(ListOfVertices.Count);
            for (int i = 0; i < ListOfVertices.Count; i++)
            {
                if (i == 0)
                {
                    ListOfVertices[i].Color = table[0];
                }
                else
                {
                    bool[] checkerTab = new bool[ListOfVertices.Count];
                    checkerTab = checkerTab.Select(x => x = true).ToArray();
                    foreach (var vertex in ListOfVertices[i].ConnectedVertices)
                    {
                        if (vertex.Color != null)
                        {
                            //indexesToRemove.Add(Array.IndexOf(table, vertex.Color));
                            checkerTab[Array.IndexOf(table, vertex.Color)] = false;
                        }
                    }
                    //tableAfterRemoving = table.ToList();
                    //for (int j = 0; j < colorsToRemove.Count; j++)
                    //{
                    //    tableAfterRemoving.RemoveAt(indexesToRemove[j]);
                    //}
                    for (int j = 0; j < checkerTab.Length; j++)
                    {
                        if (checkerTab[j] == true)
                        {
                            ListOfVertices[i].Color = table[j];
                            break;
                        }
                    }
                    //tableAfterRemoving.Clear();
                }
            }
        }
    }
}
