using Build_IT_CalculationModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Build_IT_CalculationModule.ValidationRules
{
    public class DataValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var parameterControlViewModel = (ParameterControlViewModel)value;
            return new ValidationResult(parameterControlViewModel.IsValid, parameterControlViewModel.ParameterResource.DataValidator);
        }
    }
}
