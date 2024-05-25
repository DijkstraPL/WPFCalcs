using Build_IT_BeamStatica.Builders.Interfaces;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaModule.ViewModels.Sections
{
    public abstract class SectionViewModel : BindableBase
    {
        #region Properties
        
        public string Name { get; }

        #endregion // Properties

        #region Fields

        protected readonly ISectionPropertiesBuilder _sectionPropertiesBuilder;
        protected readonly IEventAggregator _eventAggregator;

        #endregion // Fields

        public event EventHandler DataChange;

        #region Constructors

        public SectionViewModel(ISectionPropertiesBuilder sectionPropertiesBuilder, string name, IEventAggregator eventAggregator)
        {
            _sectionPropertiesBuilder = sectionPropertiesBuilder ?? throw new ArgumentNullException(nameof(sectionPropertiesBuilder));
            Name = name ?? throw new ArgumentNullException(nameof(name));
           _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        #endregion // Constructors

        #region Public_Methods
        
        public abstract ISectionPropertiesBuilder GetBuilder();
        public abstract SectionViewModel Copy();

        #endregion // Public_Methods

        protected void RaiseDataChanged()
        {
            DataChange?.Invoke(this, EventArgs.Empty);
        }
    }
}
