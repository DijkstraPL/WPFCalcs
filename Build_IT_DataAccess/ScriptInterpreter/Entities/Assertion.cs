namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class Assertion
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int TestDataId { get; set; }
        public TestData TestData { get; set; }
    }
}