using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class TestData
    {
        public int Id { get; set; }
        public int ScriptId { get; set; }
        public Script Script { get; set; }
        public string Name { get; set; }
        public ICollection<TestParameter> TestParameters { get; private set; }
        public ICollection<Assertion> Assertions { get; private set; }

        public TestData()
        {
            TestParameters = new HashSet<TestParameter>();
            Assertions = new HashSet<Assertion>();
        }
    }
}
