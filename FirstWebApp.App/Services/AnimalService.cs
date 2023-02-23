namespace DefaultNamespace;

public interface IAnimalService
{
    List<RecordAnimal> ListaZwierzat();
}

public class AnimalService : IAnimalService
{
    public List<RecordAnimal> ListaZwierzat();

    var model = new List<RecordAnimal>();

    model.Add(new RecordHobby()
    {
        DataDodania =
        Nazwa =
        Opis =
        WielkoscStada =
        CzyZagrozone = 
    });
    
    return model;
}