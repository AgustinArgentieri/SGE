namespace SGE.Aplicacion;
/// <summary>
/// Se utiliza para validar los datos de un expediente antes de realizar operaciones en él, como agregarlo a un repositorio 
/// o modificarlo. Esto ayuda a asegurar que los datos del expediente siempre cumplan con las reglas y requisitos.
/// </summary>
public class ExpedienteValidador
{
    /// <summary>
    /// Este metodo envia un mensaje de error si no se cumplen tanto los criterios de quien solicita hacer una tarea, como 
    /// el ingreso correcto de una caratula. /// <c>validar</c> recibe el parametro <paramref name="exp"/> y devuelve un 
    /// mensaje de error.
    /// </summary>
    /// <param name="exp"> Corresponde a un expedientte</param>
    /// <param name="mensajeError"> Corresponde al o los mensajes de error que se enviaran en las validaciones</param>
    /// <returns>Devuelve true si mensajeError está vacío (es decir, no hay errores de validación), y false en caso contrario.</returns>

    public bool Validar(Expediente exp, out string mensajeError)
    {
        mensajeError = "";
        if (string.IsNullOrWhiteSpace(exp.Caratula))
        {
            mensajeError = "Debe ingresar una caratula.\n";
        }
        if (exp.UsuarioId < 1)
        {
            mensajeError += "El id del usuario debe ser mayor que cero.\n";
        }
        return mensajeError == "";
    }
}
