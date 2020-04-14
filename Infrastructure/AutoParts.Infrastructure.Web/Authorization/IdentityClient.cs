using System.Net.Http;
using System.Threading.Tasks;
using AutoParts.Infrastructure.Exceptions;
using AutoParts.Infrastructure.Web.Options;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace AutoParts.Infrastructure.Web.Authorization
{
    /// <summary>
    /// Interactor with Identity Server
    /// </summary>
    public class IdentityClient : IIdentityClient
    {
        #region Private fields

        private readonly HttpClient httpClient;

        private readonly IdentityOptions identityOptions;

        #endregion Private fields

        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityClient"/> class. 
        /// </summary>
        /// <param name="identityOptions"></param>
        /// <param name="httpClient"></param>
        public IdentityClient(HttpClient httpClient, IOptions<IdentityOptions> identityOptions)
        {
            this.identityOptions = identityOptions.Value;
            this.httpClient = httpClient;
        }

        #endregion Public constructors

        #region Public methods

        /// <summary>
        /// Get Access token by user credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<TokenResponse> GetAccessTokenAsync(string email, string password)
        {
            var tokenEndpoint = await this.GetTokenEndpoint();
            var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = this.identityOptions.ClientId,
                ClientSecret = this.identityOptions.ClientSecret,
                UserName = email,
                Password = password
            });
            return tokenResponse;
        }

        /// <summary>
        /// Get Access token by refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TokenResponse> GetRefreshedTokenAsync(string token)
        {
            var tokenEndpoint = await this.GetTokenEndpoint();
            var tokenResponse = await httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = this.identityOptions.ClientId,
                ClientSecret = this.identityOptions.ClientSecret,
                RefreshToken = token
            });
            return tokenResponse;
        }

        /// <summary>
        /// Revoke token async
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TokenRevocationResponse> RevokeAccessTokenAsync(string token)
        {
            var revokeEndpoint = await GetRevokeEndpoint();
            var tokenResponse = await httpClient.RevokeTokenAsync(new TokenRevocationRequest
            {
                Address = revokeEndpoint,
                ClientId = identityOptions.ClientId,
                ClientSecret = identityOptions.ClientSecret,
                Token = token
            });
            return tokenResponse;
        }

        /// <summary>
        /// Get endpoint necessary to generate token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTokenEndpoint()
        {
            var disco = await GetDiscoveryDocument();
            return disco.TokenEndpoint;
        }

        /// <summary>
        /// Get endpoint necessary to revoke token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetRevokeEndpoint()
        {
            var disco = await GetDiscoveryDocument();
            return disco.RevocationEndpoint;
        }

        /// <summary>
        /// Get discovery document
        /// </summary>
        /// <returns></returns>
        public async Task<DiscoveryDocumentResponse> GetDiscoveryDocument()
        {
            var dicoveryDocument = await httpClient.GetDiscoveryDocumentAsync(this.identityOptions.IdentityServer);

            if (dicoveryDocument.IsError)
            {
                throw new ApiException();
            }
            
            return dicoveryDocument;
        }

        #endregion Public methods
    }
}
