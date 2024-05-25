using Build_IT_BeamStatica.Loads.ContinuousLoads.ShearLoadResults;
using Build_IT_BeamStatica.Loads.Interfaces;
using Build_IT_BeamStatica.Spans.Interfaces;
using System;

namespace Build_IT_BeamStatica.Loads.ContinuousLoads
{
    internal class ContinuousShearLoad : ContinuousLoad
    {
        #region Factories

        public static IContinousLoad Create(double startPosition, double startValue, double endPosition, double endValue)
        {
            return new ContinuousShearLoad(
                new LoadData(startPosition, startValue),
                new LoadData(endPosition, endValue));
        }

        public static IContinousLoad Create(
            ILoadWithPosition startLoadWithPosition, 
            ILoadWithPosition endLoadWithPosition)
        {
            return new ContinuousShearLoad(startLoadWithPosition, endLoadWithPosition);
        }

        #endregion // Factories

        #region Constructors

        private ContinuousShearLoad(ILoadWithPosition startPosition, ILoadWithPosition endPosition) 
            : base(startPosition, endPosition)
        {
            ShearResult = new ShearResult(this);
            BendingMomentResult = new BendingMomentResult(this);

            RotationResult = new RotationResult(this);
            VerticalDeflectionResult = new VerticalDeflectionResult(this);
        }

        #endregion // Constructors

        #region Public_Methods

        public override double CalculateSpanLoadVectorShearMember(ISpan span, bool leftNode)
        {
            double closerLoad = leftNode ? -this.StartPosition.Value : -this.EndPosition.Value;
            double furtherLoad = leftNode ? -this.EndPosition.Value : -this.StartPosition.Value;
            double distanceFromCalculatedNode = leftNode ? this.StartPosition.Position : span.Length - this.EndPosition.Position;
            double loadLength = this.EndPosition.Position - this.StartPosition.Position;
            double distanceToOtherNode = leftNode ? span.Length - this.EndPosition.Position : this.StartPosition.Position;

            return 1.0 / 20 * furtherLoad * loadLength *
                (3 * Math.Pow(loadLength, 3) +
                5 * Math.Pow(loadLength, 2) * distanceFromCalculatedNode +
                10 * Math.Pow(distanceToOtherNode, 3) +
                30 * Math.Pow(distanceToOtherNode, 2) * loadLength +
                30 * Math.Pow(distanceToOtherNode, 2) * distanceFromCalculatedNode +
                15 * Math.Pow(loadLength, 2) * distanceToOtherNode +
                20 * distanceFromCalculatedNode * loadLength * distanceToOtherNode) /
                Math.Pow(span.Length, 3) +
                1.0 / 20 * closerLoad * loadLength *
                (7 * Math.Pow(loadLength, 3) +
                15 * Math.Pow(loadLength, 2) * distanceFromCalculatedNode +
                10 * Math.Pow(distanceToOtherNode, 3) +
                 30 * Math.Pow(distanceToOtherNode, 2) * loadLength +
                 30 * Math.Pow(distanceToOtherNode, 2) * distanceFromCalculatedNode +
                 25 * Math.Pow(loadLength, 2) * distanceToOtherNode +
                 40 * distanceFromCalculatedNode * loadLength * distanceToOtherNode) /
                 Math.Pow(span.Length, 3);
        }

        public override double CalculateSpanLoadVectorBendingMomentMember(ISpan span, bool leftNode)
        {
            double sign = leftNode ? 1.0 : -1.0;
            double closerLoad = leftNode ? -this.StartPosition.Value : -this.EndPosition.Value;
            double furtherLoad = leftNode ? -this.EndPosition.Value : -this.StartPosition.Value;
            double distanceFromCalculatedNode = leftNode ? this.StartPosition.Position : span.Length - this.EndPosition.Position;
            double loadLength = this.EndPosition.Position - this.StartPosition.Position;
            double distanceToOtherNode = leftNode ? span.Length - this.EndPosition.Position : this.StartPosition.Position;

            return sign / 60 * closerLoad * loadLength *
                   (3 * Math.Pow(loadLength, 3) +
                   15 * Math.Pow(loadLength, 2) * distanceFromCalculatedNode +
                   10 * Math.Pow(distanceToOtherNode, 2) * loadLength +
                   30 * Math.Pow(distanceToOtherNode, 2) * distanceFromCalculatedNode +
                   10 * Math.Pow(loadLength, 2) * distanceToOtherNode +
                   40 * distanceFromCalculatedNode * loadLength * distanceToOtherNode) /
                   Math.Pow(span.Length, 2) +
                   sign / 60 * furtherLoad * loadLength *
                   (2 * Math.Pow(loadLength, 3) +
                   5 * Math.Pow(loadLength, 2) * distanceFromCalculatedNode +
                   20 * Math.Pow(distanceToOtherNode, 2) * loadLength +
                   30 * Math.Pow(distanceToOtherNode, 2) * distanceFromCalculatedNode +
                   10 * Math.Pow(loadLength, 2) * distanceToOtherNode +
                   20 * distanceFromCalculatedNode * loadLength * distanceToOtherNode) /
                   Math.Pow(span.Length, 2);
        }

        #endregion // Public_Methods
    }
}
