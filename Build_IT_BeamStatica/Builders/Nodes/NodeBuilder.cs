using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Builders.PointLoads.Interfaces;
using Build_IT_BeamStatica.Data;
using System;
using System.Collections.Generic;

namespace Build_IT_BeamStatica.Builders
{
    public class NodeBuilder<SELF> :  INodeBuilder where SELF : NodeBuilder<SELF>
    {
        #region Fields
        
        protected readonly NodeData _nodeData;
        protected IList<INodePointLoadBuilder> _pointLoadBuilders = new List<INodePointLoadBuilder>();

        #endregion // Fields

        #region Constructors
        
        public NodeBuilder()
        {
            _nodeData = new NodeData();
        }

        #endregion // Constructors

        #region Public_Methods
        
        public SELF With(INodePointLoadBuilder pointLoadBuilder)
        {
            _pointLoadBuilders.Add(pointLoadBuilder);
            return (SELF)this;
        }

        public NodeData Build()
        {
            foreach (var pointLoadBuilder in _pointLoadBuilders)
                _nodeData.PointLoads.Add(pointLoadBuilder.Build());

            if (!Validate())
                throw new InvalidOperationException();
            return _nodeData;
        }

        public bool Validate()
        {
            return true;
        }

        #endregion // Public_Methods
    }
}
