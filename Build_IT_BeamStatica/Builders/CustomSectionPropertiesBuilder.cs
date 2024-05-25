using Build_IT_BeamStatica.Builders.Interfaces;
using Build_IT_BeamStatica.Data;
using System;

namespace Build_IT_BeamStatica.Builders
{
    public class CustomSectionPropertiesBuilder : ISectionPropertiesBuilder
    {
        #region Fields

        private double _momentOfInteria = double.NaN;
        private double _area = double.NaN;
        private double _circumference = double.NaN;
        private double _solidHeight = double.NaN;

        #endregion // Fields

        #region Constructors

        internal CustomSectionPropertiesBuilder()
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public CustomSectionPropertiesBuilder WithMomentOfInertia(double momentOfInertia)
        {
            _momentOfInteria = momentOfInertia;
            return this;
        }
        public CustomSectionPropertiesBuilder WithArea(double momentOfInertia)
        {
            _momentOfInteria = momentOfInertia;
            return this;
        }

        public SectionData Build()
        {
            if (!Validate())
                throw new InvalidOperationException();

            return new SectionData { MomentOfInteria = _momentOfInteria, Area = _area, Circumference = _circumference, SolidHeight = _solidHeight };
        }

        public bool Validate()
        {
            return !double.IsNaN(_momentOfInteria) &&
                !double.IsNaN(_area) &&
                !double.IsNaN(_circumference) &&
                !double.IsNaN(_solidHeight);
        }

        #endregion // Public_Methods
    }
}
