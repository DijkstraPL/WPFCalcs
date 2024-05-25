using Build_IT_BeamStatica;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaModule.Events
{
    public class BeamCalculatedEvent : PubSubEvent<BeamCalculationResult>
    {
    }
}
