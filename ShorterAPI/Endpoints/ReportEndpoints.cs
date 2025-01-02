using MediatR;
using ShorterAPI.Commands;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.Queries;

namespace ShorterAPI.Endpoints;

public static class ReportEndpoints
{
    public static RouteGroupBuilder MapReport(this RouteGroupBuilder group)
    {

        group.MapGet("/redirect/{id}", ByShortyId).RequireAuthorization();

        group.MapGet("/redirect/page/{pageId}/size/{pageSize}", AllRedirects).RequireAuthorization();

        return group;
    }

    static async Task<IResult> ByShortyId(int id, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            ShortyReportQuery shortyCommand = new ShortyReportQuery
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

    static async Task<IResult> AllRedirects(int pageId, int pageSize, IMediator mediator, HttpContext httpContext)
    {

        var userClaim = httpContext.User.Identity?.Name ?? string.Empty;

        if (!string.IsNullOrEmpty(userClaim))
        {
            string _User = userClaim;

            AllShortyReportQuery shortyCommand = new AllShortyReportQuery
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
