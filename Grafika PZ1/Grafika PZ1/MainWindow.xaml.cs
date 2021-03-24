using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grafika_PZ1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string shapeSelection;

        public string ShapeSelection
        {
            get { return shapeSelection; }
            set { shapeSelection = value; }
        }

        private List<Point> polygonPoints;

        public List<Point> PolygonPoints
        {
            get { return polygonPoints; }
            set { polygonPoints = value; }
        }

        private List<Line> polygonDots;

        public List<Line> PolygonDots
        {
            get { return polygonDots; }
            set { polygonDots = value; }
        }



        public MainWindow()
        {
            InitializeComponent();
            shapeSelection = "";
            polygonPoints = new List<Point>();
            polygonDots = new List<Line>();
            this.DataContext = this;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            shapeSelection = "Ellipse";
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            shapeSelection = "Rectangle";
        }

        private void PolygonButton_Click(object sender, RoutedEventArgs e)
        {
            shapeSelection = "Polygon";
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            shapeSelection = "Image";
        }

        private void PaintingCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (shapeSelection == "Ellipse" || shapeSelection == "Rectangle")
            {
                ShapesSettings shapesSettings = new ShapesSettings(shapeSelection, PaintingCanvas, Mouse.GetPosition(PaintingCanvas));
                shapesSettings.ShowDialog();
                return;
            }

            if (shapeSelection == "Polygon")
            {
                Line dot = new Line();
                dot.Stroke = System.Windows.Media.Brushes.Black;
                dot.StrokeThickness = 1;

                Point mousePoint = Mouse.GetPosition(PaintingCanvas);
                dot.X1 = mousePoint.X;
                dot.X2 = mousePoint.X + 2;
                dot.Y1 = mousePoint.Y;
                dot.Y2 = mousePoint.Y;

                PaintingCanvas.Children.Add(dot);
                polygonDots.Add(dot);

                polygonPoints.Add(mousePoint);
            }
        }
            
        private void PaintingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(shapeSelection == "Polygon")
            {
                PolygonSettings polygonSettings = new PolygonSettings(PaintingCanvas, PolygonPoints);
                polygonSettings.ShowDialog();
      
                foreach(Line dot in polygonDots)
                {
                    PaintingCanvas.Children.Remove(dot);
                }
                
                return;
            }
        }
    }
}
