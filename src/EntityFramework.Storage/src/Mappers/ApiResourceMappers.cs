// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using Ourstudio.IdentityServer.EntityFramework.Entities;

namespace Ourstudio.IdentityServer.EntityFramework.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for API resources.
    /// </summary>
    public static class ApiResourceMappers
    {
        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.ApiResource ToModel(this ApiResource entity)
        {
            if (entity == null) return null;

            return new Models.ApiResource
            {
                Enabled = entity.Enabled,
                Name = entity.Name,
                DisplayName = entity.DisplayName,
                Description = entity.Description,
                ShowInDiscoveryDocument = entity.ShowInDiscoveryDocument,
                AllowedAccessTokenSigningAlgorithms = AllowedSigningAlgorithmsConverter.Convert(entity.AllowedAccessTokenSigningAlgorithms),
                ApiSecrets = entity.Secrets?.Select(x => new Models.Secret
                {
                    Type = x.Type,
                    Value = x.Value,
                    Description = x.Description,
                    Expiration = x.Expiration
                }).ToList() ?? new List<Models.Secret>(),
                Scopes = entity.Scopes?.Select(x => x.Scope).ToList() ?? new List<string>(),
                UserClaims = entity.UserClaims?.Select(x => x.Type).ToList() ?? new List<string>(),
                Properties = entity.Properties == null
                    ? new Dictionary<string, string>()
                    : entity.Properties.ToDictionary(x => x.Key, x => x.Value)
            };
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ApiResource ToEntity(this Models.ApiResource model)
        {
            if (model == null) return null;

            return new ApiResource
            {
                Enabled = model.Enabled,
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument,
                AllowedAccessTokenSigningAlgorithms = AllowedSigningAlgorithmsConverter.Convert(model.AllowedAccessTokenSigningAlgorithms),
                Secrets = model.ApiSecrets?.Select(x => new ApiResourceSecret
                {
                    Type = x.Type,
                    Value = x.Value,
                    Description = x.Description,
                    Expiration = x.Expiration
                }).ToList(),
                Scopes = model.Scopes?.Select(x => new ApiResourceScope { Scope = x }).ToList(),
                UserClaims = model.UserClaims?.Select(x => new ApiResourceClaim { Type = x }).ToList(),
                Properties = model.Properties?.Select(x => new ApiResourceProperty { Key = x.Key, Value = x.Value }).ToList()
            };
        }
    }
}
