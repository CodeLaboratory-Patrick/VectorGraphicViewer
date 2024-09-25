using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using VectorGraphicViewer.Models;
using VectorGraphicViewer.Rendering;
using VectorGraphicViewer.Parsers;
using VectorGraphicViewer.Reader;

namespace VectorGraphicViewerUnitTest
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class AdditionalTests
    {
        private ShapeRender _shapeRender;
        private Canvas _canvas;
        private JsonShapeParser _jsonParser;
        private string _testFilePath;

        [SetUp]
        public void SetUp()
        {
            _canvas = new Canvas();
            _shapeRender = new ShapeRender(_canvas);
            _jsonParser = new JsonShapeParser();
            _testFilePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "shapesTest.json");
        }

        [Test]
        public void ParsePoint_ShouldHandleCommaAndSemicolon()
        {
            var point1 = JsonShapeParser.ParsePoint("-1,5; 3,4");
            var point2 = JsonShapeParser.ParsePoint("2,2; 5,7");

            Assert.That(point1.X, Is.EqualTo(-1.5));
            Assert.That(point1.Y, Is.EqualTo(3.4));
            Assert.That(point2.X, Is.EqualTo(2.2));
            Assert.That(point2.Y, Is.EqualTo(5.7));
        }

        [Test]
        public void ParseJson_ShouldHandleAllShapeTypes()
        {

            List<CustomShapeModel> shapes = ShapeFileReader.ReadShapesFromFile(_testFilePath);

            Assert.That(shapes.Count, Is.EqualTo(3));
            Assert.That(shapes[0], Is.TypeOf<VectorLine>());
            Assert.That(shapes[1], Is.TypeOf<Circle>());
            Assert.That(shapes[2], Is.TypeOf<Triangle>());
        }

        [Test]
        public void RenderShapes_ShouldHandleEmptyList()
        {
            _shapeRender.RenderShapes(new List<CustomShapeModel>(), 800, 600);
            Assert.That(_canvas.Children.Count, Is.EqualTo(0));
        }

        [Test]
        public void ParsePoint_ShouldThrowFormatException_WhenInvalidFormat()
        {
            Assert.Throws<FormatException>(() => JsonShapeParser.ParsePoint("invalid format"));
        }

        [Test]
        public void ParseColor_ShouldThrowFormatException_WhenInvalidFormat()
        {
            Assert.Throws<FormatException>(() => JsonShapeParser.ParseColor("invalid; color; format"));
        }
    }
}