namespace SGE.Aplicacion;

public class CasoDeUsoUsuarioConsultaTodos (IUsuarioRepositorio repo, IServicioAutorizacion autorizador)
{
    public List<Usuario> Ejecutar (int usuarioId)
    {
        if (!autorizador.PoseeElPermiso(usuarioId,Permiso.UsuarioListar))
            throw new AutorizacionException(usuarioId,Permiso.UsuarioListar);
        return repo.ListarUsuarios();
    }
}