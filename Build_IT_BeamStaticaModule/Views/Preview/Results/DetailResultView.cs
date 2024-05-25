namespace Build_IT_BeamStaticaModule.Views.Preview.Results
{
    public class DetailResultView
    {
        #region Properties
        
        public double Position { get; }
        public double Value { get; }
        public double MultipliedValue => Value * _multiplier;
        public string Unit => _resultView.Unit;

        #endregion // Properties

        #region Fields
        
        private readonly double _multiplier;
        private readonly ResultView _resultView;

        #endregion // Fields

        #region Constructors
        
        public DetailResultView(double position, double value, double multiplier, ResultView resultView)
        {
            Position = position;
            Value = value;
            _multiplier = multiplier;
            _resultView = resultView;
        }

        #endregion // Constructors
    }
}
