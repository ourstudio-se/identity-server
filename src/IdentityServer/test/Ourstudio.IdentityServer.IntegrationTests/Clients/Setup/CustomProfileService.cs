using System.Threading.Tasks;
using Ourstudio.IdentityServer.Models;
using Ourstudio.IdentityServer.Test;
using Microsoft.Extensions.Logging;

namespace Ourstudio.IdentityServer.IntegrationTests.Clients.Setup
{
    class CustomProfileService : TestUserProfileService
    {
        public CustomProfileService(TestUserStore users, ILogger<TestUserProfileService> logger) : base(users, logger)
        { }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);

            if (context.Subject.Identity.AuthenticationType == "custom")
            {
                var extraClaim = context.Subject.FindFirst("extra_claim");
                if (extraClaim != null)
                {
                    context.IssuedClaims.Add(extraClaim);
                }
            }
        }
    }
}