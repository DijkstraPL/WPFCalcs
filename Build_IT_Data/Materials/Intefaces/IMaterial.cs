using Build_IT_CommonTools.Attributes;

namespace Build_IT_Data.Materials.Intefaces
{
    public interface IMaterial
    {
        [Abbreviation("E")]
        [Unit("GPa")]
        double YoungModulus { get; }

        [Abbreviation("gamma")]
        [Unit("kg/m3")]
        double Density { get; }

        [Abbreviation("alpha")]
        [Unit("1/K")]
        double ThermalExpansionCoefficient { get; }
    }
}
