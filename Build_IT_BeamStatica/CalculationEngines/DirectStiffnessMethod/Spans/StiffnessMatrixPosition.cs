using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces;
using System;

namespace Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans
{
    internal class StiffnessMatrixPosition : IStiffnessMatrixPosition
    {
        #region Properties

        public short RowNumber { get; }
        public short ColumnNumber { get; }
        public double Value { get; set;  }

        #endregion // Properties

        #region Constructors
        
        public StiffnessMatrixPosition(double value, short rowNumber, short columnNumber)
        {
            Value = value;
            RowNumber = rowNumber >= 0 ? rowNumber :
                throw new ArgumentOutOfRangeException(nameof(rowNumber));
            ColumnNumber = columnNumber >= 0 ? columnNumber :
                throw new ArgumentOutOfRangeException(nameof(columnNumber));
        }

        #endregion // Constructors
    }
}
