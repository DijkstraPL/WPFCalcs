using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.Results.Interfaces;

namespace Build_IT_BeamStatica.Results.OnSpan
{
    internal class ResultsContainer : IResultsContainer
    {
        #region Properties

        public IGetResult NormalForce { get; }
        public IGetResult Shear { get; }
        public IGetResult BendingMoment { get; }
        public IGetResult HorizontalDeflection { get; }
        public IGetResult VerticalDeflection { get; }
        public IGetResult Rotation { get; }

        #endregion // Properties

        #region Fields

        private IBeam _beam;

        #endregion // Fields

        #region Constructors

        public ResultsContainer(IBeam beam)
        {
            _beam = beam;

            NormalForce = new NormalForceResult(_beam);
            Shear = new ShearResult(_beam);
            BendingMoment = new BendingMomentResult(_beam);
            HorizontalDeflection = new HorizontalDeflectionResult(_beam);
            VerticalDeflection = new VerticalDeflectionResult(_beam);
            Rotation = new RotationResult(_beam);
        }

        #endregion // Constructors

        #region Public_Methods

        public void SetResults()
        {
            NormalForce.SetValues();
            Shear.SetValues();
            BendingMoment.SetValues();
            HorizontalDeflection.SetValues();
            VerticalDeflection.SetValues();
            Rotation.SetValues();
        }

        #endregion // Public_Methods
    }
}