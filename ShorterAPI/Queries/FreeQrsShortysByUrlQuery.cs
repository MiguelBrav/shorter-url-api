using MediatR;

namespace ShorterAPI.Queries;

public class FreeQrsShortysByUrlQuery : IRequest<IResult>
{
    public string UserName { get; set; } = string.Empty;
    public List<string> Urls { get; set; } = new();
}
