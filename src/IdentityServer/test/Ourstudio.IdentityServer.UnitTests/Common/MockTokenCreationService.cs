using Ourstudio.IdentityServer.Models;
using Ourstudio.IdentityServer.Services;
using System.Threading.Tasks;

namespace Ourstudio.IdentityServer.UnitTests.Common
{
    class MockTokenCreationService : ITokenCreationService
    {
        public string Token { get; set; }

        public Task<string> CreateTokenAsync(Token token)
        {
            return Task.FromResult(Token);
        }
    }
}
