namespace SGE.Aplicacion;
/// <summary>
/// Esta clase define una especificación para el cambio de estado de un expediente, la cual proporciona
/// una forma de determinar el estado de un expediente.
/// </summary>
public class EspecificacionCambioEstado
{
    /*
    public Dictionary<EtiquetaTramite, EstadoExpediente> dic = new Dictionary<EtiquetaTramite, EstadoExpediente>
    {
        { EtiquetaTramite.Resolucion, EstadoExpediente.ConResolucion },
        { EtiquetaTramite.PaseAEstudio, EstadoExpediente.ParaResolver },
        { EtiquetaTramite.PaseAlArchivo, EstadoExpediente.Finalizado },

    };

    public EstadoExpediente? Obtener(EtiquetaTramite etiqueta) 
    {
        if (dic.ContainsKey(etiqueta))
        {
            return dic[etiqueta]; //devuelve el valor de la llave solicitada. En este caso, la llave es la etiqueta del tramite y el valor el estado del expediente.
        }
        else
        {
            return null; // Devuelve null si la clave no se encuentra en el diccionario.
        }
    }
    */

    /// <summary>
    /// Este método se utiliza para obtener el estado de un expediente basado en la etiqueta de un trámite. Recibe
    /// un parámetro.
    /// </summary>
    /// <param name="etiqueta">Corresponde a la etiqueta del trámite</param>
    /// <returns>Devuelve un estado de expediente (EstadoExpediente) si la etiqueta del trámite corresponde
    /// a un cambio de estado, y null en caso contrario.</returns>
    public EstadoExpediente? Obtener(EtiquetaTramite etiqueta)
    {
        switch (etiqueta)
        {
                case EtiquetaTramite.Resolucion: return EstadoExpediente.ConResolucion;
                case EtiquetaTramite.PaseAEstudio: return EstadoExpediente.ParaResolver;
                case EtiquetaTramite.PaseAlArchivo: return EstadoExpediente.Finalizado;
                default: return null;
        }
    }
}
