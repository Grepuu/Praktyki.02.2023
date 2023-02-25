using ForestApp.Data;
using ForestApp.Models.Animal;
using Microsoft.EntityFrameworkCore;

namespace ForestApp.Repositories;

public interface IAnimalRepository
{
    Task<AnimalEntity?> GetAnimalById(int id);
    Task AddAnimal(AnimalEntity animal);
    Task UpdateAnimal(AnimalEntity animal);
    Task DeleteAnimal(AnimalEntity animal);
}

public class AnimalRepository : IAnimalRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AnimalRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AnimalEntity?> GetAnimalById(int id)
    {
        return await _dbContext.Animals
            .Include(a => a.Forest)
            .FirstOrDefaultAsync(t => t.Id == id) ?? throw new InvalidOperationException("Animal not found");;
    }

    public async Task AddAnimal(AnimalEntity animal)
    {
        await _dbContext.Animals.AddAsync(animal);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAnimal(AnimalEntity animal)
    {
        _dbContext.Animals.Update(animal);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAnimal(AnimalEntity animal)
    {
        _dbContext.Animals.Remove(animal);
        await _dbContext.SaveChangesAsync();
    }
}
