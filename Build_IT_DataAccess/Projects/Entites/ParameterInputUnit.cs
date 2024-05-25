using Build_IT_DataAccess.ScriptInterpreter.Entities;

namespace Build_IT_DataAccess.Projects.Entites
{
    public class ParameterInputUnit
    {
        public int UnitId { get; init; }
        public virtual Unit Unit { get; init; }

        public int ParameterInputId { get; init; }
        public virtual ParameterInput ParameterInput { get; init; }

        public double Power { get; init; }
    }

}
