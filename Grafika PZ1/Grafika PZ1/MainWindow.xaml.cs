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

        private List<UIElement> listOfElements;

        public List<UIElement> ListOfElements
        {
            get { return listOfElements; }
            set { listOfElements = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
            shapeSelection = "";
            polygonPoints = new List<Point>();
            polygonDots = new List<Line>();
            listOfElements = new List<UIElement>();
            this.DataContext = this;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            if (shapeSelection == "")
            {
                shapeSelection = "Ellipse";
                EllipseButton.Background = Brushes.LightBlue;
            }
            else
            {
                if (shapeSelection == "Ellipse")
                {
                    shapeSelection = "";
                    EllipseButton.Background = Brushes.Transparent;
                }
                else
                {
                    shapeSelection = "Ellipse";
                    EllipseButton.Background = Brushes.LightBlue;
                    RectangleButton.Background = Brushes.Transparent;
                    PolygonButton.Background = Brushes.Transparent;
                    ImageButton.Background = Brushes.Transparent;
                }
            }
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            if (shapeSelection == "")
            {
                shapeSelection = "Rectangle";
                RectangleButton.Background = Brushes.LightBlue;
            }
            else
            {
                if(shapeSelection == "Rectangle")
                {
                    shapeSelection = "";
                    RectangleButton.Background = Brushes.Transparent;
                }
                else
                {
                    shapeSelection = "Rectangle";
                    EllipseButton.Background = Brushes.Transparent;
                    RectangleButton.Background = Brushes.LightBlue;
                    PolygonButton.Background = Brushes.Transparent;
                    ImageButton.Background = Brushes.Transparent;
                }
            }
        }

        private void PolygonButton_Click(object sender, RoutedEventArgs e)
        {
            if (shapeSelection == "")
            {
                shapeSelection = "Polygon";
                PolygonButton.Background = Brushes.LightBlue;
            }
            else
            {
                if (shapeSelection == "Polygon")
                {
                    shapeSelection = "";
                    PolygonButton.Background = Brushes.Transparent;
                    PolygonPoints.Clear();
                    foreach (Line dot in PolygonDots)
                    {
                        PaintingCanvas.Children.Remove(dot);
                    }
                    polygonDots.Clear();
                }
                else
                {
                    shapeSelection = "Polygon";
                    EllipseButton.Background = Brushes.Transparent;
                    RectangleButton.Background = Brushes.Transparent;
                    PolygonButton.Background = Brushes.LightBlue;
                    ImageButton.Background = Brushes.Transparent;
                }
            }
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (shapeSelection == "")
            {
                shapeSelection = "Image";
                ImageButton.Background = Brushes.LightBlue;
            }
            else
            {
                if (shapeSelection == "Image")
                {
                    shapeSelection = "";
                    ImageButton.Background = Brushes.Transparent;
                }
                else
                {
                    shapeSelection = "Image";
                    EllipseButton.Background = Brushes.Transparent;
                    RectangleButton.Background = Brushes.Transparent;
                    PolygonButton.Background = Brushes.Transparent;
                    ImageButton.Background = Brushes.LightBlue;
                }
            }
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
            
            if(shapeSelection == "Image")
            {
                ImageSettings imageSettings = new ImageSettings(PaintingCanvas, Mouse.GetPosition(PaintingCanvas));
                imageSettings.ShowDialog();
                return;
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

            if(shapeSelection == "")
            {
                if(e.OriginalSource is Ellipse)
                {
                    Ellipse ellipse = (Ellipse)e.OriginalSource;

                    Point position = ellipse.TranslatePoint(new Point(0, 0), PaintingCanvas);

                    ShapesSettings shapesSettings = new ShapesSettings("Ellipse", PaintingCanvas, position,
                                                                        ellipse.Width.ToString(),
                                                                        ellipse.Height.ToString(),
                                                                        ((SolidColorBrush)ellipse.Fill).Color,
                                                                        ((SolidColorBrush)ellipse.Stroke).Color,
                                                                        ellipse.StrokeThickness.ToString(),
                                                                        ellipse, null);

                    shapesSettings.ShowDialog();

                    return;
                }

                if(e.OriginalSource is Rectangle)
                {
                    Rectangle rectangle = (Rectangle)e.OriginalSource;

                    Point position = rectangle.TranslatePoint(new Point(0, 0), PaintingCanvas);

                    ShapesSettings shapesSettings = new ShapesSettings("Rectangle", PaintingCanvas, position,
                                                                        rectangle.Width.ToString(),
                                                                        rectangle.Height.ToString(),
                                                                        ((SolidColorBrush)rectangle.Fill).Color,
                                                                        ((SolidColorBrush)rectangle.Stroke).Color,
                                                                        rectangle.StrokeThickness.ToString(),
                                                                        null, rectangle);

                    shapesSettings.ShowDialog();

                    return;
                }

                if(e.OriginalSource is Polygon)
                {
                    Polygon polygon = (Polygon)e.OriginalSource;

                    PolygonSettings polygonSettings = new PolygonSettings(PaintingCanvas, polygon.Points.ToList(), ((SolidColorBrush)polygon.Fill).Color, ((SolidColorBrush)polygon.Stroke).Color, polygon);

                    polygonSettings.ShowDialog();

                    return;
                }

                if(e.OriginalSource is Image)
                {
                    Image image = (Image)e.OriginalSource;

                    Point position = image.TranslatePoint(new Point(0, 0), PaintingCanvas);

                    ImageSettings imageSettings = new ImageSettings(PaintingCanvas, position, image.Width.ToString(), image.Height.ToString(), image.Source.ToString(), image);

                    imageSettings.ShowDialog();

                    return;
                }
            }
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            PolygonPoints.Clear();
            foreach (Line dot in PolygonDots)
            {
                PaintingCanvas.Children.Remove(dot);
            }
            polygonDots.Clear();

            if (PaintingCanvas.Children.Count != 0)
            {
                listOfElements.Add(PaintingCanvas.Children[PaintingCanvas.Children.Count - 1]);
                PaintingCanvas.Children.Remove(PaintingCanvas.Children[PaintingCanvas.Children.Count - 1]);
            }
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            if (listOfElements.Count != 0)
            {
                PaintingCanvas.Children.Add(listOfElements.Last());
                listOfElements.Remove(listOfElements.Last());
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(UIElement element in PaintingCanvas.Children)
            {
                listOfElements.Add(element);
            }

            PaintingCanvas.Children.Clear();
        }
    }
}
