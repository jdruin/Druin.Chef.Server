using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Organization.Endpoints
{
    public class CookbookEndpoint
    {
        private readonly string baseUrl;
        private readonly string organization;
        private readonly RequestHelper requestHelper;

        public CookbookEndpoint(IRequester request, string organization)
        {
            this.baseUrl = "/organizations/" + organization + "/containers/";
            this.organization = organization;
            this.requestHelper = new RequestHelper(request, organization);
        }


    }
}
