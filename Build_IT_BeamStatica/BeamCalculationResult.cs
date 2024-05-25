using Build_IT_BeamStatica.Results.Interfaces;
using System.Collections.Generic;

namespace Build_IT_BeamStatica
{
    public class BeamCalculationResult
    {
        #region Properties
        
        private SortedDictionary<double, double> _normalForces = new SortedDictionary<double, double>();
        public IReadOnlyDictionary<double, double> NormalForces => _normalForces;

        private SortedDictionary<double, double> _shearForces = new SortedDictionary<double, double>();
        public IReadOnlyDictionary<double, double> ShearForces => _shearForces;

        private SortedDictionary<double, double> _bendingMoments = new SortedDictionary<double, double>();
        public IReadOnlyDictionary<double, double> BendingMoments => _bendingMoments;

        private SortedDictionary<double, double> _horizontalDeflections = new SortedDictionary<double, double>();
        public IReadOnlyDictionary<double, double> HorizontalDeflections => _horizontalDeflections;

        private SortedDictionary<double, double> _verticalDeflections = new SortedDictionary<double, double>();
        public IReadOnlyDictionary<double, double> VerticalDeflections => _verticalDeflections;

        private SortedDictionary<double, double> _rotations = new SortedDictionary<double, double>();
        public IReadOnlyDictionary<double, double> Rotations => _rotations;

        public IResultsContainer ResultsContainer { get; }

        #endregion // Properties

        #region Constructors

        public BeamCalculationResult(IResultsContainer resultsContainer)
        {
            ResultsContainer = resultsContainer;

            SetResults(_normalForces, resultsContainer.NormalForce.Values);
            SetResults(_shearForces, resultsContainer.Shear.Values);
            SetResults(_bendingMoments, resultsContainer.BendingMoment.Values);
            SetResults(_horizontalDeflections, resultsContainer.HorizontalDeflection.Values);
            SetResults(_verticalDeflections, resultsContainer.VerticalDeflection.Values);
            SetResults(_rotations, resultsContainer.Rotation.Values);
        }

        #endregion // Constructors

        #region Private_Methods
        
        private void SetResults(IDictionary<double, double> resultContainer, ICollection<IResultValue> results)
        {
            foreach (var result in results)
                resultContainer.Add(result.Position ?? 0, result.Value);
        }

        #endregion // Private_Methods
    }
}
