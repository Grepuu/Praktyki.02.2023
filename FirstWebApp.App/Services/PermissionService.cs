namespace DefaultNamespace;

public class PermissionService
{
    List<RecordPermission> ListaPermisji();
}

public class PermissionService : IPermissionService
{
    public List<RecordPermission> ListaPermisji();

    var model = new List<RecordPermission>();

    model.Add(new RecordPermission()
    {
        Id =
        DataDodania = 
        Tytul =
        Opis =
        OdKiedy =
        DoKiedy = 

    });
    
    return model;
}
