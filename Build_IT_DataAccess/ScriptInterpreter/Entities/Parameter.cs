using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public ValueTypes ValueType { get; set; }
        public string Value { get; set; }
        public string VisibilityValidator { get; set; }
        public string DataValidator { get; set; }
        public ValueOptionSettings ValueOptionSetting { get; set; }
        public ParameterOptions ParameterOptions { get; set; }
        public ParameterGroup ParameterGroup { get; set; }
        public int? ParameterGroupId { get; set; }
        public string AccordingTo { get; set; }
        public string Notes { get; set; }
        public Script Script { get; set; }
        public int ScriptId { get; set; }
        public ICollection<ValueOption> ValueOptions { get; private set; }
        public ICollection<ParameterFigure> ParameterFigures { get; private set; }
        public ICollection<ParameterUnit> ParameterUnits { get; private set; }
        public ICollection<ParameterTranslation> ParametersTranslations { get; private set; }

        public Parameter()
        {
            ValueOptions = new HashSet<ValueOption>();
            ParameterFigures = new HashSet<ParameterFigure>();
            ParameterUnits = new HashSet<ParameterUnit>();
            ParametersTranslations = new HashSet<ParameterTranslation>();
        }
    }
}
