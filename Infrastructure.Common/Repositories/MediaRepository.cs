using System.Linq.Expressions;
using Application.Common.Repositories;
using DataAccess.Entities;
using Domain.Common.Filters;
using Domain.Common.Models.Media;
using Domain.Common.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Common.Repositories;

/// <inheritdoc cref="IMediaRepository"/>
public class MediaRepository : IMediaRepository
{
    private readonly IMongoCollection<Media> _mediaCollection;

    /// <summary>
    /// Instantiates <see cref="MediaRepository"/>.
    /// </summary>
    /// <param name="databaseOptions">Database options.</param>
    public MediaRepository(IOptions<DatabaseOptions> databaseOptions)
    {
        var optionsValue = databaseOptions.Value;

        var client = new MongoClient(optionsValue.ConnectionString);
        var database = client.GetDatabase(optionsValue.DatabaseName);
        _mediaCollection = database.GetCollection<Media>(optionsValue.MediaCollectionName);
    }

    /// <inheritdoc />
    public async Task<(List<MediaModel>, long)> GetAllAsync()
    {
        IFindFluent<Media, Media> query = _mediaCollection.Find(_ => true);
        long totalCount = await query.CountDocumentsAsync();
        List<MediaModel> models = await query
            .Project(e => new MediaModel
            {
                Id = e.Id,
                Url = e.Url,
                GroupKey = e.GroupKey,
                CreatedDate = e.CreatedDate
            })
            .ToListAsync();

        return (models, totalCount);
    }

    /// <inheritdoc />
    public async Task<(List<MediaModel>, long)> GetAllAsync(PaginationFilter paginationFilter)
    {
        var skip = paginationFilter.PageSize * (paginationFilter.PageNumber - 1);
        var limit = paginationFilter.PageSize;

        IFindFluent<Media, Media> query = _mediaCollection.Find(_ => true);
        long totalCount = await query.CountDocumentsAsync();
        List<MediaModel> models = await query
            .Skip(skip)
            .Limit(limit)
            .Project(e => new MediaModel
            {
                Id = e.Id,
                Url = e.Url,
                GroupKey = e.GroupKey,
                CreatedDate = e.CreatedDate
            })
            .ToListAsync();

        return (models, totalCount);
    }

    /// <inheritdoc />
    public Task<List<Media>> FindAsync(Expression<Func<Media, bool>> filter)
        => _mediaCollection
            .Find(filter)
            .ToListAsync();

    /// <inheritdoc />
    public Task<List<Media>> FindAsync(Expression<Func<Media, bool>> filter, PaginationFilter paginationFilter)
    {
        var skip = paginationFilter.PageSize * (paginationFilter.PageNumber - 1);
        var limit = paginationFilter.PageSize;

        return _mediaCollection
            .Find(filter)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
    }

    /// <inheritdoc />
    public Task<Media> GetByIdAsync(string id)
        => _mediaCollection
            .Find(m => m.Id == id)
            .FirstAsync();

    /// <inheritdoc />
    public Task CreateAsync(Media entity)
        => _mediaCollection.InsertOneAsync(entity);

    /// <inheritdoc />
    public async Task<long> DeleteByIdAsync(string id)
    {
        var deletionResult = await _mediaCollection.DeleteOneAsync(m => m.Id == id);
        return deletionResult.DeletedCount;
    }

    /// <inheritdoc />
    public async Task<long> DeleteManyAsync(IEnumerable<string> ids)
    {
        var deletionResult = await _mediaCollection.DeleteManyAsync(m => ids.Contains(m.Id));
        return deletionResult.DeletedCount;
    }
}