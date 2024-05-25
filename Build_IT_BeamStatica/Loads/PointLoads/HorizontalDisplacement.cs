using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.PointLoads
{
    internal class HorizontalDisplacement : SpanConcentratedLoad
    {
        #region Properties

        public override bool IncludeInSpanLoadCalculations => true;

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Use in node loads.
        /// </summary>
        /// <param name="value">mm</param>
        public HorizontalDisplacement(double value) : base(value)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateHorizontalDisplacement() => this.Value;

        public override double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? -1.0 : 1.0;
            if (span.LeftNode.ConcentratedForces.Contains(this))
                sign *= -1;
            return sign * span.Material.YoungModulus * span.Section.Area * Value
                / span.Length / 10; // kN
        }

        #endregion // Public_Methods
    }
}
