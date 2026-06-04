// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Ourstudio.IdentityServer.Models;

namespace Ourstudio.IdentityServer.EntityFramework.Mappers
{
    /// <summary>
    /// Extension methods to map to/from entity/model for clients.
    /// </summary>
    public static class ClientMappers
    {
        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.Client ToModel(this Entities.Client entity)
        {
            if (entity == null) return null;

            return new Models.Client
            {
                Enabled = entity.Enabled,
                ClientId = entity.ClientId,
                ProtocolType = entity.ProtocolType,
                RequireClientSecret = entity.RequireClientSecret,
                ClientName = entity.ClientName,
                Description = entity.Description,
                ClientUri = entity.ClientUri,
                LogoUri = entity.LogoUri,
                RequireConsent = entity.RequireConsent,
                AllowRememberConsent = entity.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = entity.AlwaysIncludeUserClaimsInIdToken,
                RequirePkce = entity.RequirePkce,
                AllowPlainTextPkce = entity.AllowPlainTextPkce,
                RequireRequestObject = entity.RequireRequestObject,
                AllowAccessTokensViaBrowser = entity.AllowAccessTokensViaBrowser,
                FrontChannelLogoutUri = entity.FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = entity.FrontChannelLogoutSessionRequired,
                BackChannelLogoutUri = entity.BackChannelLogoutUri,
                BackChannelLogoutSessionRequired = entity.BackChannelLogoutSessionRequired,
                AllowOfflineAccess = entity.AllowOfflineAccess,
                IdentityTokenLifetime = entity.IdentityTokenLifetime,
                AllowedIdentityTokenSigningAlgorithms = AllowedSigningAlgorithmsConverter.Convert(entity.AllowedIdentityTokenSigningAlgorithms),
                AccessTokenLifetime = entity.AccessTokenLifetime,
                AuthorizationCodeLifetime = entity.AuthorizationCodeLifetime,
                ConsentLifetime = entity.ConsentLifetime,
                AbsoluteRefreshTokenLifetime = entity.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = entity.SlidingRefreshTokenLifetime,
                RefreshTokenUsage = (TokenUsage)entity.RefreshTokenUsage,
                UpdateAccessTokenClaimsOnRefresh = entity.UpdateAccessTokenClaimsOnRefresh,
                RefreshTokenExpiration = (TokenExpiration)entity.RefreshTokenExpiration,
                AccessTokenType = (AccessTokenType)entity.AccessTokenType,
                EnableLocalLogin = entity.EnableLocalLogin,
                IncludeJwtId = entity.IncludeJwtId,
                AlwaysSendClientClaims = entity.AlwaysSendClientClaims,
                ClientClaimsPrefix = entity.ClientClaimsPrefix,
                PairWiseSubjectSalt = entity.PairWiseSubjectSalt,
                UserSsoLifetime = entity.UserSsoLifetime,
                UserCodeType = entity.UserCodeType,
                DeviceCodeLifetime = entity.DeviceCodeLifetime,
                AllowedGrantTypes = entity.AllowedGrantTypes?.Select(x => x.GrantType).ToList() ?? new List<string>(),
                RedirectUris = entity.RedirectUris?.Select(x => x.RedirectUri).ToList() ?? new List<string>(),
                PostLogoutRedirectUris = entity.PostLogoutRedirectUris?.Select(x => x.PostLogoutRedirectUri).ToList() ?? new List<string>(),
                AllowedScopes = entity.AllowedScopes?.Select(x => x.Scope).ToList() ?? new List<string>(),
                IdentityProviderRestrictions = entity.IdentityProviderRestrictions?.Select(x => x.Provider).ToList() ?? new List<string>(),
                AllowedCorsOrigins = entity.AllowedCorsOrigins?.Select(x => x.Origin).ToList() ?? new List<string>(),
                ClientSecrets = entity.ClientSecrets?.Select(x => new Secret
                {
                    Type = x.Type,
                    Value = x.Value,
                    Description = x.Description,
                    Expiration = x.Expiration
                }).ToList() ?? new List<Secret>(),
                Claims = entity.Claims?.Select(x => new ClientClaim(x.Type, x.Value, ClaimValueTypes.String)).ToList() ?? new List<ClientClaim>(),
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
        public static Entities.Client ToEntity(this Models.Client model)
        {
            if (model == null) return null;

            return new Entities.Client
            {
                Enabled = model.Enabled,
                ClientId = model.ClientId,
                ProtocolType = model.ProtocolType,
                RequireClientSecret = model.RequireClientSecret,
                ClientName = model.ClientName,
                Description = model.Description,
                ClientUri = model.ClientUri,
                LogoUri = model.LogoUri,
                RequireConsent = model.RequireConsent,
                AllowRememberConsent = model.AllowRememberConsent,
                AlwaysIncludeUserClaimsInIdToken = model.AlwaysIncludeUserClaimsInIdToken,
                RequirePkce = model.RequirePkce,
                AllowPlainTextPkce = model.AllowPlainTextPkce,
                RequireRequestObject = model.RequireRequestObject,
                AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser,
                FrontChannelLogoutUri = model.FrontChannelLogoutUri,
                FrontChannelLogoutSessionRequired = model.FrontChannelLogoutSessionRequired,
                BackChannelLogoutUri = model.BackChannelLogoutUri,
                BackChannelLogoutSessionRequired = model.BackChannelLogoutSessionRequired,
                AllowOfflineAccess = model.AllowOfflineAccess,
                IdentityTokenLifetime = model.IdentityTokenLifetime,
                AllowedIdentityTokenSigningAlgorithms = AllowedSigningAlgorithmsConverter.Convert(model.AllowedIdentityTokenSigningAlgorithms),
                AccessTokenLifetime = model.AccessTokenLifetime,
                AuthorizationCodeLifetime = model.AuthorizationCodeLifetime,
                ConsentLifetime = model.ConsentLifetime,
                AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime,
                RefreshTokenUsage = (int)model.RefreshTokenUsage,
                UpdateAccessTokenClaimsOnRefresh = model.UpdateAccessTokenClaimsOnRefresh,
                RefreshTokenExpiration = (int)model.RefreshTokenExpiration,
                AccessTokenType = (int)model.AccessTokenType,
                EnableLocalLogin = model.EnableLocalLogin,
                IncludeJwtId = model.IncludeJwtId,
                AlwaysSendClientClaims = model.AlwaysSendClientClaims,
                ClientClaimsPrefix = model.ClientClaimsPrefix,
                PairWiseSubjectSalt = model.PairWiseSubjectSalt,
                UserSsoLifetime = model.UserSsoLifetime,
                UserCodeType = model.UserCodeType,
                DeviceCodeLifetime = model.DeviceCodeLifetime,
                // Model collections are field-initialized, so these are non-null in practice. An explicitly-null
                // model collection maps to a null entity list (EF treats that as "no children"), which differs
                // harmlessly from AutoMapper's previous null-to-empty behavior.
                AllowedGrantTypes = model.AllowedGrantTypes?.Select(x => new Entities.ClientGrantType { GrantType = x }).ToList(),
                RedirectUris = model.RedirectUris?.Select(x => new Entities.ClientRedirectUri { RedirectUri = x }).ToList(),
                PostLogoutRedirectUris = model.PostLogoutRedirectUris?.Select(x => new Entities.ClientPostLogoutRedirectUri { PostLogoutRedirectUri = x }).ToList(),
                AllowedScopes = model.AllowedScopes?.Select(x => new Entities.ClientScope { Scope = x }).ToList(),
                IdentityProviderRestrictions = model.IdentityProviderRestrictions?.Select(x => new Entities.ClientIdPRestriction { Provider = x }).ToList(),
                AllowedCorsOrigins = model.AllowedCorsOrigins?.Select(x => new Entities.ClientCorsOrigin { Origin = x }).ToList(),
                ClientSecrets = model.ClientSecrets?.Select(x => new Entities.ClientSecret
                {
                    Type = x.Type,
                    Value = x.Value,
                    Description = x.Description,
                    Expiration = x.Expiration
                }).ToList(),
                Claims = model.Claims?.Select(x => new Entities.ClientClaim { Type = x.Type, Value = x.Value }).ToList(),
                Properties = model.Properties?.Select(x => new Entities.ClientProperty { Key = x.Key, Value = x.Value }).ToList()
            };
        }
    }
}
