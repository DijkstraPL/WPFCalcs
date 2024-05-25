using Build_IT_BeamStatica.Loads.Enums;

namespace Build_IT_BeamStatica.Data
{
    public class ContinuousLoadData
    {
        #region Properties
        
        public double StartPosition { get; set; }
        public double EndPosition { get; set; }
        public double StartValue { get; set; }
        public double EndValue { get; set; }
        public double Angle { get; set; }
        public ContinuousLoadType ContinuousLoadType { get; set; }

        #endregion // Properties
    }
}
