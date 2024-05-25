using Build_IT_BeamStatica.Spans.Interfaces;
using System;

namespace Build_IT_BeamStatica.Loads.PointLoads
{
    internal class ShearLoad : SpanConcentratedLoad
    {
        #region Constructors

        /// <summary>
        /// Use in node loads.
        /// </summary>
        /// <param name="value">kN</param>
        public ShearLoad(double value) : base(value) 
        {
        }

        /// <summary>
        /// Use in span loads.
        /// </summary>
        /// <param name="value">kN</param>
        /// <param name="position">m</param>
        public ShearLoad(double value, double position) : base(value, position)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateShear() 
            => this.Value;

        public override double CalculateBendingMoment(double distanceFromLoad) 
            => this.Value * distanceFromLoad;

        public override double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode)
        {
            double distanceFromCloserNode = leftNode ? this.Position : span.Length - this.Position;
            double distanceFromOtherNode = leftNode ? span.Length - this.Position : this.Position;
            return (-this.Value * Math.Pow(distanceFromOtherNode, 2) *
                (3 * distanceFromCloserNode + distanceFromOtherNode)) /
                Math.Pow(span.Length, 3);
        }

        public override double CalculateSpanLoadBendingMomentMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? 1.0 : -1.0;
            double distanceFromCloserNode = leftNode ? this.Position : span.Length - this.Position;
            double distanceFromOtherNode = leftNode ? span.Length - this.Position : this.Position;
            return sign * (-this.Value * distanceFromCloserNode * Math.Pow(distanceFromOtherNode, 2)) / Math.Pow(span.Length, 2);
        }

        public override double CalculateJointLoadVectorShearMember() => this.Value;

        #endregion // Public_Methods
    }
}
