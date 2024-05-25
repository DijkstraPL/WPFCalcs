using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class ScriptGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ScriptGroupTranslation> ScriptGroupTranslations { get; private set; }

        public ScriptGroup()
        {
            ScriptGroupTranslations = new HashSet<ScriptGroupTranslation>();
        }
    }
}
