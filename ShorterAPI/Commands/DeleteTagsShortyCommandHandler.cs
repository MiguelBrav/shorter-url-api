using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;

namespace ShorterAPI.Commands;

public class DeleteTagsShortyCommandHandler : IRequestHandler<DeleteTagsShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteTagsShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(DeleteTagsShortyCommand request, CancellationToken cancellationToken)
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

        var tags = request.Tags
          .Select(t => t.ToLower().Trim())
          .Where(t => !string.IsNullOrWhiteSpace(t))
          .Distinct()
          .ToList();

        if (!tags.Any())
            return TypedResults.BadRequest("No valid tags provided");

        try
        {
            await _unitOfWork.TagShortyRepository.RemoveTagsFromShorty(request.ShortyId, tags);

            await _unitOfWork.Save();

            return TypedResults.Ok(tags);
        }
        catch (Exception ex)
        {
            var detail = new { Error = ex.Message, Inner = ex.InnerException?.Message };
            return TypedResults.BadRequest(detail);
        }
    }
}
