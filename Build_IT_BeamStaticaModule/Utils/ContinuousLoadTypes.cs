using Build_IT_BeamStaticaModule.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaModule.Utils
{
    public static class ContinuousLoadTypes
    {
        #region Properties

        public static string DefaultContinuousLoad = "Shear load";

        public static IEnumerable<string> SpanContinuousLoads { get; } = new List<string>
        {
            "Angled load",
            "Bending moment",
            "Normal load",
            "Shear load",
            "Span extend",
            "Temperature difference",
            "Up-down temperature difference",
        };

        public static IReadOnlyDictionary<string, ContinuousLoadType> Types { get; }
        = new Dictionary<string, ContinuousLoadType>
        {
            ["Angled load"] = ContinuousLoadType.AngledLoad,
            ["Bending moment"] = ContinuousLoadType.BendingMoment,
            ["Normal load"] = ContinuousLoadType.NormalLoad,
            ["Shear load"] = ContinuousLoadType.ShearLoad,
            ["Span extend"] = ContinuousLoadType.SpanExtend,
            ["Temperature difference"] = ContinuousLoadType.TemperatureDifference,
            ["Up-down temperature difference"] = ContinuousLoadType.UpDownTemperatureDifference
        };

        #endregion // Properties
    }
}
