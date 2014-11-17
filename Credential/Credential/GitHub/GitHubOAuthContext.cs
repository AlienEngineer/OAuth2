namespace Credential.GitHub
{
    public class GitHubOAuthContext : OAuthContext
    {
        public GitHubScope Scope { get; set; }
    }
}