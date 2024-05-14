namespace SGE.Aplicacion;
/// <summary>
/// Es una implementación específica en el sistema de gestión de expedientes. Esta clase se 
/// encarga de la creación de nuevos expedientes en el sistema, haciendo validación y creación de un 
/// nuevo expediente en el sistema, asegurando que se cumplan todas las reglas y permisos de seguridad.
/// La clase recibe tres dependencias en su constructor.
/// </summary>
/// <param name="repo"> Corresponde a la Interface de Repositorio de expedientes.</param>
/// <param name="expValidador"> Corresponde al validador de Expedientes</param>
/// <param name="autorizador">Corresponde al servicio de autorizacion </param>
public class CasoDeUsoExpedienteAlta(IExpedienteRepositorio repo, ExpedienteValidador expValidador, IServicioAutorizacion autorizador)
{
    /// <summary>
    /// Ejecutar valida y controla que posea permiso el usuario para dar de alta a un expediente,
    /// en caso que no cumpla con los requisitos, se lanzara una excepcion.
    /// </summary>
    /// <param name="exp"> Corresponde a un expediente </param>
    /// <exception cref="ValidacionException"></exception>
    /// <exception cref="AutorizacionException"></exception>
    public void Ejecutar(Expediente exp)
    {
        if (!expValidador.Validar(exp, out string mensajeError))
        {
            throw new ValidacionException(mensajeError);
        }
        if (!autorizador.PoseeElPermiso(exp.UsuarioId, Permiso.ExpedienteAlta))
        {
            //mensajeError="Usuario no autorizado."; Esto es conveniente? o como lo dejamos actualmente?
            throw new AutorizacionException(exp.UsuarioId, Permiso.ExpedienteAlta);
        }
        repo.AgregarExpediente(exp);
    }
}
