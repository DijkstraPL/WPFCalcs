using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using System;

namespace Build_IT_BeamStatica.Loads.PointLoads
{
    internal class AngledLoad : SpanConcentratedLoad
    {
        #region Fields

        private ISpanLoad _horizontalLoad;
        private ISpanLoad _verticalLoad;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Use in node loads.
        /// </summary>
        /// <param name="value">kN</param>
        /// <param name="angle">deg</param>
        public AngledLoad(double value, double angle) : base(value)
        {
            var angleInRadians = ConvertToRadians(angle);
            _horizontalLoad = new NormalLoad(value * Math.Sin(angleInRadians));
            _verticalLoad = new ShearLoad(value * Math.Cos(angleInRadians));
        }

        /// <summary>
        /// Use in span loads.
        /// </summary>
        /// <param name="value">kN</param>
        /// <param name="position">m</param>
        /// <param name="angle">deg</param>
        public AngledLoad(double value, double position, double angle) : base(value, position)
        {
            var angleInRadians = ConvertToRadians(angle);
            _horizontalLoad = new NormalLoad(value * Math.Sin(angleInRadians), position);
            _verticalLoad = new ShearLoad(value * Math.Cos(angleInRadians), position);
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateNormalForce()
            => _horizontalLoad.CalculateNormalForce();

        public override double CalculateShear()
            => _verticalLoad.CalculateShear();

        public override double CalculateBendingMoment(double distanceFromLoad)
            => _verticalLoad.CalculateBendingMoment(distanceFromLoad);

        public override double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode)
            => _horizontalLoad.CalculateSpanLoadVectorNormalForceMember(span, leftNode);

        public override double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode)
            => _verticalLoad.CalculateSpanLoadVectorShearMember(span, leftNode);

        public override double CalculateSpanLoadBendingMomentMember(ISpan span, bool leftNode)
           => _verticalLoad.CalculateSpanLoadBendingMomentMember(span, leftNode);

        public override double CalculateJointLoadVectorNormalForceMember()
            => _horizontalLoad.CalculateJointLoadVectorNormalForceMember();

        public override double CalculateJointLoadVectorShearMember()
            => _verticalLoad.CalculateJointLoadVectorShearMember();

        #endregion // Public_Methods

        #region Private_Methods

        private double ConvertToRadians(double angle)
            => angle * Math.PI / 180;

        #endregion // Private_Methods
    }
}
