// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using FluentAssertions;
using Ourstudio.IdentityServer.EntityFramework.Mappers;
using Ourstudio.IdentityServer.Models;
using Xunit;

namespace Ourstudio.IdentityServer.EntityFramework.UnitTests.Mappers
{
    public class IdentityResourcesMappersTests
    {
        [Fact]
        public void All_properties_roundtrip()
        {
            var model = new IdentityResource
            {
                Enabled = false,
                Name = "name",
                DisplayName = "display_name",
                Description = "description",
                Required = true,
                Emphasize = true,
                ShowInDiscoveryDocument = false,
                UserClaims = { "c1", "c2" },
                Properties = { { "key", "value" } }
            };

            var mappedModel = model.ToEntity().ToModel();

            mappedModel.Should().BeEquivalentTo(model);
        }

        [Fact]
        public void CanMapIdentityResources()
        {
            var model = new IdentityResource();
            var mappedEntity = model.ToEntity();
            var mappedModel = mappedEntity.ToModel();

            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntity);
        }
    }
}