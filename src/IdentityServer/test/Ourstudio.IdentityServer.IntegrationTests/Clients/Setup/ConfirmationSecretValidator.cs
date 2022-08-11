using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ourstudio.IdentityServer.Models;
using Ourstudio.IdentityServer.Validation;
using Newtonsoft.Json;

namespace Ourstudio.IdentityServer.IntegrationTests.Clients.Setup
{
    public class ConfirmationSecretValidator : ISecretValidator
    {
        public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
        {
            if (secrets.Any())
            {
                if (secrets.First().Type == "confirmation.test")
                {
                    var cnf = new Dictionary<string, string>
                    {
                        { "x5t#S256", "foo" }
                    };

                    var result = new SecretValidationResult
                    {
                        Success = true,
                        Confirmation = JsonConvert.SerializeObject(cnf)
                    };

                    return Task.FromResult(result);
                }
            }

            return Task.FromResult(new SecretValidationResult { Success = false });
        }
    }
}