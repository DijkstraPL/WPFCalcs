using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UnitGroup UnitGroup { get; set; }
        public int UnitGroupId { get; set; }
        public ICollection<UnitTranslation> UnitTranslations { get; private set; }

        public Unit()
        {
            UnitTranslations = new HashSet<UnitTranslation>();
        }
    }
}
