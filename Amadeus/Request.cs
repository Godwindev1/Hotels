using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;
using System.Text;
using Hotel.Data.Amadeus.data;
using Microsoft.VisualBasic;


namespace Hotel.Amadeus
{

        public class Request
        {

            /// <summary>
            /// The HTTPClient verb to use for API calls.
            /// </summary>
            /// <value>HTTPClient verb.</value>
            public string verb { get; set; }

            /// <summary>
            /// The scheme to use for API calls. 
            /// </summary>
            /// <value>The scheme.</value>
            public string scheme { get; set; }

            /// <summary>
            /// The host domain to use for API calls.
            /// </summary>
            /// <value>The host.</value>
            public string host { get; set; }

            /// <summary>
            /// The path use for API calls.
            /// </summary>
            /// <value>The path.</value>
            public string path { get; set; }


            public HttpMessageHeaders? body { get; set; }

            public HttpMessageHeaders? _params { get; set; }

            public static AccessToken ? BearerToken { get; set; } = null;


            /// <summary>
            /// The headers for this request.
            /// </summary>
            /// <value>The headers.</value>
            public Dictionary<string, string> headers { get; set; }

            /// <summary>
            /// The full URI for this request, based on the
            /// verb, port, path, host, etc.
            /// </summary>
            /// <value>The URI.</value>
            public string uri { get; set; }


            public Request(string verb, string path, HttpMessageHeaders? URLqueryParameters, HttpMessageHeaders? body, string host)
            {

                this.verb = verb;
                this.host = host; // amadeus test host
                this.path = path;
                this.body = body;
                this._params = URLqueryParameters;


                scheme = Constants.HTTPS;

                prepareUrl();
                prepareHeaders();
            }

            public static void InitializeBearerToken(AccessToken accessToken)
            {
                BearerToken = new AccessToken();
                BearerToken.access_token = accessToken.access_token;
            }

            public static void ClearPreviousBearerToken()
            {
                BearerToken =  null;
            }


            public void prepareUrl()
            {
                if (_params != null)
                {
                    this.uri = scheme + "://" + host + "/" + path + "?" + _params.toQueryString();
                }
                else
                {
                    this.uri = scheme + "://" + host + "/" + path;
                }
            }

            /// <summary>
            /// Prepares the headers to be sent in the request
            /// </summary>
            public void prepareHeaders()
            {
                this.headers = new Dictionary<string, string>();
                headers[Constants.ACCEPT] = "application/json, application/vnd.amadeus+json";
                if (Request.BearerToken != null)
                {
                    headers[Constants.AUTHORIZATION] = "Bearer " + Request.BearerToken.access_token;
                }
            }

       

            public static HttpRequestMessage ConvertToHttpRequestMessage(Request HTTPRequest)
            {
                HttpRequestMessage Request = new HttpRequestMessage()
                {
                    Method = new HttpMethod(HTTPRequest.verb),
                    RequestUri = new Uri(HTTPRequest.uri),
                };

                if (HTTPRequest.body != null)
                {
                    Request.Content = new FormUrlEncodedContent(HTTPRequest.body);
                }

                foreach (KeyValuePair<string, string> kvp in HTTPRequest.headers)
                {
                    Request.Headers.Add(kvp.Key, kvp.Value);
                }

                return Request;

            }



            public override string ToString()
            {
                var sb = new StringBuilder(this.GetType() + "(");
                sb.Append("verb=" + verb + ",");
                sb.Append("host=" + host + ",");
                sb.Append("path=" + path + ",");
                sb.Append("params=" + _params + ",");
                sb.Append("bearerToken=" + Request.BearerToken + ",");
                return sb.ToString();
            }

        }
    

}
