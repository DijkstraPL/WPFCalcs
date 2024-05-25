using Build_IT_BeamStaticaModule.Utils;
using Build_IT_BeamStaticaModule.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Build_IT_BeamStaticaModule.Views.Loads.Span
{
    /// <summary>
    /// Interaction logic for SpanContinuousLoadContainerDataView.xaml
    /// </summary>
    public partial class SpanContinuousLoadContainerDataView : UserControl
    {
        public ContinuousLoadType ContinuousLoadType
        {
            get { return (ContinuousLoadType)GetValue(ContinuousLoadTypeProperty); }
            set { SetValue(ContinuousLoadTypeProperty, value); }
        }

        public static readonly DependencyProperty ContinuousLoadTypeProperty =
            DependencyProperty.Register(nameof(ContinuousLoadType), typeof(ContinuousLoadType), typeof(SpanContinuousLoadContainerDataView),
                new PropertyMetadata(ContinuousLoadTypes.Types[ContinuousLoadTypes.DefaultContinuousLoad] ,new PropertyChangedCallback(ContinuousLoadTypeChanged)));

        private UserControl _contentData;

        public SpanContinuousLoadContainerDataView()
        {
            InitializeComponent();

           _contentData = GetContinuousLoadContent(ContinuousLoadTypes.Types[ContinuousLoadTypes.DefaultContinuousLoad]);
           continuousLoadData.Content = _contentData;

        }

        private static void ContinuousLoadTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spanContinuousLoadContainerDataView = d as SpanContinuousLoadContainerDataView;
            if (spanContinuousLoadContainerDataView == null)
                return;

            spanContinuousLoadContainerDataView._contentData = GetContinuousLoadContent((ContinuousLoadType)e.NewValue);
            spanContinuousLoadContainerDataView.continuousLoadData.Content = spanContinuousLoadContainerDataView._contentData;
        }

        private static UserControl GetContinuousLoadContent(ContinuousLoadType continuousLoadType)
        {
            switch (continuousLoadType)
            {
                case ContinuousLoadType.TemperatureDifference:
                    return new SpanContinuousTemperatureDifferenceDataView();
                case ContinuousLoadType.AngledLoad:
                    return new SpanContinuousAngledLoadDataView();
                case ContinuousLoadType.BendingMoment:
                    return new SpanContinuousBendingMomentDataView();
                case ContinuousLoadType.NormalLoad:
                    return new SpanContinuousNormalLoadDataView();
                case ContinuousLoadType.ShearLoad:
                    return new SpanContinuousShearLoadDataView();
                case ContinuousLoadType.SpanExtend:
                    return new SpanContinuousSpanExtendDataView();
                case ContinuousLoadType.UpDownTemperatureDifference:
                    return new SpanContinuousUpDownTemperatureDifferenceDataView();
                default:
                    throw new NotImplementedException(continuousLoadType.ToString());
            }
        }
    }
}
