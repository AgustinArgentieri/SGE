namespace SGE.Aplicacion;

public class CasoDeUsoUsuarioModificacion (IUsuarioRepositorio repoU, IServicioAutorizacion autorizador):CasoDeUsoUsuario(repoU)
{
    public void Ejecutar (Usuario usuario)
    {
        if (!autorizador.PoseeElPermiso(usuario.UsuarioId, Permiso.UsuarioModificacion))
            throw new AutorizacionException(usuario.UsuarioId,Permiso.UsuarioModificacion);
        repoU.ModificarUsuario(usuario);
    }
}