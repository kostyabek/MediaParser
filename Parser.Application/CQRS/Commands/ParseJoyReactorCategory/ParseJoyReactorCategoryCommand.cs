using FluentResults;
using MediatR;
using Parser.Application.CQRS.Commands.Base;

namespace Parser.Application.CQRS.Commands.ParseJoyReactorCategory;

/// <summary>
/// Command for initiating parsing process of joyreactor.cc category.
/// </summary>
public class ParseJoyReactorCategoryCommand : UrlCommand, IRequest<Result>
{
}