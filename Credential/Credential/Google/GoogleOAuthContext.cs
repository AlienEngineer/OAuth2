using System;

namespace Credential.Google
{
    public class GoogleOAuthContext : OAuthContext
    {
        public GoogleScope Scope { get; set; }
        
    }
}