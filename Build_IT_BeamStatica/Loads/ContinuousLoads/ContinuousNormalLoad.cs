using Build_IT_BeamStatica.Loads.ContinuousLoads.NormalLoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads
{
    internal class ContinuousNormalLoad : ContinuousLoad
    {
        #region Factories

        public static IContinousLoad Create(double startPosition, double startValue, double endPosition, double endValue)
        {
            return new ContinuousNormalLoad(
                new LoadData(startPosition, startValue),
                new LoadData(endPosition, endValue));
        }

        public static IContinousLoad Create(
            ILoadWithPosition startLoadWithPosition,
            ILoadWithPosition endLoadWithPosition)
        {
            return new ContinuousNormalLoad(startLoadWithPosition, endLoadWithPosition);
        }

        #endregion // Factories

        #region Constructors

        private ContinuousNormalLoad(ILoadWithPosition startPosition, ILoadWithPosition endPosition)
            : base(startPosition, endPosition)
        {
            NormalForceResult = new NormalForceResult(this);
            HorizontalDeflectionResult = new HorizontalDeflectionResult(this);
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateSpanLoadVectorNormalForceMember(ISpan span, bool leftNode)
        {
            double closerLoad = leftNode ? -this.StartPosition.Value : -this.EndPosition.Value;
            double furtherLoad = leftNode ? -this.EndPosition.Value : -this.StartPosition.Value;
            double distanceFromCalculatedNode = leftNode ? this.StartPosition.Position : span.Length - this.EndPosition.Position;
            double distanceToOtherNode = leftNode ? span.Length - this.EndPosition.Position : this.StartPosition.Position;

            double totalLoadValue = (this.StartPosition.Value + this.EndPosition.Value) * this.Length / 2;

            double centerOfTheLoad = CalculateLoadCenter(closerLoad, furtherLoad);

            return -(centerOfTheLoad + distanceToOtherNode) * totalLoadValue / span.Length;
        }

        #endregion // Public_Methods

        #region Private_Methods

        private double CalculateLoadCenter(double closerLoad, double furtherLoad)
            => this.Length * (furtherLoad + 2 * closerLoad) / (3 * (closerLoad + furtherLoad));

        #endregion // Private_Methods
    }
}
