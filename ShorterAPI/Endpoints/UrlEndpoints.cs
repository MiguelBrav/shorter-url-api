using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using ShorterAPI.Commands;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Endpoints;

public static class UrlEndpoints
{
    public static RouteGroupBuilder MapUrl(this RouteGroupBuilder group)
    {
        // TODO 
        group.MapGet("/", () => "All URLs from User")
              .RequireAuthorization(); 

        group.MapGet("/{route}", (string route) => $"URL with shorty: {route} going redirect To =>");

        group.MapPost("/", Create).RequireAuthorization();

        group.MapPut("/", () => $"URL updated, your updated url is =>").RequireAuthorization();

        group.MapDelete("/{id}", (int id) => $"URL {id} deleted").RequireAuthorization();

        return group;
    }

    static async Task<Results<Created<ShortyDTO>, NotFound<string>, UnauthorizedHttpResult, BadRequest<string>>>Create(CreateShortyDTO shortyDTO, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            ShortyCommand shortyCommand = new ShortyCommand
            {
                UserId = _User,
                Shorty = shortyDTO
            };

            ApiResponse responseCommand = await mediator.Send(shortyCommand);

            if (responseCommand.StatusCode == 404)
            {
                return TypedResults.NotFound(responseCommand.ResponseMessage);
            }

            if (responseCommand.StatusCode == 400)
            {
                return TypedResults.BadRequest(responseCommand.ResponseMessage);
            }

            ShortyDTO result = JsonConvert.DeserializeObject<ShortyDTO>(responseCommand.ResponseMessage);

            return TypedResults.Created($"url/{result.Id}", result);

        } else
        {
            return TypedResults.Unauthorized();
        }
    }

}
