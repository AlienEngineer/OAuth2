using System;

namespace Credential.GitHub
{
    public class GitHubScope
    {
        /// <summary>
        /// Grants read-only access to public information (includes public user profile info, public repository info, and gists)
        /// </summary>
        public Boolean NoScope { get; set; }

        /// <summary>
        /// Grants read/write access to profile info only. Note that this scope includes user:email and user:follow.
        /// </summary>
        public Boolean User { get; set; }

        /// <summary>
        /// Grants read access to a user’s email addresses.
        /// </summary>
        public Boolean Email { get; set; }

        /// <summary>
        /// Grants access to follow or unfollow other users.
        /// </summary>
        public Boolean Follow { get; set; }
        
        /// <summary>
        /// Grants read/write access to code, commit statuses, and deployment statuses for public repositories and organizations.
        /// </summary>
        public Boolean Public { get; set; }

        /// <summary>
        /// Grants read/write access to code, commit statuses, and deployment statuses for public and private repositories and organizations.
        /// </summary>
        public Boolean Repo { get; set; }

        /// <summary>
        /// Grants access to deployment statuses for public and private repositories. This scope is only necessary to grant other users or services access to deployment statuses, without granting access to the code.
        /// </summary>
        public Boolean Deployment { get; set; }

        /// <summary>
        /// Grants read/write access to public and private repository commit statuses. This scope is only necessary to grant other users or services access to private repository commit statuses without granting access to the code.
        /// </summary>
        public Boolean Status { get; set; }

        /// <summary>
        /// Grants access to delete adminable repositories.
        /// </summary>
        public Boolean Delete { get; set; }

        /// <summary>
        /// Grants read access to a user’s notifications. repo also provides this access.
        /// </summary>
        public Boolean Notifications { get; set; }

        /// <summary>
        /// Grants write access to gists.
        /// </summary>
        public Boolean Gist { get; set; }

        /// <summary>
        /// Grants read and ping access to hooks in public or private repositories.
        /// </summary>
        public Boolean ReadHooks { get; set; }

        /// <summary>
        /// Grants read, write, and ping access to hooks in public or private repositories.
        /// </summary>
        public Boolean WriteHooks { get; set; }

        /// <summary>
        /// Grants read, write, ping, and delete access to hooks in public or private repositories.
        /// </summary>
        public Boolean AdminHooks { get; set; }

        /// <summary>
        /// Read-only access to organization, teams, and membership.
        /// </summary>
        public Boolean ReadOrganizations { get; set; }

        /// <summary>
        /// Publicize and unpublicize organization membership.
        /// </summary>
        public Boolean WriteOrganizations { get; set; }

        /// <summary>
        /// Fully manage organization, teams, and memberships.
        /// </summary>
        public Boolean AdminOrganizations { get; set; }

        /// <summary>
        /// List and view details for public keys.
        /// </summary>
        public Boolean ReadPublicKey { get; set; }

        /// <summary>
        /// Create, list, and view details for public keys.
        /// </summary>
        public Boolean WritePublicKey { get; set; }

        /// <summary>
        /// Fully manage public keys.
        /// </summary>
        public Boolean AdminPublicKey { get; set; }
    }
}