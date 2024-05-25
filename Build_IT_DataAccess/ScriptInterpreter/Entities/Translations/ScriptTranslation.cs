using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities.Translations
{
    public class ScriptTranslation
    {
        public Script Script { get; set; }
        public int ScriptId { get; set; }

        public Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}
