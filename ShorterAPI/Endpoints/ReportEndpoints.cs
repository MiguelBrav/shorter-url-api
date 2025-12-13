using MediatR;
using ShorterAPI.Queries;

namespace ShorterAPI.Endpoints;

public static class ReportEndpoints
{
    public static RouteGroupBuilder MapReportGlobal(this RouteGroupBuilder group)
    {
        group.MapGet("/top-urls/limit/{limit}", TopShortys);

        group.MapGet("/top-urls/limit/{limit}/csv", TopShortysCsv);

        group.MapGet("/last-urls/limit/{limit}", LastShortys);

        group.MapGet("/last-urls/limit/{limit}/csv", LastShortysCsv);

        group.MapGet("/random-urls/limit/{limit}", RandomShortys);

        group.MapGet("/random-urls/limit/{limit}/csv", RandomShortysCsv);

        group.MapGet("/least-used-urls/limit/{limit}", LeastUsedShortys);

        group.MapGet("/least-used-urls/limit/{limit}/csv", LeastUsedShortysCsv);

        group.MapGet("/never-used-urls/limit/{limit}", NeverUsedShortys);

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
    static async Task<IResult> LeastUsedShortys(int limit, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            LeastUsedShortysReportQuery shortyReport = new LeastUsedShortysReportQuery
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
    static async Task<IResult> NeverUsedShortys(int limit, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            NeverUsedShortysReportQuery shortyReport = new NeverUsedShortysReportQuery
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

    static async Task<IResult> TopShortysCsv(int limit, IMediator mediator, HttpContext httpContext)
    {
        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            TopShortyReportCsvQuery shortyReport = new TopShortyReportCsvQuery
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

    static async Task<IResult> RandomShortysCsv(int limit, IMediator mediator, HttpContext httpContext)
    {
        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            RandomShortyReportCsvQuery shortyReport = new RandomShortyReportCsvQuery
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

    static async Task<IResult> LastShortysCsv(int limit, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            LastShortyReportCsvQuery shortyReport = new LastShortyReportCsvQuery
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

    static async Task<IResult> LeastUsedShortysCsv(int limit, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            LeastUsedShortysReportCsvQuery shortyReport = new LeastUsedShortysReportCsvQuery
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
