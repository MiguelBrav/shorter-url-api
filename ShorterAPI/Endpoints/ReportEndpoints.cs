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

 
}
