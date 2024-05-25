using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.ScriptInterpreter.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces
{
    public interface IParameterRepository : IRepository<Parameter>
    {
        Task<IEnumerable<Parameter>> GetAllParametersForScriptAsync(long scriptId);
        Task<Parameter> GetParameterWithAllDependanciesAsync(long parameterId);
        Task<IEnumerable<Figure>> GetFiguresAsync(long parameterId);
    }
}
