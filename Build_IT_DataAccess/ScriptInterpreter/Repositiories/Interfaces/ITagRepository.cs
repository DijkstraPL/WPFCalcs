using Build_IT_DataAccess.Interfaces;
using Build_IT_DataAccess.ScriptInterpreter.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetTagsForScriptAsync(long scriptId);
    }
}