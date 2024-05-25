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
    /// Interaction logic for AngledLoad.xaml
    /// </summary>
    public partial class AngledLoad : UserControl, INotifyPropertyChanged
    {
        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register(nameof(StartValue), typeof(double), typeof(AngledLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetValue)));

        public double EndValue
        {
            get { return (double)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register(nameof(EndValue), typeof(double), typeof(AngledLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetValue)));

        public double StartPosition
        {
            get { return (double)GetValue(StartPositionProperty); }
            set { SetValue(StartPositionProperty, value); }
        }

        public static readonly DependencyProperty StartPositionProperty =
            DependencyProperty.Register(nameof(StartPosition), typeof(double), typeof(AngledLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetPosition)));

        public double EndPosition
        {
            get { return (double)GetValue(EndPositionProperty); }
            set { SetValue(EndPositionProperty, value); }
        }

        public static readonly DependencyProperty EndPositionProperty =
            DependencyProperty.Register(nameof(EndPosition), typeof(double), typeof(AngledLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetPosition)));

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(nameof(Angle), typeof(double), typeof(AngledLoad),
                new PropertyMetadata(new PropertyChangedCallback(SetAngle)));


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

        private double _lineX1;
        public double LineX1
        {
            get => _lineX1;
            set
            {
                _lineX1 = value;
                OnPropertyChanged();
            }
        }
        private double _lineX2;
        public double LineX2
        {
            get => _lineX2;
            set
            {
                _lineX2 = value;
                OnPropertyChanged();
            }
        }
        private double _lineY1;
        public double LineY1
        {
            get => _lineY1;
            set
            {
                _lineY1 = value;
                OnPropertyChanged();
            }
        }
        private double _lineY2;
        public double LineY2
        {
            get => _lineY2;
            set
            {
                _lineY2 = value;
                OnPropertyChanged();
            }
        }

        public AngledLoad()
        {
            InitializeComponent();
        }
        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            const int defaultValue = -50;

            var angledLoad = d as AngledLoad;
            if (angledLoad == null)
                return;

            var startValue = Math.Abs(angledLoad.StartValue);
            var endValue = Math.Abs(angledLoad.EndValue);

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

            angledLoad.StartValueY = angledLoad.StartValue > 0 ? -finalStartValue : finalStartValue;
            angledLoad.EndValueY = angledLoad.EndValue > 0 ? -finalEndValue : finalEndValue;

            SetArrows(angledLoad);
            SetLine(angledLoad);
        }

        private static void SetPosition(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var angledLoad = d as AngledLoad;
            if (angledLoad == null)
                return;

            SetArrows(angledLoad);
            SetLine(angledLoad);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void SetAngle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var angledLoad = d as AngledLoad;
            if (angledLoad == null)
                return;

            SetArrows(angledLoad);
            SetLine(angledLoad);
        }

        private static void SetArrows(AngledLoad angledLoad)
        {
            const double space = 0.5;

            angledLoad._arrows.Clear();

            var length = angledLoad.EndPosition - angledLoad.StartPosition;

            int amount = (int)(length / space + 1 + 0.5);
            double spacing = length / amount;

            for (int i = 0; i <= amount; i++)
            {
                var position = angledLoad.StartPosition + spacing * i;
                var value = angledLoad.StartValueY + (angledLoad.EndValueY - angledLoad.StartValueY) / length * (position - angledLoad.StartPosition);
                angledLoad._arrows.Add(new Arrow(position, value, arrow => Math.Abs(arrow.Value) > 10));
            }
            angledLoad.OnPropertyChanged(nameof(Arrows));
        }

        private static void SetLine(AngledLoad angledLoad)
        {
            var startPoint = new Point(angledLoad.StartPosition * 100, angledLoad.StartValueY);
            var endPoint = new Point(angledLoad.EndPosition * 100, angledLoad.EndValueY);
            var startCenterPoint = new Point(angledLoad.StartPosition * 100, 0);
            var endCenterPoint = new Point(angledLoad.EndPosition * 100, 0);
            
            var finalStartPoint = RotatePoint(startPoint, startCenterPoint, angledLoad.Angle);
            var finalEndPoint = RotatePoint(endPoint, endCenterPoint, angledLoad.Angle);

            angledLoad.LineX1 = finalStartPoint.X;
            angledLoad.LineY1 = finalStartPoint.Y;
            angledLoad.LineX2 = finalEndPoint.X;
            angledLoad.LineY2 = finalEndPoint.Y;
        }

        private static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }
    }
}
