using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.ContinuousLoads
{
    public class ContinuousNormalLoadBuilder : IContinuousLoadBuilder
    {
        #region Fields

        private readonly ContinuousLoadData _continuousLoadData;

        #endregion // Fields

        #region Constructors

        public ContinuousNormalLoadBuilder()
        {
            _continuousLoadData = new ContinuousLoadData();
            _continuousLoadData.ContinuousLoadType = ContinuousLoadType.ContinuousNormalLoad;
        }

        #endregion // Constructors

        #region Public_Methods

        public ContinuousNormalLoadBuilder WithStartPosition(double position)
        {
            _continuousLoadData.StartPosition = position;
            return this;
        }
        public ContinuousNormalLoadBuilder WithEndPosition(double position)
        {
            _continuousLoadData.EndPosition = position;
            return this;
        }
        public ContinuousNormalLoadBuilder WithStartValue(double value)
        {
            _continuousLoadData.StartValue = value;
            return this;
        }
        public ContinuousNormalLoadBuilder WithEndValue(double value)
        {
            _continuousLoadData.EndValue = value;
            return this;
        }

        public ContinuousLoadData Build(SpanData spanData)
        {
            if (spanData == null)
                throw new ArgumentNullException(nameof(spanData));

            if (_continuousLoadData.EndPosition <= _continuousLoadData.StartPosition)
                throw new ArgumentOutOfRangeException(nameof(_continuousLoadData.EndPosition));

            return _continuousLoadData;
        }

        public bool Validate()
        {
            return _continuousLoadData.EndPosition > _continuousLoadData.StartPosition;
        }

        #endregion // Public_Methods
    }
}
