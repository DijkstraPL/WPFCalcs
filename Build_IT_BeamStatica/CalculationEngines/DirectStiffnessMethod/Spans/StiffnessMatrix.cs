using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Build_IT_CommonTools.MatrixMath.Wrappers;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans
{
    internal class StiffnessMatrix : IStiffnessMatrix
    {
        #region Properties

        public ICollection<IStiffnessMatrixPosition> MatrixOfPositions { get; private set; } 
            = new List<IStiffnessMatrixPosition>();

        public MatrixAdapter Matrix { get; private set; }
        public int Size { get; private set; }

        #endregion // Properties

        #region Fields
        
        private MatrixAdapter _transformationMatrix;
        private readonly ISpan _span;

        #endregion // Fields

        #region Constructors
        
        public StiffnessMatrix(ISpan span)
        {
            _span = span ?? throw new ArgumentNullException(nameof(span));
        }

        #endregion // Constructors

        #region Public_Methods
        
        public void Calculate()
        {
            CalculateStiffnessMatrixForGeneralBeam();
        }

        #endregion // Public_Methods

        #region Private_Methods

        private void CalculateStiffnessMatrixForGeneralBeam()
        {
            var horizontalValue = _span.Section.Area * _span.Material.YoungModulus / _span.Length * 100; // kN
            var verticalValue = _span.Material.YoungModulus * _span.Section.MomentOfInteria / Math.Pow(_span.Length, 3) / 100; // kN/m

            SetLeftNodeHorizontalMovementColumn(horizontalValue);
            SetLeftNodeVerticalMovementColumn(verticalValue);
            SetLeftNodeRightRotationColumn(verticalValue);
            SetRightNodeHorizontalMovementColumn(horizontalValue);
            SetRightNodeVerticalMovementColumn(verticalValue);
            SetRightNodeLeftRotationColumn(verticalValue);

            SetSize();
            SetMatrix();

            if (_span.LeftNode.Angle != 0 || _span.RightNode.Angle != 0)
                AdjustMatrix();
        }

        private void SetLeftNodeHorizontalMovementColumn(double horizontalValue)
        {
            MatrixOfPositions.Add(new StiffnessMatrixPosition(horizontalValue, _span.LeftNode.HorizontalMovementNumber, _span.LeftNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.LeftNode.HorizontalMovementNumber, _span.LeftNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.LeftNode.HorizontalMovementNumber, _span.LeftNode.RightRotationNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(-horizontalValue, _span.LeftNode.HorizontalMovementNumber, _span.RightNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.LeftNode.HorizontalMovementNumber, _span.RightNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.LeftNode.HorizontalMovementNumber, _span.RightNode.LeftRotationNumber));
        }

        private void SetLeftNodeVerticalMovementColumn(double verticalValue)
        {
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.LeftNode.VerticalMovementNumber, _span.LeftNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 12, _span.LeftNode.VerticalMovementNumber, _span.LeftNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 6 * _span.Length, _span.LeftNode.VerticalMovementNumber, _span.LeftNode.RightRotationNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.LeftNode.VerticalMovementNumber, _span.RightNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * -12, _span.LeftNode.VerticalMovementNumber, _span.RightNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 6 * _span.Length, _span.LeftNode.VerticalMovementNumber, _span.RightNode.LeftRotationNumber));
        }

        private void SetLeftNodeRightRotationColumn(double verticalValue)
        {
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.LeftNode.RightRotationNumber, _span.LeftNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 6 * _span.Length, _span.LeftNode.RightRotationNumber, _span.LeftNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 4 * Math.Pow(_span.Length, 2), _span.LeftNode.RightRotationNumber, _span.LeftNode.RightRotationNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.LeftNode.RightRotationNumber, _span.RightNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * -6 * _span.Length, _span.LeftNode.RightRotationNumber, _span.RightNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 2 * Math.Pow(_span.Length, 2), _span.LeftNode.RightRotationNumber, _span.RightNode.LeftRotationNumber));
        }

        private void SetRightNodeHorizontalMovementColumn(double horizontalValue)
        {
            MatrixOfPositions.Add(new StiffnessMatrixPosition(-horizontalValue, _span.RightNode.HorizontalMovementNumber, _span.LeftNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.RightNode.HorizontalMovementNumber, _span.LeftNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.RightNode.HorizontalMovementNumber, _span.LeftNode.RightRotationNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(horizontalValue, _span.RightNode.HorizontalMovementNumber, _span.RightNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.RightNode.HorizontalMovementNumber, _span.RightNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.RightNode.HorizontalMovementNumber, _span.RightNode.LeftRotationNumber));
        }

        private void SetRightNodeVerticalMovementColumn(double verticalValue)
        {
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.RightNode.VerticalMovementNumber, _span.LeftNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * -12, _span.RightNode.VerticalMovementNumber, _span.LeftNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * -6 * _span.Length, _span.RightNode.VerticalMovementNumber, _span.LeftNode.RightRotationNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.RightNode.VerticalMovementNumber, _span.RightNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 12, _span.RightNode.VerticalMovementNumber, _span.RightNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * -6 * _span.Length, _span.RightNode.VerticalMovementNumber, _span.RightNode.LeftRotationNumber));
        }

        private void SetRightNodeLeftRotationColumn(double verticalValue)
        {
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.RightNode.LeftRotationNumber, _span.LeftNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 6 * _span.Length, _span.RightNode.LeftRotationNumber, _span.LeftNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 2 * Math.Pow(_span.Length, 2), _span.RightNode.LeftRotationNumber, _span.LeftNode.RightRotationNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(0, _span.RightNode.LeftRotationNumber, _span.RightNode.HorizontalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * -6 * _span.Length, _span.RightNode.LeftRotationNumber, _span.RightNode.VerticalMovementNumber));
            MatrixOfPositions.Add(new StiffnessMatrixPosition(verticalValue * 4 * Math.Pow(_span.Length, 2), _span.RightNode.LeftRotationNumber, _span.RightNode.LeftRotationNumber));
        }

        private void SetSize()
        {
            Size = (int)Math.Sqrt(MatrixOfPositions.Count);
        }

        private void SetMatrix()
        {
            Matrix = MatrixAdapter.Create(Size, Size);

            int i = 0;
            int j = 0;
            foreach (var position in MatrixOfPositions)
            {
                Matrix[j, i] = position.Value;
                i++;
                if (i % Size == 0)
                {
                    j++;
                    i = 0;
                }
            }
        }

        private void AdjustMatrix()
        {
            CalculateTransformationMatrix();
            Matrix = _transformationMatrix.Multiply(Matrix).Multiply(_transformationMatrix.Transpose());
            SetAdjustedMatrix();
        }

        private void CalculateTransformationMatrix()
        {
            _transformationMatrix = MatrixAdapter.Create(Size, Size);
            double leftAngle = _span.LeftNode.Angle * Math.PI / 180;
            double rightAngle = _span.RightNode.Angle * Math.PI / 180;
            _transformationMatrix[0, 0] = Math.Cos(leftAngle);
            _transformationMatrix[0, 1] = -Math.Sin(leftAngle);
            _transformationMatrix[1, 0] = Math.Sin(leftAngle);
            _transformationMatrix[1, 1] = Math.Cos(leftAngle);
            _transformationMatrix[2, 2] = 1;
            _transformationMatrix[3, 3] = Math.Cos(rightAngle);
            _transformationMatrix[3, 4] = -Math.Sin(rightAngle);
            _transformationMatrix[4, 3] = Math.Sin(rightAngle);
            _transformationMatrix[4, 4] = Math.Cos(rightAngle);
            _transformationMatrix[5, 5] = 1;
        }

        private void SetAdjustedMatrix()
        {
            int i = 0;
            int j = 0;
            foreach (var position in MatrixOfPositions)
            {
                position.Value = Matrix[j, i];
                i++;
                if (i % Size == 0)
                {
                    j++;
                    i = 0;
                }
            }
        }

        #endregion // Private_Methods
    }
}
