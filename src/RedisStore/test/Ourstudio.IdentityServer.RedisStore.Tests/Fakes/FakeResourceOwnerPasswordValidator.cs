using IdentityModel;
using Ourstudio.IdentityServer.Validation;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ourstudio.IdentityServer.RedisStore.Tests.Cache
{
    class FakeResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(subject: "1",
                authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                claims: new List<Claim> { });

            return Task.CompletedTask;
        }
    }
}
