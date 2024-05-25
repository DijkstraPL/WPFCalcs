using Build_IT_BeamStaticaModule.Utils;
using Build_IT_BeamStaticaModule.Utils.Enums;
using Build_IT_BeamStaticaModule.Views.Preview.PointLoads;
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

namespace Build_IT_BeamStaticaModule.Views.Preview.ContinuousLoads
{
    /// <summary>
    /// Interaction logic for ContinuousLoad.xaml
    /// </summary>
    public partial class ContinuousLoad : UserControl
    {
        public ContinuousLoadType ContinuousLoadType
        {
            get { return (ContinuousLoadType)GetValue(ContinuousLoadTypeProperty); }
            set { SetValue(ContinuousLoadTypeProperty, value); }
        }

        public static readonly DependencyProperty ContinuousLoadTypeProperty =
            DependencyProperty.Register(nameof(ContinuousLoadType), typeof(ContinuousLoadType), typeof(ContinuousLoad),
                new PropertyMetadata(ContinuousLoadTypes.Types[ContinuousLoadTypes.DefaultContinuousLoad], new PropertyChangedCallback(SetContinuousLoad)));

        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register(nameof(StartValue), typeof(double), typeof(ContinuousLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetValue)));

        public double EndValue
        {
            get { return (double)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register(nameof(EndValue), typeof(double), typeof(ContinuousLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetValue)));

        public double StartPosition
        {
            get { return (double)GetValue(StartPositionProperty); }
            set { SetValue(StartPositionProperty, value); }
        }

        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register(nameof(StartPosition), typeof(double), typeof(ContinuousLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetPosition)));

        public double EndPosition
        {
            get { return (double)GetValue(EndPositionProperty); }
            set { SetValue(EndPositionProperty, value); }
        }

        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register(nameof(EndPosition), typeof(double), typeof(ContinuousLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetPosition)));

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(nameof(Angle), typeof(double), typeof(ContinuousLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetAngle)));

        public double SpanLength
        {
            get { return (double)GetValue(SpanLengthProperty); }
            set { SetValue(SpanLengthProperty, value); }
        }

        public static readonly DependencyProperty SpanLengthProperty =
            DependencyProperty.Register(nameof(SpanLength), typeof(double), typeof(ContinuousLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetSpanLength)));
        
        private UserControl _continuousLoadPicture;

        public ContinuousLoad()
        {
            InitializeComponent();
            _continuousLoadPicture = GetContinuousLoad(ContinuousLoadType, this);
            continuousLoadPicture.Content = _continuousLoadPicture;
        }

        private static void SetContinuousLoad(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var continuousLoad = d as ContinuousLoad;
            if (continuousLoad == null)
                return;
            continuousLoad._continuousLoadPicture = GetContinuousLoad((ContinuousLoadType)e.NewValue, continuousLoad);

            continuousLoad.continuousLoadPicture.Content = continuousLoad._continuousLoadPicture;
        }

        private static UserControl GetContinuousLoad(ContinuousLoadType continuousLoadType, ContinuousLoad continuousLoad)
        {
            switch (continuousLoadType)
            {
                case ContinuousLoadType.ShearLoad:
                    return SetShearLoad(continuousLoad);
                case ContinuousLoadType.NormalLoad:
                    return SetNormalLoad(continuousLoad);
                case ContinuousLoadType.BendingMoment:
                    return SetBendingMoment(continuousLoad);
                case ContinuousLoadType.AngledLoad:
                    return SetAngledLoad(continuousLoad);
                case ContinuousLoadType.SpanExtend:
                    return SetSpanExtend(continuousLoad);
                case ContinuousLoadType.TemperatureDifference:
                    return SetTemperatureDifference(continuousLoad);
                case ContinuousLoadType.UpDownTemperatureDifference:
                    return SetUpDownTemperatureDifference(continuousLoad);
                default:
                    throw new NotImplementedException(continuousLoadType.ToString());
            }
        }

        private static ShearLoad SetShearLoad(ContinuousLoad continuousLoad)
        {
            var shearLoad = new ShearLoad();

            Binding shearStartLoadBinding = new Binding(nameof(StartValue));
            shearStartLoadBinding.Source = continuousLoad;
            shearLoad.SetBinding(ShearLoad.ShearStartValueProperty, shearStartLoadBinding);
            
            Binding shearEndLoadBinding = new Binding(nameof(EndValue));
            shearEndLoadBinding.Source = continuousLoad;
            shearLoad.SetBinding(ShearLoad.ShearEndValueProperty, shearEndLoadBinding);

            Binding startPositionBinding = new Binding(nameof(StartPosition));
            startPositionBinding.Source = continuousLoad;
            shearLoad.SetBinding(ShearLoad.StartPositionProperty, startPositionBinding);

            Binding endPositionBinding = new Binding(nameof(EndPosition));
            endPositionBinding.Source = continuousLoad;
            shearLoad.SetBinding(ShearLoad.EndPositionProperty, endPositionBinding);

            return shearLoad;
        }

        private static UserControl SetNormalLoad(ContinuousLoad continuousLoad)
        {
            var normalLoad = new NormalLoad();

            Binding normalStartLoadBinding = new Binding(nameof(StartValue));
            normalStartLoadBinding.Source = continuousLoad;
            normalLoad.SetBinding(NormalLoad.NormalStartValueProperty, normalStartLoadBinding);

            Binding normalEndLoadBinding = new Binding(nameof(EndValue));
            normalEndLoadBinding.Source = continuousLoad;
            normalLoad.SetBinding(NormalLoad.NormalEndValueProperty, normalEndLoadBinding);

            Binding startPositionBinding = new Binding(nameof(StartPosition));
            startPositionBinding.Source = continuousLoad;
            normalLoad.SetBinding(NormalLoad.StartPositionProperty, startPositionBinding);

            Binding endPositionBinding = new Binding(nameof(EndPosition));
            endPositionBinding.Source = continuousLoad;
            normalLoad.SetBinding(NormalLoad.EndPositionProperty, endPositionBinding);

            return normalLoad;
        }

        private static UserControl SetBendingMoment(ContinuousLoad continuousLoad)
        {
            var bendingMoment = new BendingMoment();

            Binding bendingMomentBinding = new Binding(nameof(StartValue));
            bendingMomentBinding.Source = continuousLoad;
            bendingMoment.SetBinding(BendingMoment.BendingMomentValueProperty, bendingMomentBinding);

            Binding spanLengthBinding = new Binding(nameof(SpanLength));
            spanLengthBinding.Source = continuousLoad;
            bendingMoment.SetBinding(BendingMoment.LengthProperty, spanLengthBinding);

            return bendingMoment;
        }

        private static UserControl SetAngledLoad(ContinuousLoad continuousLoad)
        {
            var angledLoad = new AngledLoad();

            Binding startLoadBinding = new Binding(nameof(StartValue));
            startLoadBinding.Source = continuousLoad;
            angledLoad.SetBinding(AngledLoad.StartValueProperty, startLoadBinding);

            Binding endLoadBinding = new Binding(nameof(EndValue));
            endLoadBinding.Source = continuousLoad;
            angledLoad.SetBinding(AngledLoad.EndValueProperty, endLoadBinding);

            Binding startPositionBinding = new Binding(nameof(StartPosition));
            startPositionBinding.Source = continuousLoad;
            angledLoad.SetBinding(AngledLoad.StartPositionProperty, startPositionBinding);

            Binding endPositionBinding = new Binding(nameof(EndPosition));
            endPositionBinding.Source = continuousLoad;
            angledLoad.SetBinding(AngledLoad.EndPositionProperty, endPositionBinding);

            Binding angleBinding = new Binding(nameof(Angle));
            angleBinding.Source = continuousLoad;
            angledLoad.SetBinding(AngledLoad.AngleProperty, angleBinding);

            return angledLoad;
        }

        private static UserControl SetSpanExtend(ContinuousLoad continuousLoad)
        {
            var spanExtend = new SpanExtend();

            Binding spanExtendBinding = new Binding(nameof(StartValue));
            spanExtendBinding.Source = continuousLoad;
            spanExtend.SetBinding(SpanExtend.SpanExtendValueProperty, spanExtendBinding);

            Binding spanLengthBinding = new Binding(nameof(SpanLength));
            spanLengthBinding.Source = continuousLoad;
            spanExtend.SetBinding(SpanExtend.LengthProperty, spanLengthBinding);

            return spanExtend;
        }

        private static UserControl SetTemperatureDifference(ContinuousLoad continuousLoad)
        {
            var alongTemperatureDifference = new AlongTemperatureDifference();

            Binding alongTemperatureDifferenceBinding = new Binding(nameof(StartValue));
            alongTemperatureDifferenceBinding.Source = continuousLoad;
            alongTemperatureDifference.SetBinding(AlongTemperatureDifference.AlongTemperatureDifferenceValueProperty, alongTemperatureDifferenceBinding);

            Binding spanLengthBinding = new Binding(nameof(SpanLength));
            spanLengthBinding.Source = continuousLoad;
            alongTemperatureDifference.SetBinding(AlongTemperatureDifference.LengthProperty, spanLengthBinding);

            return alongTemperatureDifference;
        }

        private static UserControl SetUpDownTemperatureDifference(ContinuousLoad continuousLoad)
        {
            var upDownTemperatureDifference = new UpDownTemperatureDifference();

            Binding upTemperatureDifferenceBinding = new Binding(nameof(EndValue));
            upTemperatureDifferenceBinding.Source = continuousLoad;
            upDownTemperatureDifference.SetBinding(UpDownTemperatureDifference.UpTemperatureValueProperty, upTemperatureDifferenceBinding);

            Binding downTemperatureDifferenceBinding = new Binding(nameof(StartValue));
            downTemperatureDifferenceBinding.Source = continuousLoad;
            upDownTemperatureDifference.SetBinding(UpDownTemperatureDifference.DownTemperatureValueProperty, downTemperatureDifferenceBinding);

            Binding spanLengthBinding = new Binding(nameof(SpanLength));
            spanLengthBinding.Source = continuousLoad;
            upDownTemperatureDifference.SetBinding(UpDownTemperatureDifference.LengthProperty, spanLengthBinding);

            return upDownTemperatureDifference;
        }

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void SetAngle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void SetPosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void SetSpanLength(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
