using Build_IT_CommonTools.Attributes;
using Build_IT_Data.Materials.Intefaces;
using System;

namespace Build_IT_Data.Materials
{
    public class Material : IMaterial
    {
        [Abbreviation("E")]
        [Unit("GPa")]
        public double YoungModulus { get; }

        [Abbreviation("gamma")]
        [Unit("kg/m3")]
        public double Density { get; protected set; }

        [Abbreviation("alpha")]
        [Unit("1/K")]
        public double ThermalExpansionCoefficient { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="youngModulus">E in GPa</param>
        /// <param name="thermalExpansionCoefficient">alpha in 1/K</param>
        protected Material(double youngModulus, double thermalExpansionCoefficient)
        {
            YoungModulus = youngModulus > 0 ? youngModulus : 
                throw new ArgumentOutOfRangeException(nameof(youngModulus));
            ThermalExpansionCoefficient = 
                thermalExpansionCoefficient >= 0 ? thermalExpansionCoefficient : 
                throw new ArgumentOutOfRangeException(nameof(thermalExpansionCoefficient));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="youngModulus">E in GPa</param>
        /// <param name="density">gamma in kg/m3</param>
        /// <param name="thermalExpansionCoefficient">alpha in 1/K</param>
        public Material(double youngModulus, double density, double thermalExpansionCoefficient)
            : this(youngModulus, thermalExpansionCoefficient)
        {
            Density = density >= 0 ? density :
                throw new ArgumentOutOfRangeException(nameof(density));
        }
    }
}
