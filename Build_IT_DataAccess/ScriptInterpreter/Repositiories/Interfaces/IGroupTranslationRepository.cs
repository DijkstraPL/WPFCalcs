using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces
{
    public interface IGroupTranslationRepository
    {
        Task<IEnumerable<ParameterGroupTranslation>> GetGroupTranslations(long scriptId, Language language);
        Task<ParameterGroupTranslation> GetGroupTranslation(long id);
        Task<ParameterGroupTranslation> GetGroupTranslation(long id, Language language);
        Task AddGroupTranslationAsync(ParameterGroupTranslation groupTranslation);
        void RemoveGroupTranslation(ParameterGroupTranslation groupTranslation);
    }
}
