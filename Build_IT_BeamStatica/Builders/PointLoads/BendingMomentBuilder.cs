using Build_IT_BeamStatica.Builders.PointLoads.Interfaces;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.PointLoads
{
    public class SpanBendingMomentBuilder : BendingMomentBuilder<SpanBendingMomentBuilder>, ISpanPointLoadBuilder
    {
        #region Public_Methods

        public SpanBendingMomentBuilder WithPosition(double position)
        {
            _pointLoadData.Position = position;
            return this;
        }

        #endregion // Public_Methods
    }

    public class BendingMomentBuilder : BendingMomentBuilder<BendingMomentBuilder>, INodePointLoadBuilder
    {
    }

    public abstract class BendingMomentBuilder<SELF> where SELF : BendingMomentBuilder<SELF>
    {
        #region Fields

        protected readonly PointLoadData _pointLoadData;

        #endregion // Fields

        #region Constructors

        public BendingMomentBuilder()
        {
            _pointLoadData = new PointLoadData();
            _pointLoadData.PointLoadType = PointLoadType.BendingMoment;
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
