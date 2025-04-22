using MediatR;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Queries;

public class RandomShortyReportQueryHandler : IRequestHandler<RandomShortyReportQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public RandomShortyReportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(RandomShortyReportQuery request, CancellationToken cancellationToken)
    {
        if (request.Limit == 0)
        {
            return TypedResults.BadRequest("The limit need to be greater than zero.");
        }

        IEnumerable<RandomShortyResponse> response = await _unitOfWork.ShortyRepository.GetRandomShortys(request.Limit);

        return TypedResults.Ok(response);
    }
}

