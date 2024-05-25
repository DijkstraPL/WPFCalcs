using Build_IT_BeamStatica.Data;
using Build_IT_BeamStatica.Factories;
using Build_IT_BeamStatica.Loads.Enums;
using Build_IT_BeamStatica.Nodes.Interfaces;
using Build_IT_BeamStatica.Results.OnSpan;
using Build_IT_BeamStatica.Spans.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_BeamStatica
{
    public class BeamCalculator
    {
        #region Properties

        public BeamData BeamData { get; }

        #endregion // Properties

        #region Constructors

        public BeamCalculator(BeamData beamData)
        {
            BeamData = beamData ?? throw new ArgumentNullException(nameof(beamData));
        }

        #endregion // Constructors

        #region Public_Methods

        public BeamCalculationResult Calculate()
        {
            var spans = new List<ISpan>();
            var nodes = new List<INode>();

            foreach (var spanData in BeamData.SpanDatas)
            {
                MaterialData material = spanData.Material;
                SectionData section = spanData.Section;

                INode leftNode = SetLeftNode(nodes, spanData);
                INode rightNode = SetRightNode(nodes, spanData);

                var span = SpanFactory.Create(
                    leftNode, spanData.Length, rightNode, material, section, spanData.IncludeSelfWeight);
                spans.Add(span);

                SetContinousLoads(spanData, span);
                SetPointLoads(spanData, leftNode, rightNode, span);
            }

            var beam = BeamFactory.Create(spans, nodes, BeamData.IncludeSelfWeight);
            beam.CalculationEngine.Calculate();

            var resultsContainer = new ResultsContainer(beam);
            resultsContainer.SetResults();
            return new BeamCalculationResult(resultsContainer);
        }

        #endregion // Public_Methods

        #region Private_Methods
               
        private INode SetLeftNode(List<INode> nodes, SpanData spanData)
        {
            INode leftNode;
            if (nodes.Count == 0)
            {
                leftNode = NodeFactory.Create(spanData.LeftNode, spanData.LeftNode.Angle);
                foreach (var pointLoad in spanData.LeftNode.PointLoads)
                {
                    var load = LoadFactory.CreatePointLoad(
                            pointLoad.PointLoadType,
                            pointLoad.Position,
                            pointLoad.Value,
                            pointLoad.Angle);
                    leftNode.ConcentratedForces.Add(load);
                }
                nodes.Add(leftNode);
            }
            else
                leftNode = nodes.Last();
            return leftNode;
        }

        private INode SetRightNode(List<INode> nodes, SpanData spanData)
        {
            INode rightNode = NodeFactory.Create(spanData.RightNode, spanData.RightNode.Angle);
            foreach (var pointLoad in spanData.RightNode.PointLoads)
            {
                var load = LoadFactory.CreatePointLoad(
                        pointLoad.PointLoadType,
                        pointLoad.Position,
                        pointLoad.Value,
                        pointLoad.Angle);
                rightNode.ConcentratedForces.Add(load);
            }
            nodes.Add(rightNode);
            return rightNode;
        }

        private void SetContinousLoads(SpanData spanData, ISpan span)
        {
            foreach (var continousLoad in spanData.ContinuousLoads)
            {
                span.ContinousLoads.Add(
                    LoadFactory.CreateContinousLoad(
                        continousLoad.ContinuousLoadType,
                        span,
                        continousLoad.StartPosition,
                        continousLoad.StartValue,
                        continousLoad.EndPosition,
                        continousLoad.EndValue,
                        continousLoad.Angle));
            }
        }

        private void SetPointLoads(SpanData spanData, INode leftNode, INode rightNode, ISpan span)
        {
            foreach (var pointLoad in spanData.PointLoads)
            {
                var load = LoadFactory.CreatePointLoad(
                        pointLoad.PointLoadType,
                        pointLoad.Position,
                        pointLoad.Value,
                        pointLoad.Angle);

                if (pointLoad.PointLoadType == PointLoadType.HorizontalDisplacement ||
                    pointLoad.PointLoadType == PointLoadType.VerticalDisplacement ||
                    pointLoad.PointLoadType == PointLoadType.RotationDisplacement)
                {
                    if (pointLoad.Position < span.Length / 2)
                        leftNode.ConcentratedForces.Add(load);
                    else
                        rightNode.ConcentratedForces.Add(load);
                }
                else
                    span.PointLoads.Add(load);
            }
        }

        #endregion // Private_Methods
    }
}
