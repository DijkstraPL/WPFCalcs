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
    /// Interaction logic for HorizontalDisplacement.xaml
    /// </summary>
    public partial class HorizontalDisplacement : UserControl
    {
        public double HorizontalDisplacementValue
        {
            get { return (double)GetValue(HorizontalDisplacementValueProperty); }
            set { SetValue(HorizontalDisplacementValueProperty, value); }
        }

        public static readonly DependencyProperty HorizontalDisplacementValueProperty =
            DependencyProperty.Register(nameof(HorizontalDisplacementValue), typeof(double), typeof(HorizontalDisplacement),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetHorizontalDisplacementValue)));

        public HorizontalDisplacement()
        {
            InitializeComponent();
        }

        private static void SetHorizontalDisplacementValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var horizontalDisplacement = d as HorizontalDisplacement;
            if (horizontalDisplacement == null)
                return;

            var scaleX = horizontalDisplacement.HorizontalDisplacementValue > 0 ? 1 : -1;
            horizontalDisplacement.RenderTransform = new ScaleTransform(scaleX, scaleY:1);
        }
    }
}
