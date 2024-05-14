namespace SGE.Aplicacion;
/// <summary>
  /// El propósito es el de corroborar que el usuario efectivamente cuente con la autorización para la alta, 
  /// baja y modificacion de un expediente y/o tramite. La misma es lanzada cuando el usuario no posee el permiso.
  /// </summary>
  /// <param name="usuarioId"> Corresponde al numero de usuario</param>
  /// <param name="permiso"> Corresponde al tipo de permiso</param>
public class AutorizacionException(int usuarioId, Permiso permiso) : Exception($"El usuario {usuarioId} no posee el permiso: {permiso}")
{
}
