using Build_IT_BeamStatica.Spans.Interfaces;
using System;

namespace Build_IT_BeamStatica.Loads.PointLoads
{
    internal class RotationDisplacement : SpanConcentratedLoad
    {
        #region Properties

        public override bool IncludeInSpanLoadCalculations => true;

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Use in node loads.
        /// </summary>
        /// <param name="value">deg</param>
        public RotationDisplacement(double value) : base(value * Math.PI / 180)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateRotationDisplacement() => -this.Value;

        public override double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? 1.0 : -1.0;
            if (span.LeftNode.ConcentratedForces.Contains(this))
                sign *= -1;
            return sign * 6 * span.Material.YoungModulus * span.Section.MomentOfInteria * Value
                / Math.Pow(span.Length, 3) / 10; // kN
        }

        public override double CalculateSpanLoadBendingMomentMember(ISpan span, bool leftNode)
        {
            double multipler;
            if ((span.LeftNode.ConcentratedForces.Contains(this) && leftNode) ||
                (span.RightNode.ConcentratedForces.Contains(this) && !leftNode))
                multipler = -4.0;
            else
                multipler = -2.0;
            return multipler * span.Material.YoungModulus * span.Section.MomentOfInteria * Value
                / Math.Pow(span.Length, 2) / 10; // kNm
        }

        #endregion // Public_Methods
    }
}
