using System.Text;

namespace Hotel.Amadeus
{
    public class HttpMessageHeaders : Dictionary<string, string>
    {
        public HttpMessageHeaders()
        {
        }

        public HttpMessageHeaders(string ParameterName, string ParameterValue)
        {
            this.Add(ParameterName, ParameterValue);
        }

        public void AddParameter(string ParameterName, string ParameterValue)
        {
            this.Add(ParameterName, ParameterValue);
        }

        public void RemoveParameter(string ParameterName)
        {
            this.Remove(ParameterName);
        }

        public string GetParameter(string ParameterName)
        {
            return this[ParameterName];
        }

        public string toQueryString()
        {
            if (this != null)
            {
                StringBuilder query = new StringBuilder();
                bool first = true;
                foreach (KeyValuePair<string, string> entry in this)
                {
                    if (!first)
                    {
                        query.Append("&");
                    }
                    first = false;
                    try
                    {
                        query.Append(System.Web.HttpUtility.UrlEncode(entry.Key, Encoding.UTF8));
                        query.Append("=");
                        query.Append(System.Web.HttpUtility.UrlEncode(entry.Value, Encoding.UTF8));
                    }
                    catch { }
                }

                return query.ToString();
            }
            else
            {
                return "";
            }

        }
    }
}
