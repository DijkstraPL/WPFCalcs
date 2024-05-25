using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_Infrastructure.Data.Interfaces;
using Build_IT_Infrastructure.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Build_IT_Infrastructure.Data.ScriptRepository.Parameters.Queries
{
    public class GetAllEditableParametersForScriptQuery : IRequest<IEnumerable<ParameterResource>>
    {
        #region Fields
        
        private readonly string _url = Address.Url + "api/scripts/{scriptId}/parameters";
        private readonly long _scriptId;

        #endregion // Fields

        #region Constructors
        
        public GetAllEditableParametersForScriptQuery(long scriptId)
        {
            _scriptId = scriptId;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public async Task<IEnumerable<ParameterResource>> Execute()
        {
            using (WebClient webClient = new WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(_url.Replace("{scriptId}", _scriptId.ToString()));
                return JsonConvert.DeserializeObject<List<ParameterResource>>(json).Where(p => (p.Context & ParameterOptions.Editable) != 0);
            }
        }

        #endregion // Public_Methods
    }
}