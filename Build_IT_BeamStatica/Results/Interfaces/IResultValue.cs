namespace Build_IT_BeamStatica.Results.Interfaces
{
    public interface IResultValue
    {
        #region Properties

        double Value { get; set; }
        double? Position { get; }

        #endregion // Properties
    }
}
