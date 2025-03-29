using MediatR;
using Microsoft.AspNetCore.Identity;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Entities;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Queries;

public class TopShortyReportQueryHandler : IRequestHandler<TopShortyReportQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public TopShortyReportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(TopShortyReportQuery request, CancellationToken cancellationToken)
    {
        if (request.Limit == 0)
        {
            return TypedResults.BadRequest("The limit need to be greater than zero.");
        }

        IEnumerable<ShortyTopResponse> response = await _unitOfWork.ShortyRepository.GetTopShortys(request.Limit);

        return TypedResults.Ok(response);
    }
}
