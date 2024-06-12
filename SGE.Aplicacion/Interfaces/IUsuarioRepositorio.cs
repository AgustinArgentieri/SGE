namespace SGE.Aplicacion;

public interface IUsuarioRepositorio
{
    public void CrearUsuario(Usuario u);
    public List<Usuario> ListarUsuarios();
    public void BajaUsuario(int uId);
    public void ModificarUsuario(Usuario u);
    public List<Permiso> ListarPermisos(int uId);
}
