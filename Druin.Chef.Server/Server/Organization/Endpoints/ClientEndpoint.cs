﻿using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Organization.Endpoints
{
    public class ClientEndpoint
    {

        private readonly IChefConnection conn;
        private readonly string baseUrl;
        private readonly IRequester request;
        public ClientEndpoint(IRequester request, string organization)
        {
            this.conn = request.GetChefConnection();
            this.baseUrl = "/organizations/" + organization + "/license";
            this.request = request;
        }
    }
}