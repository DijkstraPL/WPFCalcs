using Build_IT_Infrastructure.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Build_IT_Desktop.ViewModels
{
    public class NavigationTabViewModel : BindableBase
    {
        #region Properties
        
        public ICommand NavigateCommand { get; }

        #endregion // Properties

        #region Fields
        
        private readonly IRegionManager _regionManager;

        #endregion // Fields

        #region Constructors
        
        public NavigationTabViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));

            NavigateCommand = new DelegateCommand<string>(viewName => Navigate(viewName));
        }

        #endregion // Constructors

        #region Public_Methods
        
        public void Navigate(string viewName)
        {
            var contentRegion = _regionManager.Regions[Regions.CONTENT_REGION];
            var view = contentRegion.GetView(viewName);

            if (view != null)
                contentRegion.Activate(view);
        }

        #endregion // Public_Methods
    }
}
