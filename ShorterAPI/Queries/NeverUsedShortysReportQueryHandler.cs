using MediatR;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Responses;

namespace ShorterAPI.Queries;

public class NeverUsedShortysReportQueryHandler : IRequestHandler<NeverUsedShortysReportQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public NeverUsedShortysReportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(NeverUsedShortysReportQuery request, CancellationToken cancellationToken)
    {
        if (request.Limit == 0)
        {
            return TypedResults.BadRequest("The limit need to be greater than zero.");
        }

        IEnumerable<ShortyNeverUsedResponse> response = await _unitOfWork.ShortyRepository.GetNeverUsedShortys(request.Limit);

        return TypedResults.Ok(response);
    }
}
