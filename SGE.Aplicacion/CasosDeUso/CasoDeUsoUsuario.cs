namespace SGE.Aplicacion;

public abstract class CasoDeUsoUsuario(IUsuarioRepositorio repoU)
{
    protected IUsuarioRepositorio RepoU { get; } = repoU;
}