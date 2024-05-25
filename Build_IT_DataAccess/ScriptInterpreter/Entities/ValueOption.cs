using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class ValueOption
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public Parameter Parameter { get; set; }
        public int ParameterId { get; set; }
        public ICollection<ValueOptionTranslation> ValueOptionsTranslations { get; private set; }

        public ValueOption()
        {
            ValueOptionsTranslations = new HashSet<ValueOptionTranslation>();
        }
    }
}
