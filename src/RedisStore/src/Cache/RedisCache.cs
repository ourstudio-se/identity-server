﻿using Ourstudio.IdentityServer.Services;
using Ourstudio.IdentityServer.Stores.Serialization;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ourstudio.IdentityServer.RedisStore.Cache
{
    /// <summary>
    /// Redis based implementation for ICache<typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisCache<T> : ICache<T> where T : class
    {
        private readonly IDatabase database;

        private readonly RedisCacheOptions options;

        private readonly ILogger<RedisCache<T>> logger;

        public RedisCache(RedisMultiplexer<RedisCacheOptions> multiplexer, ILogger<RedisCache<T>> logger)
        {
            if (multiplexer is null)
                throw new ArgumentNullException(nameof(multiplexer));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            this.options = multiplexer.RedisOptions;
            this.database = multiplexer.Database;
        }

        private string GetKey(string key) => $"{this.options.KeyPrefix}{typeof(T).FullName}:{key}";

        public async Task<T> GetAsync(string key)
        {
            var cacheKey = GetKey(key);
            var item = await this.database.StringGetAsync(cacheKey);
            if (item.HasValue)
            {
                logger.LogDebug("retrieved {type} with Key: {key} from Redis Cache successfully.", typeof(T).FullName, key);
                return Deserialize(item);
            }
            else
            {
                logger.LogDebug("missed {type} with Key: {key} from Redis Cache.", typeof(T).FullName, key);
                return default(T);
            }
        }

        public async Task SetAsync(string key, T item, TimeSpan expiration)
        {
            var cacheKey = GetKey(key);
            await this.database.StringSetAsync(cacheKey, Serialize(item), expiration);
            logger.LogDebug("persisted {type} with Key: {key} in Redis Cache successfully.", typeof(T).FullName, key);
        }

        #region Json
        private JsonSerializerOptions SerializerSettings
        {
            get
            {
                var settings = new JsonSerializerOptions();
                settings.Converters.Add(new ClaimConverter());
                return settings;
            }
        }

        private T Deserialize(string json)
        {
            return JsonSerializer.Deserialize<T>(json, this.SerializerSettings);
        }

        private string Serialize(T item)
        {
            return JsonSerializer.Serialize(item, this.SerializerSettings);
        }
        #endregion
    }
}
