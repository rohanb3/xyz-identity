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
    public class HttpClientRelationsService : IHttpClientRelationsService
    {
        private readonly string _publicApiUrl = null;
        private readonly IHttpContextAccessor _httpContextAccessor = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="options"></param>
        public HttpClientRelationsService(IHttpContextAccessor httpContextAccessor, IOptionsMonitor<ServiceOption> options)
        {
            _publicApiUrl = options.CurrentValue?.PublicApiUrl ??
                throw new InvalidOperationException("Missing URL to public-api");
            _httpContextAccessor = httpContextAccessor ?? 
                throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <inheritdoc />
        public async Task<CompanyModel> GetCompanyById(int id)
        {
            var uri = new Uri($"{_publicApiUrl}company/{id}");
            var responseString = await SendGetRequest(uri);

            return GetPublicApiResponse<CompanyModel>(responseString);
        }

        /// <inheritdoc />
        public async Task<BranchModel> GetBranchById(Guid id)
        {
            var uri = new Uri($"{_publicApiUrl}/branch/{id.ToString()}");
            var responseString = await SendGetRequest(uri);

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

        private async Task<string> SendGetRequest(Uri uri)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                   _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(' ').LastOrDefault());
                var response = await client.GetAsync(client.BaseAddress);
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new AccessException("You don't have permissions to do that");
                }
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                var responseString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var data = JToken.Parse(responseString);
                    if (data as JObject != null)
                    {
                        if (int.Parse(data["status"].ToString()) == StatusCodes.Status404NotFound)
                        {
                            throw new KeyNotFoundException();
                        }
                    }

                    throw new ApplicationException(responseString);
                }

                return responseString;
            }
        }
    }
}
