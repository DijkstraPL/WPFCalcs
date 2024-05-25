using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.ContinuousLoads
{
    public class UpDownTemperatureDifferenceLoadBuilder : IContinuousLoadBuilder
    {
        #region Fields

        private readonly ContinuousLoadData _continuousLoadData;

        #endregion // Fields

        #region Constructors

        #region Public_Methods
        
        public UpDownTemperatureDifferenceLoadBuilder()
        {
            _continuousLoadData = new ContinuousLoadData();
            _continuousLoadData.ContinuousLoadType = ContinuousLoadType.UpDownTemperatureDifferenceLoad;
        }

        #endregion // Constructors

        public UpDownTemperatureDifferenceLoadBuilder WithUpperTemperature(double value)
        {
            _continuousLoadData.EndValue = value;
            return this;
        }

        public UpDownTemperatureDifferenceLoadBuilder WithLowerTemperature(double value)
        {
            _continuousLoadData.StartValue = value;
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

        #endregion // Public_Methods
    }
}
