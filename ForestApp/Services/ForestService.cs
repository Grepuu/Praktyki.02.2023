using ForestApp.Models.Forest;
using ForestApp.Repositories;

namespace ForestApp.Services;

public interface IForestService
{
    Task AddForest(ForestEntity forest);
    Task RemoveForest(int forestId);
    Task<ForestEntity?> GetForestById(int forestId);
    Task<List<ForestEntity>> GetAllForests();
    Task<ForestEntity?> GetForestForUser(string userId, int forestId);
}

public class ForestService : IForestService
{
    private readonly IForestRepository _forestRepository;
    private readonly IUserService _userService;

    public ForestService(IForestRepository forestRepository, IUserService userService)
    {
        _forestRepository = forestRepository;
        _userService = userService;
    }

    public async Task AddForest(ForestEntity forest)
    {
        await _forestRepository.AddForest(forest);
    }

    public async Task RemoveForest(int forestId)
    {
        await _forestRepository.RemoveForest(forestId);
    }

    public async Task<ForestEntity?> GetForestById(int forestId)
    {
        return await _forestRepository.GetForestById(forestId);
    }

    public async Task<List<ForestEntity>> GetAllForests()
    {
        return await _forestRepository.GetAllForests();
    }
    
    public async Task<ForestEntity?> GetForestForUser(string userId, int forestId)
    {
        var forest = await _forestRepository.GetForestById(forestId);
        if (forest == null || forest.OwnerId != userId)
        {
            return null;
        }

        return forest;
    }
}