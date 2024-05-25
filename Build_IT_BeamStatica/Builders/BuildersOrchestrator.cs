using Build_IT_BeamStatica.Builders.ContinuousLoads;
using Build_IT_BeamStatica.Builders.Nodes;
using Build_IT_BeamStatica.Builders.PointLoads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders
{
    public static class BuildersOrchestrator
    {
        public static BeamBuilder Beam => new BeamBuilder();
        public static SpanBuilder Span => new SpanBuilder();
        public static MaterialBuilder Material => new MaterialBuilder();
        public static CustomSectionPropertiesBuilder CustomSectionProperties => new CustomSectionPropertiesBuilder();
        public static SectionPropertiesBuilder SectionProperties => new SectionPropertiesBuilder();

        #region Nodes

        public static class Node
        {
            public static FixedNodeBuilder Fixed => new FixedNodeBuilder();
            public static FreeNodeBuilder Free => new FreeNodeBuilder();
            public static HingeBuilder Hinge => new HingeBuilder();
            public static PinNodeBuilder Pin => new PinNodeBuilder();
            public static SleeveNodeBuilder Sleeve => new SleeveNodeBuilder();
            public static SupportedNodeBuilder Support => new SupportedNodeBuilder();
            public static SupportedNodeWithHingeBuilder SupportWithHinge => new SupportedNodeWithHingeBuilder();
            public static TelescopeNodeBuilder Telescope => new TelescopeNodeBuilder();
        }

        #endregion // Nodes


        #region PointLoads

        public static class PointLoad
        {
            public static class OnSpan
            {
                public static SpanAngledLoadBuilder AngledLoad => new SpanAngledLoadBuilder();
                public static SpanBendingMomentBuilder BendingMoment => new SpanBendingMomentBuilder();
                public static SpanNormalLoadBuilder NormalLoad => new SpanNormalLoadBuilder();
                public static SpanShearLoadBuilder ShearLoad => new SpanShearLoadBuilder();
            }

            public static AngledLoadBuilder AngledLoad => new AngledLoadBuilder();
            public static BendingMomentBuilder BendingMoment => new BendingMomentBuilder();
            public static HorizontalDisplacementBuilder HorizontalDisplacement => new HorizontalDisplacementBuilder();
            public static NormalLoadBuilder NormalLoad => new NormalLoadBuilder();
            public static RotationDisplacementBuilder RotationDisplacement => new RotationDisplacementBuilder();
            public static ShearLoadBuilder ShearLoad => new ShearLoadBuilder();
            public static VerticalDisplacementBuilder VerticalDisplacement => new VerticalDisplacementBuilder();
        }

        #endregion // PointLoads

        #region ContinuousLoads

        public static class ContinuousLoad
        {
            public static AlongTemperatureDifferenceLoadBuilder AlongTemperatureDifferenceContinousLoad => new AlongTemperatureDifferenceLoadBuilder();
            public static UpDownTemperatureDifferenceLoadBuilder UpDownTemperatureDifferenceLoad => new UpDownTemperatureDifferenceLoadBuilder();
            public static ContinuousShearLoadBuilder ShearLoad => new ContinuousShearLoadBuilder();
            public static ContinuousNormalLoadBuilder NormalLoad => new ContinuousNormalLoadBuilder();
            public static ContinuousBendingMomentLoadBuilder BendingMomentLoad => new ContinuousBendingMomentLoadBuilder();
            public static SpanExtendLoadBuilder SpanExtend => new SpanExtendLoadBuilder();
            public static ContinuousAngledLoadBuilder AngledLoad => new ContinuousAngledLoadBuilder();
        }

        #endregion // ContinuousLoads
    }
}
