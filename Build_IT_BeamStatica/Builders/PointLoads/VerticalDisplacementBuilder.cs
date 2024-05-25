using Build_IT_BeamStatica.Builders.PointLoads.Interfaces;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using System;

namespace Build_IT_BeamStatica.Builders.PointLoads
{
    public class VerticalDisplacementBuilder : VerticalDisplacementBuilder<VerticalDisplacementBuilder>, INodePointLoadBuilder
    {
    }

    public abstract class VerticalDisplacementBuilder<SELF> where SELF : VerticalDisplacementBuilder<SELF>
    {
        #region Fields

        protected readonly PointLoadData _pointLoadData;

        #endregion // Fields

        #region Constructors

        public VerticalDisplacementBuilder()
        {
            _pointLoadData = new PointLoadData();
            _pointLoadData.PointLoadType = PointLoadType.VerticalDisplacement;
        }

        #endregion // Constructors

        #region Public_Methods

        public SELF WithValue(double value)
        {
            _pointLoadData.Value = value;
            return (SELF)this;
        }

        public PointLoadData Build()
        {
            if (!Validate())
                throw new InvalidOperationException();

            return _pointLoadData;
        }

        public bool Validate()
        {
            return true;
        }

        #endregion // Public_Methods
    }
}
