using Build_IT_BeamStatica.Spans.Interfaces;
using System;

namespace Build_IT_BeamStatica.Loads.PointLoads
{
    internal class VerticalDisplacement : SpanConcentratedLoad
    {
        #region Properties

        public override bool IncludeInSpanLoadCalculations => true;

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Use in node loads.
        /// </summary>
        /// <param name="value">mm</param>
        public VerticalDisplacement(double value) : base(value)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateVerticalDisplacement() => this.Value;

        public override double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? -1.0 : 1.0;
            if (span.LeftNode.ConcentratedForces.Contains(this))
                sign *= -1;
            return sign * 12 * span.Material.YoungModulus * span.Section.MomentOfInteria * Value
                / Math.Pow(span.Length, 3) / 100000; // kN
        }

        public override double CalculateSpanLoadBendingMomentMember(ISpan span, bool leftNode)
        {
            double sign = -1.0;
            if (span.LeftNode.ConcentratedForces.Contains(this))
                sign *= -1;
            return sign * 6 * span.Material.YoungModulus * span.Section.MomentOfInteria * Value
                / Math.Pow(span.Length, 2) / 100000; // kNm
        }

        #endregion // Public_Methods
    }
}
