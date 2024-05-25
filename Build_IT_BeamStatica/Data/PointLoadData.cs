using Build_IT_BeamStatica.Loads.Enums;

namespace Build_IT_BeamStatica.Data
{
    public class PointLoadData
    {
        #region Properties
        
        public double Position { get; set; }
        public double Value { get; set; }
        public double Angle { get; set; }
        public PointLoadType PointLoadType { get; set; }

        #endregion // Properties
    }
}
