using ShorterAPI.Domain.Interfaces;

namespace ShorterAPI.Domain.Services;

public class ShortyService : IShortyService
{
    public string GenerateShortyUrl(string chars, int length = 6)
    {
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length)
                                    .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
