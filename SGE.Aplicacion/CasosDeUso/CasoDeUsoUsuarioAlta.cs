namespace SGE.Aplicacion;

public class CasoDeUsoUsuarioAlta(IUsuarioRepositorio repo)
{
    public void Ejecutar(Usuario u)
    {
        repo.CrearUsuario(u);
    }
}
