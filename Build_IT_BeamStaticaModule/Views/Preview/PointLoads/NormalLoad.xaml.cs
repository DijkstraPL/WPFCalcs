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
    /// Interaction logic for NormalLoad.xaml
    /// </summary>
    public partial class NormalLoad : UserControl
    {
        public double NormalLoadValue
        {
            get { return (double)GetValue(NormalLoadValueProperty); }
            set { SetValue(NormalLoadValueProperty, value); }
        }

        public static readonly DependencyProperty NormalLoadValueProperty =
            DependencyProperty.Register(nameof(NormalLoadValue), typeof(double), typeof(NormalLoad),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetNormalLoadValue)));

        public NormalLoad()
        {
            InitializeComponent();
        }

        private static void SetNormalLoadValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var normalLoad = d as NormalLoad;
            if (normalLoad == null)
                return;

            var scaleX = normalLoad.NormalLoadValue > 0 ? 1 : -1;
            normalLoad.RenderTransform = new ScaleTransform(scaleX, scaleY: 1);
        }
    }
}
