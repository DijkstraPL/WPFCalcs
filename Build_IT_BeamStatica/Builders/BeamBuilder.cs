using Build_IT_BeamStatica.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_BeamStatica.Builders
{
    public class BeamBuilder 
    {
        #region Fields

        private readonly BeamData _beamData;
        private readonly IList<SpanBuilder> _spanBuilders = new List<SpanBuilder>();

        #endregion // Fields

        #region Constructors

        internal BeamBuilder()
        {
            _beamData = new BeamData();
        }

        #endregion // Constructors

        #region Public_Methods

        public BeamBuilder With(SpanBuilder spanBuilder)
        {
            _spanBuilders.Add(spanBuilder);
            return this;
        }

        public BeamData Build()
        {
            if (!Validate())
                throw new InvalidOperationException();

            foreach (var spanBuilder in _spanBuilders)
            {
                var spanData = spanBuilder.Build();
                if (_beamData.SpanDatas.LastOrDefault()?.RightNode != null)
                    spanData.LeftNode = _beamData.SpanDatas.Last().RightNode;
                _beamData.SpanDatas.Add(spanData);
            }

            return _beamData;
        }

        public bool Validate() => true;

        #endregion // Public_Methods


    }
}
