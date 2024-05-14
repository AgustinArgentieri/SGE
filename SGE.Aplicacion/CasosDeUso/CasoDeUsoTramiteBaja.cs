namespace SGE.Aplicacion;
/// <summary>
/// Esta clase se encarga de la eliminación de trámites existentes en el sistema, la misma  
/// recibe tres dependencias en su constructor, se asegura que se cumplan todas las reglas 
/// y permisos de seguridad, y se actualiza el estado del expediente asociado.
/// </summary>
/// <param name="repoT"> Corresponde a la interfaz de repositorio de tramites </param>
/// <param name="autorizador">Corresponde al servicio de autorizacion </param>
/// <param name="actualizarEstado">Corresponde al servicio de actualización de estado</param>
public class CasoDeUsoTramiteBaja(ITramiteRepositorio repoT, IServicioAutorizacion autorizador, ServicioActualizacionEstado actualizarEstado)
{
    /// <summary>
    /// Este metodo recibe dos parámetros: el ID del trámite que se desea eliminar (tramiteId) 
    /// y el ID del usuario que realiza la acción (usuarioId). Verifica que el ID del usuario sea
    /// válido, en caso que no lo sea, se lanza una excepción ValidacionException con un mensaje de error.
    /// Tambien verifica que el usuario tiene el permiso necesario (Permiso.TramiteBaja) para eliminar un
    /// trámite, en caso que no lo tenga, se lanza una excepción AutorizacionException.
    /// En caso que se cumpla con los rquisitos,  se elimina el trámite del repositorio de trámites utilizando 
    /// el método EliminarTramite, el cual devuelve el ID del expediente asociado al trámite eliminado. Finalmente, 
    /// se actualiza el estado del expediente asociado al trámite eliminado utilizando el método  del servicio
    /// de actualización de estado.
    /// </summary>
    /// <param name="tramiteId">Corresponde al numero de tramite </param>
    /// <param name="usuarioId">Corresponde al numero de usuario </param>
    /// <exception cref="ValidacionException"></exception>
    /// <exception cref="AutorizacionException"></exception>
    public void Ejecutar(int tramiteId, int usuarioId)
    {
        if (usuarioId < 1)
            throw new ValidacionException("Id de usuario Incorrecto");
        if (!autorizador.PoseeElPermiso(usuarioId, Permiso.TramiteBaja))
            throw new AutorizacionException(usuarioId, Permiso.TramiteBaja);
        int expedienteId = repoT.EliminarTramite(tramiteId);
        actualizarEstado.Ejecutar(expedienteId);
    }
}
