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
        group.MapGet("/page/{pageId}/size/{pageSize}", AllShortys).RequireAuthorization();

        group.MapGet("/{route}", Redirect);

        group.MapGet("/id/{id}", ById).RequireAuthorization();

        group.MapGet("/id/{id}/qr", QrById).RequireAuthorization();

        group.MapGet("/id/{id}/stats", StatsById).RequireAuthorization();

        group.MapGet("/check/{shorturl}", CheckName);

        group.MapPost("/", Create).RequireAuthorization();

        group.MapPut("/", Update).RequireAuthorization();

        group.MapDelete("/{id}", Delete).RequireAuthorization();

        group.MapPost("/generate", Generate).RequireAuthorization();

        group.MapPost("/generate/bulk", GenerateBulk).RequireAuthorization();

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

    static async Task<IResult> AllShortys(int pageId, int pageSize, IMediator mediator, HttpContext httpContext)
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

    static async Task<IResult> StatsById(int id, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            ShortyStatsByIdQuery shortyCommand = new ShortyStatsByIdQuery
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

    static async Task<IResult> CheckName(string shorturl, IMediator mediator, HttpContext httpContext)
    {

        CheckShortyNameQuery checkShortyNameQuery = new CheckShortyNameQuery
        {
            ShortyUrl = new ShortyUrlDTO { ShortyName = shorturl }
        };

        return await mediator.Send(checkShortyNameQuery);

    }

    static async Task<IResult> Redirect(string route, IMediator mediator, HttpContext httpContext)
    {
        RedirectQuery shortyCommand = new RedirectQuery
        {
            ShortyUrl = route
        };

        return await mediator.Send(shortyCommand);
    }

    static async Task<IResult> Generate(GenerateShortyDTO shortyDTO, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            GenerateShortyCommand shortyCommand = new GenerateShortyCommand
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

    static async Task<IResult> GenerateBulk(List<GenerateShortyDTO> shortysDTO, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            GenerateBulkShortyCommand shortyBulkCommand = new GenerateBulkShortyCommand
            {
                UserName = _User,
                Shortys = shortysDTO
            };

            return await mediator.Send(shortyBulkCommand);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> QrById(int id, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            QRShortyQuery shortyQuery = new QRShortyQuery
            {
                UserName = _User,
                ShortyId = id
            };

            return await mediator.Send(shortyQuery);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

}
