using MediatR;

namespace ShorterAPI.Queries;

public class AllShortyReportQuery : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public int PageSize { get; set; }
    public int PageNumber { get; set; }

}
