using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Credential.GitHub;

namespace Credential
{
    public class GitHubOAuth : OAuth<GitHubOAuthContext>
    {
        public GitHubOAuth(GitHubOAuthContext context)
            : base(context)
        {
        }

        protected override Tokens Decode(string result)
        {
            var token = result.Split('&')
                .Select(entry =>
                {
                    var pair = entry.Split('=');

                    return new KeyValuePair<String, String>(pair[0], pair[1]);
                })
                .FirstOrDefault(entry => "access_token".Equals(entry.Key));

            return new Tokens
            {
                Access_Token = token.Value
            };
        }

        protected override IEnumerable<KeyValuePair<string, string>> GetBody()
        {
            yield return new KeyValuePair<string, string>();
        }

        protected override void ApiSpecificQueries(NameValueCollection query)
        {
            // Nothing to add here
        }

        protected override string StringifyScope(GitHubOAuthContext context)
        {
            var sb = new StringBuilder();
            var scope = context.Scope;
            
            if (scope.NoScope) { 
                sb.Append("");
                return sb.ToString();
            }


            if (scope.User) { sb.Append("+user"); }
            if (scope.Email) { sb.Append("+user:email"); }
            if (scope.Follow) { sb.Append("+user:follow"); }
            if (scope.Public) { sb.Append("+public_repo"); }
            if (scope.Repo) { sb.Append("+repo"); }
            if (scope.Deployment) { sb.Append("+repo_deployment"); }
            if (scope.Status) { sb.Append("+repo:status"); }
            if (scope.Delete) { sb.Append("+delete_repo"); }
            if (scope.Notifications) { sb.Append("+notifications"); }
            if (scope.Gist) { sb.Append("+gist"); }
            if (scope.ReadHooks) { sb.Append("+read:repo_hook"); }
            if (scope.WriteHooks) { sb.Append("+write:repo_hook"); }
            if (scope.AdminHooks) { sb.Append("+admin:repo_hook"); }
            if (scope.ReadOrganizations) { sb.Append("+read:org"); }
            if (scope.WriteOrganizations) { sb.Append("+write:org"); }
            if (scope.AdminOrganizations) { sb.Append("+admin:org"); }
            if (scope.ReadPublicKey) { sb.Append("+read:public_key"); }
            if (scope.WritePublicKey) { sb.Append("+write:public_key"); }
            if (scope.AdminPublicKey) { sb.Append("+admin:public_key"); }

            return sb
                .ToString()
                .Substring(1); // Remove initial +
        }

    }
}
