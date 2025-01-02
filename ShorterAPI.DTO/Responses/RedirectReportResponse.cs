using ShorterAPI.DTO.Entities;

namespace ShorterAPI.DTO.Responses;

public class RedirectReportResponse
{
    public int ShortyId { get; set; }

    public int Total {  get; set; }

    public List<RedirectShorty> Redirects { get; set; } = new List<RedirectShorty>();

    public RedirectReportResponse() { }
    public RedirectReportResponse(List<LogRedirect> logRedirects)
    {
        if (logRedirects == null)
            throw new ArgumentNullException(nameof(logRedirects));

        Total = logRedirects.Count;
        ShortyId = logRedirects.Select(x => x.ShortyId).FirstOrDefault();
        Redirects = logRedirects.Select(log => new RedirectShorty
        {
            Id = log.Id,
            dateTime = log.RedirectDate
        }).ToList();
    }
}

public class RedirectShorty
{
    public int Id { get; set; }
    public DateTime dateTime { get; set; }
}
