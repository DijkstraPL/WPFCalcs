using MathNet.Numerics.LinearAlgebra;
using System;
using System.Linq;

namespace Build_IT_CommonTools.MatrixMath.Wrappers
{
    public class VectorAdapter
    {
        public double this[int i]
        {
            get => _vector[i];
            set { _vector[i] = value; }
        }

        private readonly Vector<double> _vector;

        public static VectorAdapter Create(int size)
            => new VectorAdapter(Vector<double>.Build.Dense(size));
        public static VectorAdapter Create(double[] values)
            => new VectorAdapter(Vector<double>.Build.Dense(values));

        internal VectorAdapter(Vector<double> vector)
        {
            _vector = vector;
        }

        public Vector<double> GetOriginal() => _vector;

        public bool Any(Func<double, bool> function) 
            => _vector.Any(dv => function(dv));

        public VectorAdapter Add(VectorAdapter vector) 
            => new VectorAdapter(_vector.Add(vector.GetOriginal()));

        public static VectorAdapter operator -(VectorAdapter vectorA, VectorAdapter vectorB)
            => new VectorAdapter(vectorA.GetOriginal() - vectorB.GetOriginal());

        public static VectorAdapter operator *(VectorAdapter vector, double value)
            => new VectorAdapter(vector.GetOriginal() * value);

        public static VectorAdapter operator *(MatrixAdapter matrix, VectorAdapter vector)
            => new VectorAdapter(matrix.GetOriginal() * vector.GetOriginal());
    }
}
