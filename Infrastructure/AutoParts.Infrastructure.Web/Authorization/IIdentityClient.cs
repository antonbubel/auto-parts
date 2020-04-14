using IdentityModel.Client;
using System.Threading.Tasks;

namespace AutoParts.Infrastructure.Web.Authorization
{
    /// <summary>
    /// Defines methods for interacting with Identity Server
    /// </summary>
    public interface IIdentityClient
    {
        /// <summary>
        /// Get Access token by user credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<TokenResponse> GetAccessTokenAsync(string email, string password);

        /// <summary>
        /// Get Access token by refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TokenResponse> GetRefreshedTokenAsync(string token);

        /// <summary>
        /// Revoke token async
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TokenRevocationResponse> RevokeAccessTokenAsync(string token);

        /// <summary>
        /// Get endpoint necessary to generate token
        /// </summary>
        /// <returns></returns>
        Task<string> GetTokenEndpoint();

        /// <summary>
        /// Get endpoint necessary to revoke token
        /// </summary>
        /// <returns></returns>
        Task<string> GetRevokeEndpoint();
    }
}
