using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Entities.Base;

/// <summary>
/// Base id entity.
/// </summary>
public class BaseIdEntity
{
    /// <summary>
    /// Gets or sets id of the entity.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}