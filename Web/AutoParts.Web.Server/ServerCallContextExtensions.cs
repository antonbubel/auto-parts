namespace AutoParts.Web.Server
{
    using Grpc.Core;

    using System.Linq;
    using System.Security.Claims;

    public static class ServerCallContextExtensions
    {
        public static long? GetLoggedInUserId(this ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            var user = httpContext.User;

            if (httpContext.User.Identity.IsAuthenticated)
            {
                return long.Parse(httpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
            }

            return null;
        }
    }
}
