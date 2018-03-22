using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Organization.Endpoints
{
    public class ContainerEndpoint
    {
        private readonly string baseUrl;
        private readonly string organization;
        private readonly RequestHelper requestHelper;
        public ContainerEndpoint(IRequester request, string organization)
        {
            this.baseUrl = "/organizations/" + organization + "/containers/";
            this.organization = organization;
            this.requestHelper = new RequestHelper(request, organization);
        }

        public async Task<Dictionary<string, Uri>> GetContainersAsync()
        {
            var result = await requestHelper.GenericRequest<Dictionary<string, Uri>>(HttpMethod.Get, new Uri(baseUrl));
            return result;
        }

        public Dictionary<string, Uri> GetContainers()
        {
            return GetContainersAsync().Result;
        }
    }
}
