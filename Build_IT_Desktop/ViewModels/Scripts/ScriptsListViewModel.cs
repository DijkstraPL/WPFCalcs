using Build_IT_Infrastructure.Data.ScriptRepository.Scripts.Queries;
using Build_IT_Infrastructure.Models;
using Prism.Ioc;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_Desktop.ViewModels.Scripts
{
    public class ScriptsListViewModel : BindableBase
    {
        #region Properties
        
        private IEnumerable<ScriptContainerViewModel> _scripts;
        public IEnumerable<ScriptContainerViewModel> Scripts
        {
            get { return _scripts; }
            set { SetProperty(ref _scripts, value); }
        }

        #endregion // Properties

        #region Fields
        
        private IContainerExtension _container;

        #endregion // Fields

        #region Constructors
        
        public ScriptsListViewModel(IContainerExtension container)
        {
            _container = container;

            var allScriptsQuery = _container.Resolve<GetAllScriptsQuery>();

            Task.Factory.StartNew(async () =>
            {
                var scripts = await allScriptsQuery.Execute();
                Scripts = new List<ScriptContainerViewModel>(scripts.Select(s =>
                    _container.Resolve<ScriptContainerViewModel>((typeof(ScriptResource), s))));
            });
        }

        #endregion // Constructors
    }
}
