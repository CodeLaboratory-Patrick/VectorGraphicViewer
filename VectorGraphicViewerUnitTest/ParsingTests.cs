using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorGraphicViewer.Models;
using VectorGraphicViewer.Parsers;

namespace VectorGraphicViewerUnitTest
{
    [TestFixture]
    public class ParsingTests
    {
        [Test]
        public void ParsePoint_CustomPoint()
        {
            string pointString = "10;20";

            Point point = JsonShapeParser.ParsePoint(pointString);

            Assert.That(point.X, Is.EqualTo(10));
            Assert.That(point.Y, Is.EqualTo(20));
        }

        [Test]
        public void ParseColor_CustomColor()
        {
            string colorString = "127;255;255;255";

            Color color = JsonShapeParser.ParseColor(colorString);

            Assert.That(color.A, Is.EqualTo(127));
            Assert.That(color.R, Is.EqualTo(255));
            Assert.That(color.G, Is.EqualTo(255));
            Assert.That(color.B, Is.EqualTo(255));
        }
    }
}
