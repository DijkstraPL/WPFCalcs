using Build_IT_BeamStaticaModule.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaModule.Utils
{
    public static class PointLoadTypes
    {
        #region Properties
        
        public static string DefaultPointLoad = "Shear load";

        public static IEnumerable<string> PointLoads { get; } = new List<string>
        {
            "Angled load",
            "Bending moment",
            "Horizontal displacement",
            "Normal load",
            "Rotation displacement",
            "Shear load",
            "Vertical displacement"
        };
        public static IEnumerable<string> SpanPointLoads { get; } = new List<string>
        {
            "Angled load",
            "Bending moment",
            "Normal load",
            "Shear load",
        };

        public static IReadOnlyDictionary<string, PointLoadType> Types { get; }
        = new Dictionary<string, PointLoadType>
        {
            ["Angled load"] = PointLoadType.AngledLoad,
            ["Bending moment"] = PointLoadType.BendingMoment,
            ["Horizontal displacement"] = PointLoadType.HorizontalDisplacement,
            ["Normal load"] = PointLoadType.NormalLoad,
            ["Rotation displacement"] = PointLoadType.RotationDisplacement,
            ["Shear load"] = PointLoadType.ShearLoad,
            ["Vertical displacement"] = PointLoadType.VerticalDisplacement
        };

        #endregion // Properties
    }
}
