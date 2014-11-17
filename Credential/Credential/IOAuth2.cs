using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Credential
{

    public interface IOAuth2
    {

        /// <summary>
        /// Redirects the user to the Authentication Server.
        /// </summary>
        /// <returns></returns>
        Uri MakeAuthorizationServerUri();

        /// <summary>
        /// Exchanges the code for authentication tokens.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns>
        /// The tokens
        /// </returns>
        Task<Tokens> ExchangeCodeAsync(NameValueCollection queryString);

    }

    public class Tokens
    {
        public String Access_Token { get; set; }
        public String Id_Token { get; set; }
    }
}
