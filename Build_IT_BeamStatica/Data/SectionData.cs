using Build_IT_CommonTools.Attributes;

namespace Build_IT_BeamStatica.Data
{
    public class SectionData 
    {
        #region Properties

        [Abbreviation("I")]
        [Unit("cm4")]
        public double MomentOfInteria { get; set; }

        [Abbreviation("A")]
        [Unit("cm2")]
        public double Area { get; set; }

        [Abbreviation("C")]
        [Unit("cm")]
        public double Circumference { get; set; }

        public double SolidHeight { get; set; }


        #endregion // Properties
    }
}
