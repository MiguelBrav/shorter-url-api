using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class ShortyCommandHandler : IRequestHandler<ShortyCommand, ApiResponse>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public ShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<ApiResponse> Handle(ShortyCommand request, CancellationToken cancellationToken)
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

        bool isExists = await _unitOfWork.ShortyRepository.isExists(request.Shorty.ShortUrl);

        if (isExists)
        {
            response.Response = false;
            response.ResponseMessage = "ShortUrl already taken, try another.";
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;
        }

        Shorty shorty = new Shorty(
            request.Shorty.Title,
            request.Shorty.FullUrl,
            userExists.Id,
            request.Shorty.Description,
            request.Shorty.ShortUrl
        );

        try
        {
            await _unitOfWork.ShortyRepository.Create(shorty);
            await _unitOfWork.Save();

            ShortyDTO result = new ShortyDTO();
            result.Id = shorty.Id;
            result.ShortUrl = shorty.ShortUrl;

            response.Response = true;
            response.ResponseMessage = JsonConvert.SerializeObject(result);
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }
        catch (Exception)
        {
            response.Response = false;
            response.ResponseMessage = "Url not saved";
            response.StatusCode = StatusCodes.Status400BadRequest;

            return response;
        }

    }
}
