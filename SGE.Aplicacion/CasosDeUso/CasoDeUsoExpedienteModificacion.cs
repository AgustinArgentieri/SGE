namespace SGE.Aplicacion;
/// <summary>
///  Esta clase se encarga de la modificación de expedientes existentes en el sistema, asegurandose
///  que se cumplan todas las reglas y permisos de seguridad.la misma recibe dos dependencias en su
///  constructor.
/// </summary>
/// <param name="repo">Corresponde a la interfaz del repositorio de expedientes</param>
/// <param name="autorizador">Corresponde a la interfaz del sevicio de autorizacion </param>
public class CasoDeUsoExpedienteModificacion (IExpedienteRepositorio repo, ExpedienteValidador expValidador,
IServicioAutorizacion autorizador)

{
    /// <summary>
    /// El metodo Ejecutar Recibe tres parámetros: el ID del expediente que se desea modificar (ExpedienteId),
    /// el ID del usuario que realiza la acción (UsuarioId) y la nueva carátula del expediente (caratula).
    /// Verifica que el usuario tiene el permiso necesario (Permiso.ExpedienteModificacion) para modificar un
    /// expediente, en caso que lo posea, se modifica el expediente en el repositorio de expedientes
    /// utilizando el método ModificarExpediente. Se actualiza la carátula del expediente y se registra 
    /// la fecha y hora de la modificación, caso contrario, se lanza una excepción AutorizacionException.
    /// </summary>
    /// <param name="ExpedienteId">Corresponde al ID del expediente</param>
    /// <param name="UsuarioId">Corresponde al Id del usuario</param>
    /// <param name="caratula">Corresponde a la caratula del expediente</param>
    /// <exception cref="AutorizacionException"></exception>
    public void Ejecutar (Expediente exp){

        if (!expValidador.Validar(exp, out string mensajeError))
            throw new ValidacionException(mensajeError);

        if (!autorizador.PoseeElPermiso(exp.UsuarioId, Permiso.ExpedienteModificacion)){
            throw new AutorizacionException(exp.UsuarioId, Permiso.ExpedienteModificacion);
        }
        exp.FechaModificacion = DateTime.Now;
        repo.ModificarExpediente(exp);
    }
}