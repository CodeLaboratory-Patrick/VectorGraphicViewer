using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorGraphicViewer.Models
{
    public abstract class CustomShapeModel
    {
        public string Type { get; set; }
        public Color Color { get; set; }
    }

    public class VectorLine : CustomShapeModel
    {
        public Point A { get; set; }
        public Point B { get; set; }
    }
    public class Circle : CustomShapeModel
    {
        public Point Center { get; set; }
        public double Radius { get; set; }
        public bool Filled { get; set; }
    }

    public class Triangle : CustomShapeModel
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }

        public bool Filled { get; set; }
    }

    public class Rectangle : CustomShapeModel
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }
        public Point D { get; set; }

        public bool Filled { get; set; }
    }

    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public struct Color
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
}
