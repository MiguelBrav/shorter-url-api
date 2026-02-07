using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.Interfaces;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Responses;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Commands;

public class CloneShortyCommandHandler : IRequestHandler<CloneShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IShortyService _shortyService;

    private readonly IConfiguration _configuration;

    private readonly string _charsShorty;

    private readonly int _shortyLenght;

    public CloneShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, IShortyService shortyService, IConfiguration configuration)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _shortyService = shortyService;
        _configuration = configuration;
        _charsShorty = _configuration.GetValue<string>("CharsShorty") ?? string.Empty;
        _shortyLenght = _configuration.GetValue<int>("ShortyNameLenght");
    }
    public async Task<IResult> Handle(CloneShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        bool isExists;

        string _shortyURL = string.Empty;

        Shorty shorty = await _unitOfWork.ShortyRepository.ByIdByUser(userExists.Id, request.Shorty.Id);

        if (shorty is null)
        {
            return TypedResults.NotFound();
        }

        string requestedShorty = request.Shorty.ShortUrl;

        if (!string.IsNullOrWhiteSpace(requestedShorty))
        {
            isExists = await _unitOfWork.ShortyRepository.isExists(requestedShorty);

            _shortyURL = isExists
                ? _shortyService.GenerateShortyUrl(_charsShorty, _shortyLenght)
                : requestedShorty;
        }
        else
        {
            _shortyURL = _shortyService.GenerateShortyUrl(_charsShorty, _shortyLenght);
        }

        while (await _unitOfWork.ShortyRepository.isExists(_shortyURL))
        {
            _shortyURL = _shortyService.GenerateShortyUrl(_charsShorty, _shortyLenght);
        }

        Shorty cloneShorty = new Shorty(
            shorty.Title,
            shorty.FullUrl,
            userExists.Id,
            shorty.Description,
            _shortyURL
        );

        try
        {
            await _unitOfWork.ShortyRepository.Create(cloneShorty);
            await _unitOfWork.Save();

            ShortyDTO result = new ShortyDTO();
            result.Id = cloneShorty.Id;
            result.ShortUrl = cloneShorty.ShortUrl;

            return TypedResults.Created($"url/{result.Id}", result);
        }
        catch (Exception)
        {
            return TypedResults.BadRequest("Url not saved");
        }
    }
}
