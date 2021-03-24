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
using System.Windows.Shapes;

namespace Grafika_PZ1
{
    /// <summary>
    /// Interaction logic for PolygonSettings.xaml
    /// </summary>
    public partial class PolygonSettings : Window
    {
        private Canvas paintingCanvas;

        public Canvas PaintingCanvas
        {
            get { return paintingCanvas; }
            set { paintingCanvas = value; }
        }

        private List<Point> points;

        public List<Point> Points
        {
            get { return points; }
            set { points = value; }
        }


        public PolygonSettings(Canvas paintingCanvas, List<Point> points)
        {
            InitializeComponent();
            this.paintingCanvas = paintingCanvas;
            this.points = points;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (points != null)
            {
                Polygon polygon = new Polygon();

                BrushConverter conv = new BrushConverter();

                string fillColorString = FillColorField.SelectedItem.ToString().Split(' ')[1];
                SolidColorBrush fillColor = (SolidColorBrush)conv.ConvertFromString(fillColorString);
                polygon.Fill = fillColor;

                string borderColorString = BorderColorField.SelectedItem.ToString().Split(' ')[1];
                SolidColorBrush borderColor = (SolidColorBrush)conv.ConvertFromString(borderColorString);
                polygon.Stroke = borderColor;

                polygon.StrokeThickness = 1;

                polygon.HorizontalAlignment = HorizontalAlignment.Left;
                polygon.VerticalAlignment = VerticalAlignment.Center;

                polygon.Points = new PointCollection(points);

                paintingCanvas.Children.Add(polygon);

                points.Clear();

                this.Close();
            }
        }
    }
}
