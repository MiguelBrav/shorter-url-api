using MediatR;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Queries;

public class RedirectQueryHandler : IRequestHandler<RedirectQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public RedirectQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(RedirectQuery request, CancellationToken cancellationToken)
    {
        Shorty shorty = await _unitOfWork.ShortyRepository.isExistsShorty(request.ShortyUrl);

        if (shorty is null)
        {
            return TypedResults.NoContent();
        }

        return TypedResults.Redirect(shorty.FullUrl);
    }
}
