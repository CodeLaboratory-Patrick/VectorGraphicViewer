using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using VectorGraphicViewer.Models;
using VectorGraphicViewer.Rendering;


namespace VectorGraphicViewerUnitTest
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class RendererTests
    {
        private ShapeRender _shapeRender;
        private Canvas _canvas;

        [SetUp]
        public void SetUp()
        {
            _canvas = new Canvas();
            _shapeRender = new ShapeRender(_canvas);
        }

        [Test]
        public void RenderShapes_ShouldAddShapesToCanvas()
        {
            var shapes = new List<CustomShapeModel>
            {
                new VectorLine { A = new Point { X = 0, Y = 0 }, B = new Point { X = 100, Y = 100 }, Color = new Color { A = 255, R = 0, G = 0, B = 255 } }
            };

            _shapeRender.RenderShapes(shapes, 800, 600);

            Assert.That(_canvas.Children.Count, Is.EqualTo(1), "There should be one shape added to the canvas.");
            Assert.That(_canvas.Children[0], Is.TypeOf<Line>(), "The first shape should be a Line.");
        }

        [Test]
        public void RenderShapes_ShouldAddMultipleShapesToCanvas()
        {
            var shapes = new List<CustomShapeModel>
    {
        new VectorLine { A = new Point { X = 0, Y = 0 }, B = new Point { X = 100, Y = 100 }, Color = new Color { A = 255, R = 0, G = 0, B = 255 } },
        new Circle { Center = new Point { X = 50, Y = 50 }, Radius = 25, Filled = true, Color = new Color { A = 255, R = 0, G = 255, B = 0 } }
    };
            _shapeRender.RenderShapes(shapes, 800, 600);

            Assert.That(_canvas.Children.Count, Is.EqualTo(2), "There should be two shapes added to the canvas.");
            Assert.That(_canvas.Children[0], Is.TypeOf<Line>(), "The first shape should be a Line.");
            Assert.That(_canvas.Children[1], Is.TypeOf<Ellipse>(), "The second shape should be an Ellipse (Circle).");
        }

    }
}
