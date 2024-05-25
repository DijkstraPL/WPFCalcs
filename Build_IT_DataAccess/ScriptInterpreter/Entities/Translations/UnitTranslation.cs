using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities.Translations
{
    public class UnitTranslation
    {
        public Unit Unit { get; set; }
        public int UnitId { get; set; }

        public Language Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
