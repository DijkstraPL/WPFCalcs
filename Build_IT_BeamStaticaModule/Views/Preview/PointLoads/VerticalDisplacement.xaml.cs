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
    /// Interaction logic for VerticalDisplacement.xaml
    /// </summary>
    public partial class VerticalDisplacement : UserControl
    {
        public double VerticalDisplacementValue
        {
            get { return (double)GetValue(VerticalDisplacementValueProperty); }
            set { SetValue(VerticalDisplacementValueProperty, value); }
        }

        public static readonly DependencyProperty VerticalDisplacementValueProperty =
            DependencyProperty.Register(nameof(VerticalDisplacementValue), typeof(double), typeof(VerticalDisplacement),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetVerticalDisplacementValue)));

        public VerticalDisplacement()
        {
            InitializeComponent();
        }

        private static void SetVerticalDisplacementValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var verticalDisplacement = d as VerticalDisplacement;
            if (verticalDisplacement == null)
                return;

            var scaleY = verticalDisplacement.VerticalDisplacementValue > 0 ? -1 : 1;
            verticalDisplacement.RenderTransform = new ScaleTransform(scaleX: 1, scaleY);
        }
    }
}
