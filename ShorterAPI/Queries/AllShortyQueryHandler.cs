using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Commands;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Queries;

public class AllShortyQueryHandler : IRequestHandler<AllShortyQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public AllShortyQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(AllShortyQuery request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        IEnumerable<Shorty> shortys = await _unitOfWork.ShortyRepository.GetAllByUser(userExists.Id, request.PageNumber, request.PageSize);

        if (shortys is null || shortys?.Count() == 0)
        {
            return TypedResults.NoContent();
        }

        return TypedResults.Ok(shortys);
    }
}
