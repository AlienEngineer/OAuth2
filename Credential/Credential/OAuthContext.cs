using System;

namespace Credential
{
    public class OAuthContext
    {
        public Uri GET { get; set; }
        public Uri POST { get; set; }


        public String ClientId { get; set; }
        public String ClientSecret { get; set; }

        public String RedirectUri { get; set; }
        public String State { get; set; }
    }
}