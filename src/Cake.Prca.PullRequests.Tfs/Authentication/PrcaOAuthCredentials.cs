﻿namespace Cake.Prca.PullRequests.Tfs.Authentication
{
    /// <summary>
    /// Credentials for OAuth authentication.
    /// </summary>
    public class PrcaOAuthCredentials : IPrcaCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrcaOAuthCredentials"/> class.
        /// </summary>
        /// <param name="accessToken">OAuth access token.</param>
        public PrcaOAuthCredentials(string accessToken)
        {
            accessToken.NotNullOrWhiteSpace(nameof(accessToken));

            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Gets the OAuth access token.
        /// </summary>
        public string AccessToken { get; private set; }
    }
}
