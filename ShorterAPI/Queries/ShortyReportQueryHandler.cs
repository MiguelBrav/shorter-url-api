using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;
using System.Linq;

namespace ShorterAPI.Queries;

public class ShortyReportQueryHandler : IRequestHandler<ShortyReportQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public ShortyReportQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(ShortyReportQuery request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        Shorty shorty = await _unitOfWork.ShortyRepository.ByIdByUser(userExists.Id, request.ShortyId);

        if (shorty?.CreatedUser != userExists.Id)
        {
            return TypedResults.Unauthorized();
        }

        IEnumerable<LogRedirect> logRedirects = await _unitOfWork.LogRepository.ByShorty(shorty.Id);

        if (!logRedirects.Any())
        {
            return TypedResults.NoContent();
        }

        RedirectReportResponse redirectReport = new RedirectReportResponse(logRedirects.ToList());

        return TypedResults.Ok(redirectReport);
    }
}
