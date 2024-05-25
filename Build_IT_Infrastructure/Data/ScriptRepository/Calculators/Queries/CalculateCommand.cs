using Build_IT_Infrastructure.Data.Interfaces;
using Build_IT_Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Build_IT_Infrastructure.Data.ScriptRepository.Calculators.Queries
{
    public class CalculateQuery : IRequest<List<ParameterResource>>
    {
        #region Fields
        
        private readonly string _url = Address.Url + "api/scripts/{scriptId}/calculate";
        private long _scriptId;
        private List<ParameterResource> _data;

        #endregion // Fields

        #region Constructors
        
        public CalculateQuery(long scriptId, List<ParameterResource> data)
        {
            _scriptId = scriptId;
            _data = data;
        }

        #endregion // Constructors

        #region Public_Methods
        
        public async Task<List<ParameterResource>> Execute()
        {
            var json = JsonConvert.SerializeObject(_data);
            var content = new StringContent(json);
            content.Headers.Remove("Content-Type");
            content.Headers.Add("Content-Type", "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_url);
                var respone = await httpClient.PostAsync(
                    _url.Replace("{scriptId}", _scriptId.ToString()), content);
                var result = await respone.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ParameterResource>>(result);
            }
        }

        #endregion // Public_Methods
    }
}
