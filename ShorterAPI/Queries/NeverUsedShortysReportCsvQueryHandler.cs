using MediatR;
using ShorterAPI.Domain.UOW;
using ShorterAPI.DTO.Responses;
using ShorterAPI.Helpers;

namespace ShorterAPI.Queries;

public class NeverUsedShortysReportCsvQueryHandler : IRequestHandler<NeverUsedShortysReportCsvQuery, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public NeverUsedShortysReportCsvQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<IResult> Handle(NeverUsedShortysReportCsvQuery request, CancellationToken cancellationToken)
    {
        if (request.Limit == 0)
        {
            return TypedResults.BadRequest("The limit need to be greater than zero.");
        }

        IEnumerable<ShortyNeverUsedResponse> response = await _unitOfWork.ShortyRepository.GetNeverUsedShortys(request.Limit);

        byte[] bytes = CsvHelper.ExportToCsv(response);

        string base64 = Convert.ToBase64String(bytes);

        string fileName = $"never_shortys_{DateTime.UtcNow:yyyyMMddHHmmss}.csv";

        return TypedResults.Ok(new
        {
            FileName = fileName,
            ContentBase64 = base64
        });
    }
}
