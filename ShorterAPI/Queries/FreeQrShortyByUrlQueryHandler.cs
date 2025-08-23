using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.Helpers;

namespace ShorterAPI.Queries;

public class FreeQrShortyByUrlQueryHandler : IRequestHandler<FreeQrShortyByUrlQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public FreeQrShortyByUrlQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(FreeQrShortyByUrlQuery request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        string base64 = QrHelper.GenerateQrBase64(request.FullUrl);

        return TypedResults.Ok(new
        {
            url = request.FullUrl,
            qrCodeBase64 = $"data:image/png;base64,{base64}"
        });
    }
}
