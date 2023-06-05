using FleetJourney.Application.Services.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FleetJourney.Application.Services;

internal sealed class CacheService<TEntity> : ICacheService<TEntity>
    where TEntity : class, new()
{
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _distributedCacheOptions;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
        _distributedCacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(3)
        };
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync(string key, CancellationToken cancellationToken)
    {
        string? cachedEntity = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedEntity is null)
        {
            return Enumerable.Empty<TEntity>();
        }

        return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(cachedEntity)!;
    }

    public async Task<TEntity?> GetAsync(string key, CancellationToken cancellationToken)
    {
        string? cachedEntity = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedEntity is null)
        {
            return null;
        }

        return JsonConvert.DeserializeObject<TEntity>(cachedEntity);
    }

    public async Task<TEntity?> GetOrCreateAsync(string key, Func<Task<TEntity?>> createEntity,
        CancellationToken cancellationToken)
    {
        var cachedEntity = await GetAsync(key, cancellationToken);
        if (cachedEntity is not null)
        {
            return cachedEntity;
        }

        var entity = await createEntity();
        if (entity is null)
        {
            return null;
        }

        string jsonEntity = JsonConvert.SerializeObject(entity, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        await SetAsync(key, jsonEntity, cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllOrCreateAsync(string key,
        Func<Task<IEnumerable<TEntity>>> createEntity, CancellationToken cancellationToken)
    {
        var cachedEntity = (await GetAllAsync(key, cancellationToken)).ToList();
        if (cachedEntity.Any())
        {
            return cachedEntity;
        }

        var entity = (await createEntity()).ToList();
        if (!entity.Any())
        {
            return Enumerable.Empty<TEntity>();
        }

        string jsonEntity = JsonConvert.SerializeObject(entity, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        await SetAsync(key, jsonEntity, cancellationToken);
        return entity;
    }

    public Task SetAsync(string key, string jsonEntity, CancellationToken cancellationToken)
    {
        return _distributedCache.SetStringAsync(key, jsonEntity, _distributedCacheOptions, cancellationToken);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken)
    {
        return _distributedCache.RemoveAsync(key, cancellationToken);
    }
}