namespace Build_IT_BeamStatica.Loads.Interfaces
{
    public interface ILoadWithPosition : ILoad
    {
        #region Properties

        double Position { get; }

        #endregion // Properties
    }
}
