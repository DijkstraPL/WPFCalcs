using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

namespace Build_IT_BeamStaticaModule.Views.Preview.Results
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl, INotifyPropertyChanged
    {
        public IReadOnlyDictionary<double, double> ResultValues
        {
            get { return (IReadOnlyDictionary<double, double>)GetValue(ResultValuesProperty); }
            set { SetValue(ResultValuesProperty, value); }
        }

        public static readonly DependencyProperty ResultValuesProperty =
            DependencyProperty.Register(nameof(ResultValues), typeof(IReadOnlyDictionary<double, double>), typeof(ResultView),
                new PropertyMetadata(new PropertyChangedCallback(RefreshResults)));

        public bool ShowResults
        {
            get { return (bool)GetValue(ShowResultsProperty); }
            set { SetValue(ShowResultsProperty, value); }
        }

        public static readonly DependencyProperty ShowResultsProperty =
            DependencyProperty.Register(nameof(ShowResults), typeof(bool), typeof(ResultView),
                new PropertyMetadata(false, CheckVisibility));

        public Brush ResultBrush
        {
            get { return (Brush)GetValue(ResultBrushProperty); }
            set { SetValue(ResultBrushProperty, value); }
        }

        public static readonly DependencyProperty ResultBrushProperty =
            DependencyProperty.Register(nameof(ResultBrush), typeof(Brush), typeof(ResultView),
                new PropertyMetadata(null));

        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register(nameof(Unit), typeof(string), typeof(ResultView),
                new PropertyMetadata(string.Empty, SetUnit));

        public bool ConnectToBeam
        {
            get { return (bool)GetValue(ConnectToBeamProperty); }
            set { SetValue(ConnectToBeamProperty, value); }
        }

        public static readonly DependencyProperty ConnectToBeamProperty =
            DependencyProperty.Register(nameof(ConnectToBeam), typeof(bool), typeof(ResultView),
                new PropertyMetadata(false, SetConnectToBeam));

        public double Multiplier
        {
            get { return (double)GetValue(MultiplierProperty); }
            set { SetValue(MultiplierProperty, value); }
        }

        public static readonly DependencyProperty MultiplierProperty =
            DependencyProperty.Register(nameof(Multiplier), typeof(double), typeof(ResultView),
                new PropertyMetadata(1.0, SetMultiplier));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register(nameof(Scale), typeof(double), typeof(ResultView),
                new PropertyMetadata(1.0, SetMultiplier));

        private PathGeometry _geometry;
        public PathGeometry Geometry
        {
            get => _geometry;
            set
            {
                _geometry = value;
                OnPropertyChanged();
            }
        }
        private IList<DetailResultView> _detailResultViews = new ObservableCollection<DetailResultView>();
        public IEnumerable<DetailResultView> DetailResultViews => _detailResultViews;

        public event PropertyChangedEventHandler PropertyChanged;

        public ResultView()
        {
            InitializeComponent();
        }

        private static void RefreshResults(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resultView = d as ResultView;
            if (resultView == null)
                return;

            var results = (IReadOnlyDictionary<double, double>)e.NewValue;
            if (results == null || results.Count == 0)
                return;

            resultView.SetResults();
        }


        private static void CheckVisibility(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resultView = d as ResultView;
            if (resultView == null)
                return;
        }

        private static void SetUnit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resultView = d as ResultView;
            if (resultView == null)
                return;

            resultView.Unit = (string)e.NewValue;
            resultView.OnPropertyChanged(nameof(Unit));
        }

        private static void SetConnectToBeam(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void SetMultiplier(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resultView = d as ResultView;
            if (resultView == null)
                return;

            resultView.SetResults();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetResults()
        {
            if (ResultValues == null)
                return;

            _detailResultViews.Clear();
            var pathSegments = new List<PathSegment>();
            int index = 0;
            foreach (var resultValue in ResultValues)
            {
                pathSegments.Add(new LineSegment(new Point(resultValue.Key * 100, resultValue.Value * Multiplier * Scale), isStroked: true));
                if (index++ % 10 == 0)
                    _detailResultViews.Add(new DetailResultView(resultValue.Key, resultValue.Value, Multiplier * Scale, this));
            }
            if (ConnectToBeam)
                pathSegments.Add(new LineSegment(new Point(ResultValues.Last().Key * 100, 0), isStroked: true));
            var firstPoint = ConnectToBeam ? new Point(0, 0) : (pathSegments.First() as LineSegment).Point;

            var pathFigure = new PathFigure(firstPoint, pathSegments, closed: false);
            var pathFigures = new List<PathFigure> { pathFigure };
            Geometry = new PathGeometry(pathFigures, FillRule.Nonzero, new ScaleTransform());

            OnPropertyChanged(nameof(DetailResultViews));
        }
    }
}
