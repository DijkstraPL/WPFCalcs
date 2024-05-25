using Build_IT_BeamStatica.Loads.ContinuousLoads.NormalLoadResults;
using Build_IT_BeamStatica.Loads.ContinuousLoads.ShearLoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using System;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads
{
    internal class ContinuousAngledLoad : ContinuousLoad
    {
        #region Properties

        public double Angle { get; }

        #endregion // Properties

        #region Fields
        
        private IContinousLoad _horizontalContinousLoad;
        private IContinousLoad _verticalContinousLoad;

        #endregion // Fields

        #region Factories

        public static IContinousLoad Create(double startPosition, double startValue, 
            double endPosition, double endValue, double angle)
        {
            return new ContinuousAngledLoad(
                new LoadData(startPosition, startValue),
                new LoadData(endPosition, endValue),
                angle);
        }

        public static IContinousLoad Create(
            ILoadWithPosition startLoadWithPosition,
            ILoadWithPosition endLoadWithPosition,
            double angle)
        {
            return new ContinuousAngledLoad(startLoadWithPosition, endLoadWithPosition, angle);
        }

        #endregion // Factories

        #region Constructors

        private ContinuousAngledLoad(ILoadWithPosition startPosition, ILoadWithPosition endPosition, double angle)
            : base(startPosition, endPosition)
        {
            Angle = angle % 360;
            SetContinousLoads();

            NormalForceResult = new NormalForceResult(_horizontalContinousLoad);
            ShearResult = new ShearResult(_verticalContinousLoad);
            BendingMomentResult = new BendingMomentResult(_verticalContinousLoad);

            RotationResult = new RotationResult(_verticalContinousLoad);
            HorizontalDeflectionResult = new HorizontalDeflectionResult(_horizontalContinousLoad);
            VerticalDeflectionResult = new VerticalDeflectionResult(_verticalContinousLoad);
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode) 
            => _horizontalContinousLoad.CalculateSpanLoadVectorNormalForceMember(span, leftNode);

        public override double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode) 
            => _verticalContinousLoad.CalculateSpanLoadVectorShearMember(span, leftNode);

        public override double CalculateSpanLoadVectorBendingMomentMember(ISpan span, bool leftNode)
            => _verticalContinousLoad.CalculateSpanLoadVectorBendingMomentMember(span, leftNode);

        #endregion // Public_Methods

        #region Private_Methods
        
        private void SetContinousLoads()
        {
            double angleInRadians = Angle * Math.PI / 180;

            _horizontalContinousLoad = ContinuousNormalLoad.Create(
                this.StartPosition.Position,
                this.StartPosition.Value * Math.Sin(angleInRadians),
                this.EndPosition.Position,
                this.EndPosition.Value * Math.Sin(angleInRadians));
            _verticalContinousLoad = ContinuousShearLoad.Create(
                this.StartPosition.Position,
                this.StartPosition.Value * Math.Cos(angleInRadians),
                this.EndPosition.Position,
                this.EndPosition.Value * Math.Cos(angleInRadians));
        }

        #endregion // Private_Methods
    }
}
