using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Threading.Tasks;
using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Global.Models;
using Newtonsoft.Json;

namespace Druin.Chef.Server.Server.Global.Endpoints
{
    public class OrganizationEndpoint
    {
        private readonly IChefConnection conn;
        private readonly string baseUrl;
        private readonly string organization;
        private readonly IRequester request;
        public OrganizationEndpoint(IRequester request, string organization)
        {
            this.conn = request.GetChefConnection();
            this.organization = organization;
            this.baseUrl = "/organizations/" ;
            this.request = request;
        }

        public async Task<Dictionary<string, Uri>> GetOrganizationsAsync()
        {
            try
            {
                var url = baseUrl + organization + "/organizations";
                var orgs = await request.GetRequestAsync(url);
                var content = await orgs.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Dictionary<string, Uri>>(content);
                return result;
            }
            catch (WebException ex)
            {

                throw new ChefExceptionBuilder(conn.UserId, ex);
            }

            
        }

        public Dictionary<string, Uri> GetOrganizations()
        {
            return GetOrganizationsAsync().Result;
        }

        public async Task<OrganizationModel> CreateOrganizationAsync(string name, string fullName)
        {

            try
            {
                dynamic newOrg = new ExpandoObject();
                newOrg.name = name;
                newOrg.full_name = fullName;

                var orgJson = JsonConvert.SerializeObject(newOrg);
                var org = await request.PostRequestAsync(baseUrl, orgJson);
                var content = await org.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<OrganizationModel>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
            
        }

        public OrganizationModel CreateOrganization(string name, string fullName)
        {
            return CreateOrganizationAsync(name, fullName).Result;
        }

        public async Task<OrganizationModel> DeleteOrganizationAsync(string name)
        {
            try
            {
                var fullUrl = baseUrl + name;
                var org = await request.DeleteRequestAsync(fullUrl);
                var content = await org.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<OrganizationModel>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public OrganizationModel DeleteOrganization(string name)
        {
            return DeleteOrganizationAsync(name).Result;
        }

        public async Task<OrganizationModel> GetOrganizationAsync(string name)
        {
            try
            {
                var fullUrl = baseUrl + name;
                var org = await request.GetRequestAsync(fullUrl);
                var content = await org.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<OrganizationModel>(content);
                return result;
            }
            catch (WebException ex )
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }

        }

        public OrganizationModel GetOrganization(string name)
        {
            return GetOrganizationAsync(name).Result;
        }

        public async Task<OrganizationModel> UpdateOrganizationAsync(string name, string fullName)
        {
            try
            {
                dynamic newOrg = new ExpandoObject();
                newOrg.name = name;
                newOrg.full_name = fullName;

                var fullUrl = baseUrl + name;

                var orgJson = JsonConvert.SerializeObject(newOrg);
                var org = await request.PutRequestAsync(fullUrl, orgJson);
                var content = await org.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<OrganizationModel>(content);
                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
        }

        public OrganizationModel UpdateOrganization(string name, string fullName)
        {
            return UpdateOrganizationAsync(name, fullName).Result;
        }

    }
}
