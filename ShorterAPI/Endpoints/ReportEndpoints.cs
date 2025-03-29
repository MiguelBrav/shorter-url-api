using MediatR;
using ShorterAPI.Queries;

namespace ShorterAPI.Endpoints;

public static class ReportEndpoints
{
    public static RouteGroupBuilder MapReportGlobal(this RouteGroupBuilder group)
    {
        group.MapGet("/top-urls/limit/{limit}", TopShortys);

        return group;
    }

    static async Task<IResult> TopShortys(int limit, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            TopShortyReportQuery shortyCommand = new TopShortyReportQuery
            {
                Limit   = limit,
            };

            return await mediator.Send(shortyCommand);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }
}
