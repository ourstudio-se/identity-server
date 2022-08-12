# Ourstudio.IdentityServer vs IdentityServer4

Ourstudio.IdentityServer is a re-packaged [OpenID Connect](https://openid.net/connect/) and [OAuth 2.0](https://tools.ietf.org/html/rfc6749) implementation based on [IdentityServer4](https://github.com/IdentityServer/IdentityServer4). IdentityServer4 was founded by [Dominick Baier](https://twitter.com/leastprivilege) and [Brock Allen](https://twitter.com/brocklallen) and is officially [certified](https://openid.net/certification/) by the [OpenID Foundation](https://openid.net) and thus spec-compliant and interoperable.

IdentityServer4 is part of the [.NET Foundation](https://www.dotnetfoundation.org/), and operates under their [code of conduct](https://www.dotnetfoundation.org/code-of-conduct). It is licensed under [Apache 2](https://opensource.org/licenses/Apache-2.0) (an OSI approved license).

## Documentation

The documentation for Ourstudio.IdentityServer is based on the original documentation for IdentityServer4, and is found at https://ourstudio-se.github.io/identity-server.

## Standing on the shoulders of giants

Ourstudio.IdentityServer was created to fill the void IdentityServer4 left when discontinued in '22, and aims to be as performant and as reliant as IdentityServer4 but supporting the latest .NET versions and the latest dependency versions. The work done by Dominick and Brock is not to be diminished - we're truly continuing the work of excellent developers.

## The current state

Ourstudio.IdentityServer v1 supports .NET6 and should be a drop-in replacement for the latest IdentityServer4 release.

## The future state

We're aiming to reduce the surface area of the framework and might deprecate and remove different parts of it - such as legacy grant types and crpto algorithms not deemed best practice.

## Acknowledgements
Ourstudio.IdentityServer is built using the following great open source projects and free services:

* [ASP.NET Core](https://github.com/dotnet/aspnetcore)
* [Bullseye](https://github.com/adamralph/bullseye)
* [SimpleExec](https://github.com/adamralph/simple-exec)
* [Json.Net](http://www.newtonsoft.com/json)
* [XUnit](https://xunit.github.io/)
* [Fluent Assertions](http://www.fluentassertions.com/)
* [GitReleaseManager](https://github.com/GitTools/GitReleaseManager)
