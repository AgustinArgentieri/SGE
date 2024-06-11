using SGE.Aplicacion;
using Microsoft.EntityFrameworkCore;

namespace SGE.Repositorios;

public class SGESqlite : IExpedienteRepositorio, ITramiteRepositorio, IUsuarioRepositorio
{
    public static void Inicializar()
    {
        using var context = new SGEContext();
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Se creó base de datos");
        }
        else
        {
            //public Tramite(int expedienteId, String contenido, int usuarioId, EtiquetaTramite etiqueta)
            //Console.WriteLine("Agrego un tramite");
            //context.Add(new Expediente());
            //context.Add(new Tramite(1, "CONTENIDOOO", 1, EtiquetaTramite.Notificacion));
            //context.SaveChanges();
            Console.WriteLine("-- Tabla Expedientes --");
            foreach (var ex in context.Expedientes)
            {
                Console.WriteLine($"{ex.ExpedienteId} {ex.Caratula} {ex.FechaCreacion} {ex.FechaModificacion} {ex.UsuarioId} {ex.Estado}");
            }
            Console.WriteLine("-- Tabla Tramites --");
            foreach (var tr in context.Tramites)
            {
                Console.WriteLine($"{tr.TramiteId} {tr.ExpedienteId} {tr.Etiqueta} {tr.Contenido} {tr.FechaCreacion} ");
            }


        }
    }

    //IExpedienteRepositorio
    public void AgregarExpediente(Expediente exp)
    {
        using var context = new SGEContext();
        context.Add(exp);
        context.SaveChanges();
    }
    public void EliminarExpediente(int expedienteId)
    {
        using var context = new SGEContext();
        var expedienteBorrar = context.Expedientes.Where(e => e.ExpedienteId == expedienteId).SingleOrDefault();
        if (expedienteBorrar != null)
        {
            context.Remove(expedienteBorrar); //se borra realmente con el db.SaveChanges()
        }
        context.SaveChanges();
    }
    public Expediente? ConsultarExpediente(int expedienteId)
    {
        using var context = new SGEContext();
        var expe = context.Expedientes.Where(exp => exp.ExpedienteId == expedienteId).SingleOrDefault() 
            ?? throw new RepositorioException($"No se encontro el expediente con id: {expedienteId}");
        return expe;
    }
    public List<Expediente>? ListarExpedientes()
    {
        using var context = new SGEContext();
        return context.Expedientes.ToList();
    }
    public void ModificarExpediente(Expediente exp)
    { 
        using var context = new SGEContext();
        var expedienteModificar = context.Expedientes.Where(e => e.ExpedienteId == exp.ExpedienteId).SingleOrDefault();
        
        if (expedienteModificar != null)
        {
            expedienteModificar = exp;
            context.SaveChanges();
        }
        else
            throw new RepositorioException($"No se encontro el expediente con id: {exp.ExpedienteId}");
    }
    
    public void ModificarEstadoExpediente(int ExpedienteId, EstadoExpediente nuevoEstado)
    {
        using var context = new SGEContext();
        var expedienteModificar = context.Expedientes.Where(e => e.ExpedienteId == ExpedienteId).SingleOrDefault();
        
        if (expedienteModificar != null)
        {
            expedienteModificar.Estado = nuevoEstado;
            context.SaveChanges();
        }
        else
            throw new RepositorioException($"No se encontro el expediente con id: {ExpedienteId}");
    }
    //ITramiteRepositorio
    public List<Tramite>? ConsultarTramites(int expedienteId)
    {  
        using var context = new SGEContext();
        var tramites = context.Tramites.Where(t => t.ExpedienteId == expedienteId)
            ?? throw new RepositorioException($"No se encontraron tramites con id de expediente: {expedienteId}");
        return tramites.ToList();
    }
    public void AgregarTramite(Tramite tramite)
    { 
        using var context = new SGEContext();
        context.Add(tramite);
        context.SaveChanges();
    }
    public int EliminarTramite(int tramiteId)
    {
        using var context = new SGEContext();
        var tramiteBorrar = context.Tramites.Where(t => t.TramiteId == tramiteId).SingleOrDefault()
            ?? throw new RepositorioException($"No se encontro el tramite con id: {tramiteId}");
        var expId = tramiteBorrar.ExpedienteId;
        context.Remove(tramiteBorrar); //se borra realmente con el db.SaveChanges()
        context.SaveChanges();
        return expId;
    }
    public void EliminarTramites(int expedienteId)
    { 
        using var context = new SGEContext();
        var expe = context.Expedientes.Where(e => e.ExpedienteId == expedienteId).SingleOrDefault()
            ?? throw new RepositorioException($"No se encontro el expediente con id: {expedienteId}");
        expe.Tramites = new List<Tramite>();
        context.SaveChanges();
    }
    public void ModificarTramite(Tramite tramiteNuevo)
    {
        using var context = new SGEContext();
        var tramiteModificar = context.Tramites.Where(t => t.TramiteId == tramiteNuevo.TramiteId).SingleOrDefault()        
            ?? throw new RepositorioException($"No se encontro el tramite con id: {tramiteNuevo.TramiteId}");
        tramiteModificar = tramiteNuevo;
        context.SaveChanges();
    }

    public Tramite? ConsultarTramite(int tramiteId)
    {
        using var context = new SGEContext();
        var tramite = context.Tramites.Where(t => t.TramiteId == tramiteId).SingleOrDefault()        
            ?? throw new RepositorioException($"No se encontro el tramite con id: {tramiteId}");
        return tramite;
    }
    public List<Tramite>? ConsultarTramitesPorEtiqueta(EtiquetaTramite etiqueta)
    { 
        using var context = new SGEContext();
        var tramites = context.Tramites.Where(t => t.Etiqueta == etiqueta)        
            ?? throw new RepositorioException($"No se encontro el tramite con la etiqueta: {etiqueta}");
        return tramites.ToList();
    }

    //IUsuarioRepositorio
    public void CrearUsuario()
    { }
    public List<Usuario> ListarUsuarios()
    { return null; }
    public void BajaUsuario()
    { }
    public void ModificarUsuario()
    { }

}

