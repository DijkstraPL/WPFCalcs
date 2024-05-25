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
    /// Interaction logic for BendingMoment.xaml
    /// </summary>
    public partial class BendingMoment : UserControl
    {
        public double BendingMomentValue
        {
            get { return (double)GetValue(BendingMomentProperty); }
            set { SetValue(BendingMomentProperty, value); }
        }

        public static readonly DependencyProperty BendingMomentProperty =
            DependencyProperty.Register(nameof(BendingMomentValue), typeof(double), typeof(BendingMoment),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetBendingMomentValue)));

        public BendingMoment()
        {
            InitializeComponent();
        }

        private static void SetBendingMomentValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bendingMoment = d as BendingMoment;
            if (bendingMoment == null)
                return;

            var scale = bendingMoment.BendingMomentValue >= 0 ? -1 : 1;
            bendingMoment.RenderTransform = new ScaleTransform(scaleX: scale, scaleY: -1);
        }
    }
}
