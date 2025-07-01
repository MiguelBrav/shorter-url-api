using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShorterAPI.Commands;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.Queries;

namespace ShorterAPI.Endpoints;

public static class FavoriteEndpoints
{
    public static RouteGroupBuilder MapFavs(this RouteGroupBuilder group)
    {
        group.MapPost("/", Create).RequireAuthorization();

        group.MapDelete("/{id}", Delete).RequireAuthorization();

        group.MapDelete("/", DeleteByIds).RequireAuthorization();

        group.MapGet("/page/{pageId}/size/{pageSize}", AllFavorites).RequireAuthorization();

        return group;
    }

    static async Task<IResult> Create(ShortyIdDTO favDTO, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            FavoriteShortyCommand favoriteShortyCommand = new FavoriteShortyCommand
            {
                UserName = _User,
                FavShorty = favDTO
            };

            return await mediator.Send(favoriteShortyCommand);
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

            DeleteFavShortyCommand favoriteShortyCommand = new DeleteFavShortyCommand
            {
                UserName = _User,
                FavShorty = new ShortyIdDTO { Id = id }
            };

            return await mediator.Send(favoriteShortyCommand);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> DeleteByIds([FromQuery(Name = "Id")] int[] shortyIds, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            DeleteFavsShortyCommand favoriteShortyCommand = new DeleteFavsShortyCommand
            {
                UserName = _User,
                ShortyIds = shortyIds.ToList()
            };

            return await mediator.Send(favoriteShortyCommand);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> AllFavorites(int pageId, int pageSize, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            FavoriteShortyQuery shortyCommand = new FavoriteShortyQuery
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
