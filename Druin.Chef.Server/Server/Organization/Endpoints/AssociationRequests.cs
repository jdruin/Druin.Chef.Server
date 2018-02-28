using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Organization.Models;
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
    public class AssociationRequests
    {
        private readonly IChefConnection conn;
        private readonly string baseUrl;
        private readonly string organization;
        private readonly IRequester request;
        public AssociationRequests(IRequester request, string organization)
        {
            this.conn = request.GetChefConnection();
            this.organization = organization;
            this.baseUrl = "/organizations/" + organization + "/association_requests/";
            this.request = request;
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

        
        //public async Task<AssociationRequestModel> CreateAssociationRequestAsync(string username)
        //{
        //    try
        //    {
        //        dynamic newAr = new ExpandoObject();
        //        newAr.name
        //        var ar = await request.PostRequestAsync()
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
