namespace SGE.Aplicacion;
/// <summary>
///  Esta interfaz proporciona un conjunto de operaciones que se pueden realizar en un repositorio de expedientes, 
///  permitiendo agregar, eliminar, consultar, listar y modificar expedientes. Provee la declaracion de los metodos
///  que se trabajaran respecto  a los expedientes, en todas las clases que lo implementen, como es el caso
///  del RepositorioExpedienteTXT,
/// </summary>
public interface IExpedienteRepositorio
{
    public void AgregarExpediente(Expediente exp);
    public void EliminarExpediente(int expedienteId);
    public Expediente? ConsultarExpediente(int expedienteId);
    public List<Expediente>? ListarExpedientes();
    public void ModificarExpediente(Expediente exp);
    public void ModificarEstadoExpediente(int ExpedienteId, EstadoExpediente nuevoEstado);
}
