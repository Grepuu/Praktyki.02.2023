using ForestApp.Data;
using ForestApp.Models.Forest;
using Microsoft.EntityFrameworkCore;

namespace ForestApp.Repositories;

public interface IForestRepository
{
    Task AddForest(ForestEntity forest);
    Task RemoveForest(int forestId);
    Task<ForestEntity?> GetForestById(int forestId);
    Task<List<ForestEntity>> GetAllForests();
}

public class ForestRepository : IForestRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ForestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddForest(ForestEntity forest)
    {
        await _dbContext.Forests.AddAsync(forest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveForest(int forestId)
    {
        var forest = await _dbContext.Forests.FindAsync(forestId);

        if (forest != null)
        {
            _dbContext.Forests.Remove(forest);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<ForestEntity?> GetForestById(int forestId)
    {
        return await _dbContext.Forests.FindAsync(forestId);
    }

    public async Task<List<ForestEntity>> GetAllForests()
    {
        return await _dbContext.Forests.ToListAsync();
    }
}