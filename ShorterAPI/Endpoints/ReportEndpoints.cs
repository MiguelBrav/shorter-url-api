using MediatR;
using ShorterAPI.Queries;

namespace ShorterAPI.Endpoints;

public static class ReportEndpoints
{
    public static RouteGroupBuilder MapReportGlobal(this RouteGroupBuilder group)
    {
        group.MapGet("/top-urls/limit/{limit}", TopShortys);

        group.MapGet("/last-urls/limit/{limit}", LastShortys);

        group.MapGet("/random-urls/limit/{limit}", RandomShortys);

        return group;
    }

    static async Task<IResult> TopShortys(int limit, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            TopShortyReportQuery shortyReport = new TopShortyReportQuery
            {
                Limit   = limit,
            };

            return await mediator.Send(shortyReport);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> LastShortys(int limit, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            LastShortyReportQuery shortyReport = new LastShortyReportQuery
            {
                Limit   = limit,
            };

            return await mediator.Send(shortyReport);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

    static async Task<IResult> RandomShortys(int limit, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            RandomShortyReportQuery shortyReport = new RandomShortyReportQuery
            {
                Limit   = limit,
            };

            return await mediator.Send(shortyReport);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }
}
