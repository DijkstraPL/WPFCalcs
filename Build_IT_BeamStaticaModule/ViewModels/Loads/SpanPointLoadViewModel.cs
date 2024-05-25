using Prism.Commands;
using System;

namespace Build_IT_BeamStaticaModule.ViewModels.Loads
{
    public class SpanPointLoadViewModel : PointLoadViewModel
    {        
        #region Properties

        private double _position;
        public double Position
        {
            get => _position;
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > _spanViewModel.Length)
                    value = _spanViewModel.Length;

                _position = value;
                _setLoad?.Invoke();
            }
        }

        #endregion // Properties

        #region Fields

        private readonly SpanViewModel _spanViewModel;

        #endregion // Fields

        #region Constructors

        public SpanPointLoadViewModel(SpanViewModel span, Action setLoad) : base(setLoad)
        {
            _spanViewModel = span ?? throw new ArgumentNullException(nameof(span));

            RemoveCommand = new DelegateCommand(() => _spanViewModel.RemoveLoad(this));
        }

        #endregion // Constructors
    }
}
