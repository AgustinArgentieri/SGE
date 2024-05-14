namespace SGE.Aplicacion;
/// <summary>
/// Esta clase se encarga de la creación de nuevos trámites en el sistema, recibe cuatro 
/// dependencias en su constructor. Se asegura que se cumplan todas las reglas y permisos
/// de seguridad, y actualiza el estado del expediente asociado.
/// </summary>
/// <param name="repoTra">Corresponde a la interfaz del repositorio de tramites</param>
/// <param name="validador">Corresponde al validador de trámites</param>
/// <param name="autorizador">Corresponde al servicio de autorización </param>
/// <param name="actualizarEstado">Corresponde al servicio de actualización de estado</param>
public class CasoDeUsoTramiteAlta (ITramiteRepositorio repoTra, TramiteValidador validador, 
IServicioAutorizacion autorizador, ServicioActualizacionEstado actualizarEstado)
{
        /// <summary>
        /// El metodo recibe un objeto Tramite que representa el trámite que se desea crear. 
        /// Se utiliza el validador de trámites para verificar que el trámite cumple con todas 
        /// las reglas necesarias para su creación. Si el trámite no es válido, se lanza una
        /// excepción ValidacionException con un mensaje de error, tambien se verifica que el 
        /// usuario posea permiso par crear un tramite, en caso que no lo posea se lanzaruna excepcion.
        /// En caso que se cumpla con los requisitos el trámite se agrega al repositorio de trámites
        /// utilizando el método AgregarTramite.Finalmente, se actualiza el estado del expediente asociado
        /// al trámite utilizando el servicio de actualización de estado.
        /// </summary>
        /// <param name="tra">Corresponde a un objeto Tramite</param>
        /// <exception cref="ValidacionException"></exception>
        /// <exception cref="AutorizacionException"></exception>
    public void Ejecutar(Tramite tra){
        if (!validador.Validar(tra, out string messageError))
        {
            throw new ValidacionException(messageError);
        }
        if (!autorizador.PoseeElPermiso(tra.UsuarioId,Permiso.TramiteAlta))
        {
            throw new AutorizacionException(tra.UsuarioId,Permiso.TramiteAlta);
        }
        repoTra.AgregarTramite(tra);
        actualizarEstado.Ejecutar(tra.ExpedienteId);
    }
}