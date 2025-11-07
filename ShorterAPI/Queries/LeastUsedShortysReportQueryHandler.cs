using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Queries;

public class LeastUsedShortysReportQueryHandler : IRequestHandler<LeastUsedShortysReportQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public LeastUsedShortysReportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(LeastUsedShortysReportQuery request, CancellationToken cancellationToken)
    {
        if (request.Limit == 0)
        {
            return TypedResults.BadRequest("The limit need to be greater than zero.");
        }

        IEnumerable<ShortyLeastUsedResponse> response = await _unitOfWork.ShortyRepository.GetLeastUsedShortys(request.Limit);

        return TypedResults.Ok(response);
    }
}
