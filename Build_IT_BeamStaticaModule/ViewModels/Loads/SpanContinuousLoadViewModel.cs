using Build_IT_BeamStaticaModule.Utils;
using Build_IT_BeamStaticaModule.Utils.Enums;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Build_IT_BeamStaticaModule.ViewModels.Loads
{
    public class SpanContinuousLoadViewModel : BindableBase
    {
        private double _startValue;
        public double StartValue
        {
            get => _startValue;
            set
            {
                _startValue = value;
                _setLoad?.Invoke();
            }
        }

        private double _startPosition;
        public double StartPosition
        {
            get => _startPosition;
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > _spanViewModel.Length)
                    value = _spanViewModel.Length;

                SetProperty(ref _startPosition, value);

                if (EndPosition < StartPosition)
                    EndPosition = StartPosition;

                _setLoad?.Invoke();
            }
        }

        private double _endValue;
        public double EndValue
        {
            get => _endValue;
            set
            {
                _endValue = value;
                _setLoad?.Invoke();
            }
        }

        private double _endPosition;
        public double EndPosition
        {
            get => _endPosition;
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > _spanViewModel.Length)
                    value = _spanViewModel.Length;

                SetProperty(ref _endPosition, value);

                if (EndPosition < StartPosition)
                    StartPosition = EndPosition;

                _setLoad?.Invoke();
            }
        }

        private double _angle;
        public double Angle
        {
            get => _angle;
            set
            {
                SetProperty(ref _angle, value);
                _setLoad?.Invoke();
            }
        }

        private ContinuousLoadType _continuousLoad = ContinuousLoadTypes.Types[ContinuousLoadTypes.DefaultContinuousLoad];
        public ContinuousLoadType Type
        {
            get => _continuousLoad;
            set
            {
                SetProperty(ref _continuousLoad, value);
                RaisePropertyChanged(nameof(Unit));
                RaisePropertyChanged(nameof(TypeText));
                RaisePropertyChanged(nameof(HasAngle));
                _setLoad?.Invoke();
            }
        }

        public string Unit => GetLoadUnit();

        public string TypeText
        {
            get => ContinuousLoadTypes.Types.FirstOrDefault(t => t.Value == Type).Key;
            set => Type = ContinuousLoadTypes.Types[value];
        }
        public bool HasAngle => Type == ContinuousLoadType.AngledLoad;

        public ICommand RemoveCommand { get; protected set; }

        public double SpanLength => _spanViewModel.Length;

        #region Fields

        private readonly SpanViewModel _spanViewModel;
        private readonly Action _setLoad;

        #endregion // Fields

        #region Constructors
        
        public SpanContinuousLoadViewModel(SpanViewModel spanViewModel, Action setLoad)
        {
            _spanViewModel = spanViewModel ?? throw new ArgumentNullException(nameof(spanViewModel));
            _setLoad = setLoad;

            EndPosition = _spanViewModel.Length;
                        
            RemoveCommand = new DelegateCommand(() => _spanViewModel.RemoveLoad(this));
        }

        #endregion // Constructors

        #region Private_Methods
        
        private string GetLoadUnit()
        {
            switch (Type)
            {
                case ContinuousLoadType.AngledLoad:
                    return "kN/m";
                case ContinuousLoadType.BendingMoment:
                    return "kNm/m";
                case ContinuousLoadType.ShearLoad:
                    return "kN/m";
                case ContinuousLoadType.NormalLoad:
                    return "kN/m";
                case ContinuousLoadType.SpanExtend:
                    return "mm";
                case ContinuousLoadType.TemperatureDifference:
                    return "ΔK";
                case ContinuousLoadType.UpDownTemperatureDifference:
                    return "ΔK";
                default:
                    throw new NotImplementedException(Type.ToString());
            }
        }

        #endregion // Private_Methods
    }
}
