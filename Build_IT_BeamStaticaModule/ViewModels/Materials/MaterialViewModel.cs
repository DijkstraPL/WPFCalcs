using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Build_IT_BeamStaticaModule.ViewModels.Materials
{
    public class MaterialViewModel
    {
        public string Name { get; }
        public double ThermalExpansionCoefficient { get; }
        public double YoungModulus { get; }
        public double Density { get; }

        public bool IsRemovable { get; }

        public ICommand RemoveMaterialCommand { get; }

        private readonly SpanDetailViewModel _spanDetailViewModel;

        public MaterialViewModel(SpanDetailViewModel spanDetailViewModel, string name, double density, double youngModulus, double thermalExpansionCoefficient, bool isRemovable = true)
        {
            _spanDetailViewModel = spanDetailViewModel ?? throw new ArgumentNullException(nameof(spanDetailViewModel));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ThermalExpansionCoefficient = thermalExpansionCoefficient;
            YoungModulus = youngModulus;
            Density = density;
            IsRemovable = isRemovable;

            RemoveMaterialCommand = new DelegateCommand(() =>
            {
                if (IsRemovable)
                    _spanDetailViewModel.RemoveMaterial(this);
            });
        }
    }
}
