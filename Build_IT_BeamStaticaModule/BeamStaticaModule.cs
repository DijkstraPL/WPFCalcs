using Build_IT_BeamStaticaModule.Utils;
using Build_IT_BeamStaticaModule.ViewModels;
using Build_IT_BeamStaticaModule.ViewModels.Interfaces;
using Build_IT_BeamStaticaModule.Views;
using Build_IT_BeamStaticaModule.Views.Preview;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStaticaModule
{
    public class BeamStaticaModule : IModule
    {
        #region Public_Methods

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Regions.SIDE_PANEL, typeof(SidePanelView));
            regionManager.RegisterViewWithRegion(Regions.SPAN_LIST, typeof(SpanListView));
            regionManager.RegisterViewWithRegion(Regions.SPAN_DETAILS, typeof(SpanDetailView));
            regionManager.RegisterViewWithRegion(Regions.BEAM_PREVIEW, typeof(BeamPreviewView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISpanListViewModel, SpanListViewModel>();
        }

        #endregion // Public_Methods
    }
}
