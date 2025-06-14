using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class FavoriteShortyCommandHandler : IRequestHandler<FavoriteShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public FavoriteShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(FavoriteShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        Shorty shorty = await _unitOfWork.ShortyRepository.ById(request.FavShorty.Id);

        if (shorty is null || shorty?.CreatedUser != userExists.Id)
        {
            return TypedResults.BadRequest("Shorty does not exists");
        }

        try
        {
            FavoriteShorty validateShorty = await _unitOfWork.FavoriteShortyRepository.ByIdByUser(
                 userExists.Id, request.FavShorty.Id);

            if (validateShorty is not null)
            {
                return TypedResults.BadRequest("Favorite already saved");
            }

            FavoriteShorty favShorty = new FavoriteShorty(request.FavShorty.Id, userExists.Id);

            bool addedFav = await _unitOfWork.FavoriteShortyRepository.Create(favShorty);
            await _unitOfWork.Save();

            return TypedResults.Created($"favs/", new { shorty.Id });
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Favorite not saved");
        }

    }
}
