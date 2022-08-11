using System.Threading.Tasks;
using FluentAssertions;
using Ourstudio.IdentityServer.Services;
using Xunit;

namespace Ourstudio.IdentityServer.UnitTests.Services.Default
{
    public class NumericUserCodeGeneratorTests
    {
        [Fact]
        public async Task GenerateAsync_should_return_expected_code()
        {
            var sut = new NumericUserCodeGenerator();

            var userCode = await sut.GenerateAsync();
            var userCodeInt = int.Parse(userCode);

            userCodeInt.Should().BeGreaterOrEqualTo(100000000);
            userCodeInt.Should().BeLessOrEqualTo(999999999);
        }
    }
}