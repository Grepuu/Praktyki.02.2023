using ForestApp.Models.Tree;
using ForestApp.Repositories;

namespace ForestApp.Services;

public interface ITreeService
{
    Task<TreeEntity> CreateTree(TreeEntity tree);
    Task<TreeEntity?> GetTreeById(int id);
    Task UpdateTree(TreeEntity tree);
    Task DeleteTree(TreeEntity tree);
}

public class TreeService : ITreeService
{
    private readonly ITreeRepository _treeRepository;

    public TreeService(ITreeRepository treeRepository)
    {
        _treeRepository = treeRepository;
    }

    public async Task<TreeEntity> CreateTree(TreeEntity tree)
    {
        await _treeRepository.AddTree(tree);
        return tree;
    }

    public async Task<TreeEntity?> GetTreeById(int id)
    {
        return await _treeRepository.GetTreeById(id);
    }

    public async Task UpdateTree(TreeEntity tree)
    {
        await _treeRepository.UpdateTree(tree);
    }

    public async Task DeleteTree(TreeEntity tree)
    {
        await _treeRepository.DeleteTree(tree);
    }
}