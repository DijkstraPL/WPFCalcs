using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AlongTemperatureDifference.xaml
    /// </summary>
    public partial class AlongTemperatureDifference : UserControl, INotifyPropertyChanged
    {
        public double AlongTemperatureDifferenceValue
        {
            get { return (double)GetValue(AlongTemperatureDifferenceValueProperty); }
            set { SetValue(AlongTemperatureDifferenceValueProperty, value); }
        }

        public static readonly DependencyProperty AlongTemperatureDifferenceValueProperty =
            DependencyProperty.Register(nameof(AlongTemperatureDifferenceValue), typeof(double), typeof(AlongTemperatureDifference),
                new PropertyMetadata(new PropertyChangedCallback(SetAlongTemperatureDifferenceValue)));

        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(nameof(Length), typeof(double), typeof(AlongTemperatureDifference),
                new PropertyMetadata(new PropertyChangedCallback(SetLength)));

        public bool IsPositive => AlongTemperatureDifferenceValue > 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public AlongTemperatureDifference()
        {
            InitializeComponent();
        }

        private static void SetAlongTemperatureDifferenceValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var alongTemperatureDifference = d as AlongTemperatureDifference;
            if (alongTemperatureDifference == null)
                return;

            SetSymbol(alongTemperatureDifference);
        }

        private static void SetLength(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var alongTemperatureDifference = d as AlongTemperatureDifference;
            if (alongTemperatureDifference == null)
                return;

            SetSymbol(alongTemperatureDifference);
        }

        private static void SetSymbol(AlongTemperatureDifference alongTemperatureDifference)
        {
            alongTemperatureDifference.OnPropertyChanged(nameof(IsPositive));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
