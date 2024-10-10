using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using ShorterAPI.Commands;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Responses;
using ShorterAPI.Queries;

namespace ShorterAPI.Endpoints;

public static class UrlEndpoints
{
    public static RouteGroupBuilder MapUrl(this RouteGroupBuilder group)
    {
        // TODO 
        group.MapGet("/page/{pageId}/size/{pageSize}", AllShortys).RequireAuthorization(); 

        group.MapGet("/{route}", (string route) => $"URL with shorty: {route} going redirect To =>");

        group.MapPost("/", Create).RequireAuthorization();

        group.MapPut("/", Update).RequireAuthorization();

        group.MapDelete("/{id}", Delete).RequireAuthorization();

        return group;
    }

    static async Task<IResult> Create(CreateShortyDTO shortyDTO, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            ShortyCommand shortyCommand = new ShortyCommand
            {
                UserName = _User,
                Shorty = shortyDTO
            };

            return await mediator.Send(shortyCommand);
        } 
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> Update(UpdateShortyDTO shortyDTO, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            UpdateShortyCommand shortyCommand = new UpdateShortyCommand
            {
                UserName = _User,
                Shorty = shortyDTO
            };

            return await mediator.Send(shortyCommand);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> Delete(int id, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            DeleteShortyCommand shortyCommand = new DeleteShortyCommand
            {
                UserName = _User,
                Shorty = new ShortyIdDTO { Id = id }
            };

            return await mediator.Send(shortyCommand);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> AllShortys(int pageId,int pageSize, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            AllShortyQuery shortyCommand = new AllShortyQuery
            {
                UserName = _User,
                PageNumber = pageId,
                PageSize = pageSize,
            };

            return await mediator.Send(shortyCommand);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

}
