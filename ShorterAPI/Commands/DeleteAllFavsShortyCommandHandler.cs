

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class DeleteAllFavsShortyCommandHandler : IRequestHandler<DeleteAllFavsShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteAllFavsShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(DeleteAllFavsShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        try
        {
            bool isDeleted = await _unitOfWork.FavoriteShortyRepository.DeleteAllByUser(userExists.Id);

            if (!isDeleted)
                return TypedResults.NotFound("No shortys found for the user.");

            await _unitOfWork.Save();

            return TypedResults.Ok("All Favorite shortys deleted successfully.");
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Favorite shorties could not be deleted.");
        }
    }
}
