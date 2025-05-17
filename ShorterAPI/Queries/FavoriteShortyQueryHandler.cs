using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;
using ShorterAPI.Infrastructure.Migrations;

namespace ShorterAPI.Queries;

public class FavoriteShortyQueryHandler : IRequestHandler<FavoriteShortyQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public FavoriteShortyQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(FavoriteShortyQuery request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        IEnumerable<FavoriteShortyResponse> response = await _unitOfWork.FavoriteShortyRepository.ByUser(userExists.Id,request.PageNumber, request.PageSize);

        if (!response.Any())
        {
            return TypedResults.NoContent();
        }

        return TypedResults.Ok(response);
    }
}
