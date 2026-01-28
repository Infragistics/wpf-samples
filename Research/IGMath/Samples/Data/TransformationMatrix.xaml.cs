using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGMath.Resources;
using Infragistics.Math;
using Infragistics.Samples.Framework;

namespace IGMath.Samples.Data
{
    public partial class TransformationMatrix : SampleContainer
    {
        // variables used for the transformation
        double xTranslation = 10;
        double yTranslation = 10;
        double xScale = 150;
        double yScale = 150;
        double angle = 45;

        public TransformationMatrix()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DrawShape();
        }

        private void shapeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (shapeCombo != null)
                DrawShape();
        }

        private void DrawShape()
        {
            var currentCombo = this.shapeCombo.SelectedItem as ComboBoxItem;
            string shapeTag = currentCombo.Tag.ToString();
            if (shapeTag == "0") // Square
            {
                Point point1 = new Point(50, -50);
                Point point2 = new Point(-50, -50);
                Point point3 = new Point(-50, 50);
                Point point4 = new Point(50, 50);
                PointCollection points = new PointCollection();
                points.Add(point1);
                points.Add(point2);
                points.Add(point3);
                points.Add(point4);
                polygon.Points = points;
            }
            if (shapeTag == "1") // Triangle
            {
                Point point1 = new Point(0, -57.33);
                Point point2 = new Point(-50, 28.67);
                Point point3 = new Point(50, 28.67);
                PointCollection points = new PointCollection();
                points.Add(point1);
                points.Add(point2);
                points.Add(point3);
                polygon.Points = points;
            }
            if (shapeTag == "2") // Pentagram
            {
                Point point1 = new Point(29.39, 40.45);
                Point point2 = new Point(-47.55, -15.45);
                Point point3 = new Point(47.55, -15.45);
                Point point4 = new Point(-29.39, 40.45);
                Point point5 = new Point(-1.49, -50);
                PointCollection points = new PointCollection();
                points.Add(point1);
                points.Add(point2);
                points.Add(point3);
                points.Add(point4);
                points.Add(point5);
                polygon.Points = points;
            }
            if (shapeTag == "3") // Sandglass
            {
                Point point1 = new Point(40, 75);
                Point point2 = new Point(-40, 75);
                Point point3 = new Point(-40, 25);
                Point point4 = new Point(-25, 0);
                Point point5 = new Point(-40, -25);
                Point point6 = new Point(-40, -75);
                Point point7 = new Point(40, -75);
                Point point8 = new Point(40, -25);
                Point point9 = new Point(25, 0);
                Point point10 = new Point(40, 25);
                PointCollection points = new PointCollection();
                points.Add(point1);
                points.Add(point2);
                points.Add(point3);
                points.Add(point4);
                points.Add(point5);
                points.Add(point6);
                points.Add(point7);
                points.Add(point8);
                points.Add(point9);
                points.Add(point10);
                polygon.Points = points;
            }
        }

        // Apply the transformation
        private void transformButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the user input for the transformation values
            try
            {
                xTranslation = this.xTranslationSlider.Value;
                yTranslation = this.yTranslationSlider.Value;
                xScale = xScaleSlider.Value / 100;
                yScale = yScaleSlider.Value / 100;
                angle = angleSlider.Value * Infragistics.Math.Constant.Pi / 180;
            }
            catch (FormatException)
            {
                MessageBox.Show(MathStrings.ErrorInvalidValue);
                return;
            }

            // Initialize an array with the elements for the transformation matrix
            double[] elements = new double[] { xScale * Compute.Cos(angle), yScale * Compute.Sin(angle), xTranslation,
                -xScale * Compute.Sin(angle), yScale * Compute.Cos(angle), yTranslation,
                0, 0, 1 };
            // Define the translation matrix with dimentions 3x3
            Infragistics.Math.Matrix translationMatrix = new Infragistics.Math.Matrix(elements, new int[] { 3, 3 });

            PointCollection newPoints = new PointCollection();
            PointCollection oldPoints = new PointCollection();
            // Apply the transformation to each of the edges of the polygon
            foreach (Point p in polygon.Points)
            {
                // Initialize a vector with the normalized coordinates of the point
                double[] normalVector = new double[] { p.X, p.Y, 1 };
                Infragistics.Math.Matrix pointMatrix = new Infragistics.Math.Matrix(normalVector, new int[] { 1, 3 });
                // Apply the transformation (Multiply the two matrixes)
                pointMatrix = Compute.MatrixProduct(pointMatrix, translationMatrix);
                Point newPoint = new Point(pointMatrix[0], pointMatrix[1]);

                // Check if the point is inside the grid
                if (newPoint.X < -LayoutRoot.ActualWidth / 2 || newPoint.X > LayoutRoot.ActualWidth / 2)
                {
                    MessageBox.Show(MathStrings.InvalidPolygon);
                    newPoints = polygon.Points;
                    break;
                }
                if (newPoint.Y < -LayoutRoot.ActualHeight / 2 || newPoint.Y > LayoutRoot.ActualHeight / 2)
                {
                    MessageBox.Show(MathStrings.InvalidPolygon);
                    newPoints = polygon.Points;
                    break;
                }

                newPoints.Add(newPoint);
            }
            // Set the new points for the edges of the polygon
            polygon.Points = newPoints;
        }
    }
}
