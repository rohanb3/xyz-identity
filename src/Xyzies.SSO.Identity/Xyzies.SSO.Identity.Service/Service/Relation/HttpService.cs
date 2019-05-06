using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.Services.Helpers;
using Xyzies.SSO.Identity.Services.Models.Branch;
using Xyzies.SSO.Identity.Services.Models.Company;

namespace Xyzies.SSO.Identity.Services.Service.Relation
{
    /// <inheritdoc />
    public class HttpService : IHttpService
    {
        private readonly string _publicApiUrl = null;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="options"></param>
        public HttpService(IOptionsMonitor<ServiceOption> options)
        {
            _publicApiUrl = options.CurrentValue?.PublicApiUrl ??
                throw new InvalidOperationException("Missing URL to public-api");
        }

        /// <inheritdoc />
        public async Task<CompanyModel> GetCompanyById(int id, string token = null)
        {
            var uri = new Uri($"{_publicApiUrl}company/{id}");
            var responseString = await SendGetRequest(uri, token);

            return GetPublicApiResponse<CompanyModel>(responseString);
        }

        /// <inheritdoc />
        public async Task<BranchModel> GetBranchById(Guid id, string token = null)
        {
            var uri = new Uri($"{_publicApiUrl}/branch/{id.ToString()}");
            var responseString = await SendGetRequest(uri, token);

            return GetPublicApiResponse<BranchModel>(responseString);
        }

        private T GetPublicApiResponse<T>(string responseString)
        {
            if(string.IsNullOrWhiteSpace(responseString))
            {
                return default(T);
            }
            var data = JToken.Parse(responseString);
            if ((data as JObject) != null && data["data"] != null)
            {
                return data["data"].ToObject<T>();
            }
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        private async Task<string> SendGetRequest(Uri uri, string token = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                if(!string.IsNullOrWhiteSpace(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                var response = await client.GetAsync(client.BaseAddress);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(responseString);
                }

                return responseString;
            }
        }
    }
}
