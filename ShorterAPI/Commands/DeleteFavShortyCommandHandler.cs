using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class DeleteFavShortyCommandHandler : IRequestHandler<DeleteFavShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteFavShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(DeleteFavShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }             

        try
        {
            bool isDeleted = await _unitOfWork.FavoriteShortyRepository.DeleteByIdByUser(userExists.Id, request.FavShorty.Id);
            if (!isDeleted)
                return TypedResults.NotFound($"Favorite Shorty {request.FavShorty.Id} not found for the user.");

            await _unitOfWork.Save();

            return TypedResults.Ok($"Favorite Shorty {request.FavShorty.Id} deleted");
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Shorty not deleted");
        }
    }
}
