using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities.Translations
{
    public class ValueOptionTranslation
    {
        public ValueOption ValueOption { get; set; }
        public int ValueOptionId { get; set; }

        public Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
