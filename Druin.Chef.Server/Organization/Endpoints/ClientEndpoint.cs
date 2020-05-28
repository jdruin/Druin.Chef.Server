using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Druin.Chef.Server.Global.Models;
using Newtonsoft.Json;
using Druin.Chef.Server.Organization.Models;
using System.Net;
using Druin.Chef.Core.Exceptions;
using System.Dynamic;
using Druin.Chef.Server.Support;
using System.Net.Http;

namespace Druin.Chef.Server.Organization.Endpoints
{
    public class ClientEndpoint
    {

        private readonly string baseUrl;
        private readonly string organization;
        private readonly RequestHelper requestHelper;
        public ClientEndpoint(IRequester request, string organization)
        {
            this.baseUrl = "/organizations/" + organization + "/clients/";
            this.organization = organization;
            this.requestHelper = new RequestHelper(request, organization);
        }

        public async Task<List<KeyModel>> GetClientKeysAsync(string clientname)
        {
            var fullUrl = baseUrl + clientname + "/keys";
            var clientKeysRaw = await requestHelper.GenericRequest<List<KeyModel>>(HttpMethod.Get, new Uri(fullUrl));
            var result = new List<KeyModel>();

            var keyHelper = new KeyHelper(requestHelper.RequestObject(), organization);

            foreach (var rawKey in clientKeysRaw)
            {
                var finalKey = await keyHelper.RetrieveFullKeyModel(rawKey.uri);
                result.Add(finalKey);
            }

            return result;

        }

        public List<KeyModel> GetClientKeys(string clientname)
        {
            return GetClientKeysAsync(clientname).Result;
        }

        public async Task<KeyModel> CreateClientKeyAsync(string clientName, string keyName, string publicKey, DateTime expirationDate)
        {
            dynamic newKey = new ExpandoObject();
            newKey.name = keyName;
            newKey.public_key = publicKey;
            newKey.expiration_date = expirationDate;

            var fullUrl = baseUrl + clientName + "/keys";

            var result = await requestHelper.GenericRequest<KeyModel>(HttpMethod.Post, newKey, new Uri(fullUrl));
            var keyHelper = new KeyHelper(requestHelper.RequestObject(), organization);
            var fullKey = await keyHelper.RetrieveFullKeyModel(result.uri);

            return fullKey;

        }

        public KeyModel CreateClientKey(string clientName, string keyName, string publicKey, DateTime expirationDate)
        {
            return CreateClientKeyAsync(clientName, keyName, publicKey, expirationDate).Result;
        }

        public async Task<KeyModel> DeleteClientKeyAsync(string clientName, string keyName)
        {
            var fullUrl = baseUrl + clientName + "/keys/" + keyName;

            var result = await requestHelper.GenericRequest<KeyModel>(HttpMethod.Delete, new Uri(fullUrl));
            return result;

        }

        public KeyModel DeleteClientKey(string clientName, string keyName)
        {
            return DeleteClientKeyAsync(clientName, keyName).Result;
        }

        public async Task<List<ClientModel>> GetClientsAsync()
        {
            var rawClients = await requestHelper.GenericRequest<Dictionary<string, Uri>>(HttpMethod.Get, new Uri(baseUrl));
            var result = new List<ClientModel>();

            foreach (var rawClient in rawClients)
            {
                var client = await requestHelper.GenericRequest<ClientModel>(HttpMethod.Get, rawClient.Value);
                result.Add(client);
            }
            return result;
        }

        public List<ClientModel> GetClients()
        {
            return GetClientsAsync().Result;
        }

        public async Task<ClientModel> CreateClientAsync(string name, bool createKey)
        {
            dynamic newClient = new ExpandoObject();
            newClient.name = name;
            newClient.create_key = createKey;

            var result = await requestHelper.GenericRequest<ClientModel>(HttpMethod.Post, newClient, new Uri(baseUrl));
            return result;
        }

        public ClientModel CreateClient(string name, bool createKey)
        {
            return CreateClientAsync(name, createKey).Result;
        }

        public async Task<ClientModel> DeleteClientAsync(string clientName)
        {
            var fullUrl = baseUrl + clientName;

            var result = await requestHelper.GenericRequest<ClientModel>(HttpMethod.Delete, new Uri(fullUrl));
            return result;
        }

        public ClientModel DeleteClient(string clientName)
        {
            return DeleteClientAsync(clientName).Result;
        }

        public async Task<ClientModel> GetClientAsync(string clientName)
        {
            var fullUrl = baseUrl + clientName;

            var result = await requestHelper.GenericRequest<ClientModel>(HttpMethod.Get, new Uri(fullUrl));
            return result;
        }

        public ClientModel GetCLient(string clientName)
        {
            return GetClientAsync(clientName).Result;
        }

        public async Task<ClientModel> UpdateClientNameAsync(string currentName, string newName)
        {
            var fullUrl = baseUrl + currentName;

            dynamic newClient = new ExpandoObject();
            newClient.name = newName;

            var updatedClient = await requestHelper.GenericRequest<Uri>(HttpMethod.Put, newClient, new Uri(fullUrl));

            var result = await requestHelper.GenericRequest<ClientModel>(HttpMethod.Get, new Uri(baseUrl + newName));

            result.uri = new Uri(baseUrl + newName);

            return result;
        }

        public ClientModel UpdateClientName(string currentName, string newName)
        {
            return UpdateClientNameAsync(currentName, newName).Result;
        }

    }
}
