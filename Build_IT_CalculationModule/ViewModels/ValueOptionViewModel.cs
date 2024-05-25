using Build_IT_CalculationModule.ViewModels.Interfaces;
using Build_IT_Infrastructure.Models;
using System;

namespace Build_IT_CalculationModule.ViewModels
{
    public class ValueOptionViewModel
    {
        #region Properties
        
        public string Name => _valueOptionResource.Name;
        public string Value => _valueOptionResource.Value;

        public bool IsOptionChecked
        {
            get => _parameterData.ParameterValue == Value;
            set
            {
                if (value)
                    _parameterData.ParameterValue = Value;
            }
        }

        public string ParameterName => _parameterData.ParameterName;

        #endregion // Properties

        #region Fields

        private readonly ValueOptionResource _valueOptionResource;
        private readonly IParameterData _parameterData;

        #endregion // Fields

        #region Constructors

        public ValueOptionViewModel(ValueOptionResource valueOptionResource, IParameterData parameterData)
        {
            _valueOptionResource = valueOptionResource ?? throw new ArgumentNullException(nameof(valueOptionResource));
            _parameterData = parameterData ?? throw new ArgumentNullException(nameof(parameterData));
        }

        #endregion // Constructors
    }
}