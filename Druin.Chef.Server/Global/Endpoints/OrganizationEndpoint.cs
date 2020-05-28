using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Global.Models;
using Druin.Chef.Server.Support;
using Newtonsoft.Json;

namespace Druin.Chef.Server.Global.Endpoints
{
    public class OrganizationEndpoint
    {
        private readonly string baseUrl;
        private readonly string organization;
        private readonly RequestHelper requestHelper;
        public OrganizationEndpoint(IRequester request, string organization)
        {
            this.organization = organization;
            this.baseUrl = "/organizations/";
            this.requestHelper = new RequestHelper(request, organization);
        }

        public async Task<Dictionary<string, Uri>> GetOrganizationsAsync()
        {
            var fullUrl = baseUrl + organization + "/organizations";
            var result = await requestHelper.GenericRequest<Dictionary<string, Uri>>(HttpMethod.Get, new Uri(fullUrl));

            return result;
        }

        public Dictionary<string, Uri> GetOrganizations()
        {
            return GetOrganizationsAsync().Result;
        }

        public async Task<OrganizationModel> CreateOrganizationAsync(string name, string fullName)
        {
            dynamic newOrg = new ExpandoObject();
            newOrg.name = name;
            newOrg.full_name = fullName;

            var result = await requestHelper.GenericRequest<OrganizationModel>(HttpMethod.Post, newOrg, new Uri(baseUrl));
            return result;

        }

        public OrganizationModel CreateOrganization(string name, string fullName)
        {
            return CreateOrganizationAsync(name, fullName).Result;
        }

        public async Task<OrganizationModel> DeleteOrganizationAsync(string name)
        {
            var fullUrl = baseUrl + name;
            var result = await requestHelper.GenericRequest<OrganizationModel>(HttpMethod.Delete, new Uri(baseUrl));
            return result;
        }

        public OrganizationModel DeleteOrganization(string name)
        {
            return DeleteOrganizationAsync(name).Result;
        }

        public async Task<OrganizationModel> GetOrganizationAsync(string name)
        {
            var fullUrl = baseUrl + name;
            var result = await requestHelper.GenericRequest<OrganizationModel>(HttpMethod.Get, new Uri(fullUrl));
            return result;
        }

        public OrganizationModel GetOrganization(string name)
        {
            return GetOrganizationAsync(name).Result;
        }

        public async Task<OrganizationModel> UpdateOrganizationAsync(string name, string fullName)
        {
            dynamic newOrg = new ExpandoObject();
            newOrg.name = name;
            newOrg.full_name = fullName;

            var fullUrl = baseUrl + name;

            var result = await requestHelper.GenericRequest<OrganizationModel>(HttpMethod.Put, newOrg, new Uri(fullUrl));
            return result;

        }

        public OrganizationModel UpdateOrganization(string name, string fullName)
        {
            return UpdateOrganizationAsync(name, fullName).Result;
        }

    }
}
