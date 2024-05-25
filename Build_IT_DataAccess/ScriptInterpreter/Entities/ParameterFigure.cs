namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class ParameterFigure
    {
        public int ParameterId { get; set; }
        public int FigureId { get; set; }
        public Parameter Parameter { get; set; }
        public Figure Figure { get; set; }
    }
}
