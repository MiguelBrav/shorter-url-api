namespace ShorterAPI.DTO.Responses;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public object? Response { get; set; }
    public string? ResponseMessage { get; set; }
}
