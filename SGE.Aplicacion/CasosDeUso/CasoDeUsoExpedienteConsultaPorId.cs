namespace SGE.Aplicacion;
/// <summary>
/// Implementación de un caso de uso específico en el sistema de gestión de expedientes. Esta clase se encarga 
/// de la consulta de expedientes existentes en el sistema por su ID, la misma recibe dos dependencias en su
/// constructor.
/// </summary>
/// <param name="repoExp">Corresponde a la interfaz del repositorio de expedientes</param>
/// <param name="repoTra">Corresponde a la interfaz del repositorio de tramites</param>
public class CasoDeUsoExpedienteConsultaPorId(IExpedienteRepositorio repoExp,ITramiteRepositorio repoTra)
{
    /// <summary>
    /// Este metodo recibe un parámetro, el ID del expediente que se desea consultar, devuelve el expediente 
    /// consultado con sus trámites asociados. Si el expediente existe (es decir, no es nulo), el método consulta los trámites asociados al 
    /// expediente del repositorio de trámites utilizando el método ConsultarTramites y los asigna al expediente.
    /// </summary>
    /// <param name="expedienteId">Corresponde al ID del expediente que se desea consultar</param>
    /// <returns>Retorna el expediente consultado</returns>
        public Expediente? Ejecutar(int expedienteId)
    {
        Expediente? exp = repoExp.ConsultarExpediente(expedienteId);
        if (exp is not null)
           exp.Tramites = repoTra.ConsultarTramites(expedienteId);
        return exp;
    }
}
