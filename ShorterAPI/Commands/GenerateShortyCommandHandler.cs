using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.Interfaces;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.DTOs;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Commands;

public class GenerateShortyCommandHandler : IRequestHandler<GenerateShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IShortyService _shortyService;

    private readonly IConfiguration _configuration;

    private readonly string _charsShorty;

    private readonly int _shortyLenght;

    public GenerateShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, IShortyService shortyService, IConfiguration configuration)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _shortyService = shortyService;
        _configuration = configuration;
        _charsShorty = _configuration.GetValue<string>("CharsShorty") ?? string.Empty;
        _shortyLenght = _configuration.GetValue<int>("ShortyNameLenght");
    }
    public async Task<IResult> Handle(GenerateShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        if (request.Shorty.Length.HasValue && request.Shorty.Length.Value < 2)
        {
            return TypedResults.BadRequest("Length must be at least 2.");
        }

        bool isExists;

        string _shortyURL;

        do
        {
            int shortyLenght = request.Shorty.Length.HasValue ? request.Shorty.Length.Value : _shortyLenght;

            _shortyURL = _shortyService.GenerateShortyUrl(_charsShorty, shortyLenght);            

            isExists = await _unitOfWork.ShortyRepository.isExists(_shortyURL);

        } while (isExists);

        Shorty shorty = new Shorty(
            request.Shorty.FullUrl,
            userExists.Id,
            _shortyURL
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
