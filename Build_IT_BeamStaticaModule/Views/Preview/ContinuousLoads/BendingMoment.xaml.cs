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
    /// Interaction logic for BendingMoment.xaml
    /// </summary>
    public partial class BendingMoment : UserControl, INotifyPropertyChanged
    {
        public double BendingMomentValue
        {
            get { return (double)GetValue(BendingMomentValueProperty); }
            set { SetValue(BendingMomentValueProperty, value); }
        }

        public static readonly DependencyProperty BendingMomentValueProperty =
            DependencyProperty.Register(nameof(BendingMomentValue), typeof(double), typeof(BendingMoment),
                new PropertyMetadata(new PropertyChangedCallback(SetBendingMomentValue)));
        
        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register(nameof(Length), typeof(double), typeof(BendingMoment),
                new PropertyMetadata(new PropertyChangedCallback(SetLength)));

        private IList<Arrow> _arrows = new ObservableCollection<Arrow>();
        public IEnumerable<Arrow> Arrows => _arrows;

        public event PropertyChangedEventHandler PropertyChanged;

        public BendingMoment()
        {
            InitializeComponent();
        }

        private static void SetBendingMomentValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bendingMoment = d as BendingMoment;
            if (bendingMoment == null)
                return;

            SetArrows(bendingMoment);
        }

        private static void SetLength(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bendingMoment = d as BendingMoment;
            if (bendingMoment == null)
                return;

            SetArrows(bendingMoment);
        }

        private static void SetArrows(BendingMoment bendingMoment)
        {
            const double space = 0.5;

            bendingMoment._arrows.Clear();

            double length = bendingMoment.Length;

            int amount = (int)(length / space + 1 + 0.5);
            double spacing = length / amount;

            for (int i = 0; i <= amount; i++)
            {
                var position = spacing * i;
                bendingMoment._arrows.Add(new Arrow(position, bendingMoment.BendingMomentValue, arrow => arrow.Value != 0));
            }
            bendingMoment.OnPropertyChanged(nameof(Arrows));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
