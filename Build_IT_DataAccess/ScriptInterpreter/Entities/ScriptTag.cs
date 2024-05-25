namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class ScriptTag
    {        
        public int ScriptId { get; set; }
        public int TagId { get; set; }
        public Script Script { get; set; }
        public Tag Tag { get; set; }
    }
}
