using System.Collections.Generic;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class ParameterGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VisibilityValidator { get; set; }
        public ICollection<Parameter> Parameters { get; private set; }
        public int ScriptId { get; set; }
        public Script Script { get; set; }

        public ParameterGroup()
        {
            Parameters = new HashSet<Parameter>();
        }
    }
}
