using Build_IT_BeamStatica;
using Build_IT_BeamStaticaModule.Events;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Build_IT_BeamStaticaModule.ViewModels.Preview
{
    public class BeamPreviewViewModel : BindableBase
    {
        #region Properties
        
        public IList<SpanGeometry> _spans = new ObservableCollection<SpanGeometry>();
        public IEnumerable<SpanGeometry> Spans => _spans;

        private double _length;
        public double Length
        {
            get => _length;
            private set => SetProperty(ref _length, value);
        }

        public BeamCalculationResult BeamCalculationResult { get; private set; }

        private bool _showBendingMoment;
        public bool ShowBendingMoment
        {
            get => _showBendingMoment;
            set
            {
                SetProperty(ref _showBendingMoment, value);
                RaisePropertyChanged(nameof(BendingMomentAtPosition));
            }
        }

        private bool _showShearForces;
        public bool ShowShearForces
        {
            get => _showShearForces;
            set
            {
                SetProperty(ref _showShearForces, value);
                RaisePropertyChanged(nameof(ShearForceAtPosition));
            }
        }

        private bool _showNormalForces;
        public bool ShowNormalForces
        {
            get => _showNormalForces;
            set
            {
                SetProperty(ref _showNormalForces, value);
                RaisePropertyChanged(nameof(NormalForceAtPosition));
            }
        }

        private bool _showVerticalDeflections;
        public bool ShowVerticalDeflections
        {
            get => _showVerticalDeflections;
            set
            {
                SetProperty(ref _showVerticalDeflections, value);
                RaisePropertyChanged(nameof(VerticalDeflectionAtPosition));
            }
        }

        private bool _showHorizontalDeflections;
        public bool ShowHorizontalDeflections
        {
            get => _showHorizontalDeflections;
            set
            {
                SetProperty(ref _showHorizontalDeflections, value);
                RaisePropertyChanged(nameof(HorizontalDeflectionAtPosition));
            }
        }

        private bool _showRotations;
        public bool ShowRotations
        {
            get => _showRotations;
            set
            {
                SetProperty(ref _showRotations, value);
                RaisePropertyChanged(nameof(RotationAtPosition));
            }
        }

        private bool _showValues;
        public bool ShowValues
        {
            get => _showValues;
            private set => SetProperty(ref _showValues, value);
        }

        private double _position;
        public double Position
        {
            get => _position;
            set
            {
                if (value > Length / 100)
                    value = Length / 100;
                else if (value < 0)
                    value = 0;

                SetProperty(ref _position, value);
                ShowValues = true;
                RefreshResults(enforceShow: true);
            }
        }

        private double _shearScale = 1;
        public double ShearScale
        {
            get => _shearScale;
            set => SetProperty(ref _shearScale, value);
        }
        private double _bendingMomentScale = 1;
        public double BendingMomentScale
        {
            get => _bendingMomentScale;
            set => SetProperty(ref _bendingMomentScale, value);
        }
        private double _normalScale = 1;
        public double NormalScale
        {
            get => _normalScale;
            set => SetProperty(ref _normalScale, value);
        }
        private double _verticalDeflectionScale = 1;
        public double VerticalDeflectionScale
        {
            get => _verticalDeflectionScale;
            set => SetProperty(ref _verticalDeflectionScale, value);
        }
        private double _horizontalDeflectionScale = 1;
        public double HorizontalDeflectionScale
        {
            get => _horizontalDeflectionScale;
            set => SetProperty(ref _horizontalDeflectionScale, value);
        }
        private double _rotationScale = 1;
        public double RotationScale
        {
            get => _rotationScale;
            set => SetProperty(ref _rotationScale, value);
        }

        public double NormalForceAtPosition => BeamCalculationResult == null ? 0 : GetValueAtPosition(BeamCalculationResult.NormalForces);
        public double ShearForceAtPosition => BeamCalculationResult == null ? 0 : GetValueAtPosition(BeamCalculationResult.ShearForces);
        public double BendingMomentAtPosition => BeamCalculationResult == null ? 0 : GetValueAtPosition(BeamCalculationResult.BendingMoments);
        public double VerticalDeflectionAtPosition => BeamCalculationResult == null ? 0 : GetValueAtPosition(BeamCalculationResult.VerticalDeflections);
        public double HorizontalDeflectionAtPosition => BeamCalculationResult == null ? 0 : GetValueAtPosition(BeamCalculationResult.HorizontalDeflections);
        public double RotationAtPosition => BeamCalculationResult == null ? 0 : GetValueAtPosition(BeamCalculationResult.Rotations);
        
        #endregion // Properties

        #region Fields

        private readonly IEventAggregator _eventAggregator;

        #endregion // Fields

        #region Constructors
        
        public BeamPreviewViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            _eventAggregator.GetEvent<BeamEditedEvent>().Subscribe(spanListViewModel => OnBeamEdited(spanListViewModel));
            _eventAggregator.GetEvent<BeamCalculatedEvent>().Subscribe(beamCalculationResult => ShowResults(beamCalculationResult));
        }

        #endregion // Constructors

        #region Private_Methods
        
        private void OnBeamEdited(SpanListViewModel spanListViewModel)
        {
            SetSpanViewModel(spanListViewModel);
            ShowValues = false;
        }

        private void SetSpanViewModel(SpanListViewModel spanListViewModel)
        {
            _spans.Clear();
            foreach (var spanViewModel in spanListViewModel.SpanViewModels)
            {
                double lastPoint = Spans.LastOrDefault()?.EndPoint ?? 0;
                double nextPoint = lastPoint + spanViewModel.Length * 100;
                var spanGeometry = new SpanGeometry(spanViewModel, lastPoint, nextPoint);
                _spans.Add(spanGeometry);
            }

            Length = _spans.LastOrDefault()?.EndPoint ?? 0;

            if (Position > Length)
                Position = Length;
        }

        private void ShowResults(BeamCalculationResult beamCalculationResult)
        {
            BeamCalculationResult = beamCalculationResult;
            RaisePropertyChanged(nameof(beamCalculationResult));
            RefreshResults();
        }

        private double GetValueAtPosition(IReadOnlyDictionary<double, double> values)
        {
            int maxKey = Array.BinarySearch(values.Keys.ToArray(), Position);
            return maxKey >= 0 ? values.ElementAt(maxKey).Value : values.ElementAt(~maxKey - 1).Value;
        }

        private void RefreshResults(bool enforceShow = false)
        {
            if (enforceShow || ShowNormalForces)
                RaisePropertyChanged(nameof(NormalForceAtPosition));
            if (enforceShow || ShowShearForces)
                RaisePropertyChanged(nameof(ShearForceAtPosition));
            if (enforceShow || ShowBendingMoment)
                RaisePropertyChanged(nameof(BendingMomentAtPosition));
            if (enforceShow || ShowVerticalDeflections)
                RaisePropertyChanged(nameof(VerticalDeflectionAtPosition));
            if (enforceShow || ShowHorizontalDeflections)
                RaisePropertyChanged(nameof(HorizontalDeflectionAtPosition));
            if (enforceShow || ShowRotations)
                RaisePropertyChanged(nameof(RotationAtPosition));
        }

        #endregion // Private_Methods
    }
}
