using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Global.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Global.Endpoints
{
    public class UserEndpoint
    {
        private readonly IChefConnection conn;
        private readonly string baseUrl;
        private readonly string organization;
        private readonly IRequester request;
        public UserEndpoint(IRequester request, string organization)
        {
            this.conn = request.GetChefConnection();
            this.organization = organization;
            this.baseUrl = "/users/";
            this.request = request;
        }

        public async Task<Dictionary<string, Uri>> GetUsersAsync()
        {
            try
            {
                var users = await request.GetRequestAsync(baseUrl);
                var content = await users.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string, Uri>>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public Dictionary<string, Uri> GetUsers()
        {
            return GetUsersAsync().Result;
        }

        public async Task<UserModel> CreateUserAsync(string username, string display_name, string email, string first_name, string last_name, string password, string public_key, string middle_name = "")
        {
            try
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

                var result = await NewUser(baseUrl, newUser);
                
                return result;

            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public async Task<UserModel> CreateUserAsync(string username)
        {
            try
            {
                dynamic newUser = new ExpandoObject();
                newUser.name = username;

                var fullUrl = baseUrl + username;

                var result = await NewUser(fullUrl, newUser);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }
        public UserModel CreateUser(string username, string display_name, string email, string first_name, string last_name, string password, string public_key, string middle_name = "")
        {
            return CreateUserAsync(username, display_name, email, first_name, last_name, password, public_key, middle_name).Result;
        }

        public UserModel CreateUser(string username)
        {
            return CreateUserAsync(username).Result;
        }

        public async Task<string> DeleteUserAsync(string username)
        {
            try
            {
                var fullUrl = baseUrl + username;

                var user = await request.DeleteRequestAsync(fullUrl);
                var content = await user.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(content);

                return result.name;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }

            
        }

        public string DeleteUser(string username)
        {
            return DeleteUserAsync(username).Result;
        }

        public async Task<string> GetUserAsync(string username)
        {
            var fullUrl = baseUrl + username;

            var user = await request.GetRequestAsync(fullUrl);
            var content = await user.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);

            return result.name;
        }

        public string GetUser(string username)
        {
            return GetUserAsync(username).Result;
        }

        public async Task<UserModel> UpdateUserAsync(string username, bool admin = false)
        {

            try
            {
                dynamic newUser = new ExpandoObject();
                newUser.name = username;
                newUser.admin = admin;

                var fullUrl = baseUrl + username;

                string userJson = JsonConvert.SerializeObject(newUser);
                var user = await request.PutRequestAsync(fullUrl, userJson);
                var content = await user.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UserModel>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
            

        }

        public UserModel UpdateUser(string username, bool admin = false)
        {
            return UpdateUserAsync(username, admin).Result;
        }

        public async Task<List<KeyModel>> GetUserKeysAsync(string username)
        {
            try
            {
                var fullUrl = baseUrl + username + "/keys/";

                var keys = await request.GetRequestAsync(fullUrl);
                var content = await keys.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<KeyModel>>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public List<KeyModel> GetUserKeys(string username)
        {
            return GetUserKeysAsync(username).Result;
        }

        public async Task<KeyModel> AddUserKeyAsync(string username, string keyName, string publicKey, DateTime expirationDate)
        {

            try
            {
                var fullUrl = baseUrl + username + "/keys/";

                dynamic newKey = new ExpandoObject();
                newKey.name = keyName;
                newKey.public_key = publicKey;
                newKey.expiration_date = expirationDate;

                string keyJson = JsonConvert.SerializeObject(newKey);
                var key = await request.PostRequestAsync(fullUrl, keyJson);
                var content = await key.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KeyModel>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex) ;
            }
            
        }

        public KeyModel AddUserKey(string username, string keyName, string publicKey, DateTime expirationDate)
        {
            return AddUserKeyAsync(username, keyName, publicKey, expirationDate).Result;
        }

        public async Task<KeyModel> DeleteUserKeyAsync(string username, string keyname)
        {

            try
            {
                var fullUrl = baseUrl + username + "/keys/" + keyname;

                var key = await request.DeleteRequestAsync(fullUrl);
                var content = await key.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KeyModel>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
            
        }

        public KeyModel DeleteUserKey(string username, string keyname)
        {
            return DeleteUserKeyAsync(username, keyname).Result;
        }

        public async Task<KeyModel> GetUserKeyAsync(string username, string keyname)
        {

            try
            {
                var fullUrl = baseUrl + username + "/keys/" + keyname;

                var key = await request.GetRequestAsync(fullUrl);
                var content = await key.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KeyModel>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
            
        }

        public KeyModel GetUserKey(string username, string keyname)
        {
            return GetUserKeyAsync(username, keyname).Result;
        }

        public async Task<KeyModel> UpdateUserKeyAsync(string username, string keyname, string publicKey, DateTime expirationDate)
        {
            try
            {
                var fullUrl = baseUrl + username + "/keys/" + keyname;

                dynamic newKey = new ExpandoObject();
                newKey.name = keyname;
                newKey.public_key = publicKey;
                newKey.expiration_date = expirationDate;

                string keyJson = JsonConvert.SerializeObject(newKey);
                var key = await request.PutRequestAsync(fullUrl, keyJson);
                var content = await key.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KeyModel>(content);

                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public KeyModel UpdateUserKey(string username, string keyname, string publicKey, DateTime expirationDate)
        {
            return UpdateUserKeyAsync(username, keyname, publicKey, expirationDate).Result;
        }


        private async Task<UserModel> NewUser(string url, object newUser)
        {
            string userJson = JsonConvert.SerializeObject(newUser);
            var user = await request.PostRequestAsync(url, userJson);
            var content = await user.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserModel>(content);
            return result;
        }

    }
}
