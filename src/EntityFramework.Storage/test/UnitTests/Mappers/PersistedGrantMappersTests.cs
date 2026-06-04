// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using FluentAssertions;
using Ourstudio.IdentityServer.EntityFramework.Mappers;
using Ourstudio.IdentityServer.Models;
using Xunit;

namespace Ourstudio.IdentityServer.EntityFramework.UnitTests.Mappers
{
    public class PersistedGrantMappersTests
    {
        [Fact]
        public void All_properties_roundtrip()
        {
            var model = new PersistedGrant
            {
                Key = "key",
                Type = "type",
                SubjectId = "subject_id",
                SessionId = "session_id",
                ClientId = "client_id",
                Description = "description",
                CreationTime = new DateTime(2020, 1, 2, 3, 4, 5),
                Expiration = new DateTime(2021, 2, 3, 4, 5, 6),
                ConsumedTime = new DateTime(2022, 3, 4, 5, 6, 7),
                Data = "data"
            };

            var mappedModel = model.ToEntity().ToModel();

            mappedModel.Should().BeEquivalentTo(model);
        }

        [Fact]
        public void CanMap()
        {
            var model = new PersistedGrant()
            {
                ConsumedTime = new System.DateTime(2020, 02, 03, 4, 5, 6)
            };
            
            var mappedEntity = model.ToEntity();
            mappedEntity.ConsumedTime.Value.Should().Be(new System.DateTime(2020, 02, 03, 4, 5, 6));
            
            var mappedModel = mappedEntity.ToModel();
            mappedModel.ConsumedTime.Value.Should().Be(new System.DateTime(2020, 02, 03, 4, 5, 6));

            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntity);
        }
    }
}