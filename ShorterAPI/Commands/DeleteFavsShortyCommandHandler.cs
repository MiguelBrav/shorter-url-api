using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class DeleteFavsShortyCommandHandler : IRequestHandler<DeleteFavsShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteFavsShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(DeleteFavsShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        try
        {
            bool isDeleted = await _unitOfWork.FavoriteShortyRepository.DeleteByIdsByUser(userExists.Id, request.ShortyIds);

            if (!isDeleted)
                return TypedResults.NotFound("No matching favorite shortys found for the user.");

            await _unitOfWork.Save();

            return TypedResults.Ok("Selected favorite shortys deleted successfully.");
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Favorite shorties could not be deleted.");
        }
    }
}
