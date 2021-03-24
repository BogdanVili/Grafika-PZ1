using Microsoft.Win32;
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
    /// Interaction logic for ImageSettings.xaml
    /// </summary>
    public partial class ImageSettings : Window
    {
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


        private string filename;

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        

        public ImageSettings(Canvas paintingCanvas, Point canvasLocation)
        {
            InitializeComponent();
            this.paintingCanvas = paintingCanvas;
            this.canvasLocation = canvasLocation;
            filename = string.Empty;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DrawButton_Click(object sender, RoutedEventArgs e)
        {
            if(Validation())
            {
                Image image = new Image
                {
                    Source = new BitmapImage(new Uri(Filename)),
                    Width = Double.Parse(WidthField.Text),
                    Height = Double.Parse(HeightField.Text),
                    Stretch = Stretch.Fill
                };

                Canvas.SetLeft(image, canvasLocation.X);
                Canvas.SetTop(image, canvasLocation.Y);
                PaintingCanvas.Children.Add(image);
            }
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg";

            if (openFileDialog.ShowDialog() == true)
            {
                filename = openFileDialog.FileName;
                FileNameTextBlock.Text = filename;
            }
        }

        private bool Validation()
        {
            bool validate = true; 

            if (WidthField.Text == string.Empty)
            {
                WidthField.BorderBrush = new SolidColorBrush(Colors.Red);
                validate = false;
            }
            if (HeightField.Text == string.Empty)
            {
                HeightField.BorderBrush = new SolidColorBrush(Colors.Red);
                validate = false;
            }
            if (filename == string.Empty)
            {
                MessageBox.Show("Select an Image!");
                validate = false;
            }

            return validate;
        }
    }
}
