using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.ContinuousLoads
{
    public class ContinuousShearLoadBuilder : IContinuousLoadBuilder
    {
        #region Fields

        private readonly ContinuousLoadData _continuousLoadData;

        #endregion // Fields

        #region Constructors

        public ContinuousShearLoadBuilder()
        {
            _continuousLoadData = new ContinuousLoadData();
            _continuousLoadData.ContinuousLoadType = ContinuousLoadType.ContinuousShearLoad;
        }

        #endregion // Constructors

        #region Public_Methods

        public ContinuousShearLoadBuilder WithStartPosition(double position)
        {
            _continuousLoadData.StartPosition = position;
            return this;
        }
        public ContinuousShearLoadBuilder WithEndPosition(double position)
        {
            _continuousLoadData.EndPosition = position;
            return this;
        }
        public ContinuousShearLoadBuilder WithStartValue(double value)
        {
            _continuousLoadData.StartValue = value;
            return this;
        }
        public ContinuousShearLoadBuilder WithEndValue(double value)
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
