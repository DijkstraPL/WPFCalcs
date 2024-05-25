using Build_IT_BeamStatica.Results.Interfaces;

namespace Build_IT_BeamStatica.Nodes.Interfaces
{
    public interface IShearForceProvider
    {
        #region Properties

        IResultValue ShearForce { get; }

        #endregion // Properties
    }
}
