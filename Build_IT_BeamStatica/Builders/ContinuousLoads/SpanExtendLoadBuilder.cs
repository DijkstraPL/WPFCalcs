using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.ContinuousLoads
{
    public class SpanExtendLoadBuilder : IContinuousLoadBuilder
    {
        #region Fields

        private readonly ContinuousLoadData _continuousLoadData;

        #endregion // Fields

        #region Constructors

        public SpanExtendLoadBuilder()
        {
            _continuousLoadData = new ContinuousLoadData();
            _continuousLoadData.ContinuousLoadType = ContinuousLoadType.SpanExtendLoad;
        }

        #endregion // Constructors
        
        public SpanExtendLoadBuilder WithLengthIncrease(double value)
        {
            _continuousLoadData.EndValue = value;
            return this;
        }

        public SpanExtendLoadBuilder WithLengthDecrease(double value)
        {
            _continuousLoadData.EndValue = -value;
            return this;
        }

        public ContinuousLoadData Build(SpanData spanData)
        {
            if (spanData == null)
                throw new ArgumentNullException(nameof(spanData));

            _continuousLoadData.EndPosition = spanData.Length;
            return _continuousLoadData;
        }

        public bool Validate() => true;
    }
}
