namespace SGE.Aplicacion;

public interface IUsuarioRepositorio
{
    public void CrearUsuario();
    public List<Usuario> ListarUsuarios();
    public void BajaUsuario();
    public void ModificarUsuario();
}
