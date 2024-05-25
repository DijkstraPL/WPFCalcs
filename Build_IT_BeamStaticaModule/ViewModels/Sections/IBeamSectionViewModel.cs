using Build_IT_BeamStatica.Builders;
using Build_IT_BeamStatica.Builders.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaModule.ViewModels.Sections
{
    public  class IBeamSectionViewModel : SectionViewModel
    {
        public IBeamSectionViewModel(IEventAggregator eventAggregator) : base(BuildersOrchestrator.CustomSectionProperties, "I-beam", eventAggregator)
        {
        }

        public override SectionViewModel Copy()
        {
            throw new NotImplementedException();
        }

        public override ISectionPropertiesBuilder GetBuilder()
        {
            throw new NotImplementedException();
        }
    }
}
