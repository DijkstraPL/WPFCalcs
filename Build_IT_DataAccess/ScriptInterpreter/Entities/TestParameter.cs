using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class TestParameter
    {
        public int ParameterUnitId { get; set; }
        public ParameterUnit ParameterUnit { get; set; }

        public int TestDataId { get; set; }
        public TestData TestData { get; set; }

        public string Value { get; set; }
    }
}
