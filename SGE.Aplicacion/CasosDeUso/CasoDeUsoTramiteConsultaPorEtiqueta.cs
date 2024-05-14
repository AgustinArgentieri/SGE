namespace SGE.Aplicacion;
/// <summary>
/// Esta clase se encarga de la consulta de trámites existentes en el sistema por su etiqueta. 
/// </summary>
public class CasoDeUsoTramiteConsultaPorEtiqueta (ITramiteRepositorio repoT)
{
    /// <summary>
    /// Este metodo recibe dos parámetros: la etiqueta del trámite que se desea consultar 
    /// (EtiquetaTramite) y el repositorio de trámites (ITramiteRepositorio), el cual 
    /// utiliza el repositorio de trámites para listar todos los trámites que tienen la
    /// etiqueta proporcionada utilizando el método ConsultarTramitesPorEtiqueta, a su vez utiliza
    /// el repositorio de trámites para listar todos los trámites que tienen la etiqueta proporcionada 
    /// utilizando el método ConsultarTramitesPorEtiqueta.  Este método devuelve una lista de todos los 
    /// trámites que tienen la etiqueta proporcionada. Si no hay trámites con esa etiqueta en el sistema, 
    /// el método devuelve null.
    /// </summary>
    /// <param name="etiqueta">Corresponde a la etiqueta del tramite</param>
    /// <param name="repoT">Corresponde a la interfaz del repositorio de tramites</param>
    /// <returns>Retorna una lista con los tramites que posea la etiqueta correspondiente</returns>
    public List<Tramite>? Ejecutar(EtiquetaTramite etiqueta)
    {
        return repoT.ConsultarTramitesPorEtiqueta(etiqueta);
    }
}

// cambiamos parametro por inyeccion