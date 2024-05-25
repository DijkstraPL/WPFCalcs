using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities.Translations
{
    public class ParameterTranslation
    {
        public Parameter Parameter { get; set; }
        public int ParameterId { get; set; }

        public Language Language { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}
