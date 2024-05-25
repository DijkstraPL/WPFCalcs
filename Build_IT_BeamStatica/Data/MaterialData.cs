using Build_IT_CommonTools.Attributes;

namespace Build_IT_BeamStatica.Data
{
    public class MaterialData
    {
        #region Properties

        [Abbreviation("E_cm")]
        [Unit("GPa")]
        public double YoungModulus { get; set; }

        [Abbreviation("γ")]
        [Unit("100*kN/m3")]
        public double Density { get; set; }

        [Abbreviation("l_x")]
        [Unit("1/K")]
        public double ThermalExpansionCoefficient { get; set; }

        #endregion // Properties
    }
}
