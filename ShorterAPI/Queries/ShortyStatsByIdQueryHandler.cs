using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Queries;

public class ShortyStatsByIdQueryHandler : IRequestHandler<ShortyStatsByIdQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public ShortyStatsByIdQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(ShortyStatsByIdQuery request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        Shorty shorty = await _unitOfWork.ShortyRepository.ByIdByUser(userExists.Id, request.ShortyId);

        if (shorty is null)
        {
            return TypedResults.NoContent();
        }

        int _accessCount = await _unitOfWork.LogRepository.CountByShorty(request.ShortyId);

        ShortyStatsResponse response = new ShortyStatsResponse(shorty, _accessCount);

        return TypedResults.Ok(response);
    }
}
