using System;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities.Enums
{
    [Flags]
    public enum ParameterOptions
    {
        None = 0,
        Visible = 1,
        Editable = 2,
        Calculation = 4,
        StaticData = 8,
        Important = 16,
        Optional = 32,
    }
}
