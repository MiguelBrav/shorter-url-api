using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.Helpers;

namespace ShorterAPI.Queries;

public class ShortyQueryHandler : IRequestHandler<ShortyQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public ShortyQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(ShortyQuery request, CancellationToken cancellationToken)
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

        return TypedResults.Ok(shorty);
    }
}
