using Microsoft.EntityFrameworkCore;
using ShorterAPI.Domain.Interfaces;
using ShorterAPI.DTO.Entities;

namespace ShorterAPI.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ApplicationDbContext _context;

    public TagRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetOrCreate(string tagName)
    {
        tagName = tagName.ToLower().Trim();

        Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName);

        if (tag != null)
            return tag.Id;

        tag = new Tag(tagName);
        _context.Tags.Add(tag);

        return tag.Id;
    }

    public async Task AssignToShorty(int shortyId, int tagId)
    {
        var exists = await _context.ShortyTags
            .AnyAsync(st => st.ShortyId == shortyId && st.TagId == tagId);

        if (exists) return;

        _context.ShortyTags.Add(new ShortyTag
        {
            ShortyId = shortyId,
            TagId = tagId
        });
    }

    public async Task<IEnumerable<Tag>> GetOrCreateMany(IEnumerable<string> tags)
    {
        var normalizedTags = tags
            .Select(t => t.ToLower().Trim())
            .Distinct()
            .ToList();

        var existingTags = await _context.Tags
            .Where(t => normalizedTags.Contains(t.Name))
            .ToListAsync();

        var existingNames = existingTags
            .Select(t => t.Name)
            .ToHashSet();

        var newTags = normalizedTags
            .Where(t => !existingNames.Contains(t))
            .Select(t => new Tag(t))
            .ToList();

        if (newTags.Any())
        {
            _context.Tags.AddRange(newTags);
        }

        return existingTags
            .Concat(newTags);
    }

    public async Task AssignManyToShorty(int shortyId, IEnumerable<Tag> tagEntities)
    {
        var tagIds = tagEntities.Where(t => t.Id > 0).Select(t => t.Id).ToList();

        var existingRelations = await _context.ShortyTags
            .Where(st => st.ShortyId == shortyId && tagIds.Contains(st.TagId))
            .Select(st => st.TagId)
            .ToListAsync();

        var relationsToAdd = new List<ShortyTag>();

        var existingTagRelations = tagEntities
            .Where(t => t.Id > 0 && !existingRelations.Contains(t.Id))
            .Select(t => new ShortyTag
            {
                ShortyId = shortyId,
                TagId = t.Id
            });

        relationsToAdd.AddRange(existingTagRelations);

        var newTagEntities = tagEntities.Where(t => t.Id == 0).ToList();
        foreach (var newTag in newTagEntities)
        {
            relationsToAdd.Add(new ShortyTag
            {
                ShortyId = shortyId,
                Tag = newTag
            });
        }

        if (relationsToAdd.Any())
        {
            _context.ShortyTags.AddRange(relationsToAdd);
        }
    }

    public async Task<IEnumerable<Tag>> GetByShorty(int shortyId)
    {
        var tags = await _context.ShortyTags
            .Where(st => st.ShortyId == shortyId)
            .Include(st => st.Tag)
            .Select(st => st.Tag)
            .ToListAsync();

        return tags;
    }
}
