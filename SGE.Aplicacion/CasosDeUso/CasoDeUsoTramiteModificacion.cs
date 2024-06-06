namespace SGE.Aplicacion;
/// <summary>
/// Esta clase se encarga de la modificación de trámites existentes en el sistema, la cual recibe 
/// tres dependencias en su constructor, se aseguran que se cumplan todas las reglas de negocio 
/// y permisos de seguridad, y actualiza el estado del expediente asociado.
/// </summary>
/// <param name="repo">Corresponde a la interfaz del repositorio de tramites</param>
/// <param name="autorizador">Corresponde a la interfaz de servicio de autorización</param>
/// <param name="actualizarEstado">Corresponde al servicio de actualización de estado</param>
public class CasoDeUsoTramiteModificacion(ITramiteRepositorio repo,TramiteValidador validador,IServicioAutorizacion autorizador, ServicioActualizacionEstado actualizarEstado)
{
    /// <summary>
    /// Este metodo recibe cinco parámetros:  (tramiteId),  (usuarioId), (EtiquetaTramite) y el nuevo contenido del 
    /// trámite (contenido). El metodo verifica que el ID del usuario sea válido. Si no lo es, se lanza una excepción 
    /// ValidacionException con un mensaje de error. Luego, se verifica que el usuario tenga permiso  
    /// (Permiso.TramiteModificacion) para modificar un trámite. Si no lo tiene, se lanza una excepción AutorizacionException.
    /// Se consulta el trámite existente en el repositorio de trámites utilizando el método ConsultarTramite, si no existe, 
    /// se lanza una excepción RepositorioException, caso contrario, se crea un nuevo objeto Tramite con los datos modificados 
    /// y se actualiza el trámite en el repositorio de trámites utilizando el método ModificarTramite. Finalmente, se actualiza el
    /// estado del expediente asociado al trámite modificado utilizando el el servicio de actualización de estado.
    /// </summary>
    /// <param name="tramiteId">Cooresponde al ID del trámite que se desea modificar</param>
    /// <param name="usuarioId">Corresponde al ID del usuario que realiza la acción</param>    
    /// <param name="etiqueta">Corresponde a la la nueva etiqueta del trámite</param>
    /// <param name="contenido">Corresponde al nuevo contenido del tramite</param>
    /// <exception cref="ValidacionException"></exception>
    /// <exception cref="AutorizacionException"></exception>
    /// <exception cref="RepositorioException"></exception>
    public void Ejecutar (Tramite tra) 
    {
        if (!validador.Validar(tra, out string messageError))
            throw new ValidacionException(messageError);

        if (!autorizador.PoseeElPermiso(tra.UsuarioId,Permiso.TramiteModificacion))
            throw new AutorizacionException(tra.UsuarioId,Permiso.TramiteModificacion);
        Tramite? tramiteViejo = repo.ConsultarTramite(tra.TramiteId);

        Tramite tramiteNuevo = new Tramite()
        {
            TramiteId=tra.TramiteId,
            ExpedienteId=tramiteViejo?.ExpedienteId?? 0,
            Etiqueta=tra.Etiqueta,
            Contenido=tra.Contenido,
            FechaCreacion=tramiteViejo?.FechaCreacion?? DateTime.Now,
            FechaModificacion=DateTime.Now,
            UsuarioId=tra.UsuarioId,
        };
        repo.ModificarTramite(tramiteNuevo);
        actualizarEstado.Ejecutar(tramiteViejo?.ExpedienteId?? 0);
    }
}
