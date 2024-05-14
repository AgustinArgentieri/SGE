namespace SGE.Aplicacion;
/// <summary>
///  Esta interfaz provee la declaracion de los metodos que se trabajaran respecto 
///  al repositorio de tramites TXT en las clases que lo implementen, por lo tanto, 
///  proporciona un conjunto de operaciones que se pueden realizar en dicho repositorio,
///  permitiendo consultar, agregar, eliminar y modificar trámites.
/// </summary>
public interface ITramiteRepositorio
{
    List<Tramite>? ConsultarTramites(int expedienteId);
    void AgregarTramite(Tramite tramite);
    int EliminarTramite(int tramiteId); //devuelve un entero con el expediente asociado al tramite eliminado.
    void EliminarTramites(int expedienteId);
    void ModificarTramite(Tramite tramiteNuevo);
    Tramite? ConsultarTramite(int tramiteId);
    List<Tramite>? ConsultarTramitesPorEtiqueta(EtiquetaTramite etiqueta);
}
