// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using FluentAssertions;
using Ourstudio.IdentityServer.EntityFramework.Mappers;
using Ourstudio.IdentityServer.Models;
using Xunit;
using Client = Ourstudio.IdentityServer.Models.Client;

namespace Ourstudio.IdentityServer.EntityFramework.UnitTests.Mappers
{
    public class ClientMappersTests
    {
        [Fact]
        public void All_properties_roundtrip()
        {
            var model = new Client
            {
                Enabled = false,
                ClientId = "client_id",
                ProtocolType = "custom",
                ClientSecrets = { new Secret("secret_value", "secret_desc", new DateTime(2030, 1, 1)) { Type = "custom_type" } },
                RequireClientSecret = false,
                ClientName = "client_name",
                Description = "description",
                ClientUri = "https://client_uri",
                LogoUri = "https://logo_uri",
                RequireConsent = true,
                AllowRememberConsent = false,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowedGrantTypes = { "authorization_code" },
                RequirePkce = false,
                AllowPlainTextPkce = true,
                RequireRequestObject = true,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { "https://redirect" },
                PostLogoutRedirectUris = { "https://post_logout" },
                FrontChannelLogoutUri = "https://front_channel",
                FrontChannelLogoutSessionRequired = false,
                BackChannelLogoutUri = "https://back_channel",
                BackChannelLogoutSessionRequired = false,
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile" },
                IdentityTokenLifetime = 1,
                AllowedIdentityTokenSigningAlgorithms = { "RS256", "ES256" },
                AccessTokenLifetime = 2,
                AuthorizationCodeLifetime = 3,
                AbsoluteRefreshTokenLifetime = 4,
                SlidingRefreshTokenLifetime = 5,
                ConsentLifetime = 6,
                RefreshTokenUsage = TokenUsage.ReUse,
                UpdateAccessTokenClaimsOnRefresh = true,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AccessTokenType = AccessTokenType.Reference,
                EnableLocalLogin = false,
                IdentityProviderRestrictions = { "google" },
                IncludeJwtId = false,
                Claims = { new ClientClaim("claim_type", "claim_value", ClaimValueTypes.String) },
                AlwaysSendClientClaims = true,
                ClientClaimsPrefix = "prefix_",
                PairWiseSubjectSalt = "salt",
                UserSsoLifetime = 7,
                UserCodeType = "user_code_type",
                DeviceCodeLifetime = 8,
                AllowedCorsOrigins = { "https://cors" },
                Properties = { { "key", "value" } }
            };

            var mappedModel = model.ToEntity().ToModel();

            mappedModel.Should().BeEquivalentTo(model);
        }

        [Fact]
        public void Can_Map()
        {
            var model = new Client();
            var mappedEntity = model.ToEntity();
            var mappedModel = mappedEntity.ToModel();

            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntity);
        }

        [Fact]
        public void Properties_Map()
        {
            var model = new Client()
            {
                Properties =
                {
                    {"foo1", "bar1"},
                    {"foo2", "bar2"},
                }
            };


            var mappedEntity = model.ToEntity();

            mappedEntity.Properties.Count.Should().Be(2);
            var foo1 = mappedEntity.Properties.FirstOrDefault(x => x.Key == "foo1");
            foo1.Should().NotBeNull();
            foo1.Value.Should().Be("bar1");
            var foo2 = mappedEntity.Properties.FirstOrDefault(x => x.Key == "foo2");
            foo2.Should().NotBeNull();
            foo2.Value.Should().Be("bar2");



            var mappedModel = mappedEntity.ToModel();

            mappedModel.Properties.Count.Should().Be(2);
            mappedModel.Properties.ContainsKey("foo1").Should().BeTrue();
            mappedModel.Properties.ContainsKey("foo2").Should().BeTrue();
            mappedModel.Properties["foo1"].Should().Be("bar1");
            mappedModel.Properties["foo2"].Should().Be("bar2");
        }

        [Fact]
        public void duplicates_properties_in_db_map()
        {
            var entity = new Ourstudio.IdentityServer.EntityFramework.Entities.Client
            {
                Properties = new System.Collections.Generic.List<Entities.ClientProperty>()
                {
                    new Entities.ClientProperty{Key = "foo1", Value = "bar1"},
                    new Entities.ClientProperty{Key = "foo1", Value = "bar2"},
                }
            };

            Action modelAction = () => entity.ToModel();
            modelAction.Should().Throw<Exception>();
        }

        [Fact]
        public void missing_values_should_use_defaults()
        {
            var entity = new Ourstudio.IdentityServer.EntityFramework.Entities.Client
            {
                ClientSecrets = new System.Collections.Generic.List<Entities.ClientSecret>
                {
                    new Entities.ClientSecret
                    {
                    }
                }
            };

            var def = new Client
            {
                ClientSecrets = { new Models.Secret("foo") }
            };

            var model = entity.ToModel();
            model.ProtocolType.Should().Be(def.ProtocolType);
            model.ClientSecrets.First().Type.Should().Be(def.ClientSecrets.First().Type);
        }
    }
}