using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseUri = new Uri("https://api.opscode.com:443");

            var privateKey = System.IO.File.ReadAllText(@"C:\Users\jdrui\Documents\jdruin-hosted-chef.pem");

            var conn = new ChefConnection()
            {
                ChefServer = baseUri.ToString(),
                PrivateKey = privateKey,
                UserId = "jdruin"
            };

            var requester = new Requester(conn);
            var result = requester.GetRequestAsync("/organizations/learningapi/roles").Result;

            Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            Console.ReadKey();
        }
    }
}
