using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Druin.Chef.Server.Server.Global.Models;
using Newtonsoft.Json;
using Druin.Chef.Server.Server.Organization.Models;
using System.Net;
using Druin.Chef.Core.Exceptions;
using System.Dynamic;
using Druin.Chef.Server.Server.Support;

namespace Druin.Chef.Server.Server.Organization.Endpoints
{
    public class ClientEndpoint
    {

        private readonly IChefConnection conn;
        private readonly string baseUrl;
        private readonly IRequester request;
        private string organization;
        public ClientEndpoint(IRequester request, string organization)
        {
            this.conn = request.GetChefConnection();
            this.baseUrl = "/organizations/" + organization + "/clients/";
            this.request = request;
            this.organization = organization;
        }

        public async Task<List<KeyModel>> GetClientKeysAsync(string clientname)
        {
            try
            {
                var fullUrl = baseUrl + clientname + "/keys";

                var keys = await request.GetRequestAsync(fullUrl);
                var content = await keys.Content.ReadAsStringAsync();
                var clientKeysRaw = JsonConvert.DeserializeObject<List<KeyModel>>(content);

                var result = new List<KeyModel>();

                var keyHelper = new KeyHelper(request, organization);

                foreach (var rawKey in clientKeysRaw)
                {
                    var finalKey = await keyHelper.BuildFullKeyModel(rawKey.uri);
                    result.Add(finalKey);
                }

                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }

        }

        public List<KeyModel> GetClientKeys(string clientname)
        {
            return GetClientKeysAsync(clientname).Result;
        }

        public async Task<KeyModel> CreateClientKeyAsync(string clientName, string keyName, string publicKey, DateTime expirationDate)
        {
            try
            {
                dynamic newKey = new ExpandoObject();
                newKey.name = keyName;
                newKey.public_key = publicKey;
                newKey.expiration_date = expirationDate;

                var fullUrl = baseUrl + clientName + "/keys";

                string keyJson = JsonConvert.SerializeObject(newKey);
                var key = await request.PostRequestAsync(fullUrl, keyJson);
                var content = await key.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KeyModel>(content);

                var keyHelper = new KeyHelper(request, organization);
                var fullKey = await keyHelper.BuildFullKeyModel(result.uri);
                
                return fullKey;

            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public KeyModel CreateClientKey(string clientName, string keyName, string publicKey, DateTime expirationDate)
        {
            return CreateClientKeyAsync(clientName, keyName, publicKey, expirationDate).Result;
        }

        public async Task<KeyModel> DeleteClientKeyAsync(string clientName, string keyName)
        {
            try
            {
                var fullUrl = baseUrl + clientName + "/keys/" + keyName;

                var key = await request.DeleteRequestAsync(fullUrl);
                var content = await key.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KeyModel>(content);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public KeyModel DeleteClientKey(string clientName, string keyName)
        {
            return DeleteClientKeyAsync(clientName, keyName).Result;
        }


    }
}
