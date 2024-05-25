using Build_IT_BeamStaticaModule.Events;
using Build_IT_BeamStaticaModule.Utils;
using Build_IT_BeamStaticaModule.Utils.Enums;
using Build_IT_BeamStaticaModule.ViewModels.Loads;
using Build_IT_BeamStaticaModule.ViewModels.Materials;
using Build_IT_BeamStaticaModule.ViewModels.Sections;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Build_IT_BeamStaticaModule.ViewModels
{
    public class SpanViewModel : BindableBase
    {
        #region Properties

        public string Name { get; }

        private double _length;
        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                foreach (var pointLoad in PointLoads.Where(pl => pl.Position > _length))
                        pointLoad.Position = _length;
                foreach (var continuousLoad in ContinuousLoads.Where(pl => pl.StartPosition > _length))
                    continuousLoad.StartPosition = _length;
                foreach (var continuousLoad in ContinuousLoads.Where(pl => pl.EndPosition > _length))
                    continuousLoad.EndPosition = _length;

                _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel);
            }
        }

        private MaterialViewModel _selectedMaterial;
        public MaterialViewModel SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                SetProperty(ref _selectedMaterial, value);
                _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel);
            }
        }

        private SectionViewModel _selectedSection;
        public SectionViewModel SelectedSection
        {
            get => _selectedSection;
            set
            {
                if (_selectedSection != null)
                    _selectedSection.DataChange -= OnDataChanged;
                SetProperty(ref _selectedSection, value);
                _selectedSection.DataChange += OnDataChanged;
                _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel);
            }
        }

        public NodeViewModel LeftNode { get; }
        public NodeViewModel RightNode { get; }

        public bool IsFirstSpan => _spanListViewModel.SpanViewModels.ToList().IndexOf(this) == 0;

        private IList<SpanPointLoadViewModel> _pointLoadViewModels = new ObservableCollection<SpanPointLoadViewModel>();
        public IEnumerable<SpanPointLoadViewModel> PointLoads => _pointLoadViewModels;

        private IList<SpanContinuousLoadViewModel> _spanContinuousLoadViewModels = new ObservableCollection<SpanContinuousLoadViewModel>();
        public IEnumerable<SpanContinuousLoadViewModel> ContinuousLoads => _spanContinuousLoadViewModels;

        public ICommand RemoveCommand { get; }
        public ICommand AddPointLoadCommand { get; }
        public ICommand AddContinuousLoadCommand { get; }

        private int _index => _spanListViewModel.SpanViewModels.ToList().IndexOf(this);
        private SpanViewModel _previousSpanViewModel => _spanListViewModel.SpanViewModels.ElementAtOrDefault(_index - 1);
        private SpanViewModel _nextSpanViewModel => _spanListViewModel.SpanViewModels.ElementAtOrDefault(_index + 1);

        #endregion // Properties

        #region Fields

        private readonly SpanListViewModel _spanListViewModel;
        private readonly IEventAggregator _eventAggregator;

        #endregion // Fields

        #region Constructors

        public SpanViewModel(string name, SpanListViewModel spanListViewModel, IEventAggregator eventAggregator)
        {
            Name = name;
            _spanListViewModel = spanListViewModel ?? throw new ArgumentNullException(nameof(spanListViewModel));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            if (_previousSpanViewModel != null)
                LeftNode = _previousSpanViewModel.RightNode;
            else if (_index == -1 && _spanListViewModel.SpanViewModels?.Count() > 0)
                LeftNode = _spanListViewModel.SpanViewModels.Last().RightNode;
            else
                LeftNode = new NodeViewModel(this, () => _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel));
            RightNode = _nextSpanViewModel != null && _index != -1 ? _nextSpanViewModel.LeftNode :
                new NodeViewModel(this, () => _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel));

            RemoveCommand = new DelegateCommand(() => spanListViewModel.RemoveSpan(this));
            AddPointLoadCommand = new DelegateCommand(() =>
            _pointLoadViewModels.Add(new SpanPointLoadViewModel(this,
                () => _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel))));
            AddContinuousLoadCommand = new DelegateCommand(() =>
            _spanContinuousLoadViewModels.Add(new SpanContinuousLoadViewModel(this,
                () => _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel))));
        }

        #endregion // Constructors

        #region Internal_Methods

        internal void RemoveLoad(SpanPointLoadViewModel pointLoadViewModel)
        {
            _pointLoadViewModels.Remove(pointLoadViewModel);
            _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel);
        }

        internal void RemoveLoad(SpanContinuousLoadViewModel spanContinuousLoadViewModel)
        {
            _spanContinuousLoadViewModels.Remove(spanContinuousLoadViewModel);
            _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel);
        }

        #endregion // Internal_Methods

        private void OnDataChanged(object sender, EventArgs e)
        {
            _eventAggregator.GetEvent<BeamEditedEvent>().Publish(_spanListViewModel);
        }
    }
}