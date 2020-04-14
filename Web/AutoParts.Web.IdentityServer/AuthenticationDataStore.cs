namespace AutoParts.Web.IdentityServer
{
    using System.Collections.Generic;

    using IdentityModel;

    using IdentityServer4;
    using IdentityServer4.Models;

    /// <summary>
    /// Contains all authentication data for development
    /// </summary>
    public class AuthenticationDataStore
    {
        #region Public Methods

        /// <summary>
        /// Creates <see cref="ApiResource"/> instances
        /// </summary>
        /// <returns>List of <see cref="ApiResource"/></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ApiResources.AutoPartsApi, "AutoParts API")
                {
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.GivenName,
                        JwtClaimTypes.FamilyName
                    }
                }
            };
        }

        /// <summary>
        /// Creates <see cref="Client"/> instances
        /// </summary>
        /// <returns>List of <see cref="Client"/></returns>
        public static IEnumerable<Client> GetClients(string[] webAppCorsOrigins)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = ApiResources.AutoPartsApi,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    RequireClientSecret = false,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        ApiResources.AutoPartsApi
                    },
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AllowedCorsOrigins = webAppCorsOrigins
                },
            };
        }

        /// <summary>
        /// Creates <see cref="IdentityResource"/> instances
        /// </summary>
        /// <returns>List of <see cref="IdentityResource"/></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Phone(),
                new IdentityResource(JwtClaimTypes.Role, new [] { JwtClaimTypes.Role })
            };
        }

        #endregion Public Methods
    }
}
