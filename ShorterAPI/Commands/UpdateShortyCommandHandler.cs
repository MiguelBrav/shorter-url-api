using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class UpdateShortyCommandHandler : IRequestHandler<UpdateShortyCommand, ApiResponse>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public UpdateShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<ApiResponse> Handle(UpdateShortyCommand request, CancellationToken cancellationToken)
    {
        ApiResponse response = new ApiResponse();

        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserId);

        if (userExists == null)
        {
            response.Response = false;
            response.ResponseMessage = "The user does not exists.";
            response.StatusCode = StatusCodes.Status404NotFound;

            return response;
        }

        Shorty shortyToUpdate = await _unitOfWork.ShortyRepository.ById(request.Shorty.Id);

        if (shortyToUpdate is null || shortyToUpdate?.CreatedUser != userExists.Id)
        {
            response.Response = false;
            response.ResponseMessage = "ShortUrl does not exists";
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;
        }

        try
        {
            Shorty sameNameShorty = await _unitOfWork.ShortyRepository.isExistsShorty(request.Shorty.ShortUrl);

            if(sameNameShorty is not null && sameNameShorty.Id != shortyToUpdate.Id)
            {
                response.Response = false;
                response.ResponseMessage = "ShortUrl already taken, try another.";
                response.StatusCode = StatusCodes.Status400BadRequest;

                return response;
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

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(result);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
        catch (Exception)
        {
            response.Response = false;
            response.ResponseMessage = "Shorty not updated";
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;
        }

    }
}
