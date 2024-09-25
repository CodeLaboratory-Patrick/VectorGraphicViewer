using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VectorGraphicViewer.Models;
using VectorGraphicViewer.Parsers;
using VectorGraphicViewer.Reader;
using VectorGraphicViewer.Rendering;

namespace VectorGraphicViewer
{
    public partial class MainWindow : Window
    {
        private readonly ShapeRender _shapeRender;
        private List<CustomShapeModel> _shapes;

        public MainWindow()
        {
            InitializeComponent();
            _shapeRender = new ShapeRender(ShapeCanvas);
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            double canvasWidth = ShapeCanvas.ActualWidth;
            double canvasHeight = ShapeCanvas.ActualHeight;

            if (canvasWidth > 0 && canvasHeight > 0 && _shapes != null && _shapes.Count > 0)
            {
                RenderShapes(canvasWidth, canvasHeight);
            }
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "shapes.json";

            try
            {
                _shapes = ShapeFileReader.ReadShapesFromFile(filePath);

                if (_shapes == null || _shapes.Count == 0)
                {
                    MessageBox.Show("Shape data not found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                double canvasWidth = ShapeCanvas.ActualWidth;
                double canvasHeight = ShapeCanvas.ActualHeight;

                if (canvasWidth > 0 && canvasHeight > 0)
                {
                    RenderShapes(canvasWidth, canvasHeight);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ShapeCanvas.Children.Clear();
            ShapeCanvas.Background = new SolidColorBrush(Colors.Black);
        }

        private void RenderShapes(double canvasWidth, double canvasHeight)
        {
            System.Diagnostics.Debug.WriteLine($"Canvas size: {canvasWidth} x {canvasHeight}");
            System.Diagnostics.Debug.WriteLine($"Shapes count: {_shapes?.Count ?? 0}");

            _shapeRender.RenderShapes(_shapes, canvasWidth, canvasHeight);
        }
    }
}
