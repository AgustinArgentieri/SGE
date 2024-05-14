namespace SGE.Aplicacion;
/// <summary>
/// Es una implementación específica en el sistema de gestión de expedientes.
/// Esta clase se encarga de la eliminación de expedientes existentes en el sistema, a su vez
/// recibe tres dependencias en su constructor./// 
/// </summary>
/// <param name="repoExp">Corresponde a la interfaz del repositorio de expedientes</param>
/// <param name="repoTra">Corresponde a la interfaz del repositorio de tramites</param>
/// <param name="autorizador">Corresponde al servicio de autorizacion</param>
public class CasoDeUsoExpedienteBaja(IExpedienteRepositorio repoExp, ITramiteRepositorio repoTra, IServicioAutorizacion autorizador)
{
    /// <summary>
    /// Cuando se ejecuta este metodo se evalua si posee permiso el usuario para dar de baja un expediente, en caso q no lo posea, 
    /// se lanzara una excepcion con un mensaje de error.  Si el usuario tiene los permisos necesarios, se elimina el expediente 
    /// del repositorio de expedientes utilizando el método EliminarExpediente y se eliminan 
    /// los trámites asociados al expediente del repositorio de trámites utilizando el método EliminarTramites.
    /// </summary>
    /// <param name="expedienteId">Corresponde al numero de expediente</param>
    /// <param name="usuarioId">Corresponde al numero de usuario</param>
    /// <exception cref="ValidacionException"></exception>
    /// <exception cref="AutorizacionException"></exception>
    public void Ejecutar(int expedienteId, int usuarioId)
    {
        if (usuarioId < 1)
            throw new ValidacionException("Id de usuario Incorrecto"); //a chequear
        if (!autorizador.PoseeElPermiso(usuarioId, Permiso.ExpedienteBaja))
            throw new AutorizacionException(usuarioId, Permiso.ExpedienteBaja);
        repoExp.EliminarExpediente(expedienteId);
        repoTra.EliminarTramites(expedienteId);
    }
}
