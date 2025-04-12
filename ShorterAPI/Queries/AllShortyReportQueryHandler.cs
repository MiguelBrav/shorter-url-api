using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;
using ShorterAPI.Events;
using ShorterAPI.Infrastructure.Migrations;

namespace ShorterAPI.Queries;

public class AllShortyReportQueryHandler : IRequestHandler<AllShortyReportQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMediator _mediator;

    private readonly UserManager<IdentityUser> _userManager;

    public AllShortyReportQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _userManager = userManager;
    }
    public async Task<IResult> Handle(AllShortyReportQuery request, CancellationToken cancellationToken)
    {
        List<RedirectReportResponse> response = new List<RedirectReportResponse>();

        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        IEnumerable<Shorty> shortys = await _unitOfWork.ShortyRepository.GetAllByUser(userExists.Id, request.PageNumber, request.PageSize);

        if (!shortys.Any())
        {
            return TypedResults.NoContent();
        }

        IEnumerable<int> shortyIds = shortys.Select(s => s.Id).ToList();

        IEnumerable<LogRedirect> allLogRedirects = await _unitOfWork.LogRepository.ByShortyIds(shortyIds);

        IEnumerable<IGrouping<int, LogRedirect>> logsGroupedByShortyId = allLogRedirects.GroupBy(log => log.ShortyId);

        foreach (Shorty shorty in shortys)
        {
            IEnumerable<LogRedirect>? logRedirects = logsGroupedByShortyId.FirstOrDefault(group => group.Key == shorty.Id)?.ToList();

            if (logRedirects != null && logRedirects.Any())
            {
                RedirectReportResponse redirectReport = new RedirectReportResponse(logRedirects.ToList(), shorty);
                response.Add(redirectReport);
            }
            else
            {
                RedirectReportResponse redirectReport = new RedirectReportResponse();
                redirectReport.Total = 0;
                redirectReport.ShortyId = shorty.Id;
                redirectReport.ShortyTitle = shorty.Title;
                response.Add(redirectReport);
            }
        }

        return TypedResults.Ok(response);
    }
}
