namespace DefaultNamespace;

public class TreeService
{
    List<RecordTree> ListaDrzew();
}

public class TreeService : ITreeService
{
    public List<RecordTree> ListaDrzew();

    var model = new List<RecordTree>();

    model.Add(new RecordTree()
    {
        Id =
        DataDodania =
        Nazwa = 
        Opis =
        OpisLiscia =
        Maksymalna wysokość =

    });
    
    return model;
}