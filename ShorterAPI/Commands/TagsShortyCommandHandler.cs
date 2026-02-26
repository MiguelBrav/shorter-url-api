using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Commands;

public class TagsShortyCommandHandler : IRequestHandler<TagsShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public TagsShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(TagsShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        Shorty shortyRequest = await _unitOfWork.ShortyRepository.ById(request.ShortyId);

        if (shortyRequest is null || shortyRequest?.CreatedUser != userExists.Id)
        {
            return TypedResults.BadRequest("Shorty does not exists");
        }

        var tags = request.Tags
          .Select(t => t.ToLower().Trim())
          .Where(t => !string.IsNullOrWhiteSpace(t))
          .Distinct()
          .ToList();

        if (!tags.Any())
            return TypedResults.BadRequest("No valid tags provided");

        try
        {
            var tagEntities = await _unitOfWork.TagShortyRepository.GetOrCreateMany(tags);

            await _unitOfWork.TagShortyRepository.AssignManyToShorty(
                request.ShortyId,
                tagEntities
            );

            await _unitOfWork.Save();

            return TypedResults.Created(
                $"/tags/by-shorty/{request.ShortyId}",
                tags
            );
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("An error occurred while assigning the tags.");
        }
    }
}
