using System;
using System.Windows.Controls;
using IGMath.Resources;
using Infragistics.Math;
using Infragistics.Samples.Framework;

namespace IGMath.Samples.Data
{
    public partial class StatisticalCalculations : SampleContainer
    {
        System.Windows.Media.RotateTransform tRotate = new System.Windows.Media.RotateTransform();

        public StatisticalCalculations()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }
        void OnSampleLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            tRotate.Angle = -60;
            GenerateData(8, 12);
            createLabels(18);
        }

        // create labels
        void createLabels(int cntCol)
        {
            // clear grData0 (Grid)
            grData0.ColumnDefinitions.Clear();
            grData0.RowDefinitions.Clear();
            grData0.Children.Clear();

            TextBlock tb;
            string str = "";
            // obtain the strings in the labels
            for (int j = 0; j < cntCol; j++)
            {
                switch (j)
                {
                    case 0: str = MathStrings.monthJanuary; break;
                    case 1: str = MathStrings.monthFebruary; break;
                    case 2: str = MathStrings.monthMarch; break;
                    case 3: str = MathStrings.monthApril; break;
                    case 4: str = MathStrings.monthMay; break;
                    case 5: str = MathStrings.monthJun; break;
                    case 6: str = MathStrings.monthJuly; break;
                    case 7: str = MathStrings.monthAugust; break;
                    case 8: str = MathStrings.monthSeptember; break;
                    case 9: str = MathStrings.monthOctober; break;
                    case 10: str = MathStrings.monthNovember; break;
                    case 11: str = MathStrings.monthDecember; break;
                    case 12: str = ""; break;
                    case 13: str = MathStrings.functMin; break;
                    case 14: str = MathStrings.functMean; break;
                    case 15: str = MathStrings.functMax; break;
                    case 16: str = MathStrings.functAverageDeviation; break;
                    case 17: str = MathStrings.functMedian; break;
                }
                tb = new TextBlock();
                tb.Text = str;
                tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                tb.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                tb.Margin = new System.Windows.Thickness(j * colWidth, 0, 0, 0);
                tb.RenderTransform = tRotate;
                grData0.Children.Add(tb);
            }
        }

        // create custom colors for the grid
        System.Windows.Media.Color borderColor1 = System.Windows.Media.Color.FromArgb(255, 200, 215, 230);
        System.Windows.Media.Color borderColor2 = System.Windows.Media.Color.FromArgb(255, 250, 150, 150);
        // create constants of Width/Height to cells of the grid
        int colWidth = 35;
        int rowHeight = 22;

        void GenerateData(int cntRow, int cntCol)
        {
            // set how much columns are needed in addition 
            int cntFunc = 5;
            double[,] matrixValues = new double[cntRow, cntCol];

            // clear grData1 (Grid)
            grData1.ColumnDefinitions.Clear();
            grData1.RowDefinitions.Clear();
            grData1.Children.Clear();

            // create rows
            RowDefinition rowDef;
            for (int i = 0; i < cntRow; i++)
            {
                rowDef = new RowDefinition();
                rowDef.Height = new System.Windows.GridLength(rowHeight);
                grData1.RowDefinitions.Add(rowDef);
            }

            // create columns
            ColumnDefinition colDef;
            for (int j = 0; j < cntCol + cntFunc + 1; j++)
            {
                colDef = new ColumnDefinition();
                colDef.Width = new System.Windows.GridLength(colWidth);
                grData1.ColumnDefinitions.Add(colDef);
            }

            // obtain a random values to each cell of matrix (8x12 cells)
            Random rnd1 = new Random();
            for (int i = 0; i < cntRow; i++)
            {
                int rnd_tmp = rnd1.Next(100);
                for (int j = 0; j < cntCol; j++)
                {
                    matrixValues[i, j] = (double)(rnd1.Next(10) + rnd_tmp);

                    var txtBlock = new TextBlock();
                    var border = new Border();

                    txtBlock.Text = matrixValues[i, j].ToString();
                    txtBlock.TextAlignment = System.Windows.TextAlignment.Center;
                    txtBlock.Padding = new System.Windows.Thickness(0, 2, 0, 0);

                    // creating a border of each cell
                    border.BorderThickness = new System.Windows.Thickness(j == 0 ? 1 : 0, i == 0 ? 1 : 0, 1, 1);
                    border.BorderBrush = new System.Windows.Media.SolidColorBrush(borderColor1);

                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    border.Child = txtBlock;
                    grData1.Children.Add(border);
                }
            }

            Matrix x = new Matrix(matrixValues);
            Vector z;
            for (int i = 0; i < cntRow; i++)
            {
                z = new Vector(cntCol);
                // copy a values of each line of the matrix to a vector
                for (int j = 0; j < cntCol; j++)
                {
                    z[j] = (matrixValues[i, j]);
                }

                // calculate and view of the math functions
                viewResult(Compute.Min(z), i, cntCol + 1, cntCol);
                viewResult(Compute.Mean(z), i, cntCol + 2, cntCol);
                viewResult(Compute.Max(z), i, cntCol + 3, cntCol);
                viewResult(Compute.AverageDeviation(z), i, cntCol + 4, cntCol);
                viewResult(Compute.Median(z), i, cntCol + 5, cntCol);
            }
        }

        // view values to the right grid
        void viewResult(double res, int i, int j, int pos)
        {
            var txtBlock = new TextBlock();
            var border = new Border();

            txtBlock.Text = System.Math.Round(res, 1).ToString();
            txtBlock.TextAlignment = System.Windows.TextAlignment.Center;
            txtBlock.Padding = new System.Windows.Thickness(0, 1, 0, 0);

            // creating a border of each cell
            border.BorderThickness = new System.Windows.Thickness(j == (pos + 1) ? 1 : 0, i == 0 ? 1 : 0, 1, 1);
            border.BorderBrush = new System.Windows.Media.SolidColorBrush(borderColor2);

            Grid.SetColumn(border, j);
            Grid.SetRow(border, i);
            border.Child = txtBlock;
            grData1.Children.Add(border);
        }

        private void btn_Random_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GenerateData(8, 12);
        }
    }
}
