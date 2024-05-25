namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class ScriptFigure
    {
        public int ScriptId { get; set; }
        public int FigureId { get; set; }
        public Script Script { get; set; }
        public Figure Figure { get; set; }
    }
}
