namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class View
    {
        public int Id { get; set; }
        public int ScriptId { get; set; }
        public Script Script { get; set; }
        public string Name { get; set; }
        public string ViewDefinition { get; set; }
    }
}
