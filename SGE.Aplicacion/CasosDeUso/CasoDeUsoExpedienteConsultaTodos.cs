namespace SGE.Aplicacion;
/// <summary>
/// Esta clase se encarga de la consulta de todos los expedientes existentes en el sistema, la misma
/// recibe una dependencia en su constructor.
/// </summary>
/// <param name="repoExp">Corresponde a la interfaz de repositorio de expedientes</param>
public class CasoDeUsoExpedienteConsultaTodos(IExpedienteRepositorio repoExp)
{
  /// <summary>
  /// Este metodo muestra en pantalla todos los expedientes cargados.
  /// </summary>
  /// <returns>Retorna en una lista los tramites que estan guardados en el archivo.</returns>
  public List<Expediente>? Ejecutar()
  {
    return repoExp.ListarExpedientes();
  }
}