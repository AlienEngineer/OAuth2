using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Credential
{

    public interface IOAuth2
    {

        /// <summary>
        /// Redirects the user to the Authentication Server.
        /// </summary>
        /// <returns></returns>
        RedirectResult Authenticate();

        /// <summary>
        /// Exchanges the code for authentication tokens.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The tokens
        /// </returns>
        Task<Tokens> ExchangeCode(HttpRequestBase request);

    }

    public class Tokens
    {
        public String Access_Token { get; set; }
        public String Id_Token { get; set; }
    }
}
