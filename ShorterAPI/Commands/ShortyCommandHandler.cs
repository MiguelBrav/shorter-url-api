using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Commands;

public class ShortyCommandHandler : IRequestHandler<ShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public ShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(ShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserId);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        bool isExists = await _unitOfWork.ShortyRepository.isExists(request.Shorty.ShortUrl);

        if (isExists)
        {
            return TypedResults.BadRequest("ShortUrl already taken, try another.");
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

            return TypedResults.Created($"url/{result.Id}", result);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Url not saved");
        }

    }
}
