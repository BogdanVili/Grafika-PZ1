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
    /// Interaction logic for ShapesSettings.xaml
    /// </summary>
    public partial class ShapesSettings : Window
    {
        private string shapeSelection;

        public string ShapeSelection
        {
            get { return shapeSelection; }
            set { shapeSelection = value; }
        }

        private Canvas paintingCanvas;

        public Canvas PaintingCanvas
        {
            get { return paintingCanvas; }
            set { paintingCanvas = value; }
        }

        private Point canvasLocation;

        public Point CanvasLocation
        {
            get { return canvasLocation; }
            set { canvasLocation = value; }
        }


        public ShapesSettings(string shapeSelection, Canvas paintingCanvas, Point canvasLocation)
        {
            InitializeComponent();
            this.shapeSelection = shapeSelection;
            this.paintingCanvas = paintingCanvas;
            this.canvasLocation = canvasLocation;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if(Validation())
            {
                if(shapeSelection == "Ellipse")
                {
                    Ellipse ellipse = new Ellipse();

                    ellipse.Width = Double.Parse(WidthField.Text);
                    ellipse.Height = Double.Parse(HeightField.Text);

                    BrushConverter conv = new BrushConverter();

                    string fillColorString = FillColorField.SelectedItem.ToString().Split(' ')[1];
                    SolidColorBrush fillColor = (SolidColorBrush)conv.ConvertFromString(fillColorString);
                    ellipse.Fill = fillColor;

                    string borderColorString = BorderColorField.SelectedItem.ToString().Split(' ')[1];
                    SolidColorBrush borderColor = (SolidColorBrush)conv.ConvertFromString(borderColorString);
                    ellipse.Stroke = borderColor;

                    ellipse.StrokeThickness = Double.Parse(BorderThicknessField.Text);

                    Canvas.SetLeft(ellipse, canvasLocation.X);
                    Canvas.SetTop(ellipse, canvasLocation.Y);

                    paintingCanvas.Children.Add(ellipse);
                }

                if(shapeSelection == "Rectangle")
                {
                    Rectangle rectangle = new Rectangle();

                    rectangle.Width = Double.Parse(WidthField.Text);
                    rectangle.Height = Double.Parse(HeightField.Text);

                    BrushConverter conv = new BrushConverter();

                    string fillColorString = FillColorField.SelectedItem.ToString().Split(' ')[1];
                    SolidColorBrush fillColor = (SolidColorBrush)conv.ConvertFromString(fillColorString);
                    rectangle.Fill = fillColor;

                    string borderColorString = BorderColorField.SelectedItem.ToString().Split(' ')[1];
                    SolidColorBrush borderColor = (SolidColorBrush)conv.ConvertFromString(borderColorString);
                    rectangle.Stroke = borderColor;

                    rectangle.StrokeThickness = Double.Parse(BorderThicknessField.Text);

                    Canvas.SetLeft(rectangle, canvasLocation.X);
                    Canvas.SetTop(rectangle, canvasLocation.Y);

                    paintingCanvas.Children.Add(rectangle);
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Error some fields are empty!");
                return;
            }
        }

        private bool Validation()
        {
            if(WidthField.Text == string.Empty)
            {
                WidthField.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }    
            if(HeightField.Text == string.Empty)
            {
                HeightField.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }
            if(BorderThicknessField.Text == string.Empty)
            {
                BorderThicknessField.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }

            return true;
        }
    }
}
