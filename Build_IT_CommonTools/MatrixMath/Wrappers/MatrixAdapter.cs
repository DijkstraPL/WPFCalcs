using MathNet.Numerics.LinearAlgebra;

namespace Build_IT_CommonTools.MatrixMath.Wrappers
{
    public class MatrixAdapter
    {
        public int RowCount => _matrix.RowCount;
        public int ColumnCount => _matrix.ColumnCount;

        public double this[int i, int j]
        {
            get => _matrix[i, j];
            set { _matrix[i, j] = value; }
        }

        private readonly Matrix<double> _matrix;

        public static MatrixAdapter Create(int rows, int columns)
            => new MatrixAdapter(Matrix<double>.Build.Dense(rows, columns));

        public static MatrixAdapter Create(int rows, int columns, double[] values)
            => new MatrixAdapter(Matrix<double>.Build.Dense(rows, columns, values));

        internal MatrixAdapter(Matrix<double> matrix)
        {
            _matrix = matrix;
        }

        public Matrix<double> GetOriginal() => _matrix;

        public MatrixAdapter Inverse() 
            => new MatrixAdapter(_matrix.Inverse());

        public MatrixAdapter Multiply(MatrixAdapter matrix) 
            => new MatrixAdapter(_matrix.Multiply(matrix.GetOriginal()));

        public VectorAdapter Multiply(VectorAdapter vector)
            => new VectorAdapter(_matrix.Multiply(vector.GetOriginal()));

        public MatrixAdapter Transpose()
            => new MatrixAdapter(_matrix.Transpose());

        public double CalculateDeterminant() 
            => _matrix.Determinant();
    }
}
