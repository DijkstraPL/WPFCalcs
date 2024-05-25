using Build_IT_BeamStatica.Loads.Interfaces;

namespace Build_IT_BeamStatica.Loads
{
    internal class LoadData : ILoadWithPosition
    {
        #region Properties

        public double Position { get; }
        public double Value { get; }

        #endregion // Properties

        #region Constructors

        public LoadData(double position, double value)
        {
            Position = position;
            Value = value;
        }

        #endregion // Constructors
    }
}
