using Build_IT_Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_CalculationModule.ViewModels.Interfaces
{
    public interface IParameterData
    {
        #region Properties

        string ParameterName { get; }
        string ParameterValue { get; set; }
        string ParameterUnit { get; }
        ParameterResource ParameterResource { get; }

        #endregion // Properties
    }
}
