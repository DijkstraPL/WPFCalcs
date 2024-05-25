using Build_IT_BeamStatica.Builders;
using Build_IT_BeamStatica.Builders.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaModule.ViewModels.Sections
{
    public class RectangularSectionViewModel : SectionViewModel
    {
        #region Properties

        private double _width = 240;
        public double Width
        {
            get => _width;
            set
            {
                SetProperty(ref _width, value);
                RaiseDataChanged();
            }
        }

        private double _height = 500;
        public double Height
        {
            get => _height;
            set
            {
                SetProperty(ref _height, value);
                RaiseDataChanged();
            }
        }

        private SectionPropertiesBuilder _sectionPropertiesBuilderImpl => _sectionPropertiesBuilder as SectionPropertiesBuilder;

        #endregion // Properties

        #region Constructors

        public RectangularSectionViewModel(IEventAggregator eventAggregator)
            : base(BuildersOrchestrator.SectionProperties, "Rectangular", eventAggregator)
        {
        }

        #endregion // Constructors

        #region Public_Methods

        public override ISectionPropertiesBuilder GetBuilder()
            => _sectionPropertiesBuilderImpl
                .ClearPoints()
                .WithPoint(0, 0)
                .WithPoint(Width, 0)
                .WithPoint(Width, Height)
                .WithPoint(0, Height);

        public override SectionViewModel Copy()
        {
            var section = new RectangularSectionViewModel(_eventAggregator);
            section.Width = this.Width;
            section.Height = this.Height;

            return section;
        }

        #endregion // Public_Methods
    }
}
