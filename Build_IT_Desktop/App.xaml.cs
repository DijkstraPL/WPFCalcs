using Build_IT_BeamStaticaModule;
using Build_IT_BeamStaticaModule.Views;
using Build_IT_CalculationModule;
using Build_IT_CalculationModule.Views;
using Build_IT_Desktop.Views;
using Build_IT_Desktop.Views.Scripts;
using Build_IT_Infrastructure.Constants;
using Build_IT_Infrastructure.Data.ScriptRepository.Parameters.Queries;
using Build_IT_Infrastructure.Data.ScriptRepository.Scripts.Queries;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using System.Windows;

namespace Build_IT_Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        #region Protected_Methods
        
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<GetAllScriptsQuery>();
            containerRegistry.Register<GetAllEditableParametersForScriptQuery>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<CalculationModule>();
            moduleCatalog.AddModule<BeamStaticaModule>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();

            var scriptListView = Container.Resolve<ScriptsListView>();
            var beamView = Container.Resolve<BeamView>();
            IRegion contentRegion = regionManager.Regions[Regions.CONTENT_REGION];
            contentRegion.Add(scriptListView, ViewNames.SCRIPT_LIST_VIEW);
            contentRegion.Add(beamView, ViewNames.BEAM_VIEW);

            var headerView = Container.Resolve<HeaderView>();
            IRegion headerRegion = regionManager.Regions[Regions.HEADER_REGION];
            headerRegion.Add(headerView);

            var detailsView = Container.Resolve<ScriptFormView>();
            IRegion detailsRegion = regionManager.Regions[Regions.DETAILS_REGION];
            detailsRegion.Add(detailsView);
            
            var navigationTab = Container.Resolve<NavigationTabView>();
            IRegion navigationTabRegion = regionManager.Regions[Regions.NAVIGATION_TAB];
            navigationTabRegion.Add(navigationTab);

            contentRegion.Activate(beamView);
        }

        #endregion // Protected_Methods
    }
}
