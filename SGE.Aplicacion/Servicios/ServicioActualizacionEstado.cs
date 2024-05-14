namespace SGE.Aplicacion;
/// <summary>
/// Es un servicio que maneja la actualización del estado de un expediente, esta clase recibe
/// tres dependencias en su constructor.
/// </summary>
/// <param name="repoE">Corresponde a la interfaz del repositorio de expedientes</param>
/// <param name="repoT">Corresponde a la inyerfaz del repositorio de tramites</param>
/// <param name="esp">Corresponde a una especificacion de cambio de estado</param>
public class ServicioActualizacionEstado(IExpedienteRepositorio repoE, ITramiteRepositorio repoT, EspecificacionCambioEstado esp)
{

    /// <summary>
    /// Agregar, eliminar o modificar un tramite asociado a un expediente puede ejecutar el cambio de estado del expediente.
    /// a partir del ultimo tramite realizo el cambio de estado del expediente segun la especificacion inyectada. El mismo
    /// consulta todos los trámites asociados al expediente en el repositorio de trámites utilizando el método ConsultarTramites.
    /// Si hay trámites para el expediente, el método obtiene el último trámite y utiliza la especificación de cambio de estado 
    /// para obtener el nuevo estado del expediente basado en la etiqueta del último trámite. Si el nuevo estado del expediente 
    /// es válido (es decir, no es null), el método actualiza el estado del expediente en el repositorio de expedientes utilizando 
    /// el método ModificarEstadoExpediente.
    /// </summary>
    /// <param name="expedienteId">Corresponde al ID del expediente</param>
    public void Ejecutar(int expedienteId)
    {
        EstadoExpediente? nuevoEstadoE = null;
        List<Tramite>? listaTramites = repoT.ConsultarTramites(expedienteId); //busco todos los tramites de un expediente

        if (listaTramites is not null)  //si el expediente tiene tramites
        {
            Tramite ultimoTramite = listaTramites[listaTramites.Count - 1]; //Busco el ultimo tramite

            nuevoEstadoE = esp.Obtener(ultimoTramite.Etiqueta); //obtengo el nuevo estado llamando a la especificacion.
        }

        if (nuevoEstadoE != null) //si es necesario modificar el estado, llamo al repositorio para que haga la modificacion.
        {
            repoE.ModificarEstadoExpediente(expedienteId, (EstadoExpediente)nuevoEstadoE); //casteo el nuevoEstadoE, para eliminar la posibilidad de que sea nulo, ya que lo verifico previamente.
        }

    }


}
