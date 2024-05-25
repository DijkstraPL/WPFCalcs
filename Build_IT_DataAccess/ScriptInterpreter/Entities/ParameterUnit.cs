namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public sealed class ParameterUnit
    {
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public Parameter Parameter { get; set; }
        public int ParameterId { get; set; }
        public double Power { get; set; }
    }
}
