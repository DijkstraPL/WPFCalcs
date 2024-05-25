using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces
{
    public interface IScriptTranslationRepository
    {
        Task<IEnumerable<ScriptTranslation>> GetScriptTranslations(long scriptId);
        Task<ScriptTranslation> GetScriptTranslation(long scriptId, Language language);
        Task AddScriptTranslationAsync(ScriptTranslation scriptTranslation);
        void RemoveScriptTranslation(ScriptTranslation scriptTranslation);
    }
}
