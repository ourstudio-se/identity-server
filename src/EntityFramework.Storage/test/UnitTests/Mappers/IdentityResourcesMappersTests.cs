// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Ourstudio.IdentityServer.EntityFramework.Mappers;
using Ourstudio.IdentityServer.Models;
using Xunit;

namespace Ourstudio.IdentityServer.EntityFramework.UnitTests.Mappers
{
    public class IdentityResourcesMappersTests
    {
        [Fact]
        public void IdentityResourceAutomapperConfigurationIsValid()
        {
            IdentityResourceMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
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