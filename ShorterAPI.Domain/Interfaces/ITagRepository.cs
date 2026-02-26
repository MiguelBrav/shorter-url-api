using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Domain.Interfaces;

public interface ITagRepository
{
    Task<int> GetOrCreate(string tagName);
    Task AssignToShorty(int shortyId, int tagId);
    Task<IEnumerable<Tag>> GetOrCreateMany(IEnumerable<string> tags);
    Task AssignManyToShorty(int shortyId, IEnumerable<Tag> tags);
}
