namespace SGE.Aplicacion;

public class CasoDeUsoUsuarioConsultaTodos (IUsuarioRepositorio repoU, IServicioAutorizacion autorizador):CasoDeUsoUsuario(repoU)
{
    public List<Usuario> Ejecutar (int usuarioId)
    {
        if (!autorizador.PoseeElPermiso(usuarioId,Permiso.UsuarioListar))
            throw new AutorizacionException(usuarioId,Permiso.UsuarioListar);
        return repoU.ListarUsuarios();
    }
}