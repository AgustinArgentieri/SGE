namespace SGE.Aplicacion;


public class CasoDeUsoUsuarioConsultaRegistrado(IUsuarioRepositorio repoU):CasoDeUsoUsuario(repoU)
{
    public bool Ejecutar(string mail, string hash, out Usuario u)
    {
        repoU.Inicializar();
        return repoU.UsuarioRegistrado(mail,hash,out u);
    }
}
