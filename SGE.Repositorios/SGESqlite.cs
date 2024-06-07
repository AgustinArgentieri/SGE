namespace SGE.Repositorios;

public class SGESqlite
{
    public static void Inicializar()
    {
        using var context = new SGEContext();
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Se creó base de datos");
        }
    }
}

