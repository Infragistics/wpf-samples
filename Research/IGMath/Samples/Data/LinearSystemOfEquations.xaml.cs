using System;
using IGMath.Resources;
using Infragistics.Math;
using Infragistics.Samples.Framework;

namespace IGMath.Samples.Data
{
    public partial class LinearSystemOfEquations : SampleContainer
    {
        public LinearSystemOfEquations()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            double[,] matrixValues = new double[2, 2];
            double[] vector_tmp = new double[2];

            try
            {
                // obtain the initial argument in the equations
                matrixValues[0, 0] = double.Parse(tb1.Text);
                matrixValues[0, 1] = double.Parse(tb2.Text);
                matrixValues[1, 0] = double.Parse(tb4.Text);
                matrixValues[1, 1] = double.Parse(tb5.Text);

                // obtain the initial answers for the equations                
                vector_tmp[0] = double.Parse(tb3.Text);
                vector_tmp[1] = double.Parse(tb6.Text);
            }
            catch (FormatException)
            {
                System.Windows.MessageBox.Show(MathStrings.ErrorInvalidValue);
                return;
            }

            // and create a matrix "A" with the values
            Matrix m = new Matrix(matrixValues);

            // create an array used to specify the Vector's dimension
            int[] dim_vector = new int[] { 2, 1 };

            // and create a Vector using the anser values and dimension
            Vector eqRes = new Vector(vector_tmp, dim_vector);

            // calculate the determinant of the matrix "A" and show 
            double matDet = Compute.Determinant(m);

            // put the determinant value to a textbox
            tb7.Text = System.Math.Round(matDet, 2).ToString();

            if (matDet == 0)
            {
                tBlock1.Text = MathStrings.DeterminantIs0;
                tb8.Text = "";
                tb9.Text = "";
                return;
            }
            else
            {
                tBlock1.Text = MathStrings.Determinant;
            }

            // calculate the values for a temporary determinant matrix
            double[,] matrix_inv = new double[2, 2];
            for (int i = 0; i < matrixValues.GetLength(0); i++)
            {
                for (int j = 0; j < matrixValues.GetLength(1); j++)
                {
                    matrix_inv[i, j] = Compute.CofactorMatrix(m, i, j);
                }
            }

            // compute the inverted matrix of matrix "A"
            Matrix inv = Compute.Transpose(new Matrix(matrix_inv)).Multiply(1 / matDet);

            // multiply the inverted matrix to the vector and get the equation answers
            var v = Compute.MatrixProduct(inv, eqRes);

            // put the answers in the text boxes
            tb8.Text = System.Math.Round(v[0], 2).ToString();
            tb9.Text = System.Math.Round(v[1], 2).ToString();
        }
    }
}
