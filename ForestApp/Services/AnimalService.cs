using ForestApp.Models.Animal;
using ForestApp.Repositories;

namespace ForestApp.Services;

public interface IAnimalService
{
    Task<AnimalEntity> CreateAnimal(AnimalEntity animal);
    Task<AnimalEntity?> GetAnimalById(int id);
    Task UpdateAnimal(AnimalEntity animal);
    Task DeleteAnimal(AnimalEntity animal);
}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public async Task<AnimalEntity> CreateAnimal(AnimalEntity animal)
    {
        await _animalRepository.AddAnimal(animal);
        return animal;
    }

    public async Task<AnimalEntity?> GetAnimalById(int id)
    {
        return await _animalRepository.GetAnimalById(id);
    }

    public async Task UpdateAnimal(AnimalEntity animal)
    {
        await _animalRepository.UpdateAnimal(animal);
    }

    public async Task DeleteAnimal(AnimalEntity animal)
    {
        await _animalRepository.DeleteAnimal(animal);
    }
}