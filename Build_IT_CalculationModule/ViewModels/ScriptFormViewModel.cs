using Build_IT_CalculationModule.Data;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_Infrastructure.Data.ScriptRepository.Calculators.Queries;
using Build_IT_Infrastructure.Data.ScriptRepository.Parameters.Queries;
using Build_IT_Infrastructure.Models;
using NCalc;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Build_IT_CalculationModule.ViewModels
{
    public class ScriptFormViewModel : BindableBase
    {
        #region Properties

        private ScriptResource _selectedScript;
        public ScriptResource SelectedScript
        {
            get => _selectedScript;
            set
            {
                SetProperty(ref _selectedScript, value);
                ScriptChanged(this, EventArgs.Empty);
            }
        }

        private IEnumerable<ParameterControlViewModel> _parameterViewModels;
        public IEnumerable<ParameterControlViewModel> ParameterViewModels
        {
            get { return _parameterViewModels; }
            set
            {
                SetProperty(ref _parameterViewModels, value);
                CalculateCommand.RaiseCanExecuteChanged();
            }
        }

        private IEnumerable<ParameterResource> _calculatedParameters;
        public IEnumerable<ParameterResource> CalculatedParameters
        {
            get { return _calculatedParameters; }
            set { SetProperty(ref _calculatedParameters, value); }
        }

        public DelegateCommand CalculateCommand { get; }

        #endregion // Properties

        #region Fields

        private IContainerExtension _container;

        #endregion // Fields

        #region Events

        public event Func<object, EventArgs, Task> ScriptChanged;

        #endregion // Events

        #region Constructors

        public ScriptFormViewModel(IContainerExtension container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));

            CalculateCommand = new DelegateCommand(async () => await Calculate(),
                () => ValidateCalculation());

            ScriptChanged += (s, e) => SetParameters();
        }

        #endregion // Constructors

        #region Private_Methods

        private async Task SetParameters()
        {
            CalculatedParameters = null;

            var getAllParametersForScriptQuery = _container.Resolve<GetAllEditableParametersForScriptQuery>((typeof(long), SelectedScript.Id));
            var parameters = await getAllParametersForScriptQuery.Execute();
            ParameterViewModels = new List<ParameterControlViewModel>(parameters.Select(async p =>
                 await SetupParameterControlViewModel(p)).Select(p => p.Result));
            
            CheckVisibility();
        }

        private async Task<ParameterControlViewModel> SetupParameterControlViewModel(ParameterResource parameterResource)
        {
            var parameterControlViewModel = _container.Resolve<ParameterControlViewModel>(
                (typeof(ParameterResource), parameterResource));
            parameterControlViewModel.ValueChanged += OnParameterValueChanged;
            return parameterControlViewModel;
        }

        private void OnParameterValueChanged(object sender, ParameterControlEventArgs e)
        {
            CheckVisibility();
            CheckData();
            CalculateCommand.RaiseCanExecuteChanged();
        }

        private void CheckVisibility()
        {
            var parameters = new Dictionary<string, object>();

            foreach (var parameter in ParameterViewModels)
            {
                AddParameter(parameters, parameter);
                if (string.IsNullOrWhiteSpace(parameter.ParameterResource.VisibilityValidator))
                    continue;

                try
                {
                    var expression = new Expression(parameter.ParameterResource.VisibilityValidator,
                               EvaluateOptions.IgnoreCase & EvaluateOptions.BooleanCalculation);

                    expression.Parameters = parameters;

                    var result = (bool)expression.Evaluate();
                    if (parameter.IsVisible != result)
                        parameter.IsVisible = result;
                }
                catch
                {
                    if (!parameter.IsVisible)
                        parameter.IsVisible = true;
                }
            }
        }
        private void CheckData()
        {
            var parameters = new Dictionary<string, object>();

            foreach (var parameter in ParameterViewModels)
            {
                AddParameter(parameters, parameter);
                if (string.IsNullOrWhiteSpace(parameter.ParameterResource.DataValidator))
                    continue;

                try
                {
                    var expression = new Expression(parameter.ParameterResource.DataValidator,
                               EvaluateOptions.IgnoreCase);

                    expression.Parameters = parameters;

                    var result = (bool)expression.Evaluate();
                    if (parameter.IsValid != result)
                        parameter.IsValid = result;
                }
                catch
                {
                    if (!parameter.IsValid)
                        parameter.IsValid = true;
                }
            }
        }

        private void AddParameter(Dictionary<string, object> parameters, ParameterControlViewModel parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter.ParameterValue) && parameter.ParameterResource.ValueType == ValueTypes.Number)
                return;

            if (parameter.ParameterResource.ValueType == ValueTypes.Number)                
                parameters.Add(parameter.ParameterName, Convert.ToDouble(parameter.ParameterValue, CultureInfo.InvariantCulture));
            else
                parameters.Add(parameter.ParameterName, parameter.ParameterValue);
        }

        private async Task Calculate()
        {
            var parameters = ParameterViewModels.Select(pvm => pvm.ParameterResource).ToList();
            var calculateQuery = _container.Resolve<CalculateQuery>(
                (typeof(long), _selectedScript.Id), (typeof(List<ParameterResource>), parameters));

            CalculatedParameters = await calculateQuery.Execute();
        }

        private bool ValidateCalculation()
        {
            if (ParameterViewModels == null)
                return false;
            return ParameterViewModels.All(pvm => pvm.IsValid &&
                (pvm.IsRequired ? !string.IsNullOrWhiteSpace(pvm.ParameterValue) : true) || 
                !pvm.IsVisible);
        }

        #endregion // Private_Methods
    }
}
