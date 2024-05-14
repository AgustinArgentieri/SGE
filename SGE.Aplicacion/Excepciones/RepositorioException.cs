namespace SGE.Aplicacion;
/// <summary>
/// Esta clase es una implementación de la clase base Exception en C#. Tiene un constructor que recibe un parámetro:
/// message. Este parámetro representa el mensaje de error que se desea asociar con la excepción.
/// </summary>
public class RepositorioException : Exception
{
    /// <summary>
    ///  Al momento de buscar un expediente o tramite por su numero de ID, se lanza la excepcion con un
    ///  mensaje de error descriptivo, si este ID no existe en el repositorio.
    /// </summary>
    /// <param name="message"> El mensaje que devuelve si se produce la excepcion </param>
    public RepositorioException(string message) : base(message)
    {

    }
}
