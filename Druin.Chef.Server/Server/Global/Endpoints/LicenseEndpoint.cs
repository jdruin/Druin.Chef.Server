using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Druin.Chef.Core;
using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Global.Models;
using Druin.Chef.Server.Server.Support;
using Newtonsoft.Json;

namespace Druin.Chef.Server.Server.Global.Endpoints
{
    public class LicenseEndpoint
    {
        private readonly string baseUrl;
        private readonly RequestHelper requestHelper;
        public LicenseEndpoint(IRequester request, string organization)
        {
            this.baseUrl = "/organizations/" + organization + "/license";
            this.requestHelper = new RequestHelper(request, organization);

        }

        public LicenseModel GetLicense()
        {
           return GetLicenseAsync().Result;
            
        }

        public async Task<LicenseModel> GetLicenseAsync()
        {
            var result = await requestHelper.GenericRequest<LicenseModel>(HttpMethod.Get, new Uri(baseUrl));
            return result;
        }
    }
}
