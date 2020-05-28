using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Global.Models;
using Druin.Chef.Server.Support;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Global.Endpoints
{
    public class UserEndpoint
    {
        private readonly string baseUrl;
        private readonly RequestHelper requestHelper;
        public UserEndpoint(IRequester request, string organization)
        {
            this.baseUrl = "/users/";
            this.requestHelper = new RequestHelper(request, organization);
        }

        public async Task<Dictionary<string, Uri>> GetUsersAsync()
        {
            var result = await requestHelper.GenericRequest<Dictionary<string, Uri>>(HttpMethod.Get, new Uri(baseUrl));
            return result;
        }

        public Dictionary<string, Uri> GetUsers()
        {
            return GetUsersAsync().Result;
        }

        public async Task<UserModel> CreateUserAsync(string username, string display_name, string email, string first_name, string last_name, string password, string public_key, string middle_name = "")
        {
            dynamic newUser = new ExpandoObject();
            newUser.name = username;
            newUser.disply_name = display_name;
            newUser.email = email;
            newUser.first_name = first_name;
            newUser.last_name = last_name;
            newUser.password = password;
            newUser.public_key = public_key;
            newUser.middle_name = middle_name;

            var result = await requestHelper.GenericRequest<UserModel>(HttpMethod.Post, newUser, new Uri(baseUrl));
            return result;
        }

        public async Task<UserModel> CreateUserAsync(string username)
        {
            dynamic newUser = new ExpandoObject();
            newUser.name = username;

            var fullUrl = baseUrl + username;

            var result = await requestHelper.GenericRequest<UserModel>(HttpMethod.Post, newUser, fullUrl);
            return result;


        }
        public UserModel CreateUser(string username, string display_name, string email, string first_name, string last_name, string password, string public_key, string middle_name = "")
        {
            return CreateUserAsync(username, display_name, email, first_name, last_name, password, public_key, middle_name).Result;
        }

        public UserModel CreateUser(string username)
        {
            return CreateUserAsync(username).Result;
        }

        public async Task<UserModel> DeleteUserAsync(string username)
        {
            var fullUrl = baseUrl + username;
            var result = await requestHelper.GenericRequest<UserModel>(HttpMethod.Delete, new Uri(fullUrl));
            return result;

        }

        public UserModel DeleteUser(string username)
        {
            return DeleteUserAsync(username).Result;
        }

        public async Task<UserModel> GetUserAsync(string username)
        {
            var fullUrl = baseUrl + username;

            var result = await requestHelper.GenericRequest<UserModel>(HttpMethod.Get, new Uri(fullUrl));
            return result;
        }

        public UserModel GetUser(string username)
        {
            return GetUserAsync(username).Result;
        }

        public async Task<UserModel> UpdateUserAsync(string username, bool admin = false)
        {
            dynamic newUser = new ExpandoObject();
            newUser.name = username;
            newUser.admin = admin;

            var fullUrl = baseUrl + username;
            var result = await requestHelper.GenericRequest<UserModel>(HttpMethod.Put, new Uri(fullUrl));
            return result;

        }

        public UserModel UpdateUser(string username, bool admin = false)
        {
            return UpdateUserAsync(username, admin).Result;
        }

        public async Task<List<KeyModel>> GetUserKeysAsync(string username)
        {
            var fullUrl = baseUrl + username + "/keys/";
            var result = await requestHelper.GenericRequest<List<KeyModel>>(HttpMethod.Get, new Uri(fullUrl));
            return result;

        }

        public List<KeyModel> GetUserKeys(string username)
        {
            return GetUserKeysAsync(username).Result;
        }

        public async Task<KeyModel> AddUserKeyAsync(string username, string keyName, string publicKey, DateTime expirationDate)
        {
            var fullUrl = baseUrl + username + "/keys/";

            dynamic newKey = new ExpandoObject();
            newKey.name = keyName;
            newKey.public_key = publicKey;
            newKey.expiration_date = expirationDate;

            var result = await requestHelper.GenericRequest<KeyModel>(HttpMethod.Post, newKey, new Uri(fullUrl));
            return result;
        }

        public KeyModel AddUserKey(string username, string keyName, string publicKey, DateTime expirationDate)
        {
            return AddUserKeyAsync(username, keyName, publicKey, expirationDate).Result;
        }

        public async Task<KeyModel> DeleteUserKeyAsync(string username, string keyname)
        {
            var fullUrl = baseUrl + username + "/keys/" + keyname;
            var result = await requestHelper.GenericRequest<KeyModel>(HttpMethod.Delete, new Uri(fullUrl));
            return result;

        }

        public KeyModel DeleteUserKey(string username, string keyname)
        {
            return DeleteUserKeyAsync(username, keyname).Result;
        }

        public async Task<KeyModel> GetUserKeyAsync(string username, string keyname)
        {
            var fullUrl = baseUrl + username + "/keys/" + keyname;
            var result = await requestHelper.GenericRequest<KeyModel>(HttpMethod.Get, new Uri(fullUrl));
            return result;


        }

        public KeyModel GetUserKey(string username, string keyname)
        {
            return GetUserKeyAsync(username, keyname).Result;
        }

        public async Task<KeyModel> UpdateUserKeyAsync(string username, string keyname, string publicKey, DateTime expirationDate)
        {
            var fullUrl = baseUrl + username + "/keys/" + keyname;
            dynamic newKey = new ExpandoObject();
            newKey.name = keyname;
            newKey.public_key = publicKey;
            newKey.expiration_date = expirationDate;

            var result = await requestHelper.GenericRequest<KeyModel>(HttpMethod.Put, newKey, new Uri(fullUrl));

            return result;

        }

        public KeyModel UpdateUserKey(string username, string keyname, string publicKey, DateTime expirationDate)
        {
            return UpdateUserKeyAsync(username, keyname, publicKey, expirationDate).Result;
        }




    }
}
