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
    /// Interaction logic for ShearLoad.xaml
    /// </summary>
    public partial class ShearLoad : UserControl
    {
        public double ShearValue
        {
            get { return (double)GetValue(ShearValueProperty); }
            set { SetValue(ShearValueProperty, value); }
        }

        public static readonly DependencyProperty ShearValueProperty =
            DependencyProperty.Register(nameof(ShearValue), typeof(double), typeof(ShearLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetShearValue)));

        public ShearLoad()
        {
            InitializeComponent();
            this.RenderTransform = new RotateTransform(180);
        }

        private static void SetShearValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var shearLoad = d as ShearLoad;
            if (shearLoad == null)
                return;

            var angle = shearLoad.ShearValue >= 0 ? 180 : 0;
            shearLoad.RenderTransform = new RotateTransform(angle);
        }
    }
}
