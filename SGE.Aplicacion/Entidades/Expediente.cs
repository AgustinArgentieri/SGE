namespace SGE.Aplicacion;
/// <summary>
/// Clase publica de Expedientes, la cual posee dos constructores: un constructor sin parámetros y un constructor 
/// con dos parámetros (caratula,  usuarioId).
/// </summary>
public class Expediente
{   /// <value> Propiedad correspondiente al numero de expediente </value>
    public int ExpedienteId { get; set; } // se le debe poner el modificador publico?
    /// <value> Propiedad correspondiente a la caratula del expediente </value>
    public String? Caratula { get; set; } // como resolver este warning de string q seria con signo de pregunta, es correcto? se ouede resolver lanzando un try catch q diga q si o si tiene q poner nombre?
    /// <value> Propiedad correspondiente a la fecha de creacion del expediente </value>
    public DateTime FechaCreacion { get; set; } // se puede hacer el DateTime.Now en este set?;
    /// <value> Propiedad correspondiente a la fecha de modificacion del expediente </value>
    public DateTime FechaModificacion { get; set; }
    /// <value> Propiedad correspondiente al numero de usuario </value>
    public int UsuarioId { get; set; }
    /// <value> Propiedad correspondiente al estado del expediente </value>
    public EstadoExpediente Estado { get; set; }
    /// <value> Propiedad correspondiente a la lista de tramites asociados a un expediente </value>
    public List<Tramite>? Tramites { get; set; }
    /// <summary>
    /// Constructor vacio de la clase Expediente.
    /// </summary>
    public Expediente() { }
    /// <summary>
    /// Constructor de la clase <c>Expediente </c> que inicializa un nuevo Expediente segun (<paramref name="caratula"/>,
    /// <paramref name="usuarioId"/>). 
    /// </summary>
    /// <param name="caratula">Corresponde al nombre de la caratula del expediente</param>
    /// <param name="usuarioId">Corresponde al numero de usuario</param>
    public Expediente(String caratula, int usuarioId)
    {
        Caratula = caratula;
        FechaCreacion = DateTime.Now;
        UsuarioId = usuarioId;
        Estado = EstadoExpediente.RecienIniciado;
    }
    /// <summary>
    /// Este metodo sobreescribe el metodo toString el cual devuele la impresion de los datos de un 
    /// expediente.
    /// </summary>
    /// <returns> Reorna una cadena de texto que representa un expediente</returns>
    public override String ToString()
    {
        return $" ID Expediente: {ExpedienteId} Caratula {Caratula}" +
        $" Fecha de creacion: {FechaCreacion} Fecha de modificacion: {FechaModificacion}\n" +
        $" ID usuario: {UsuarioId} Estado: {Estado}";
    }


}
