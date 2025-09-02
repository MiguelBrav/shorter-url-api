using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.Helpers;

namespace ShorterAPI.Queries;

public class FreeQrsShortysByUrlQueryHandler : IRequestHandler<FreeQrsShortysByUrlQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public FreeQrsShortysByUrlQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(FreeQrsShortysByUrlQuery request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        var results = request.Urls
            .Where(url => !string.IsNullOrWhiteSpace(url))
            .Select(url => new
            {
                url,
                qrCodeBase64 = $"data:image/png;base64,{QrHelper.GenerateQrBase64(url)}"
            })
            .ToList();

        return TypedResults.Ok(results);
    }
}
