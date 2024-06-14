namespace SGE.Aplicacion;

public class CasoDeUsoUsuarioAlta(IUsuarioRepositorio repoU):CasoDeUsoUsuario(repoU)
{
    public void Ejecutar(Usuario u)
    {
        repoU.Inicializar();
        repoU.CrearUsuario(u);
    }
}
