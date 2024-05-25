using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.ScriptInterpreter.Entities;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces
{
    public interface IScriptRepository : IRepository<Script>
    {
        Task<IEnumerable<Script>> GetAllScriptsWithTagsAsync();
        Task<Script> GetScriptWithTagsAsync(long id);
        Task<Script> GetScriptBaseOnNameAsync(string name);

        Task<ICollection<ScriptTranslation>> GetScriptTranslations(long id);
    }
}