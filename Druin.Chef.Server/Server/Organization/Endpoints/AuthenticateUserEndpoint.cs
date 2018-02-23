using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Organization.Endpoints
{
    public class AuthenticateUserEndpoint
    {
        private readonly IChefConnection conn;
        private readonly string baseUrl;
        private readonly IRequester request;

        public AuthenticateUserEndpoint(IChefConnection conn, string organization)
        {
            this.conn = conn;
            this.baseUrl = "/organizations/" + organization + "/authenticate_user";
            this.request = new Requester(conn);
        }

        public async Task<bool> CheckAuthenticationAsync(string username, string password)
        {
            try
            {
                dynamic userToCheck = new ExpandoObject();
                userToCheck.username = username;
                userToCheck.password = password;

                var userJson = JsonConvert.SerializeObject(userToCheck);
                var user = await request.PostRequestAsync(baseUrl, userJson);
                return true;

            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;

                if (res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public bool CheckAuthentication(string username, string password)
        {
            return CheckAuthenticationAsync(username, password).Result;
        }
    }
}
