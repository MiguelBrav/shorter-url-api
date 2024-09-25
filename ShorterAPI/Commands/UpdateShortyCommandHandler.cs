using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class UpdateShortyCommandHandler : IRequestHandler<UpdateShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(UpdateShortyCommand request, CancellationToken cancellationToken)
    {
        ApiResponse response = new ApiResponse();

        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserId);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        Shorty shortyToUpdate = await _unitOfWork.ShortyRepository.ById(request.Shorty.Id);

        if (shortyToUpdate is null || shortyToUpdate?.CreatedUser != userExists.Id)
        {
            return TypedResults.BadRequest("ShortUrl does not exists");
        }

        try
        {
            Shorty sameNameShorty = await _unitOfWork.ShortyRepository.isExistsShorty(request.Shorty.ShortUrl);

            if(sameNameShorty is not null && sameNameShorty.Id != shortyToUpdate.Id)
            {
                return TypedResults.BadRequest("ShortUrl already taken, try another.");
            }

            shortyToUpdate.FullUrl = request.Shorty.FullUrl ?? shortyToUpdate.FullUrl;
            shortyToUpdate.ShortUrl = request.Shorty.ShortUrl ?? shortyToUpdate.ShortUrl;
            shortyToUpdate.Title = request.Shorty.Title ?? shortyToUpdate.Title;
            shortyToUpdate.Description = request.Shorty.Description ?? shortyToUpdate.Description;
            shortyToUpdate.UpdatedDate = DateTime.UtcNow;

            await _unitOfWork.ShortyRepository.Update(shortyToUpdate);
            await _unitOfWork.Save();

            ShortyDTO result = new ShortyDTO();
            result.Id = shortyToUpdate.Id;
            result.ShortUrl = shortyToUpdate.ShortUrl;

            return TypedResults.Ok(result);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Shorty not updated");
        }

    }
}
