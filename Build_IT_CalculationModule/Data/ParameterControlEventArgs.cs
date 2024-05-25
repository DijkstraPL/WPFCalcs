using Build_IT_CalculationModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_CalculationModule.Data
{
    public class ParameterControlEventArgs : EventArgs
    {
        #region Properties
        
        public ParameterControlViewModel ParameterControlViewModel { get; }

        #endregion // Properties

        #region Constructors
        
        public ParameterControlEventArgs(ParameterControlViewModel parameterControlViewModel)
        {
            ParameterControlViewModel = parameterControlViewModel;
        }

        #endregion // Constructors
    }
}
