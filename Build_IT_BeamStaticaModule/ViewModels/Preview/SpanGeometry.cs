using System;

namespace Build_IT_BeamStaticaModule.ViewModels.Preview
{
    public class SpanGeometry
    {
        #region Properties
        
        public double StartPoint { get; }
        public double EndPoint { get; }
        public SpanViewModel SpanViewModel { get; }

        #endregion // Properties

        #region Constructors
        
        public SpanGeometry(SpanViewModel spanViewModel, double startPoint, double endPoint)
        {
            SpanViewModel = spanViewModel ?? throw new ArgumentNullException(nameof(spanViewModel));
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        #endregion // Constructors
    }
}
