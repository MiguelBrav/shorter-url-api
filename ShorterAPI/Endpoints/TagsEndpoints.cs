using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShorterAPI.Commands;

namespace ShorterAPI.Endpoints;

public static class TagsEndpoints
{
    public static RouteGroupBuilder MapTags(this RouteGroupBuilder group)
    {
        group.MapPut("/{shortyId}/tags", AssignTags).RequireAuthorization();

        return group;
    }

    static async Task<IResult> AssignTags(int shortyId, [FromBody] List<string> tags, IMediator mediator, HttpContext httpContext)
    {
        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (string.IsNullOrEmpty(userClaim))
        {
            return TypedResults.Unauthorized();
        }

        var command = new TagsShortyCommand
        {
            UserName = userClaim,
            ShortyId = shortyId,
            Tags = tags ?? new List<string>()
        };

        return await mediator.Send(command);
    }
}
