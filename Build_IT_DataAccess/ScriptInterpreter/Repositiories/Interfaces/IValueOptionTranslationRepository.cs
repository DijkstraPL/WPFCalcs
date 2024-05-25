using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces
{
    public interface IValueOptionTranslationRepository
    {
        Task<IEnumerable<ValueOptionTranslation>> GetValueOptionsTranslations(long parameterId, Language language);
        Task<ValueOptionTranslation> GetValueOptionTranslation(long valueOptionId, Language language);
        Task<ValueOptionTranslation> GetValueOptionTranslation(long id);
        Task AddValueOptionTranslationAsync(ValueOptionTranslation valueOptionTranslation);
        void RemoveValueOptionTranslation(ValueOptionTranslation valueOptionTranslation);
    }
}
