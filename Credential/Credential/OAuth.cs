using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Credential
{
    public abstract class OAuth<TContext> : IOAuth2 where TContext : OAuthContext
    {
        private readonly TContext _context;

        protected OAuth(TContext context)
        {
            _context = context;
        }

        public Uri MakeAuthorizationServerUri()
        {
            var builder = new UriBuilder(_context.GET);

            var query = new NameValueCollection();

            query["client_id"] = _context.ClientId;
            query["redirect_uri"] = _context.RedirectUri;
            query["scope"] = StringifyScope(_context);
            query["state"] = _context.State;

            ApiSpecificQueries(query);

            builder.Query = StringifyQuery(query);

            return new Uri(builder.ToString());
        }

        private String StringifyQuery(NameValueCollection query)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < query.Count; i++)
            {
                sb.AppendFormat("&{0}={1}", query.GetKey(i), query.Get(i));
            }

            return sb
                .ToString()
                .Substring(1); // Remove initial &
        }

        public async Task<Tokens> ExchangeCodeAsync(NameValueCollection queryString)
        {
            var code = queryString["code"];
            //var session_state = request.QueryString["session_state"];
            //var state = request.QueryString["state"];

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(_context.POST, new FormUrlEncodedContent(GetBody(code)));

                var result = await response.Content.ReadAsStringAsync();

                return Decode(result);
            }
        }

        protected abstract Tokens Decode(string result);

        private IEnumerable<KeyValuePair<string, string>> GetBody(string code)
        {
            return new[]
            {
                new KeyValuePair<string, string>("code", code), 
                new KeyValuePair<string, string>("client_id", _context.ClientId), 
                new KeyValuePair<string, string>("client_secret", _context.ClientSecret), 
                new KeyValuePair<string, string>("redirect_uri", _context.RedirectUri), 
            }.Union(GetBody());
        }

        protected abstract IEnumerable<KeyValuePair<string, string>> GetBody();

        protected abstract void ApiSpecificQueries(NameValueCollection query);

        protected abstract string StringifyScope(TContext context);

    }
}