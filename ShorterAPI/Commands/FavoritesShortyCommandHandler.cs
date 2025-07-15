using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;
using System.Linq;

namespace ShorterAPI.Commands;

public class FavoritesShortyCommandHandler : IRequestHandler<FavoritesShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public FavoritesShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(FavoritesShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        try
        {
            List<Shorty> validShorties = await _unitOfWork.ShortyRepository.ByIdsByUser(userExists.Id, request.ShortyIds);

            if (!validShorties.Any())
                return TypedResults.NotFound("No valid Shorties found for the user.");

            List<FavoriteShorty> existingFavs = await _unitOfWork.FavoriteShortyRepository.ByUser(userExists.Id);

            List<FavoriteShorty> newFavorites = validShorties
                .Where(s => !existingFavs.Select(x => x.ShortyId).Contains(s.Id))
                .Select(s => new FavoriteShorty(s.Id, userExists.Id))
                .ToList();


            if (!newFavorites.Any())
                return TypedResults.BadRequest("All selected Shorties are already in favorites.");

            await _unitOfWork.FavoriteShortyRepository.CreateList(newFavorites);
            await _unitOfWork.Save();

            return TypedResults.Created("/favs", newFavorites.Select(f => f.ShortyId));
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Favorite shorties could not be created.");
        }
    }
}
