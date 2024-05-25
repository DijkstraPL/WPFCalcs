using Build_IT_BeamStatica.Builders.PointLoads.Interfaces;
using Build_IT_BeamStatica.Data;
using System;

namespace Build_IT_BeamStatica.Builders.PointLoads
{
    public class PointLoadBuilder<SELF> where SELF : PointLoadBuilder<SELF>, INodePointLoadBuilder
    {
        #region Fields
        
        protected readonly PointLoadData _pointLoadData;

        #endregion // Fields

        #region Constructors

        public PointLoadBuilder()
        {
            _pointLoadData = new PointLoadData();
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
