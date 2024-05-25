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

namespace Build_IT_BeamStaticaModule.Views
{
    /// <summary>
    /// Interaction logic for NodeDataView.xaml
    /// </summary>
    public partial class NodeDataView : UserControl
    {
        public NodeViewModel Node
        {
            get { return (NodeViewModel)GetValue(NodeProperty); }
            set { SetValue(NodeProperty, value); }
        }

        public static readonly DependencyProperty NodeProperty =
            DependencyProperty.Register(nameof(Node), typeof(NodeViewModel), typeof(NodeDataView),
                new PropertyMetadata(null));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), typeof(NodeDataView),
                new PropertyMetadata(null));

        public NodeDataView()
        {
            InitializeComponent();
        }
    }
}
