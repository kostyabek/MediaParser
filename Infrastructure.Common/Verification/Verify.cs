using Domain.Common.Enums;
using Domain.Common.Exceptions;

namespace Infrastructure.Common.Verification;

/// <summary>
/// Contains verification methods for different operations.
/// </summary>
public static class Verify
{
    /// <summary>
    /// Verifies that given entity is found in database.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <param name="name">Name of the entity.</param>
    public static void EntityFound(object? entity, string name)
    {
        if (entity is null)
        {
            Process(ErrorCode.NotFound, name);
        }
    }

    /// <summary>
    /// Verifies that given collection of entities is found in database.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    /// <param name="collection">Collection of entities.</param>
    /// <param name="name">Name of the collection.</param>
    public static void CollectionFound<T>(IEnumerable<T> collection, string name)
    {
        if (!collection.Any())
        {
            Process(ErrorCode.CollectionNotFound, name);
        }
    }

    private static void Process(ErrorCode code, string name)
    {
        switch (code)
        {
            case ErrorCode.NotFound:
                throw new NotFoundException($"Could not find {name}.");;
            case ErrorCode.CollectionNotFound:
                throw new CollectionNotFoundException($"Could not find {name}");
            default:
                throw new ArgumentOutOfRangeException(nameof(code), code, null);
        }
    }
}