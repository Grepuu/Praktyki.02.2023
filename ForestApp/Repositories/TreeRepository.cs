using ForestApp.Data;
using ForestApp.Models.Tree;
using Microsoft.EntityFrameworkCore;

namespace ForestApp.Repositories;

public interface ITreeRepository
{
    Task<TreeEntity?> GetTreeById(int id);
    Task AddTree(TreeEntity tree);
    Task UpdateTree(TreeEntity tree);
    Task DeleteTree(TreeEntity tree);
}

public class TreeRepository : ITreeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TreeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TreeEntity?> GetTreeById(int id)
    {
        return await _dbContext.Trees
            .Include(t => t.Forest)
            .FirstOrDefaultAsync(t => t.Id == id) ?? throw new InvalidOperationException("Tree not found");;
    }

    public async Task AddTree(TreeEntity tree)
    {
        await _dbContext.Trees.AddAsync(tree);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTree(TreeEntity tree)
    {
        _dbContext.Trees.Update(tree);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTree(TreeEntity tree)
    {
        _dbContext.Trees.Remove(tree);
        await _dbContext.SaveChangesAsync();
    }
}
