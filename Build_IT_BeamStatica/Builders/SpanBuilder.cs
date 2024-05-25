using Build_IT_BeamStatica.Builders.ContinuousLoads;
using Build_IT_BeamStatica.Builders.Interfaces;
using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Builders.PointLoads;
using Build_IT_BeamStatica.Builders.PointLoads.Interfaces;
using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Nodes.Enums;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStatica.Builders
{
    public class SpanBuilder 
    {
        #region Fields

        private readonly SpanData _spanData;
        private  MaterialBuilder _materialBuilder;
        private ISectionPropertiesBuilder _sectionPropertiesBuilder;
        private INodeBuilder _leftNodeBuilder;
        private INodeBuilder _rightNodeBuilder;
        private readonly IList<ISpanPointLoadBuilder> _pointLoadBuilders = new List<ISpanPointLoadBuilder>();
        private readonly IList<IContinuousLoadBuilder> _continuousLoadBuilders = new List<IContinuousLoadBuilder>();

        #endregion // Fields

        #region Constructors

        internal SpanBuilder()
        {
            _spanData = new SpanData();
        }

        #endregion // Constructors

        #region Public_Methods

        public SpanBuilder WithLength(double length)
        {
            _spanData.Length = length;
            return this;
        }

        public SpanBuilder With(MaterialBuilder materialBuilder)
        {
            _materialBuilder = materialBuilder;
            return this;
        }
        
        public SpanBuilder With(ISectionPropertiesBuilder sectionPropertiesBuilder)
        {
            _sectionPropertiesBuilder = sectionPropertiesBuilder;
            return this;
        }

        public SpanBuilder WithLeft(INodeBuilder nodeBuilder)
        {
            _leftNodeBuilder = nodeBuilder;
            return this;
        }

        public SpanBuilder WithRight(INodeBuilder nodeBuilder)
        {
            _rightNodeBuilder = nodeBuilder;
            return this;
        }
        public SpanBuilder With(ISpanPointLoadBuilder pointLoadBuilder)
        {
            _pointLoadBuilders.Add(pointLoadBuilder);
            return this;
        }
        public SpanBuilder With(IContinuousLoadBuilder continuousLoadBuilder)
        {
            _continuousLoadBuilders.Add(continuousLoadBuilder);
            return this;
        }

        public SpanBuilder IncludeSelfWeight()
        {
            _spanData.IncludeSelfWeight = true;
            return this;
        }

        public SpanData Build()
        {
            _spanData.Material = _materialBuilder.Build();
            _spanData.Section = _sectionPropertiesBuilder.Build();
            _spanData.LeftNode = _leftNodeBuilder?.Build();
            _spanData.RightNode = _rightNodeBuilder.Build();

            foreach (var pointLoadBuilder in _pointLoadBuilders)
                _spanData.PointLoads.Add(pointLoadBuilder.Build());
            foreach (var continuousLoadBuilder in _continuousLoadBuilders)
                _spanData.ContinuousLoads.Add(continuousLoadBuilder.Build(_spanData));

            if (!Validate())
                throw new NotSupportedException();
            return _spanData;
        }

        public bool Validate()
        {
            return _spanData.Length > 0 && 
                _spanData.Material != null &&
                _spanData.Section != null; 
        }

        #endregion // Public_Methods
    }

}
