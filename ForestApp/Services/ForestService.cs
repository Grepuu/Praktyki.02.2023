using ForestApp.Models.Animal;
using ForestApp.Models.Forest;
using ForestApp.Models.Permission;
using ForestApp.Models.Tree;
using ForestApp.Repositories;

namespace ForestApp.Services;

public interface IForestService
{
    Task AddForest(ForestEntity forest);
    Task RemoveForest(int forestId);
    Task<ForestEntity?> GetForestById(int forestId);
    Task<List<ForestEntity>> GetAllForests();
    Task<ForestEntity?> GetForestForUser(string userId, int forestId);
    Task<List<ForestEntity>> GetAllUserForests(string userId);
    Task AddTreeToForest(int forestId, TreeEntity tree);
    Task AddAnimalToForest(int forestId, AnimalEntity animal);
    Task AddPermissionToForest(int forestId, PermissionEntity permission);
}

public class ForestService : IForestService
{
    private readonly IForestRepository _forestRepository;
    
    public ForestService(IForestRepository forestRepository)
    {
        _forestRepository = forestRepository;
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

    public async Task<List<ForestEntity>> GetAllUserForests(string userId)
    {
        return await _forestRepository.GetAllUserForests(userId);
    }

    public async Task AddTreeToForest(int forestId, TreeEntity tree)
    {
        var forest = await _forestRepository.GetForestById(forestId);
        if (forest == null)
        {
            throw new ArgumentException("Forest with given id does not exist.", nameof(forestId));
        }

        tree.Forest = forest;
        forest.Trees.Add(tree);
        await _forestRepository.UpdateForest(forest);
    }

    public async Task AddAnimalToForest(int forestId, AnimalEntity animal)
    {
        var forest = await _forestRepository.GetForestById(forestId);
        if (forest == null)
        {
            throw new ArgumentException("Forest with given id does not exist.", nameof(forestId));
        }

        animal.Forest = forest;
        forest.Animals.Add(animal);
        await _forestRepository.UpdateForest(forest);
    }
    
    public async Task AddPermissionToForest(int forestId, PermissionEntity permission)
    {
        var forest = await _forestRepository.GetForestById(forestId);
        if (forest == null)
        {
            throw new ArgumentException("Forest with given id does not exist.", nameof(forestId));
        }

        permission.Forest = forest;
        forest.Permissions.Add(permission);
        await _forestRepository.UpdateForest(forest);
    }
}