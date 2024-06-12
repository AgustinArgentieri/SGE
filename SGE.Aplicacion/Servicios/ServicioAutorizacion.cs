namespace SGE.Aplicacion;

public class ServicioAutorizacion(IUsuarioRepositorio repoU):IServicioAutorizacion
{
    /// <summary>
    /// ste método se utiliza para verificar si un usuario tiene un permiso específico.
    /// </summary>
    /// <param name="IdUsuario"></param>
    /// <param name="permiso"></param>
    /// <returns>Devuelve true si el usuario tiene el permiso, caso contrario, false.</returns>
    public bool PoseeElPermiso(int IdUsuario, Permiso permiso)
    {
        List<Permiso> listaPermisos = repoU.ListarPermisos(IdUsuario);
        return listaPermisos.Contains(permiso);
    }
        


}
