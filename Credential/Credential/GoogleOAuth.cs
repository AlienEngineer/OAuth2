using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Credential.Google;
using Newtonsoft.Json;

namespace Credential
{
    public class GoogleOAuth : OAuth<GoogleOAuthContext>
    {
        public GoogleOAuth(GoogleOAuthContext context): base(context)
        {
        }

        

        protected override IEnumerable<KeyValuePair<string, string>> GetBody()
        {
            yield return new KeyValuePair<string, string>("grant_type", "authorization_code");
        }

        protected override void ApiSpecificQueries(NameValueCollection query)
        {
            query["response_type"] = "code";
            query["access_type"] = "online";
        }

        protected override Tokens Decode(string result)
        {
            return JsonConvert.DeserializeObject<Tokens>(result);
        }
        
        protected override string StringifyScope(GoogleOAuthContext context)
        {
            var builder = new StringBuilder();

            if (context.Scope.OpenId) { builder.Append("+openid"); }
            if (context.Scope.Profile) { builder.Append("+profile"); }
            if (context.Scope.Email) { builder.Append("+email");}

            return builder
                .ToString()
                .Substring(1); // Remove initial +
        }
    }
}