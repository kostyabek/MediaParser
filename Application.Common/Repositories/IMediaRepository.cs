using Application.Common.Repositories.Base;
using DataAccess.Entities;
using Domain.Common.Models.Media;

namespace Application.Common.Repositories;

/// <summary>
/// Media repository.
/// </summary>
public interface IMediaRepository : IBaseRepository<Media, MediaModel>
{
}