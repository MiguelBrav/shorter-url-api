using MediatR;
using ShorterAPI.Commands;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.Queries;

namespace ShorterAPI.Endpoints;

public static class FavoriteEndpoints
{
    public static RouteGroupBuilder MapFavs(this RouteGroupBuilder group)
    {
        group.MapPost("/", Create).RequireAuthorization();

        return group;
    }

    static async Task<IResult> Create(CreateFavoriteDTO favDTO, IMediator mediator, HttpContext httpContext)
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

}
