using Build_IT_BeamStatica.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.Nodes.Interfaces
{
    public interface INodeBuilder
    {
        #region Properties
        
        NodeData Build();
        bool Validate();

        #endregion // Properties
    }
}
