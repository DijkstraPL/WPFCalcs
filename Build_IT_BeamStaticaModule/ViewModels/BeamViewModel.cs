using Build_IT_BeamStatica;
using Build_IT_BeamStatica.Builders;
using Build_IT_BeamStatica.Builders.ContinuousLoads;
using Build_IT_BeamStatica.Builders.Nodes.Interfaces;
using Build_IT_BeamStatica.Builders.PointLoads;
using Build_IT_BeamStatica.Builders.PointLoads.Interfaces;
using Build_IT_BeamStaticaModule.Events;
using Build_IT_BeamStaticaModule.Utils.Enums;
using Build_IT_BeamStaticaModule.ViewModels.Interfaces;
using Build_IT_BeamStaticaModule.ViewModels.Loads;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Build_IT_BeamStatica.Builders.BuildersOrchestrator;

namespace Build_IT_BeamStaticaModule.ViewModels
{
    public class BeamViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;

        #endregion // Fields

        #region Constructors

        public BeamViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            _eventAggregator.GetEvent<BeamEditedEvent>().Subscribe(async spanListViewModel => await CalculateAsync(spanListViewModel));
        }

        #endregion // Constructors

        #region Private_Methods

        private async Task CalculateAsync(SpanListViewModel spanListViewModel)
        {
            try
            {
                await Task.Run(() =>
                {
                    var beamBuilder = Beam;

                    foreach (var spanViewModel in spanListViewModel.SpanViewModels)
                    {
                        var section = spanViewModel.SelectedSection.GetBuilder();

                        var span =
                        Span.With(section)
                        .With(Material
                            .WithDensity(spanViewModel.SelectedMaterial.Density)
                            .WithThermalExpansionCoefficient(spanViewModel.SelectedMaterial.ThermalExpansionCoefficient)
                            .WithYoungModulus(spanViewModel.SelectedMaterial.YoungModulus))
                        .WithLength(spanViewModel.Length);

                        if (spanListViewModel.IncludeSelfWeight)
                            span.IncludeSelfWeight();

                        var pointLoads = GetPointLoads(spanViewModel.PointLoads);
                        SetLoads(span, pointLoads);

                        var continuousLoads = GetContinuousLoads(spanViewModel.ContinuousLoads);
                        SetLoads(span, continuousLoads);

                        var leftNode = GetNode(spanViewModel.LeftNode);
                        span.WithLeft(leftNode);
                        var rightNode = GetNode(spanViewModel.RightNode);
                        span.WithRight(rightNode);

                        beamBuilder.With(span);
                    }

                    var beam = beamBuilder.Build();
                    var beamCalculator = new BeamCalculator(beam);
                    var result = beamCalculator.Calculate();
                    _eventAggregator.GetEvent<BeamCalculatedEvent>().Publish(result);
                });
            }
            catch
            {
                _eventAggregator.GetEvent<BeamCalculatedEvent>().Publish(null);
            }
        }

        private INodeBuilder GetNode(NodeViewModel nodeViewModel)
        {
            var nodeType = nodeViewModel.Type;
            var pointLoads = GetPointLoads(nodeViewModel.PointLoads);

            switch (nodeType)
            {
                case NodeType.Fixed:
                    return Configure(Node.Fixed, pointLoads);
                case NodeType.Free:
                    return Configure(Node.Free, pointLoads);
                case NodeType.Hinge:
                    return Configure(Node.Hinge, pointLoads);
                case NodeType.Pin:
                    return Configure(Node.Pin.With(nodeViewModel.Angle), pointLoads);
                case NodeType.Sleeve:
                    return Configure(Node.Sleeve, pointLoads);
                case NodeType.Support:
                    return Configure(Node.Support, pointLoads);
                case NodeType.SupportWithHinge:
                    return Configure(Node.SupportWithHinge, pointLoads);
                case NodeType.Telescope:
                    return Configure(Node.Telescope, pointLoads);
                default:
                    throw new NotImplementedException(nodeType.ToString());
            }
        }

        private INodeBuilder Configure<T>(NodeBuilder<T> nodeBuilder,
            IEnumerable<INodePointLoadBuilder> pointLoads)
            where T : NodeBuilder<T>
        {
            foreach (var pointLoad in pointLoads)
                nodeBuilder.With(pointLoad);

            return nodeBuilder;
        }

        private IEnumerable<INodePointLoadBuilder> GetPointLoads(IEnumerable<NodePointLoadViewModel> pointLoads)
        {
            foreach (var pointLoad in pointLoads)
            {
                switch (pointLoad.Type)
                {
                    case PointLoadType.AngledLoad:
                        yield return PointLoad.AngledLoad
                            .WithValue(pointLoad.Value)
                            .WithAngle(pointLoad.Angle);
                        continue;
                    case PointLoadType.BendingMoment:
                        yield return PointLoad.BendingMoment
                            .WithValue(pointLoad.Value);
                        continue;
                    case PointLoadType.HorizontalDisplacement:
                        yield return PointLoad.HorizontalDisplacement
                            .WithValue(pointLoad.Value);
                        continue;
                    case PointLoadType.NormalLoad:
                        yield return PointLoad.NormalLoad
                            .WithValue(pointLoad.Value);
                        continue;
                    case PointLoadType.RotationDisplacement:
                        yield return PointLoad.RotationDisplacement
                            .WithValue(pointLoad.Value);
                        continue;
                    case PointLoadType.ShearLoad:
                        yield return PointLoad.ShearLoad
                            .WithValue(pointLoad.Value);
                        continue;
                    case PointLoadType.VerticalDisplacement:
                        yield return PointLoad.VerticalDisplacement
                            .WithValue(pointLoad.Value);
                        continue;
                    default:
                        throw new NotImplementedException(pointLoad.Type.ToString());
                }
            }
        }


        private IEnumerable<ISpanPointLoadBuilder> GetPointLoads(IEnumerable<SpanPointLoadViewModel> pointLoads)
        {
            foreach (var pointLoad in pointLoads)
            {
                switch (pointLoad.Type)
                {
                    case PointLoadType.AngledLoad:
                        yield return PointLoad.OnSpan.AngledLoad
                            .WithValue(pointLoad.Value)
                            .WithAngle(pointLoad.Angle)
                            .WithPosition(pointLoad.Position);
                        continue;
                    case PointLoadType.BendingMoment:
                        yield return PointLoad.OnSpan.BendingMoment
                            .WithValue(pointLoad.Value)
                            .WithPosition(pointLoad.Position);
                        continue;
                    case PointLoadType.NormalLoad:
                        yield return PointLoad.OnSpan.NormalLoad
                            .WithValue(pointLoad.Value)
                            .WithPosition(pointLoad.Position);
                        continue;
                    case PointLoadType.ShearLoad:
                        yield return PointLoad.OnSpan.ShearLoad
                            .WithValue(pointLoad.Value)
                            .WithPosition(pointLoad.Position);
                        continue;
                    default:
                        throw new NotImplementedException(pointLoad.Type.ToString());
                }
            }
        }

        private void SetLoads(SpanBuilder span, IEnumerable<ISpanPointLoadBuilder> loads)
        {
            foreach (var load in loads)
                span.With(load);
        }

        private IEnumerable<IContinuousLoadBuilder> GetContinuousLoads(IEnumerable<SpanContinuousLoadViewModel> continuousLoads)
        {
            foreach (var continuousLoad in continuousLoads)
            {
                switch (continuousLoad.Type)
                {
                    case ContinuousLoadType.AngledLoad:
                        yield return ContinuousLoad.AngledLoad
                            .WithStartValue(continuousLoad.StartValue)
                            .WithEndValue(continuousLoad.EndValue)
                            .WithAngle(continuousLoad.Angle)
                            .WithStartPosition(continuousLoad.StartPosition)
                            .WithEndPosition(continuousLoad.EndPosition);
                        continue;
                    case ContinuousLoadType.BendingMoment:
                        yield return ContinuousLoad.BendingMomentLoad
                            .WithValue(continuousLoad.StartValue);
                        continue;
                    case ContinuousLoadType.NormalLoad:
                        yield return ContinuousLoad.NormalLoad
                            .WithStartValue(continuousLoad.StartValue)
                            .WithEndValue(continuousLoad.EndValue)
                            .WithStartPosition(continuousLoad.StartPosition)
                            .WithEndPosition(continuousLoad.EndPosition);
                        continue;
                    case ContinuousLoadType.ShearLoad:
                        yield return ContinuousLoad.ShearLoad
                            .WithStartValue(continuousLoad.StartValue)
                            .WithEndValue(continuousLoad.EndValue)
                            .WithStartPosition(continuousLoad.StartPosition)
                            .WithEndPosition(continuousLoad.EndPosition);
                        continue;
                    case ContinuousLoadType.SpanExtend:
                        yield return ContinuousLoad.SpanExtend
                            .WithLengthIncrease(continuousLoad.StartValue);
                        continue;
                    case ContinuousLoadType.TemperatureDifference:
                        yield return ContinuousLoad.AlongTemperatureDifferenceContinousLoad
                            .WithTemperatureDifference(continuousLoad.StartValue);
                        continue;
                    case ContinuousLoadType.UpDownTemperatureDifference:
                        yield return ContinuousLoad.UpDownTemperatureDifferenceLoad
                            .WithLowerTemperature(continuousLoad.StartValue)
                            .WithUpperTemperature(continuousLoad.EndValue);
                        continue;
                    default:
                        throw new NotImplementedException(continuousLoad.Type.ToString());
                }
            }
        }

        private void SetLoads(SpanBuilder span, IEnumerable<IContinuousLoadBuilder> continuousLoads)
        {
            foreach (var load in continuousLoads)
                span.With(load);
        }

        #endregion // Private_Methods
    }
}
