using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VectorGraphicViewer.Models;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;

namespace VectorGraphicViewer.Rendering
{
    public class ShapeRender
    {
        private readonly Canvas _canvas;
        private double _scale;

        public ShapeRender(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void RenderShapes(List<CustomShapeModel> shapes, double canvasWidth, double canvasHeight)
        {
            _canvas.Children.Clear();
            _scale = Math.Min(canvasWidth, canvasHeight) / 50;

            foreach (var shape in shapes)
            {
                switch (shape)
                {
                    case VectorLine line:
                        DrawLine(line, canvasWidth, canvasHeight);
                        break;
                    case Circle circle:
                        DrawCircle(circle, canvasWidth, canvasHeight);
                        break;
                    case Triangle triangle:
                        DrawTriangle(triangle, canvasWidth, canvasHeight);
                        break;
                    default:
                        throw new NotSupportedException($"Shape of type {shape.GetType().Name} is not supported for rendering.");
                }
            }
        }

        private void DrawLine(VectorLine line, double canvasWidth, double canvasHeight)
        {
            var wpfLine = new Line
            {
                X1 = ScaleX(line.A.X, canvasWidth),
                Y1 = ScaleY(line.A.Y, canvasHeight),
                X2 = ScaleX(line.B.X, canvasWidth),
                Y2 = ScaleY(line.B.Y, canvasHeight),
                Stroke = new SolidColorBrush(ToMediaColor(line.Color)),
                StrokeThickness = 1
            };
            _canvas.Children.Add(wpfLine);
        }

        private void DrawCircle(Circle circle, double canvasWidth, double canvasHeight)
        {
            var diameter = circle.Radius * 2 * _scale;
            var wpfCircle = new Ellipse
            {
                Width = diameter,
                Height = diameter,
                Stroke = new SolidColorBrush(ToMediaColor(circle.Color)),
                StrokeThickness = 1,
                Fill = circle.Filled ? new SolidColorBrush(ToMediaColor(circle.Color)) : null
            };

            Canvas.SetLeft(wpfCircle, ScaleX(circle.Center.X, canvasWidth) - diameter / 2);
            Canvas.SetTop(wpfCircle, ScaleY(circle.Center.Y, canvasHeight) - diameter / 2);
            _canvas.Children.Add(wpfCircle);
        }

        private void DrawTriangle(Triangle triangle, double canvasWidth, double canvasHeight)
        {
            var polygon = new Polygon
            {
                Points = new PointCollection
                {
                    new Point(ScaleX(triangle.A.X, canvasWidth), ScaleY(triangle.A.Y, canvasHeight)),
                    new Point(ScaleX(triangle.B.X, canvasWidth), ScaleY(triangle.B.Y, canvasHeight)),
                    new Point(ScaleX(triangle.C.X, canvasWidth), ScaleY(triangle.C.Y, canvasHeight))
                },
                Stroke = new SolidColorBrush(ToMediaColor(triangle.Color)),
                StrokeThickness = 1,
                Fill = triangle.Filled ? new SolidColorBrush(ToMediaColor(triangle.Color)) : null
            };

            _canvas.Children.Add(polygon);
        }

        private double ScaleX(double x, double canvasWidth)
        {
            return (x * _scale) + (canvasWidth / 2);
        }

        private double ScaleY(double y, double canvasHeight)
        {
            return (canvasHeight / 2) - (y * _scale);
        }

        private Color ToMediaColor(Models.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}