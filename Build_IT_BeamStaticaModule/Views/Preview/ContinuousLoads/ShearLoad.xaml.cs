using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ShearLoad.xaml
    /// </summary>
    public partial class ShearLoad : UserControl, INotifyPropertyChanged
    {
        public double ShearStartValue
        {
            get { return (double)GetValue(ShearStartValueProperty); }
            set { SetValue(ShearStartValueProperty, value); }
        }

        public static readonly DependencyProperty ShearStartValueProperty =
            DependencyProperty.Register(nameof(ShearStartValue), typeof(double), typeof(ShearLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetShearValue)));

        public double ShearEndValue
        {
            get { return (double)GetValue(ShearEndValueProperty); }
            set { SetValue(ShearEndValueProperty, value); }
        }

        public static readonly DependencyProperty ShearEndValueProperty =
            DependencyProperty.Register(nameof(ShearEndValue), typeof(double), typeof(ShearLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetShearValue)));

        public double StartPosition
        {
            get { return (double)GetValue(StartPositionProperty); }
            set { SetValue(StartPositionProperty, value); }
        }

        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register(nameof(StartPosition), typeof(double), typeof(ShearLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetPosition)));

        public double EndPosition
        {
            get { return (double)GetValue(EndPositionProperty); }
            set { SetValue(EndPositionProperty, value); }
        }

        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register(nameof(EndPosition), typeof(double), typeof(ShearLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetPosition)));

        private double _startValueY;
        public double StartValueY
        {
            get => _startValueY;
            set
            {
                _startValueY = value;
                OnPropertyChanged();
            }
        }

        private double _endValueY;
        public double EndValueY
        {
            get => _endValueY;
            set
            {
                _endValueY = value;
                OnPropertyChanged();
            }
        }

        private IList<Arrow> _arrows = new ObservableCollection<Arrow>();
        public IEnumerable<Arrow> Arrows => _arrows;

        public event PropertyChangedEventHandler PropertyChanged;

        public ShearLoad()
        {
            InitializeComponent();
        }

        private static void SetShearValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            const int defaultValue = -50;

            var shearLoad = d as ShearLoad;
            if (shearLoad == null)
                return;

            var startValue = Math.Abs(shearLoad.ShearStartValue);
            var endValue = Math.Abs(shearLoad.ShearEndValue);

            double finalStartValue;
            double finalEndValue;

            if (startValue > endValue)
            {
                finalStartValue = defaultValue;
                finalEndValue = endValue / startValue * defaultValue;
            }
            else if (startValue < endValue)
            {
                finalStartValue = startValue / endValue * defaultValue;
                finalEndValue = defaultValue;
            }
            else
                finalStartValue = finalEndValue = defaultValue;

            shearLoad.StartValueY = shearLoad.ShearStartValue > 0 ? -finalStartValue : finalStartValue;
            shearLoad.EndValueY = shearLoad.ShearEndValue > 0 ? -finalEndValue : finalEndValue;

            SetArrows(shearLoad);
        }

        private static void SetArrows(ShearLoad shearLoad)
        {
            const double space = 0.5;

            shearLoad._arrows.Clear();

            var length = shearLoad.EndPosition - shearLoad.StartPosition;

            int amount = (int)(length / space + 1 + 0.5);
            double spacing = length / amount;

            for (int i = 0; i <= amount; i++)
            {
                var position = shearLoad.StartPosition + spacing * i;
                var value = shearLoad.StartValueY + (shearLoad.EndValueY - shearLoad.StartValueY) / length * (position - shearLoad.StartPosition);
                shearLoad._arrows.Add(new Arrow(position, value, arrow => Math.Abs(arrow.Value) > 10));
            }
            shearLoad.OnPropertyChanged(nameof(Arrows));
        }

        private static void SetPosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var shearLoad = d as ShearLoad;
            if (shearLoad == null)
                return;

            SetArrows(shearLoad);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
