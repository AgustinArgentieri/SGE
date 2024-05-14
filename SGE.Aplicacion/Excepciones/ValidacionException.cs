namespace SGE.Aplicacion;
/// <summary>
/// Esta clase es una implementación de la clase base Exception en C#. Esta clase se utiliza para representar excepciones específicas 
/// que pueden ocurrir durante la validación en el actual sistema de gestión de expedientes y trámites.
/// </summary>
public class ValidacionException:Exception
{
    /// <summary>
    /// Este constructor verifica que los datos ingresados por el usuario sean validos. En el caso de los expedientes 
    /// rige la condición de que la caratula no puede ser vacía, si esto se cumple se lanza la misma. Cuando se produce 
    /// una condición de error durante la validación, se lanza una nueva instancia de ValidacionException, ejecutando
    /// un mensaje de error descriptivo.
    /// </summary>
    /// <param name="mensaje"> Corresponde al mensaje que se lanza si ocurre la excepcion</param>
    public ValidacionException(string mensaje):base(mensaje)
    {
        
    }
}
