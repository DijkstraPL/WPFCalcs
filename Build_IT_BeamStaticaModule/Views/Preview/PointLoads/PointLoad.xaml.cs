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

namespace Build_IT_BeamStaticaModule.Views.Preview.PointLoads
{
    /// <summary>
    /// Interaction logic for PointLoad.xaml
    /// </summary>
    public partial class PointLoad : UserControl
    {
        public PointLoadType PointLoadType
        {
            get { return (PointLoadType)GetValue(PointLoadTypeProperty); }
            set { SetValue(PointLoadTypeProperty, value); }
        }

        public static readonly DependencyProperty PointLoadTypeProperty =
            DependencyProperty.Register(nameof(PointLoadType), typeof(PointLoadType), typeof(PointLoad),
                new PropertyMetadata(PointLoadTypes.Types[PointLoadTypes.DefaultPointLoad], new PropertyChangedCallback(SetPointLoad)));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(PointLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetValue)));
        
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(nameof(Angle), typeof(double), typeof(PointLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetAngle)));
        
        private UserControl _pointLoadPicture;

        public PointLoad()
        {
            InitializeComponent();
            _pointLoadPicture = GetPointLoad(PointLoadType, this);
            pointLoadPicture.Content = _pointLoadPicture;
        }

        private static void SetPointLoad(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pointLoad = d as PointLoad;
            if (pointLoad == null)
                return;
            pointLoad._pointLoadPicture = GetPointLoad((PointLoadType)e.NewValue, pointLoad);

            pointLoad.pointLoadPicture.Content = pointLoad._pointLoadPicture;
        }

        private static UserControl GetPointLoad(PointLoadType pointLoadType, PointLoad pointLoad)
        {
            switch (pointLoadType)
            {
                case PointLoadType.ShearLoad:
                    return SetShearLoad(pointLoad);
                case PointLoadType.NormalLoad:
                    return SetNormalLoad(pointLoad);
                case PointLoadType.BendingMoment:
                    return SetBendingMoment(pointLoad);
                case PointLoadType.HorizontalDisplacement:
                    return SetHorizontalDisplacement(pointLoad);
                case PointLoadType.VerticalDisplacement:
                    return SetVerticalDisplacement(pointLoad);
                case PointLoadType.RotationDisplacement:
                    return SetRotationDisplacement(pointLoad);
                case PointLoadType.AngledLoad:
                    return SetAngledLoad(pointLoad);
                default:
                       throw new NotImplementedException(pointLoadType.ToString());
            }
        }

        private static ShearLoad SetShearLoad(PointLoad pointLoad)
        {
            var shearLoad = new ShearLoad();
            Binding shearLoadBinding = new Binding(nameof(Value));
            shearLoadBinding.Source = pointLoad;
            shearLoad.SetBinding(ShearLoad.ShearValueProperty, shearLoadBinding);
            return shearLoad;
        }
        private static NormalLoad SetNormalLoad(PointLoad pointLoad)
        {
            var normalLoad = new NormalLoad();
            Binding normalLoadBinding = new Binding(nameof(Value));
            normalLoadBinding.Source = pointLoad;
            normalLoad.SetBinding(NormalLoad.NormalLoadValueProperty, normalLoadBinding);
            return normalLoad;
        }
        private static BendingMoment SetBendingMoment(PointLoad pointLoad)
        {
            var bendingMoment = new BendingMoment();
            Binding bendingMomentBinding = new Binding(nameof(Value));
            bendingMomentBinding.Source = pointLoad;
            bendingMoment.SetBinding(BendingMoment.BendingMomentProperty, bendingMomentBinding);
            return bendingMoment;
        }
        private static HorizontalDisplacement SetHorizontalDisplacement(PointLoad pointLoad)
        {
            var horizontalDisplacement = new HorizontalDisplacement();
            Binding horizontalDisplacementBinding = new Binding(nameof(Value));
            horizontalDisplacementBinding.Source = pointLoad;
            horizontalDisplacement.SetBinding(HorizontalDisplacement.HorizontalDisplacementValueProperty, horizontalDisplacementBinding);
            return horizontalDisplacement;
        }
        private static VerticalDisplacement SetVerticalDisplacement(PointLoad pointLoad)
        {
            var verticalDisplacement = new VerticalDisplacement();
            Binding verticalDisplacementBinding = new Binding(nameof(Value));
            verticalDisplacementBinding.Source = pointLoad;
            verticalDisplacement.SetBinding(VerticalDisplacement.VerticalDisplacementValueProperty, verticalDisplacementBinding);
            return verticalDisplacement;
        }
        private static RotationDisplacement SetRotationDisplacement(PointLoad pointLoad)
        {
            var rotationDisplacement = new RotationDisplacement();
            Binding rotationDisplacementBinding = new Binding(nameof(Value));
            rotationDisplacementBinding.Source = pointLoad;
            rotationDisplacement.SetBinding(RotationDisplacement.RotationDisplacementProperty, rotationDisplacementBinding);
            return rotationDisplacement;
        }
        private static AngledLoad SetAngledLoad(PointLoad pointLoad)
        {
            var angledLoad = new AngledLoad();
            Binding angledLoadValueBinding = new Binding(nameof(Value));
            angledLoadValueBinding.Source = pointLoad;
            angledLoad.SetBinding(AngledLoad.AngledLoadValueProperty, angledLoadValueBinding);


            Binding angledLoadAngleBinding = new Binding(nameof(Angle));
            angledLoadAngleBinding.Source = pointLoad;
            angledLoad.SetBinding(AngledLoad.AngledLoadAngleProperty, angledLoadAngleBinding);

            return angledLoad;
        }

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        private static void SetAngle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
