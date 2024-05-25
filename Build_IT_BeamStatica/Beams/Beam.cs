using Build_IT_BeamStatica.Beams.Interfaces;
using Build_IT_BeamStatica.CalculationEngines.DirectStiffnessMethod.Beams;
using Build_IT_BeamStatica.CalculationEngines.Interfaces;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Results.Interfaces;
using Build_IT_BeamStatica.Results.OnSpan;
using Build_IT_BeamStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Build_IT_BeamStaticaTests")]
namespace Build_IT_BeamStatica.Beams
{
    internal class Beam : IBeam
    {
        #region Properties

        public double Length => Spans.Sum(s => s.Length);

        public short NumberOfDegreesOfFreedom { get; private set; }

        public IList<ISpan> Spans { get; }
        public ICollection<INode> Nodes { get; }
               
        public bool IncludeSelfWeight { get; }
        public IBeamCalculationEngine CalculationEngine { get; private set; }

        public IResultsContainer Results { get; }

        #endregion // Properties

        #region Constructors

        public Beam(IList<ISpan> spans, ICollection<INode> nodes, bool includeSelfWeight)
        {
            Spans = spans ?? throw new ArgumentNullException(nameof(spans));
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            
            CalculationEngine = new DirectStiffnessCalculationEngine(this);
            Results = new ResultsContainer(this);

            IncludeSelfWeight = includeSelfWeight;
        }

        public Beam(IList<ISpan> spans, ICollection<INode> nodes, 
            IBeamCalculationEngine beamCalculationEngine, IResultsContainer resultsContainer,
            bool includeSelfWeight)
            : this(spans, nodes, includeSelfWeight)
        {
            CalculationEngine = beamCalculationEngine ?? 
                throw new ArgumentNullException(nameof(beamCalculationEngine));
            Results = resultsContainer ??
                throw new ArgumentNullException(nameof(beamCalculationEngine));
        }

        #endregion // Constructors

        #region Public_Methods

        public void SetNumeration()
        {
            short spanCounter = 0;
            short nodeCounter = 0;

            spanCounter = SetSpanNumeration(spanCounter);
            nodeCounter = SetNodeNumeration(nodeCounter);

            SetNumberOfDegreesOfFreedom();
        }

        #endregion // Public_Methods

        #region Private_Methods

        private void SetNumberOfDegreesOfFreedom()
        {
            foreach (var node in Nodes)
                NumberOfDegreesOfFreedom += node.DegreesOfFreedom;
        }

        private short SetSpanNumeration(short spanCounter)
        {
            foreach (var span in Spans)
                span.Number = spanCounter++;
            return spanCounter;
        }

        private short SetNodeNumeration(short nodeCounter)
        {
            foreach (var node in Nodes)
                node.SetDisplacementNumeration(ref nodeCounter);
            foreach (var node in Nodes)
                node.SetReactionNumeration(ref nodeCounter);
            return nodeCounter;
        }

        #endregion // Private_Methods
    }
}
