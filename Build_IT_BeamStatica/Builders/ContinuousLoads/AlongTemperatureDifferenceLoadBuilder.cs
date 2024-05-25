using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Loads.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.ContinuousLoads
{
    public class AlongTemperatureDifferenceLoadBuilder : IContinuousLoadBuilder
    {
        #region Fields
        
        private readonly ContinuousLoadData _continuousLoadData;

        #endregion // Fields

        #region Constructors
        
        public AlongTemperatureDifferenceLoadBuilder()
        {
            _continuousLoadData = new ContinuousLoadData();
            _continuousLoadData.ContinuousLoadType = ContinuousLoadType.AlongTemperatureDifferenceLoad;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public AlongTemperatureDifferenceLoadBuilder WithTemperatureDifference(double value)
        {
            _continuousLoadData.EndValue = value;
            return this;
        }

        public ContinuousLoadData Build(SpanData spanData)
        {
            if (!Validate())
                throw new InvalidOperationException();

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
