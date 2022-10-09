using Data.Application.Queries.Media.GetAllMedia;
using Domain.Common.Filters;
using Domain.Common.Models.Media;
using FluentResults;
using Hosted.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Data.Api.Controllers
{
    [ApiController]
    [Route("api/v1.0/media")]
    public class MediaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MediaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all media records.
        /// </summary>
        /// <param name="paginationFilter">Pagination filter.</param>
        /// <returns>A paginated set of media records.</returns>
        [ProducesResponseType(typeof(List<MediaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<IReason>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationFilter paginationFilter)
        {
            var query = new GetAllMediaQuery
            {
                PaginationFilter = paginationFilter
            };

            var result = await _mediator.Send(query);

            return result.ToActionResult();
        }
    }
}