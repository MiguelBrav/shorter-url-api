using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Queries;

public class LastShortyReportQueryHandler : IRequestHandler<LastShortyReportQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public LastShortyReportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(LastShortyReportQuery request, CancellationToken cancellationToken)
    {
        if (request.Limit == 0)
        {
            return TypedResults.BadRequest("The limit need to be greater than zero.");
        }

        IEnumerable<LastShortyResponse> response = await _unitOfWork.ShortyRepository.GetLastShortys(request.Limit);

        return TypedResults.Ok(response);
    }
}

