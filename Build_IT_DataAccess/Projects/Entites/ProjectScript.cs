using Build_IT_DataAccess.ScriptInterpreter.Entities;

namespace Build_IT_DataAccess.Projects.Entites
{
    public sealed class ProjectScript
    {
        public int ScriptId { get; init; }
        public Script Script { get; init; }
        public int ProjectId { get; init; }
        public Project Project { get; init; }
    }

}
