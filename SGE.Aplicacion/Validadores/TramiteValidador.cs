namespace SGE.Aplicacion;
/// <summary>
/// La clase TramiteValidador proporciona una forma de validar los datos de un trámite antes de realizar
/// operaciones en él, como agregarlo al repositorio o modificarlo.
/// </summary>
public class TramiteValidador
{
    /// <summary>
    ///  Este método se utiliza para validar un objeto Tramite. El mismo recibe el objeto mencionado y una variable
    ///  de salida mensajeError. Si el trámite no es válido, mensajeError contendrá un mensaje de error. Si el contenido
    ///  del trámite está vacío o solo contiene espacios en blanco, se agrega un mensaje de error a mensajeError  y 
    ///  si el ID del usuario asociado al trámite es menor que 1, se agrega otro mensaje de error a mensajeError.
    /// </summary>
    /// <param name="tr">Corresponde a un objeto tramite</param>
    /// <param name="mensajeError">Corresponde al mensaje de error</param>
    /// <returns>Devuelve true si mensajeError está vacío (es decir, no hay errores de validación), y false en caso contrario.</returns>
    public bool Validar(Tramite tr, out string mensajeError)
    {
        mensajeError= "";
        if (string.IsNullOrWhiteSpace(tr.Contenido))
        {
            mensajeError = "El contenido del tramite no puede estar vacio.\n";
        }
        if (tr.UsuarioId<1)
        {
            mensajeError += "El id del usuario debe ser mayor que cero.\n";
        }
        return mensajeError == "";
    }
}
