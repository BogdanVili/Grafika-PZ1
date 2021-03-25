using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        private Ellipse ellipseToDelete;

        public Ellipse EllipseToDelete
        {
            get { return ellipseToDelete; }
            set { ellipseToDelete = value; }
        }

        private Rectangle rectangleToDelete;

        public Rectangle RectangleToDelete
        {
            get { return rectangleToDelete; }
            set { rectangleToDelete = value; }
        }


        private bool editing;

        public bool Editing
        {
            get { return editing; }
            set { editing = value; }
        }


        public ShapesSettings(string shapeSelection, Canvas paintingCanvas, Point canvasLocation)
        {
            InitializeComponent();
            this.shapeSelection = shapeSelection;
            this.paintingCanvas = paintingCanvas;
            this.canvasLocation = canvasLocation;
            FillColorField.SelectedIndex = 7;
            BorderColorField.SelectedIndex = 7;
            editing = false;
        }

        public ShapesSettings(string shapeSelection, Canvas paintingCanvas, Point canvasLocation, string width, string height, Color fillColor, Color borderColor, string borderThickness, Ellipse ellipseToDelete, Rectangle rectangleToDelete)
        {
            InitializeComponent();
            this.shapeSelection = shapeSelection;
            this.paintingCanvas = paintingCanvas;
            this.canvasLocation = canvasLocation;
            WidthField.Text = width;
            HeightField.Text = height;

            PropertyInfo[] properties = typeof(Colors).GetProperties();

            string fillColorName = GetColorName(fillColor);

            for (int i = 0; i< properties.Length; i++)
            {
                if(properties[i].Name == fillColorName)
                {
                    FillColorField.SelectedIndex = i;
                    break;
                }
            }

            string borderColorName = GetColorName(borderColor);

            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == borderColorName)
                {
                    BorderColorField.SelectedIndex = i;
                    break;
                }
            }

            BorderThicknessField.Text = borderThickness;
            this.ellipseToDelete = ellipseToDelete;
            this.rectangleToDelete = rectangleToDelete;
            editing = true;
        }
        static string GetColorName(Color col)
        {
            PropertyInfo colorProperty = typeof(Colors).GetProperties().FirstOrDefault(p => Color.AreClose((Color)p.GetValue(null), col));
            return colorProperty != null ? colorProperty.Name : "unnamed color";
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

                    if (editing)
                    {
                        paintingCanvas.Children.Remove(ellipseToDelete);
                    }
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

                    if (editing)
                    {
                        paintingCanvas.Children.Remove(rectangleToDelete);
                    }

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
            bool validate = true;

            if(WidthField.Text == string.Empty)
            {
                WidthField.BorderBrush = new SolidColorBrush(Colors.Red);
                validate = false;
            }    
            if(HeightField.Text == string.Empty)
            {
                HeightField.BorderBrush = new SolidColorBrush(Colors.Red);
                validate = false;
            }
            if(BorderThicknessField.Text == string.Empty)
            {
                BorderThicknessField.BorderBrush = new SolidColorBrush(Colors.Red);
                validate = false;
            }

            return validate;
        }
    }
}
