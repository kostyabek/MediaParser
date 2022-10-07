using Application.Common.Repositories;
using DataAccess.Entities;
using Domain.Common.Filters;
using Domain.Common.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Common.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly IMongoCollection<Media> _mediaCollection;

    public MediaRepository(IOptions<DatabaseOptions> databaseOptions)
    {
        var optionsValue = databaseOptions.Value;

        var client = new MongoClient(optionsValue.ConnectionString);
        var database = client.GetDatabase(optionsValue.DatabaseName);
        _mediaCollection = database.GetCollection<Media>(optionsValue.MediaCollectionName);
    }

    public Task<List<Media>> GetAllWithPaginationAsync(PaginationFilter paginationFilter)
    {
        var skip = paginationFilter.PageSize * (paginationFilter.PageNumber - 1);
        var limit = paginationFilter.PageSize;

        return _mediaCollection
            .Find(_ => true)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
    }

    public Task<Media> GetByIdAsync(string id)
        => _mediaCollection
            .Find(m => m.Id == id)
            .FirstAsync();

    public Task CreateAsync(Media entity)
        => _mediaCollection.InsertOneAsync(entity);

    public async Task<long> DeleteAsync(string id)
    {
        var deletionResult = await _mediaCollection.DeleteOneAsync(m => m.Id == id);
        return deletionResult.DeletedCount;
    }
}