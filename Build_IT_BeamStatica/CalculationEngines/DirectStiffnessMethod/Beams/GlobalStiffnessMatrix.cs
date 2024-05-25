using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Beams.Interfaces;
using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using Build_IT_CommonTools.MatrixMath.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Beams
{
    public class GlobalStiffnessMatrix : IGlobalStiffnessMatrix
    {
        #region Properties

        public MatrixAdapter Matrix { get; private set; }
        public MatrixAdapter InversedMatrix => Matrix.Inverse();

        #endregion //  Properties

        #region Fields

        private readonly IBeam _beam;
        private readonly IList<(ISpan span, ISpanCalculationEngine calculationEngine)> _spanCalculationEngines;

        #endregion //  Fields

        #region Constructors

        public GlobalStiffnessMatrix(IBeam beam,
            IList<(ISpan span, ISpanCalculationEngine calculationEngine)> spanCalculationEngines)
        {
            _beam = beam ?? throw new ArgumentNullException(nameof(beam));
            _spanCalculationEngines = spanCalculationEngines ?? throw new ArgumentNullException(nameof(spanCalculationEngines));
        }

        #endregion //  Constructors

        #region Public_Methods

        public void Calculate()
        {
            if (_beam.NumberOfDegreesOfFreedom != 0)
                Matrix = MatrixAdapter.Create(_beam.NumberOfDegreesOfFreedom, _beam.NumberOfDegreesOfFreedom);

            for (int row = 0; row < _beam.NumberOfDegreesOfFreedom; row++)
                for (int col = 0; col < _beam.NumberOfDegreesOfFreedom; col++)
                    SetMatrixValues(row, col);
        }

        #endregion //  Public_Methods

        #region Private_Methods

        private void SetMatrixValues(int row, int col)
        {
            Matrix[row, col] += _spanCalculationEngines.SelectMany(s => s.calculationEngine.StiffnessMatrix.MatrixOfPositions)
                .Where(m => m.RowNumber == row && m.ColumnNumber == col).Sum(m => m.Value);
        }
        
        #endregion //  Private_Methods
    }
}
