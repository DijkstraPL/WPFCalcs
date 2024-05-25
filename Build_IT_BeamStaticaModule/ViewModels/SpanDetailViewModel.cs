using Build_IT_BeamStaticaModule.Events;
using Build_IT_BeamStaticaModule.ViewModels.Materials;
using Build_IT_BeamStaticaModule.ViewModels.Sections;
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
    public class SpanDetailViewModel : BindableBase
    {
        #region Properties

        private SpanViewModel _selectedSpanViewModel;
        public SpanViewModel SelectedSpanViewModel
        {
            get => _selectedSpanViewModel;
            private set => SetProperty(ref _selectedSpanViewModel, value);
        }

        private IList<MaterialViewModel> _materials;
        public IEnumerable<MaterialViewModel> Materials => _materials;

        private string _newMaterialName;
        public string NewMaterialName
        {
            get => _newMaterialName;
            set => SetProperty(ref _newMaterialName, value);
        }

        private double _newMaterialDensity;
        public double NewMaterialDensity
        {
            get => _newMaterialDensity;
            set => SetProperty(ref _newMaterialDensity, value);
        }

        private double _newMaterialYoungModulus;
        public double NewMaterialYoungModulus
        {
            get => _newMaterialYoungModulus;
            set => SetProperty(ref _newMaterialYoungModulus, value);
        }

        private double _newMaterialThermalExpansionCoefficient;
        public double NewMaterialThermalExpansionCoefficient
        {
            get => _newMaterialThermalExpansionCoefficient;
            set => SetProperty(ref _newMaterialThermalExpansionCoefficient, value);
        }

        private bool _showMaterialDetails;
        public bool ShowMaterialDetails
        {
            get => _showMaterialDetails;
            set => SetProperty(ref _showMaterialDetails, value);
        }

        private IList<SectionViewModel> _sectionTypes = new ObservableCollection<SectionViewModel>();
        public IEnumerable<SectionViewModel> SectionTypes => _sectionTypes;

        public ICommand ExpandMaterialDataCommand { get; }
        public ICommand AddNewMaterialCommand { get; }

        public static MaterialViewModel DefaultMaterial { get; private set; }
        public static SectionViewModel DefaultSection { get; private set; }

        #endregion // Properties

        #region Fields

        private readonly IEventAggregator _eventAggregator;

        #endregion // Fields

        #region Constructors

        public SpanDetailViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            _eventAggregator.GetEvent<SpanChangedEvent>()
                .Subscribe(spanViewModel => SelectedSpanViewModel = spanViewModel);

            ExpandMaterialDataCommand = new DelegateCommand(ExpandMaterialData);
            AddNewMaterialCommand = new DelegateCommand(() =>
            {
                _materials.Add(new MaterialViewModel(this, NewMaterialName, NewMaterialDensity, NewMaterialYoungModulus, NewMaterialThermalExpansionCoefficient));
                ShowMaterialDetails = false;
                SelectedSpanViewModel.SelectedMaterial = Materials.Last();
            });

            SetMaterials();
            SetSections();
        }

        #endregion // Constructors

        internal void RemoveMaterial(MaterialViewModel materialViewModel)
        {
            if (materialViewModel == SelectedSpanViewModel.SelectedMaterial)
                SelectedSpanViewModel.SelectedMaterial = Materials.First();
            _materials.Remove(materialViewModel);
        }

        private void SetMaterials()
        {
            const double g = 9.812;

            _materials = new ObservableCollection<MaterialViewModel>
            {
                new MaterialViewModel(this, "Steel" , density: 78.50 / g * 1000, youngModulus: 210, thermalExpansionCoefficient: 0.000012, isRemovable:false),
                new MaterialViewModel(this, "Concrete C12/15" , density: 24/ g * 1000, youngModulus: 27, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C16/20" , density: 24/ g * 1000, youngModulus: 29, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C20/25" , density: 24/ g * 1000, youngModulus: 30, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C25/30" , density: 24/ g * 1000, youngModulus: 31, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C30/37" , density: 24/ g * 1000, youngModulus: 32, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C35/45" , density: 24/ g * 1000, youngModulus: 34, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C40/50" , density: 24/ g * 1000, youngModulus: 35, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C45/55" , density: 24/ g * 1000, youngModulus: 36, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C50/60" , density: 24/ g * 1000, youngModulus: 37, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C55/67" , density: 24/ g * 1000, youngModulus: 38, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C60/75" , density: 24/ g * 1000, youngModulus: 39, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C70/85" , density: 24/ g * 1000, youngModulus: 41, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C80/95" , density: 24/ g * 1000, youngModulus: 42, thermalExpansionCoefficient: 0.000010, isRemovable:false),
                new MaterialViewModel(this, "Concrete C90/105" , density: 24/ g * 1000, youngModulus: 44, thermalExpansionCoefficient: 0.000010, isRemovable:false),
            };

            DefaultMaterial = Materials.ElementAt(3);
        }

        private void SetSections()
        {
            _sectionTypes.Add(new RectangularSectionViewModel(_eventAggregator));

            DefaultSection = SectionTypes.First();
        }

        private void ExpandMaterialData()
        {
            ShowMaterialDetails = !ShowMaterialDetails;

            if (ShowMaterialDetails && SelectedSpanViewModel?.SelectedMaterial != null)
            {
                NewMaterialName = SelectedSpanViewModel.SelectedMaterial.Name + "_Copy";
                NewMaterialDensity = SelectedSpanViewModel.SelectedMaterial.Density;
                NewMaterialYoungModulus = SelectedSpanViewModel.SelectedMaterial.YoungModulus;
                NewMaterialThermalExpansionCoefficient = SelectedSpanViewModel.SelectedMaterial.ThermalExpansionCoefficient;
            }
        }
    }
}
