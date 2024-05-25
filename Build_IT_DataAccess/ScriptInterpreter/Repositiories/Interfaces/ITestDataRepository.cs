using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.ScriptInterpreter.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces
{
    public interface ITestDataRepository : IRepository<TestData>
    {
        Task<IEnumerable<TestData>> GetAllTestDataForScriptAsync(long scriptId);
        Task<TestData> GetTestDataWithAllDependanciesAsync(long testDataId);
        void RemoveWithAllDependancies(TestData testData);
    }
}
