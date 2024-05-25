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
    /// Interaction logic for AngledLoad.xaml
    /// </summary>
    public partial class AngledLoad : UserControl
    {
        public double AngledLoadValue
        {
            get { return (double)GetValue(AngledLoadValueProperty); }
            set { SetValue(AngledLoadValueProperty, value); }
        }

        public static readonly DependencyProperty AngledLoadValueProperty =
            DependencyProperty.Register(nameof(AngledLoadValue), typeof(double), typeof(AngledLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetAngledLoadValue)));

        public double AngledLoadAngle
        {
            get { return (double)GetValue(AngledLoadAngleProperty); }
            set { SetValue(AngledLoadAngleProperty, value); }
        }

        public static readonly DependencyProperty AngledLoadAngleProperty =
            DependencyProperty.Register(nameof(AngledLoadAngle), typeof(double), typeof(AngledLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetAngledLoadAngle)));

        private RotateTransform _rotateTransformFromValue = new RotateTransform(angle: 180);
        private RotateTransform _rotateTransformFromAngle = new RotateTransform(angle: 0, centerX: 0, centerY: -25);

        public AngledLoad()
        {
            InitializeComponent();

            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(_rotateTransformFromValue);
            transformGroup.Children.Add(_rotateTransformFromAngle);
            this.RenderTransform = transformGroup;
        }

        private static void SetAngledLoadValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var angledLoad = d as AngledLoad;
            if (angledLoad == null)
                return;

            var angle = angledLoad.AngledLoadValue >= 0 ? 180 : 0;
            angledLoad._rotateTransformFromValue.Angle = angle;
            angledLoad._rotateTransformFromAngle.CenterY = angledLoad.AngledLoadValue > 0 ? 25 : -25;
        }

        private static void SetAngledLoadAngle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var angledLoad = d as AngledLoad;
            if (angledLoad == null)
                return;

            var angle = (double)e.NewValue;
            angledLoad._rotateTransformFromAngle.Angle = angle;
        }
    }
}
