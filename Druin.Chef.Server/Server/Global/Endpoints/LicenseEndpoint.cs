using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Druin.Chef.Core;
using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Global.Models;
using Newtonsoft.Json;

namespace Druin.Chef.Server.Server.Global.Endpoints
{
    public class LicenseEndpoint
    {
        private readonly ChefConnection conn;
        private readonly string baseUrl;
        private readonly Requester request;
        public LicenseEndpoint(ChefConnection conn, string organization)
        {
            this.conn = conn;
            this.baseUrl = "/organizations/" + organization + "/license";
            this.request = new Requester(conn);
        }

        public LicenseModel GetLicense()
        {
           return GetLicenseAsync().Result;
            
        }

        public async Task<LicenseModel> GetLicenseAsync()
        {
            try
            {
                var license = await request.GetRequestAsync(baseUrl);
                var content = await license.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LicenseModel>(content);
                return result;
            }
            catch (WebException ex)
            {

                throw new ChefExceptionBuilder(conn.UserId, ex);
            }

            
        }
    }
}
