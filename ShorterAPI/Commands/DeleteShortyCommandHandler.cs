using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class DeleteShortyCommandHandler : IRequestHandler<DeleteShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(DeleteShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        Shorty shortyToUpdate = await _unitOfWork.ShortyRepository.ById(request.Shorty.Id);

        if (shortyToUpdate is null || shortyToUpdate?.CreatedUser != userExists.Id)
        {
            return TypedResults.BadRequest("Shorty does not exists");
        }

        try
        {
            shortyToUpdate.IsDeleted = true;
            shortyToUpdate.DeletedDate = DateTime.UtcNow;

            await _unitOfWork.ShortyRepository.Update(shortyToUpdate);
            await _unitOfWork.Save();

            return TypedResults.Ok($"Shorty {shortyToUpdate.Id} deleted");
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Shorty not deleted");
        }

    }
}
