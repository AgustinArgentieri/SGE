namespace SGE.Aplicacion;

public class CasoDeUsoUsuarioModificacion (IUsuarioRepositorio repo, IServicioAutorizacion autorizador)
{
    public void Ejecutar (Usuario usuario)
    {
        if (!autorizador.PoseeElPermiso(usuario.UsuarioId, Permiso.UsuarioModificacion))
            throw new AutorizacionException(usuario.UsuarioId,Permiso.UsuarioModificacion);
        repo.ModificarUsuario(usuario);
    }
}