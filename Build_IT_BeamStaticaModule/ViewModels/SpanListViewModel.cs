using Build_IT_BeamStaticaModule.Events;
using Build_IT_BeamStaticaModule.ViewModels.Interfaces;
using Build_IT_BeamStaticaModule.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Build_IT_BeamStaticaModule.ViewModels
{
    public class SpanListViewModel : BindableBase, ISpanListViewModel
    {
        #region Properties
        
        public IList<SpanViewModel> _spanViewModels = new ObservableCollection<SpanViewModel>();
        public IEnumerable<SpanViewModel> SpanViewModels => _spanViewModels;

        private SpanViewModel _selectedSpanViewModel;
        public SpanViewModel SelectedSpanViewModel
        {
            get => _selectedSpanViewModel;
            set
            {
                if (SetProperty(ref _selectedSpanViewModel, value))
                    _eventAggregator.GetEvent<SpanChangedEvent>().Publish(_selectedSpanViewModel);
            }
        }

        private bool _includeSelfWeight = true;
        public bool IncludeSelfWeight
        {
            get => _includeSelfWeight;
            set
            {
                if (SetProperty(ref _includeSelfWeight, value))
                    _eventAggregator.GetEvent<BeamEditedEvent>().Publish(this);
            }
        }
        
        public ICommand AddCommand { get; }

        #endregion // Properties

        #region Fields

        private readonly IEventAggregator _eventAggregator;

        #endregion // Fields

        #region Constructors

        public SpanListViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            AddCommand = new DelegateCommand(() => AddNewSpan());
        }

        #endregion // Constructors

        #region Internal_Methods
        
        internal void RemoveSpan(SpanViewModel spanViewModel)
        {
            _spanViewModels.Remove(spanViewModel);
            _eventAggregator.GetEvent<BeamEditedEvent>().Publish(this);
        }

        #endregion // Internal_Methods

        #region Private_Methods

        private void AddNewSpan()
        {
            var newSpanViewModel = new SpanViewModel("Span " + (SpanViewModels.Count() + 1), this, _eventAggregator);
            newSpanViewModel.SelectedMaterial = SpanViewModels.LastOrDefault()?.SelectedMaterial ?? SpanDetailViewModel.DefaultMaterial;
            newSpanViewModel.SelectedSection = SpanViewModels.LastOrDefault()?.SelectedSection.Copy() ?? SpanDetailViewModel.DefaultSection;
            _spanViewModels.Add(newSpanViewModel);

            SelectedSpanViewModel = SpanViewModels.LastOrDefault();

            _eventAggregator.GetEvent<BeamEditedEvent>().Publish(this);
        }

        #endregion // Private_Methods
    }
}
