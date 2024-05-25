using Build_IT_CalculationModule.ViewModels;
using Build_IT_CalculationModule.Views;
using Build_IT_Infrastructure.Constants;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace Build_IT_CalculationModule
{
    public class CalculationModule : IModule 
    {
        #region Public_Methods
        
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Regions.DETAILS_REGION, typeof(ScriptFormView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ScriptFormViewModel>();
        }

        #endregion // Public_Methods
    }
}
