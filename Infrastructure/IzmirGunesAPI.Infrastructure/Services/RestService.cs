using IzmirGunesAPI.Application.Abstractions.Services;
using IzmirGunesAPI.Application.DTOs.Rest;
using IzmirGunesAPI.Application.DTOs.Token;
using Newtonsoft.Json;

namespace IzmirGunesAPI.Infrastructure.Services
{
    public class RestService : IRestService
    {
        public async Task<RestToken> GetToken(RestContent company)
        {
            RestToken tokenModel = new RestToken();
            tokenModel.status = false;
            tokenModel.token = String.Empty;
            
                HttpResponseMessage response;

                var formContent = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("grant_type",company.GrantType),
                        new KeyValuePair<string, string>("branchcode", company.BranchCode),
                        new KeyValuePair<string, string>("password", company.Password),
                        new KeyValuePair<string, string>("username", company.UserName),
                        new KeyValuePair<string, string>("dbname", company.DbName),
                        new KeyValuePair<string, string>("dbuser", company.DbUser),
                        new KeyValuePair<string, string>("dbpassword", company.DbPassword),
                        new KeyValuePair<string, string>("dbtype", company.Dbtype)
                    });

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(new Uri(Configuration.Rest_RestUrl), "api/v2/token");

                    response =  await client.PostAsync(client.BaseAddress.AbsoluteUri, formContent);
                }
                string result = await response.Content.ReadAsStringAsync();
            response.Dispose();

                 dynamic jsonResponse = JsonConvert.DeserializeObject(result);
                tokenModel.token = jsonResponse?.access_token;
                 if (tokenModel.token != null)
                      tokenModel.status = true;
                  else            
                      tokenModel.status = false;
            return tokenModel;
        }
    }
}
