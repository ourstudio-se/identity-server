// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using IdentityModel;

#pragma warning disable 1591

namespace IdentityServer4.Stores.Serialization
{
    public class ClaimsPrincipalConverter : JsonConverter<ClaimsPrincipal>
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ClaimsPrincipal) == objectType;
        }

        public override ClaimsPrincipal Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            var source = JsonSerializer.Deserialize<ClaimsPrincipalLite>(reader.GetString());
            if (source == null) return null;

            var claims = source.Claims.Select(x => new Claim(x.Type, x.Value, x.ValueType));
            var id = new ClaimsIdentity(claims, source.AuthenticationType, JwtClaimTypes.Name, JwtClaimTypes.Role);
            var target = new ClaimsPrincipal(id);
            return target;
        }

        public override void Write(Utf8JsonWriter writer, ClaimsPrincipal source, JsonSerializerOptions options)
        {
            var target = new ClaimsPrincipalLite
            {
                AuthenticationType = source.Identity.AuthenticationType,
                Claims = source.Claims.Select(x => new ClaimLite
                    { Type = x.Type, Value = x.Value, ValueType = x.ValueType }).ToArray()
            };
            writer.WriteStringValue(JsonSerializer.Serialize(target));
        }
    }
}