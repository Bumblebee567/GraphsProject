﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    class Vertex
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Brush Color { get; set; }
        public Vertex(double x, double y, int id)
        {
            X = x;
            Y = y;
            Id = id;
        }
        public Vertex()
        {

        }
        public List<Vertex> ConnectedVertices = new List<Vertex>();
    }
}
