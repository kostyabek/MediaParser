using DataAccess.Entities;
using Hosted.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Parser.Application.CQRS.Commands.ParseJoyReactorCategory;

namespace Parser.Api.Controllers;

/// <summary>
/// Controller for <see cref="Media"/>-related operations.
/// </summary>
[ApiController]
[Route("api/v1.0/parsing")]
[Produces("application/json")]
public class ParsingController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Instantiates <see cref="ParsingController"/>.
    /// </summary>
    /// <param name="mediator">Mediator.</param>
    public ParsingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Scraps and saves CDN images' links to database from given URL to joyreactor.cc category.
    /// </summary>
    /// <param name="url">URL to start scrapping images from.</param>
    [HttpPost("joyreactor")]
    public async Task<IActionResult> ScrapFromJoyReactorAsync([FromBody] string url)
    {
        var command = new ParseJoyReactorCategoryCommand
        {
            Url = url
        };

        var result = await _mediator.Send(command);

        return result.ToActionResult();
    }
}