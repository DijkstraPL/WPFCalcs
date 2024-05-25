using Prism.Commands;
using System;

namespace Build_IT_BeamStaticaModule.ViewModels.Loads
{
    public class NodePointLoadViewModel : PointLoadViewModel
    {
        #region Fields
        
        private readonly NodeViewModel _nodeViewModel;

        #endregion // Fields

        #region Constructors
        
        public NodePointLoadViewModel(NodeViewModel nodeViewModel, Action setLoad) : base(setLoad)
        {
            _nodeViewModel = nodeViewModel ?? throw new ArgumentNullException(nameof(nodeViewModel));

            RemoveCommand = new DelegateCommand(() => _nodeViewModel.RemoveLoad(this));
        }

        #endregion // Constructors
    }
}
