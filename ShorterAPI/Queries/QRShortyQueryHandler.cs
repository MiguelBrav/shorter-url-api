using Mediator;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.Helpers;

namespace ShorterAPI.Queries;

public class QRShortyQueryHandler : IRequestHandler<QRShortyQuery, IResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly IUnitOfWork _unitOfWork;

    public QRShortyQueryHandler(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;

    }
    public async ValueTask<IResult> Handle(QRShortyQuery request, CancellationToken cancellationToken)
    {
        IdentityUser userExists = await _userManager.FindByNameAsync(request.UserName);

        if (userExists == null)
        {
            return TypedResults.NotFound("The user does not exists");
        }

        Shorty shorty = await _unitOfWork.ShortyRepository.ByIdByUser(userExists.Id, request.ShortyId);

        if (shorty is null)
        {
            return TypedResults.NoContent();
        }

        string base64 = QrHelper.GenerateQrBase64(shorty.FullUrl);

        return TypedResults.Ok(new
        {
            url = shorty.FullUrl,
            qrCodeBase64 = $"data:image/png;base64,{base64}"
        });
    }
}
