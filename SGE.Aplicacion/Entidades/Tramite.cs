namespace SGE.Aplicacion;
/// <summary>
/// Clase publica Tramite, la cual ´posee dos constructores: un constructor sin parámetros y un constructor 
/// con tres parámetros (expedienteId, contenido, usuarioId).
/// </summary>
public class Tramite
{
    /// <value> Propiedad correspondiente al numero de tramite </value>
    public int TramiteId { get; set; }
    /// <value> Propiedad correspondiente al numero de expediente </value>
    public int ExpedienteId { get; set; }
    /// <value> Propiedad correspondiente a la etiqueta del tramite </value>
    public EtiquetaTramite Etiqueta { get; set; }
    /// <value> Propiedad correspondiente al contenido del tramite </value>
    public String Contenido { get; set; } = "";
    /// <value> Propiedad correspondiente a la fecha de creacion del tramite </value>
    public DateTime FechaCreacion { get; set; }
    /// <value> Propiedad correspondiente a la fecha de modificacion del tramite </value>
    public DateTime FechaModificacion { get; set; }
    /// <value> Propiedad correspondiente al numero de usuario de la calse <c>Tramite</c> </value>
    public int UsuarioId { get; set; }

    /// <summary>
    /// El constructor sin parámetros crea una instancia de Tramite sin inicializar sus propiedades.
    /// </summary>
    public Tramite() { }

    /// <summary>
    /// inicializa las propiedades ExpedienteId, Contenido y UsuarioId con los valores proporcionados. Además, establece
    /// la propiedad Etiqueta a EscritoPresentado y la propiedad FechaCreacion a la fecha y hora actual.
    /// </summary>
    /// <param name="expedienteId"></param>
    /// <param name="contenido"></param>
    /// <param name="usuarioId"></param>
    public Tramite(int expedienteId, String contenido, int usuarioId, EtiquetaTramite etiqueta)
    {
        ExpedienteId = expedienteId;
        Etiqueta = etiqueta;
        Contenido = contenido;
        FechaCreacion = DateTime.Now;
        UsuarioId = usuarioId;
    }
    /// <summary>
    /// Este metodo sobrescribe el método ToString de la clase base. Este método devuelve una cadena de texto que 
    /// representa el estado actual del objeto Tramite.
    /// </summary>
    /// <returns> Retorna una cadena de texto que representa al trámite</returns>
    public override String ToString()
    {
        return $" ID Tramite: {TramiteId} ID Expediente: {ExpedienteId}" +
        $" Contenido: {Contenido} Fecha de creacion: {FechaCreacion}\n" +
        $" Fecha de modificacion: {FechaModificacion} ID usuario: {UsuarioId}" +
        $" Etiqueta: {Etiqueta}";
    }
}