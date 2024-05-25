using Build_IT_Infrastructure.Data.Interfaces;
using Build_IT_Infrastructure.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Build_IT_Infrastructure.Data.ScriptRepository.Scripts.Queries
{
    public class GetAllScriptsQuery : IRequest<IEnumerable<ScriptResource>>
    {
        #region Fields
   
        private readonly string _url = Address.Url + "api/scripts";

        #endregion // Fields

        #region Public_Methods

        public async Task<IEnumerable<ScriptResource>> Execute()
        {
            using (WebClient webClient = new WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(_url);
                return JsonConvert.DeserializeObject<List<ScriptResource>>(json);
            }
        }

        #endregion // Public_Methods
    }
}
