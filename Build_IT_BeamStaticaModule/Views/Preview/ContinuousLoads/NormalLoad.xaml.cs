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
using static Build_IT_BeamStaticaModule.Views.Preview.ContinuousLoads.ShearLoad;

namespace Build_IT_BeamStaticaModule.Views.Preview.ContinuousLoads
{
    /// <summary>
    /// Interaction logic for NormalLoad.xaml
    /// </summary>
    public partial class NormalLoad : UserControl, INotifyPropertyChanged
    {
        public double NormalStartValue
        {
            get { return (double)GetValue(NormalStartValueProperty); }
            set { SetValue(NormalStartValueProperty, value); }
        }

        public static readonly DependencyProperty NormalStartValueProperty =
            DependencyProperty.Register(nameof(NormalStartValue), typeof(double), typeof(NormalLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetNormalValue)));

        public double NormalEndValue
        {
            get { return (double)GetValue(NormalEndValueProperty); }
            set { SetValue(NormalEndValueProperty, value); }
        }

        public static readonly DependencyProperty NormalEndValueProperty =
            DependencyProperty.Register(nameof(NormalEndValue), typeof(double), typeof(NormalLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetNormalValue)));

        public double StartPosition
        {
            get { return (double)GetValue(StartPositionProperty); }
            set { SetValue(StartPositionProperty, value); }
        }

        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register(nameof(StartPosition), typeof(double), typeof(NormalLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetPosition)));

        public double EndPosition
        {
            get { return (double)GetValue(EndPositionProperty); }
            set { SetValue(EndPositionProperty, value); }
        }

        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register(nameof(EndPosition), typeof(double), typeof(NormalLoad),
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

        public NormalLoad()
        {
            InitializeComponent();
        }

        private static void SetNormalValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            const int defaultValue = -20;

            var normalLoad = d as NormalLoad;
            if (normalLoad == null)
                return;

            var startValue = Math.Abs(normalLoad.NormalStartValue);
            var endValue = Math.Abs(normalLoad.NormalEndValue);

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

            normalLoad.StartValueY = normalLoad.NormalStartValue > 0 ? -finalStartValue : finalStartValue;
            normalLoad.EndValueY = normalLoad.NormalEndValue > 0 ? -finalEndValue : finalEndValue;

            SetArrows(normalLoad);
        }

        private static void SetArrows(NormalLoad normalLoad)
        {
            const double space = 0.5;

            normalLoad._arrows.Clear();

            var length = normalLoad.EndPosition - normalLoad.StartPosition - 0.2;

            int amount = (int)(length / space + 1 + 0.5);
            double spacing = length / amount;

            for (int i = 0; i <= amount; i++)
            {
                var position = normalLoad.StartPosition + spacing * i;
                var value = normalLoad.StartValueY + (normalLoad.EndValueY - normalLoad.StartValueY) / length * (position - normalLoad.StartPosition);
                if (value > 0)
                    position += 0.2;
                normalLoad._arrows.Add(new Arrow(position, value, arrow => arrow.Value != 0 ));
            }
            normalLoad.OnPropertyChanged(nameof(Arrows));
        }

        private static void SetPosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var normalLoad = d as NormalLoad;
            if (normalLoad == null)
                return;

            SetArrows(normalLoad);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
