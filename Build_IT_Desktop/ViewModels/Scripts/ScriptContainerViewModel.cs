using Build_IT_CalculationModule.ViewModels;
using Build_IT_Infrastructure.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System.Collections.Generic;

namespace Build_IT_Desktop.ViewModels.Scripts
{
    public class ScriptContainerViewModel : BindableBase
    {
        #region Properties
        
        public string ScriptName => _scriptResource.Name;
        public string ScriptDescription => _scriptResource.Description;
        public string ScriptDocument => _scriptResource.AccordingTo;
        public IEnumerable<TagResource> Tags => _scriptResource.Tags;
        public DelegateCommand SetScriptCommand { get; }

        #endregion // Properties

        #region Fields
        
        private readonly ScriptResource _scriptResource;
        private readonly IContainerExtension _container;
        private readonly ScriptFormViewModel _scriptFormViewModel;

        #endregion // Fields

        #region Constructors
        
        public ScriptContainerViewModel(IContainerExtension container, ScriptResource scriptResource)
        {
            _scriptResource = scriptResource;
            _container = container;
            _scriptFormViewModel = _container.Resolve<ScriptFormViewModel>();

            SetScriptCommand = new DelegateCommand(SetScript);
        }

        #endregion // Constructors

        #region Private_Methods
        
        private void SetScript()
        {
            _scriptFormViewModel.SelectedScript = _scriptResource;
        }

        #endregion // Private_Methods
    }
}
