using Build_IT_BeamStatica.Results.Interfaces;

namespace Build_IT_BeamStatica.Beams.Interfaces
{
    public interface IResultProvider
    {
        #region Properties

        IResultsContainer Results { get; }

        #endregion // Properties
    }
}
