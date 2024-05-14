namespace SGE.Aplicacion;
/// <summary>
///  Esta interfaz provee la declaracion de los metodos que se trabajaran respecto 
///  al servicio de autorizacion en las clases que lo implementen, proporciona un mecanismo para
///  verificar los permisos de los usuarios en el sistema de gestión de expedientes y tramites, lo cual
///  asegura que los usuarios solo puedan realizar las acciones para las que están autorizados.
/// </summary>
public interface IServicioAutorizacion
{
    /// <summary>
    /// Este método se utiliza para verificar si un usuario tiene un permiso específico.
    /// </summary>
    /// <param name="IdUsuario">corresponde al Id del usuario</param>
    /// <param name="permiso">Corresponde al permiso que se desea verificar</param>
    /// <returns>Devuelve true si el usuario tiene el permiso, y false en caso contrario.</returns>
    bool PoseeElPermiso(int IdUsuario, Permiso permiso);
}
