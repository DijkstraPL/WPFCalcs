using Build_IT_BeamStaticaModule.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaModule.Utils
{
    public static class NodeTypes
    {
        #region Properties
        
        public static string DefaultNode = "Free";
        public static IEnumerable<string> Nodes { get; } = new List<string>
        {
            "Fixed",
            "Free",
            "Hinge",
            "Pin",
            "Sleeve",
            "Support",
            "Support with hinge",
            "Telescope",
        };

        public static IReadOnlyDictionary<string, NodeType> Types { get; }
        = new Dictionary<string, NodeType>
        {
            ["Fixed"] = NodeType.Fixed,
            ["Free"] = NodeType.Free,
            ["Hinge"] = NodeType.Hinge,
            ["Pin"] = NodeType.Pin,
            ["Sleeve"] = NodeType.Sleeve,
            ["Support"] = NodeType.Support,
            ["Support with hinge"] = NodeType.SupportWithHinge,
            ["Telescope"] = NodeType.Telescope,
        };

        #endregion // Properties
    }
}
