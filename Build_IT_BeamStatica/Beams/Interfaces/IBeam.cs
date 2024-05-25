using Build_IT_BeamStatica.CalculationEngines.Interfaces;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using System.Collections.Generic;

namespace Build_IT_BeamStatica.Beams.Interfaces
{
    public interface IBeam : IResultProvider
    {
        #region Properties

        double Length { get; }
        IList<ISpan> Spans { get; }
        ICollection<INode> Nodes { get; }
        short NumberOfDegreesOfFreedom { get; }
        bool IncludeSelfWeight { get; }

        IBeamCalculationEngine CalculationEngine { get; }

        #endregion // Properties

        #region Public_Methods

        void SetNumeration();

        #endregion // Public_Methods
    }
}
