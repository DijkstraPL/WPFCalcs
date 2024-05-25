using Build_IT_BeamStaticaModule.ViewModels;
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

namespace Build_IT_BeamStaticaModule.Views.Loads
{
    /// <summary>
    /// Interaction logic for NodePointLoadDataView.xaml
    /// </summary>
    public partial class NodePointLoadDataView : UserControl
    {
        public NodeViewModel Node
        {
            get { return (NodeViewModel)GetValue(NodeProperty); }
            set { SetValue(NodeProperty, value); }
        }

        public static readonly DependencyProperty NodeProperty =
            DependencyProperty.Register(nameof(Node), typeof(NodeViewModel), typeof(NodePointLoadDataView), 
                new PropertyMetadata(null));
        
        public NodePointLoadDataView()
        {
            InitializeComponent();
        }
    }
}
