using Build_IT_BeamStaticaModule.Utils;
using Build_IT_BeamStaticaModule.Utils.Enums;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Windows.Input;

namespace Build_IT_BeamStaticaModule.ViewModels.Loads
{
    public class PointLoadViewModel : BindableBase
    {
        #region Properties
        
        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
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

        private PointLoadType _pointLoad = PointLoadTypes.Types[PointLoadTypes.DefaultPointLoad];
        public PointLoadType Type
        {
            get => _pointLoad;
            set
            {
                SetProperty(ref _pointLoad, value);
                RaisePropertyChanged(nameof(Unit));
                RaisePropertyChanged(nameof(TypeText));
                RaisePropertyChanged(nameof(HasAngle));
                _setLoad?.Invoke();
            }
        }

        public string Unit => GetLoadUnit();

        public string TypeText
        {
            get => PointLoadTypes.Types.FirstOrDefault(t => t.Value == Type).Key;
            set => Type = PointLoadTypes.Types[value];
        }

        public bool HasAngle => Type == PointLoadType.AngledLoad;

        public ICommand RemoveCommand { get; protected set; }

        #endregion // Properties

        #region Fields
        
        protected readonly Action _setLoad;

        #endregion // Fields

        #region Constructors
        
        public PointLoadViewModel(Action setLoad)
        {
            _setLoad = setLoad;
        }

        #endregion // Constructors

        #region Private_Methods

        private string GetLoadUnit()
        {
            switch (Type)
            {
                case PointLoadType.AngledLoad:
                    return "kN";
                case PointLoadType.BendingMoment:
                    return "kNm";
                case PointLoadType.HorizontalDisplacement:
                    return "mm";
                case PointLoadType.NormalLoad:
                    return "kN";
                case PointLoadType.RotationDisplacement:
                    return "deg";
                case PointLoadType.ShearLoad:
                    return "kN";
                case PointLoadType.VerticalDisplacement:
                    return "mm";
                default:
                    throw new NotImplementedException(Type.ToString());
            }
        }

        #endregion // Private_Methods
    }
}
