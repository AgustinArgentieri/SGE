namespace SGE.Aplicacion;
/// <summary>
/// Esta clase se utiliza para manejar la autorización en el sistema de gestión de expedientes y trámites de manera
/// provisional.
/// </summary>
public class ServicioAutorizacionProvisorio:IServicioAutorizacion
{
    /// <summary>
    /// ste método se utiliza para verificar si un usuario tiene un permiso específico.
    /// </summary>
    /// <param name="IdUsuario"></param>
    /// <param name="permiso"></param>
    /// <returns>Devuelve true si el usuario tiene el permiso, caso contrario, false.</returns>
    public bool PoseeElPermiso(int IdUsuario, Permiso permiso) =>
        IdUsuario==1;
}
