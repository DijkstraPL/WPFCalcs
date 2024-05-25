using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.ContinuousLoads
{
    public class ContinuousBendingMomentLoadBuilder : IContinuousLoadBuilder
    {
        #region Fields

        private readonly ContinuousLoadData _continuousLoadData;

        #endregion // Fields

        #region Constructors

        public ContinuousBendingMomentLoadBuilder()
        {
            _continuousLoadData = new ContinuousLoadData();
            _continuousLoadData.ContinuousLoadType = ContinuousLoadType.ContinuousBendingMomentLoad;
        }

        #endregion // Constructors

        #region Public_Methods

        public ContinuousBendingMomentLoadBuilder WithValue(double value)
        {
            _continuousLoadData.StartValue = value;
            _continuousLoadData.EndValue = value;
            return this;
        }

        public ContinuousLoadData Build(SpanData spanData)
        {
            if (spanData == null)
                throw new ArgumentNullException(nameof(spanData));

            _continuousLoadData.EndPosition = spanData.Length;
            return _continuousLoadData;
        }

        public bool Validate()
        {
            return true;
        }

        #endregion // Public_Methods
    }
}
