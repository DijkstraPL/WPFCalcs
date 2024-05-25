using Build_IT_BeamStaticaModule.Utils;
using Build_IT_BeamStaticaModule.Utils.Enums;
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

namespace Build_IT_BeamStaticaModule.Views.Preview.Nodes
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public NodeType NodeType
        {
            get { return (NodeType)GetValue(NodeTypeProperty); }
            set { SetValue(NodeTypeProperty, value); }
        }

        public static readonly DependencyProperty NodeTypeProperty =
            DependencyProperty.Register(nameof(NodeType), typeof(NodeType), typeof(Node), 
                new PropertyMetadata(NodeTypes.Types[NodeTypes.DefaultNode], new PropertyChangedCallback(SetNode)));

        public Side Side
        {
            get { return (Side)GetValue(SideProperty); }
            set { SetValue(SideProperty, value); }
        }

        public static readonly DependencyProperty SideProperty =
            DependencyProperty.Register(nameof(Side), typeof(Side), typeof(Node),
                new PropertyMetadata(Side.Middle, new PropertyChangedCallback(SetSide)));

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(nameof(Angle), typeof(double), typeof(Node),
                new PropertyMetadata(0.0, new PropertyChangedCallback(SetAngle)));


        private UserControl _nodePicture;

        public Node()
        {
            InitializeComponent();
        }

        private static void SetNode(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var node = d as Node;
            if(node == null)
                return;

            switch (e.NewValue)
            {
                case NodeType.Hinge:
                    node._nodePicture = new Hinge();
                    break;
                case NodeType.Pin:
                    node._nodePicture = new Pin();
                    break;
                case NodeType.Fixed:
                    node._nodePicture = new Fixed();
                    break;
                case NodeType.Sleeve:
                    node._nodePicture = new Sleeve();
                    break;
                case NodeType.Support:
                    node._nodePicture = new Support();
                    break;
                case NodeType.SupportWithHinge:
                    node._nodePicture = new SupportWithHinge();
                    break;
                case NodeType.Telescope:
                    node._nodePicture = new Telescope();
                    break;
                case NodeType.Free:
                    node._nodePicture = null;
                    break;
                default:
                    throw new NotImplementedException(e.NewValue.ToString());
            }

            node.nodePicture.Content = node._nodePicture;
        }
        private static void SetSide(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var node = d as Node;
            if (node == null)
                return;

            node.Side = (Side)e.NewValue;
        }

        private static void SetAngle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var node = d as Node;
            if (node == null)
                return;

            node.Angle = (double)e.NewValue;
        }
    }
}
