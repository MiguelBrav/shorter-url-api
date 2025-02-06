namespace ShorterAPI.Domain.Interfaces;

public interface IShortyService
{
    string GenerateShortyUrl(string chars, int length = 6);
}
