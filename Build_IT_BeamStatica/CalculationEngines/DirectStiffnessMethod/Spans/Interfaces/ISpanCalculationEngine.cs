using Build_IT_CommonTools.MatrixMath.Wrappers;

namespace Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Spans.Interfaces
{
    public interface ISpanCalculationEngine
    {
        #region Properties

        IStiffnessMatrix StiffnessMatrix { get; }
                
        VectorAdapter LoadVector { get; }
        VectorAdapter Displacements { get; }
        VectorAdapter Forces { get; }

        #endregion // Properties

        #region Public_Methods
        
        void CalculateSpanLoadVector();
        void CalculateDisplacement(VectorAdapter deflectionVector, int numberOfDegreesOfFreedom);
        void CalculateForce(VectorAdapter loadVector, VectorAdapter displacements);

        #endregion // Public_Methods
    }
}
