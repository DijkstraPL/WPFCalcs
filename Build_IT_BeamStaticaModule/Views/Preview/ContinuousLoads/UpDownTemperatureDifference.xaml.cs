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
    /// Interaction logic for UpDownTemperatureDifference.xaml
    /// </summary>
    public partial class UpDownTemperatureDifference : UserControl, INotifyPropertyChanged
    {
        public double UpTemperatureValue
        {
            get { return (double)GetValue(UpTemperatureValueProperty); }
            set { SetValue(UpTemperatureValueProperty, value); }
        }

        public static readonly DependencyProperty UpTemperatureValueProperty =
            DependencyProperty.Register(nameof(UpTemperatureValue), typeof(double), typeof(UpDownTemperatureDifference),
                new PropertyMetadata(new PropertyChangedCallback(SetTemperatureDifferenceValue)));

        public double DownTemperatureValue
        {
            get { return (double)GetValue(DownTemperatureValueProperty); }
            set { SetValue(DownTemperatureValueProperty, value); }
        }

        public static readonly DependencyProperty DownTemperatureValueProperty =
            DependencyProperty.Register(nameof(DownTemperatureValue), typeof(double), typeof(UpDownTemperatureDifference),
                new PropertyMetadata(new PropertyChangedCallback(SetTemperatureDifferenceValue)));

        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(nameof(Length), typeof(double), typeof(UpDownTemperatureDifference),
                new PropertyMetadata(new PropertyChangedCallback(SetLength)));

        public bool IsPositive => UpTemperatureValue - DownTemperatureValue > 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public UpDownTemperatureDifference()
        {
            InitializeComponent();
        }

        private static void SetTemperatureDifferenceValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var upDownTemperatureDifference = d as UpDownTemperatureDifference;
            if (upDownTemperatureDifference == null)
                return;

            SetSymbol(upDownTemperatureDifference);
        }

        private static void SetLength(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var upDownTemperatureDifference = d as UpDownTemperatureDifference;
            if (upDownTemperatureDifference == null)
                return;

            SetSymbol(upDownTemperatureDifference);
        }

        private static void SetSymbol(UpDownTemperatureDifference upDownTemperatureDifference)
        {
            upDownTemperatureDifference.OnPropertyChanged(nameof(IsPositive));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
