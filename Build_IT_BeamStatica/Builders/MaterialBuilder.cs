using Build_IT_BeamStatica.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders
{
    public class MaterialBuilder
    {
        #region Fields
        
        private readonly MaterialData _materialData;

        #endregion // Fields

        #region Constructors

        internal MaterialBuilder()
        {
            _materialData = new MaterialData();
        }

        #endregion // Constructors

        #region Public_Methods
        
        public MaterialBuilder WithYoungModulus(double youngModulus)
        {
            _materialData.YoungModulus = youngModulus;
            return this;
        }

        public MaterialBuilder WithDensity(double density)
        {
            _materialData.Density = density;
            return this;
        }

        public MaterialBuilder WithThermalExpansionCoefficient(double thermalExpansionCoefficient)
        {
            _materialData.ThermalExpansionCoefficient = thermalExpansionCoefficient;
            return this;
        }

        public MaterialData Build()
        {
            if (!Validate())
                throw new InvalidOperationException();
            return _materialData;
        }

        private bool Validate() 
            => _materialData.YoungModulus != 0;

        #endregion // Public_Methods
    }

    public static class ConcreteMaterialBuilderExtension
    {
        //public static MaterialBuilder SetConcrete(this MaterialBuilder materialBuilder, ConcreteClass concrete, bool withReinforcement)
        //{
        //    double density = 2400;
        //    if (withReinforcement)
        //        density += 100;
        //    materialBuilder.SetDensity(density);
        //    materialBuilder.SetThermalExpansionCoefficient(0.000010);

        //}
    }

}
