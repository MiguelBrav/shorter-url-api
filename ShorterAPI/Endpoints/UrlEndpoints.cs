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

        group.MapGet("/{route}", Redirect);

        group.MapGet("/id/{id}", ById).RequireAuthorization();

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

    static async Task<IResult> ById(int id, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            ShortyQuery shortyCommand = new ShortyQuery
            {
                UserName = _User,
                ShortyId = id
            };

            return await mediator.Send(shortyCommand);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> Redirect(string route, IMediator mediator, HttpContext httpContext)
    {
        RedirectQuery shortyCommand = new RedirectQuery
        {
            ShortyUrl = route
        };
        
        return await mediator.Send(shortyCommand);  
    }

}
