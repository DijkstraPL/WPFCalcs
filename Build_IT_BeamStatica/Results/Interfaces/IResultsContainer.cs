namespace Build_IT_BeamStatica.Results.Interfaces
{
    public interface IResultsContainer
    {
        #region Properties

        IGetResult NormalForce { get; }
        IGetResult Shear { get; }
        IGetResult BendingMoment { get; }
        IGetResult HorizontalDeflection { get; }
        IGetResult VerticalDeflection { get; }
        IGetResult Rotation { get; }

        #endregion // Properties
    }
}