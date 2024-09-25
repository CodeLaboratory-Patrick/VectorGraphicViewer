using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VectorGraphicViewer.Models;

namespace VectorGraphicViewer.Parsers
{
    public class JsonShapeParser : IShapeParser
    {
        public List<CustomShapeModel> Parse(string jsonData)
        {
            var shapes = new List<CustomShapeModel>();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var jsonElements = JsonSerializer.Deserialize<JsonElement[]>(jsonData, options);

            foreach (var element in jsonElements)
            {
                var type = element.GetProperty("type").GetString();
                CustomShapeModel shape = type switch
                {
                    "line" => ParseLine(element),
                    "circle" => ParseCircle(element),
                    "triangle" => ParseTriangle(element),
                    "rectangle" => ParseRectangle(element),
                    _ => throw new NotSupportedException($"Shape type '{type}' is not supported.")
                };
                shapes.Add(shape);
            }

            return shapes;
        }

        private static VectorLine ParseLine(JsonElement element)
        {
            return new VectorLine
            {
                Type = "line",
                A = ParsePoint(element.GetProperty("a").GetString()),
                B = ParsePoint(element.GetProperty("b").GetString()),
                Color = ParseColor(element.GetProperty("color").GetString())
            };
        }

        private static Circle ParseCircle(JsonElement element)
        {
            return new Circle
            {
                Type = "circle",
                Center = ParsePoint(element.GetProperty("center").GetString()),
                Radius = element.GetProperty("radius").GetDouble(),
                Filled = element.GetProperty("filled").GetBoolean(),
                Color = ParseColor(element.GetProperty("color").GetString())
            };
        }

        private static Triangle ParseTriangle(JsonElement element)
        {
            return new Triangle
            {
                Type = "triangle",
                A = ParsePoint(element.GetProperty("a").GetString()),
                B = ParsePoint(element.GetProperty("b").GetString()),
                C = ParsePoint(element.GetProperty("c").GetString()),
                Filled = element.GetProperty("filled").GetBoolean(),
                Color = ParseColor(element.GetProperty("color").GetString())
            };
        }

        private static Rectangle ParseRectangle(JsonElement element)
        {
            return new Rectangle
            {
                Type = "rectangle",
                A = ParsePoint(element.GetProperty("a").GetString()),
                B = ParsePoint(element.GetProperty("b").GetString()),
                C = ParsePoint(element.GetProperty("c").GetString()),
                D = ParsePoint(element.GetProperty("d").GetString()),
                Filled = element.GetProperty("filled").GetBoolean(),
                Color = ParseColor(element.GetProperty("color").GetString())
            };
        }

        public static Point ParsePoint(string pointString)
        {
            pointString = pointString.Replace(',', '.');

            var coordinates = pointString.Split(';');
            return new Point
            {
                X = double.Parse(coordinates[0]),
                Y = double.Parse(coordinates[1])
            };
        }

        public static Color ParseColor(string colorString)
        {
            var components = colorString.Split(';');
            return new Color
            {
                A = byte.Parse(components[0]),
                R = byte.Parse(components[1]),
                G = byte.Parse(components[2]),
                B = byte.Parse(components[3])
            };
        }
    }
}
