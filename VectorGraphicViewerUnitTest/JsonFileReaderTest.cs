using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using VectorGraphicViewer.Models;
using VectorGraphicViewer.Parsers;
using VectorGraphicViewer.Reader;

namespace VectorGraphicViewerUnitTest
{
    [TestFixture]
    public class JsonFileReaderTest
    {
        private string testFilePath;

        [SetUp]
        public void Setup()
        {
            testFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "shapesTest.json");
        }

        [Test]
        public void ReadShapesFromFile_Should_Parse_Json_File_Correctly()
        {
            List<CustomShapeModel> shapes = ShapeFileReader.ReadShapesFromFile(testFilePath);

            Assert.That(shapes.Count, Is.EqualTo(3),
                string.Format("Expected 3 shapes, but found {0}.", shapes.Count));

            Assert.That(shapes[0].Type, Is.EqualTo("line"),
                string.Format("Expected first shape to be 'line', but found {0}.", shapes[0].Type));

            Assert.That(shapes[1].Type, Is.EqualTo("circle"),
                string.Format("Expected second shape to be 'circle', but found {0}.", shapes[1].Type));

            Assert.That(shapes[2].Type, Is.EqualTo("triangle"),
                string.Format("Expected third shape to be 'triangle', but found {0}.", shapes[2].Type));
        }
    }
}