namespace SGE.Aplicacion;

public class CasoDeUsoUsuarioBaja(IUsuarioRepositorio repoU,IServicioAutorizacion autorizador):CasoDeUsoUsuario(repoU)
{

    public void Ejecutar(Usuario u)
    {
    if (!autorizador.PoseeElPermiso(u.UsuarioId, Permiso.UsuarioBaja))
        {
            //mensajeError="Usuario no autorizado."; Esto es conveniente? o como lo dejamos actualmente?
            throw new AutorizacionException(u.UsuarioId, Permiso.UsuarioBaja);
        }
        repoU.BajaUsuario(u.UsuarioId);
    }
}
