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

        public MainWindow()
        {
            InitializeComponent();
            shapeSelection = "";
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
            }
        }
    }
}
