using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Organization.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Organization.Endpoints
{
    public class AssociationRequests
    {
        private readonly IChefConnection conn;
        private readonly string baseUrl;
        private readonly string organization;
        private readonly IRequester request;
        public AssociationRequests(IChefConnection conn, string organization)
        {
            this.conn = conn;
            this.organization = organization;
            this.baseUrl = "/organizations/" + organization + "/association_requests/";
            this.request = new Requester(conn);
        }

        public async Task<bool> DeleteAssociationRequestAsync(string id)
        {
            try
            {
                var fullUrl = baseUrl + id;
                var ar = await request.DeleteRequestAsync(fullUrl);
                return true;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public bool DeleteAssociationRequest(string id)
        {
            return DeleteAssociationRequestAsync(id).Result;
        }

        public async Task<AssociationRequestModel> GetAssociationRequestsAsync()
        {
            try
            {
                var ar = await request.GetRequestAsync(baseUrl);
                var content = await ar.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AssociationRequestModel>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public AssociationRequestModel GetAssociationRequests()
        {
            return GetAssociationRequestsAsync().Result;
        }

         // https://docs.chef.io/api_chef_server.html#association-requests
        //public async Task<AssociationRequestModel> CreateAssociationRequestAsync(string username)
        //{
        //    try
        //    {
        //        var ar = await request.PostRequestAsync()
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
