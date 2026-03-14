using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Queries;

public class TagsByShortyQueryHandler : IRequestHandler<TagsByShortyQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public TagsByShortyQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(TagsByShortyQuery request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        var shorty = await _unitOfWork.ShortyRepository.ByIdByUser(userExists.Id, request.ShortyId);

        if (shorty is null)
        {
            return TypedResults.NoContent();
        }

        IEnumerable<Tag> tags = await _unitOfWork.TagShortyRepository.GetByShorty(request.ShortyId);

        return TypedResults.Ok(tags.Select(t => t.Name));
    }
}
