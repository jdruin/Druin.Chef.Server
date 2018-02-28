using Druin.Chef.Core.Authentication;
using Druin.Chef.Core.Exceptions;
using Druin.Chef.Core.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Server.Server.Support
{
    internal class RequestHelper
    {
        private readonly string organization;
        private readonly IChefConnection conn;
        private readonly IRequester request;
        public RequestHelper(IRequester request, string organization)
        {
            this.request = request;
            this.conn = request.GetChefConnection();
            this.organization = organization;
        }

        public async Task<T>GenericRequest<T>(HttpMethod method, object submitObject, Uri endpoint, string parameters ="")
        {
            try
            {
                var submittedObjectJson = JsonConvert.SerializeObject(submitObject);
                HttpResponseMessage finishedRequest;
                string content;
                switch (method.Method.ToString())
                {
                    case "Post":
                        {
                            finishedRequest = await request.PostRequestAsync(endpoint.ToString(), submittedObjectJson, parameters);
                            content = await finishedRequest.Content.ReadAsStringAsync();
                            break;
                        }
                    case "Put":
                        {
                            finishedRequest = await request.PutRequestAsync(endpoint.ToString(), submittedObjectJson, parameters);
                            content = await finishedRequest.Content.ReadAsStringAsync();
                            break;
                        }

                    default:
                        throw new NotSupportedException(method.Method.ToString() + "is not supported");
                }
                

                var result = JsonConvert.DeserializeObject<T>(content);

                return result;

                

            }
            catch (WebException ex)
            {

                HttpWebResponse res = (HttpWebResponse)ex.Response;
                string message;
                switch (res.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        message = "Bad request. The contents of the request are not formatted correctly.";
                        throw new ChefException(message);
                    case HttpStatusCode.Unauthorized:
                        message = "Unauthorized. The user or client (" + conn.UserId + ") could not be authenticated. Verify the user/client name, and that the correct key was used to sign the request.";
                        throw new ChefUnauthorizedException(message);
                    case HttpStatusCode.Forbidden:
                        message = "Forbidden. " + conn.UserId + " is not authorized to perform the action.";
                        throw new ChefForbiddenException(message);
                    case HttpStatusCode.NotFound:
                        message = "Not found. The requested object does not exist.";
                        throw new ChefNotFoundException(message);
                    case HttpStatusCode.Conflict:
                        message = "Conflict. The object already exists.";
                        throw new ChefConflictException(message);
                    case HttpStatusCode.Gone:
                        message = "The object is gone";
                        throw new ChefGoneException(message);
                    case HttpStatusCode.RequestEntityTooLarge:
                        message = "Request entity too large. A request may not be larger than 1000000 bytes.";
                        throw new ChefTooLargeException(message);
                    case HttpStatusCode.InternalServerError:
                        message = "Internal Server Error";
                        throw new ChefException(message, ex);
                    default:
                        message = res.StatusDescription;
                        throw new ChefException(message, ex);
                }
            }
        }

        public async Task<T> GenericRequest<T>(HttpMethod method, Uri endpoint, string parameters = "")
        {
            try
            {
                
                HttpResponseMessage finishedRequest;
                string content;
                switch (method.Method.ToString())
                {
                    case "Delete":
                        {
                            finishedRequest = await request.DeleteRequestAsync(endpoint.ToString(), parameters);
                            content = await finishedRequest.Content.ReadAsStringAsync();
                            break;
                        }
                    case "Get":
                        {
                            finishedRequest = await request.GetRequestAsync(endpoint.ToString(), parameters);
                            content = await finishedRequest.Content.ReadAsStringAsync();
                            break;
                        }
                    
                    default:
                        throw new NotSupportedException(method.Method.ToString() + "is not supported");
                }


                var result = JsonConvert.DeserializeObject<T>(content);

                return result;



            }
            catch (WebException ex)
            {

                HttpWebResponse res = (HttpWebResponse)ex.Response;
                string message;
                switch (res.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        message = "Bad request. The contents of the request are not formatted correctly.";
                        throw new ChefException(message);
                    case HttpStatusCode.Unauthorized:
                        message = "Unauthorized. The user or client (" + conn.UserId + ") could not be authenticated. Verify the user/client name, and that the correct key was used to sign the request.";
                        throw new ChefUnauthorizedException(message);
                    case HttpStatusCode.Forbidden:
                        message = "Forbidden. " + conn.UserId + " is not authorized to perform the action.";
                        throw new ChefForbiddenException(message);
                    case HttpStatusCode.NotFound:
                        message = "Not found. The requested object does not exist.";
                        throw new ChefNotFoundException(message);
                    case HttpStatusCode.Conflict:
                        message = "Conflict. The object already exists.";
                        throw new ChefConflictException(message);
                    case HttpStatusCode.Gone:
                        message = "The object is gone";
                        throw new ChefGoneException(message);
                    case HttpStatusCode.RequestEntityTooLarge:
                        message = "Request entity too large. A request may not be larger than 1000000 bytes.";
                        throw new ChefTooLargeException(message);
                    case HttpStatusCode.InternalServerError:
                        message = "Internal Server Error";
                        throw new ChefException(message, ex);
                    default:
                        message = res.StatusDescription;
                        throw new ChefException(message, ex);
                }
            }
        }


    }
}
