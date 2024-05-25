using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities.Translations
{
    public  class ParameterGroupTranslation
    {
        public ParameterGroup Group { get; set; }
        public int GroupId { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }           
    }
}
