using Build_IT_CalculationModule.Data;
using Build_IT_CalculationModule.ViewModels.Interfaces;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_Infrastructure.Data;
using Build_IT_Infrastructure.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_CalculationModule.ViewModels
{
    public class ParameterControlViewModel : BindableBase, IParameterData
    {
        #region Properties

        public string ParameterName => ParameterResource.Name;

        public string ParameterNameMain { get; private set; }
        public string ParameterNameSubscript { get; private set; }
        public string ParameterNameSupscript { get; private set; }
        public string ParameterNameLast { get; private set; }

        public string UnitMain { get; private set; }
        public string UnitSupscript { get; private set; }

        public string ParameterValue
        {
            get => ParameterResource.Value;
            set
            {
                ParameterResource.Value = value;
                ValueChanged?.Invoke(this, new ParameterControlEventArgs(this));
                if (IsClean)
                    IsClean = false;
            }
        }
        public string ParameterUnit => ParameterResource.Unit;
        public IEnumerable<ValueOptionViewModel> ValueOptions { get; }

        public bool IsEditable => (ParameterResource.ValueOptionSetting & ValueOptionSettings.UserInput) != 0;
        public bool IsBoolean => (ParameterResource.ValueOptionSetting & ValueOptionSettings.Boolean) != 0;
        public bool ContainsValueOptions => ParameterResource.ValueOptions.Count > 0;
        public bool ShouldUseRadioButtons => ParameterResource.ValueOptions.Count < 4 &&
            ParameterResource.ValueOptions.Count > 0 &&
            !IsEditable;
        public bool IsRequired => (ParameterResource.Context & ParameterOptions.Optional) == 0;

        public ParameterResource ParameterResource { get; }
        public IEnumerable<string> FiguresAddresses { get; }


        public bool IsValueChecked
        {
            get => ParameterValue == _trueCode;
            set => ParameterValue = value ? _trueCode : _falseCode;
        }
        public bool IsDefaultValueChecked
        {
            get => string.IsNullOrWhiteSpace(ParameterValue);
            set => ParameterValue = value ? null : _falseCode;
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }
        private bool _isValid = true;
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }
        private bool _isClean = true;
        public bool IsClean
        {
            get => _isClean;
            set => SetProperty(ref _isClean, value);
        }

        #endregion // Properties

        #region Events

        public event EventHandler<ParameterControlEventArgs> ValueChanged;

        #endregion // Events

        #region Fields

        private const string _falseCode = "false";
        private const string _trueCode = "true";

        #endregion // Fields

        #region Constructors

        public ParameterControlViewModel(ParameterResource parameterResource)
        {
            ParameterResource = parameterResource ?? throw new ArgumentNullException(nameof(parameterResource));
            if (ParameterResource.Figures != null && ParameterResource.Figures.Count > 0)
                FiguresAddresses = ParameterResource.Figures.Select(f => Address.ImagesUrl + f.FileName);

            ValueOptions = ParameterResource.ValueOptions?.Select(vo => new ValueOptionViewModel(vo, this)) ?? new List<ValueOptionViewModel>();

            SetNames();
            SetUnits();
        }

        #endregion // Constructors

        #region Private_Methods

        private void SetNames()
        {
            const char subSign = '_';
            const char supSign = '^';

            int firstIndexOfSubscript = ParameterResource.Name.IndexOf(subSign);
            int lastIndexOfSubscript = ParameterResource.Name.LastIndexOf(subSign);
            int firstIndexOfSupscript = ParameterResource.Name.IndexOf(supSign);
            int lastIndexOfSupscript = ParameterResource.Name.LastIndexOf(supSign);
            int index = ParameterResource.Name.IndexOfAny(new char[] { subSign, supSign });

            ParameterNameMain = index != -1 ? ParameterResource.Name.Substring(0, index) : ParameterResource.Name;

            if (firstIndexOfSubscript != -1)
                ParameterNameSubscript = ParameterResource.Name
                    .Substring(firstIndexOfSubscript + 1, lastIndexOfSubscript - firstIndexOfSubscript - 1);
            if (firstIndexOfSupscript != -1)
                ParameterNameSupscript = ParameterResource.Name
                    .Substring(firstIndexOfSupscript + 1, lastIndexOfSupscript - firstIndexOfSupscript - 1);

            int maxIndex = Math.Max(lastIndexOfSubscript, lastIndexOfSupscript);
            if (maxIndex != -1 && maxIndex < ParameterResource.Name.Length - 1)
                ParameterNameLast = ParameterResource.Name.Substring(maxIndex + 1);
        }

        private void SetUnits()
        {
            const char subSign = '_';
            const char supSign = '^';

            int firstIndexOfSupscript = ParameterResource.Unit.IndexOf(supSign);
            int lastIndexOfSupscript = ParameterResource.Unit.LastIndexOf(supSign);
            int index = ParameterResource.Unit.IndexOfAny(new char[] { subSign, supSign });

            UnitMain = index != -1 ? ParameterResource.Unit.Substring(0, index) : ParameterResource.Unit;

            if (firstIndexOfSupscript != -1)
                UnitSupscript = ParameterResource.Unit
                    .Substring(firstIndexOfSupscript + 1, lastIndexOfSupscript - firstIndexOfSupscript - 1);
        }

        #endregion // Private_Methods
    }
}