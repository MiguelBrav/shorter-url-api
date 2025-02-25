using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShorterAPI.Domain.Interfaces;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Responses;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Commands;

public class GenerateBulkShortyCommandHandler : IRequestHandler<GenerateBulkShortyCommand, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IShortyService _shortyService;

    private readonly IConfiguration _configuration;

    private readonly string _charsShorty;

    private readonly int _shortyLenght;

    public GenerateBulkShortyCommandHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, IShortyService shortyService, IConfiguration configuration)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _shortyService = shortyService;
        _configuration = configuration;
        _charsShorty = _configuration.GetValue<string>("CharsShorty") ?? string.Empty;
        _shortyLenght = _configuration.GetValue<int>("ShortyNameLenght");
    }
    public async Task<IResult> Handle(GenerateBulkShortyCommand request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        if (request.Shortys.Count == 0 || request.Shortys is null)
        {
            return TypedResults.BadRequest("The request must contain at least one shorty.");
        }

        List<ShortyDTO> results = new List<ShortyDTO>();

        foreach (var shortyRequest in request.Shortys)
        {
            bool isExists;
            string _shortyURL;

            do
            {
                int shortyLength = shortyRequest.Length.HasValue && shortyRequest.Length.Value > 2 
                    ? shortyRequest.Length.Value : _shortyLenght;

                _shortyURL = _shortyService.GenerateShortyUrl(_charsShorty, shortyLength);

                isExists = await _unitOfWork.ShortyRepository.isExists(_shortyURL);

            } while (isExists);

            Shorty shorty = new Shorty(shortyRequest.FullUrl, userExists.Id, _shortyURL);

            try
            {
                await _unitOfWork.ShortyRepository.Create(shorty);
                await _unitOfWork.Save(); 

                results.Add(new ShortyDTO { Id = shorty.Id, ShortUrl = shorty.ShortUrl });
            }
            catch (Exception)
            {
                // TODO: add logger
                return TypedResults.BadRequest("An error occurred while saving URLs.");
            }
        }

        return TypedResults.Created("urls", results);
    }
}
