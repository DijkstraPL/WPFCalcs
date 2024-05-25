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
    /// Interaction logic for RotationDisplacement.xaml
    /// </summary>
    public partial class RotationDisplacement : UserControl
    {
        public double RotationDisplacementValue
        {
            get { return (double)GetValue(RotationDisplacementProperty); }
            set { SetValue(RotationDisplacementProperty, value); }
        }

        public static readonly DependencyProperty RotationDisplacementProperty =
            DependencyProperty.Register(nameof(RotationDisplacementValue), typeof(double), typeof(RotationDisplacement),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetRotationDisplacementValue)));

        public RotationDisplacement()
        {
            InitializeComponent();
        }

        private static void SetRotationDisplacementValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rotationDisplacement = d as RotationDisplacement;
            if (rotationDisplacement == null)
                return;

            var scale = rotationDisplacement.RotationDisplacementValue > 0 ? -1 : 1;
            rotationDisplacement.RenderTransform = new ScaleTransform(scaleX: scale, scaleY: -1);
        }
    }
}
