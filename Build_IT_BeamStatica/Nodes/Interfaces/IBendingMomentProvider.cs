using Build_IT_BeamStatica.Results.Interfaces;

namespace Build_IT_BeamStatica.Nodes.Interfaces
{
    public interface IBendingMomentProvider
    {
        #region Properties

        IResultValue BendingMoment { get; }

        #endregion // Properties
    }
}
