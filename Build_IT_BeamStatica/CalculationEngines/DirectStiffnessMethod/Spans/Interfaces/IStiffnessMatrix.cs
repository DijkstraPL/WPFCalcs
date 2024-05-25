using Build_IT_CommonTools.MatrixMath.Wrappers;
using System.Collections.Generic;

namespace Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces
{
    public interface IStiffnessMatrix
    {
        #region Properties

        ICollection<IStiffnessMatrixPosition> MatrixOfPositions { get; }
        MatrixAdapter Matrix { get; }
        int Size { get; }

        #endregion // Properties

        #region Public_Methods

        void Calculate();

        #endregion // Public_Methods
    }
}
