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
    /// Interaction logic for SpanExtend.xaml
    /// </summary>
    public partial class SpanExtend : UserControl, INotifyPropertyChanged
    {
        public double SpanExtendValue
        {
            get { return (double)GetValue(SpanExtendValueProperty); }
            set { SetValue(SpanExtendValueProperty, value); }
        }

        public static readonly DependencyProperty SpanExtendValueProperty =
            DependencyProperty.Register(nameof(SpanExtendValue), typeof(double), typeof(SpanExtend),
                new PropertyMetadata(new PropertyChangedCallback(SetSpanExtendValue)));

        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(nameof(Length), typeof(double), typeof(SpanExtend),
                new PropertyMetadata(new PropertyChangedCallback(SetLength)));

        public bool IsPositive => SpanExtendValue > 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public SpanExtend()
        {
            InitializeComponent();
        }

        private static void SetSpanExtendValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spanExtend = d as SpanExtend;
            if (spanExtend == null)
                return;

            SetArrows(spanExtend);
        }

        private static void SetLength(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spanExtend = d as SpanExtend;
            if (spanExtend == null)
                return;

            SetArrows(spanExtend);
        }

        private static void SetArrows(SpanExtend spanExtend)
        {
            spanExtend.OnPropertyChanged(nameof(IsPositive));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
