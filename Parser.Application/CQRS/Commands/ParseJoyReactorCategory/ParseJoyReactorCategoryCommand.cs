using FluentResults;
using MediatR;
using Parser.Application.CQRS.Commands.Base;

namespace Parser.Application.CQRS.Commands.ParseJoyReactorCategory;

public class ParseJoyReactorCategoryCommand : UrlCommand, IRequest<Result>
{
}