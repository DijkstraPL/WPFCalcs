namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class ValueOptionFigure
    {
        public int ValueOptionId { get; set; }
        public int FigureId { get; set; }
        public ValueOption ValueOption { get; set; }
        public Figure Figure { get; set; }
    }
}
