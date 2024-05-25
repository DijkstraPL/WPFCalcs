using Build_IT_BeamStatica.Results.Interfaces;

namespace Build_IT_BeamStatica.Nodes.Interfaces
{
    public interface IDeflectionProvider
    {
        #region Properties

        IResultValue HorizontalDeflection { get; }
        IResultValue VerticalDeflection { get; }

        #endregion // Properties
    }
}
