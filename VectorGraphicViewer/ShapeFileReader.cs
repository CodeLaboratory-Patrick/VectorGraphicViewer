using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorGraphicViewer.Models;
using VectorGraphicViewer.Parsers;

namespace VectorGraphicViewer.Reader
{
    public class ShapeFileReader
    {
        public static List<CustomShapeModel> ReadShapesFromFile(string filePath)
        {
            try
            {
                string fileExtension = Path.GetExtension(filePath).ToLower();
                IShapeParser parser = fileExtension switch
                {
                    ".json" => new JsonShapeParser(),
                    ".xml" => new XmlShapeParser(),
                    _ => throw new NotSupportedException($"File format '{fileExtension}' is not supported.")
                };

                string fileData = File.ReadAllText(filePath);
                return parser.Parse(fileData);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"File not found at path: {filePath}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while reading or parsing the file: {ex.Message}");
            }
        }
    }
}
