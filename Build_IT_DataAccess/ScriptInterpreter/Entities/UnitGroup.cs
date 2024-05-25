using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class UnitGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UnitGroupTranslation> UnitGroupTranslations { get; private set; }

        public UnitGroup()
        {
            UnitGroupTranslations = new HashSet<UnitGroupTranslation>();
        }
    }
}
