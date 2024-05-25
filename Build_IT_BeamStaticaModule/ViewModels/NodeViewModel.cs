using Build_IT_BeamStaticaModule.Utils;
using Build_IT_BeamStaticaModule.Utils.Enums;
using Build_IT_BeamStaticaModule.ViewModels.Loads;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Build_IT_BeamStaticaModule.ViewModels
{
    public class NodeViewModel : BindableBase
    {
        #region Properties

        public SpanViewModel SpanViewModel { get; set; }

        private NodeType _node = NodeTypes.Types[NodeTypes.DefaultNode];
        public NodeType Type
        {
            get => _node;
            set
            {
                _node = value;
                _setNode?.Invoke();
                RaisePropertyChanged(nameof(HasAngle));
            }
        }

        public bool HasAngle => Type == NodeType.Pin;

        private double _angle;
        public double Angle
        {
            get => HasAngle ? _angle : 0;
            set
            {
                if (HasAngle)
                {
                    _angle = value;
                    _setNode?.Invoke();
                }
            }
        }

        public string NodeText
        {
            get => NodeTypes.Types.FirstOrDefault(t => t.Value == Type).Key;
            set => Type = NodeTypes.Types[value];
        }

        private IList<NodePointLoadViewModel> _pointLoadViewModels = new ObservableCollection<NodePointLoadViewModel>();
        public IEnumerable<NodePointLoadViewModel> PointLoads => _pointLoadViewModels;

        public ICommand AddLoadCommand { get; }

        #endregion // Properties

        #region Fields

        private readonly Action _setNode;

        #endregion // Fields

        #region Constructors

        public NodeViewModel(SpanViewModel spanViewModel, Action setNode)
        {
            AddLoadCommand = new DelegateCommand(() => AddLoad());
            SpanViewModel = spanViewModel ?? throw new ArgumentNullException(nameof(spanViewModel));
            _setNode = setNode;
        }

        #endregion // Constructors

        #region Internal_Methods

        internal void RemoveLoad(NodePointLoadViewModel pointLoadViewModel)
        {
            _pointLoadViewModels.Remove(pointLoadViewModel);
            _setNode?.Invoke();
        }

        #endregion // Internal_Methods

        #region Private_Methods

        private void AddLoad()
        {
            _pointLoadViewModels.Add(new NodePointLoadViewModel(this, _setNode));
        }

        #endregion // Private_Methods
    }
}
