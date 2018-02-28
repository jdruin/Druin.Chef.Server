using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Druin.Chef.Server.Server.Global.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Support
{
    internal class KeyHelper
    {
        private readonly IChefConnection conn;
        private readonly string organization;
        private readonly IRequester request;

        public KeyHelper(IRequester request, string organization)
        {
            this.conn = request.GetChefConnection();
            this.organization = organization;
            this.request = request;
        }

        public async Task<KeyModel> BuildFullKeyModel(Uri keyUri)
        {

            try
            {
                var key = await request.GetRequestAsync(keyUri.ToString());
                var content = await key.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<KeyModel>(content);
                result.uri = keyUri;

                return result;
            }
            catch (WebException ex)
            {
                throw new ChefExceptionBuilder(conn.UserId, ex);
            }
            
        }
    }
}
