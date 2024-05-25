using Build_IT_DataAccess.ScriptInterpreter.Entities;

namespace Build_IT_DataAccess.Projects.Entites
{
    public class ParameterInput
    {
        public int ScriptDatatId { get; init; }
        public virtual ScriptData ScriptData { get; init; }

        public int ParameterId { get; init; }
        public virtual Parameter Parameter { get; init; }

        public string Value { get; init; }
    }

}
