using SGE.Aplicacion;

namespace SGE.Repositorios;
/// <summary>
///  Esta clase proporciona una implementación concreta de la interfaz
///  ITramiteRepositorio que utiliza archivos de texto para almacenar y recuperar trámites. 
/// </summary>
public class RepositorioTramiteTXT : ITramiteRepositorio
{
    /// <value> Propósito del miembro: Representa el último ID calculado para un tramite. </value>
    private int _ultID = CalcularUltID();
    /// <value> Propósito del miembro: : El nombre del archivo de texto para tramites. </value>
    private static readonly string s_nombreArch = "tramites.txt";
    /// <value> Propósito del miembro:  El nombre del archivo de texto para índices de tramites.</value>
    private static readonly string s_indice = "indiceTra.txt";

    /// <summary>
    ///  Este método estático calcula el último ID de trámite utilizado leyendo el archivo de índices.
    /// </summary>
    /// <returns>Retorna la cantidad de tramites</returns>
    public static int CalcularUltID()
    {
        int cant = 0;
        try
        {
            using var indice = new StreamReader(s_indice);
            cant = int.Parse(indice.ReadLine() ?? "0");
        }
        catch { }
        return cant;
    }

    /// <summary>
    /// Este método agrega un nuevo trámite al archivo de texto.  Escribe los detalles del 
    /// trámite en el archivo y actualiza el archivo de índices con el nuevo ID del trámite.
    /// </summary>
    /// <param name="tra">Corresponde al objeto Tramite</param>
    public void AgregarTramite(Tramite tra)
    {
        using var archivo = new StreamWriter(s_nombreArch, true);
        using var indice = new StreamWriter(s_indice, false);
        archivo.WriteLine(++_ultID);
        archivo.WriteLine(tra.ExpedienteId);
        archivo.WriteLine(tra.Etiqueta);
        archivo.WriteLine(tra.Contenido);
        archivo.WriteLine(tra.FechaCreacion);
        archivo.WriteLine(tra.FechaModificacion);
        archivo.WriteLine(tra.UsuarioId);
        indice.WriteLine(_ultID);
    }

    /// <summary>
    /// Este método escribe una lista de trámites en el archivo de texto. Se utiliza para actualizar el 
    /// archivo después de realizar operaciones como eliminar o modificar un trámite.
    /// </summary>
    /// <param name="lista">Corresponde al parametro de tipo List</param>
    public void guardarTramites(List<Tramite> lista)
    {
        using var sw = new StreamWriter(s_nombreArch, false);
        foreach (var tramite in lista)
        {
            sw.WriteLine(tramite.TramiteId);
            sw.WriteLine(tramite.ExpedienteId);
            sw.WriteLine(tramite.Etiqueta);
            sw.WriteLine(tramite.Contenido);
            sw.WriteLine(tramite.FechaCreacion);
            sw.WriteLine(tramite.FechaModificacion);
            sw.WriteLine(tramite.UsuarioId);
        }
        sw.Close();
    }

    /// <summary>
    /// Este método devuelve una lista de todos los trámites en el archivo de texto.
    /// </summary>
    /// <returns>Retorna una lista con tramites</returns>
    public List<Tramite>? ListarTramites()
    {
        List<Tramite>? listaTramites = new List<Tramite>();
        var sr = new StreamReader(s_nombreArch);
        while (!sr.EndOfStream)
        {
            Tramite tr = new Tramite()
            {
                TramiteId = int.Parse(sr.ReadLine() ?? ""),
                ExpedienteId = int.Parse(sr.ReadLine() ?? ""),
                Etiqueta = Enum.Parse<EtiquetaTramite>(sr.ReadLine() ?? ""),
                Contenido = sr.ReadLine() ?? "",
                FechaCreacion = DateTime.Parse(sr.ReadLine() ?? ""),
                FechaModificacion = DateTime.Parse(sr.ReadLine() ?? ""),
                UsuarioId = int.Parse(sr.ReadLine() ?? ""),
            };
            listaTramites.Add(tr);
        }
        sr.Close();
        return listaTramites;
    }

    /// <summary>
    /// Este método devuelve una lista de todos los trámites asociados a
    /// un expediente específico en el archivo de texto utilizando su ID.
    /// </summary>
    /// <param name="expedienteId">Corresponde al Id del expediente</param>
    /// <returns>Devuelve una lista de tramites</returns>
    public List<Tramite>? ConsultarTramites(int expedienteId)
    {
        List<Tramite>? ListaTramites = ListarTramites();
        List<Tramite>? TramitesAsociados = new List<Tramite>();

        if (ListaTramites != null) //verifico que el repositorio no este vacio.
        {
            foreach (Tramite tr in ListaTramites)
            {
                if (tr.ExpedienteId == expedienteId)
                    TramitesAsociados?.Add(tr);
            }
        }
        if (TramitesAsociados?.Count == 0)
        {
            return null;
        }
        return TramitesAsociados;
    }

    /// <summary>
    /// Este método elimina un trámite del archivo de texto. Lee todos los trámites del archivo, escribe todos 
    /// los trámites excepto el que se desea eliminar de nuevo en el archivo, y devuelve el ID del expediente
    /// asociado al trámite eliminado.
    /// </summary>
    /// <param name="tramiteId">Corresponde al Id del tramite a eliminar</param>
    /// <returns>Retorna el entero correspondiente al id del expediente asociado </returns>
    /// <exception cref="RepositorioException"></exception>
    public int EliminarTramite(int tramiteId) //cambio de void a int, para devolver el expedienteId y ejecutar el servicioActualizador
    {
        var listaTramites = ListarTramites();
        int eliminar = -1;
        int expedienteAsociado = -1;
        if (listaTramites is not null)
        {
            for (int i = 0; i < listaTramites.Count; i++)
            {
                if (listaTramites[i].TramiteId == tramiteId)
                {
                    expedienteAsociado = listaTramites[i].ExpedienteId;
                    eliminar = i;
                    break;
                }
            }
            if (eliminar !=-1)
            {
                listaTramites.RemoveAt(eliminar);
                guardarTramites(listaTramites);
            }
        }
        if (eliminar == -1)
            throw new RepositorioException($"No se encontro el tramite con el Id: {tramiteId}");
        return expedienteAsociado;
    }

    /// <summary>
    /// Este método elimina todos los trámites asociados a un expediente específico en el archivo de
    /// texto utilizando el ID del expediente.
    /// </summary>
    /// <param name="expedienteId">Corresponde al Id del expediente</param>
    public void EliminarTramites(int expedienteId) //cambio de void a int, para devolver el expedienteId y ejecutar el servicioActualizador
    {
        var listaTramites = ListarTramites();
        List<Tramite> nuevaLista = new List<Tramite>();

        if (listaTramites is not null)
        {
            for (int i = 0; i < listaTramites.Count; i++)
            {
                if (listaTramites[i].ExpedienteId != expedienteId)
                {
                    nuevaLista.Add(listaTramites[i]);
                }
            }
            guardarTramites(nuevaLista);
        }
    }

    /// <summary>
    /// Este método devuelve un trámite específico del archivo de texto utilizando su ID.
    /// </summary>
    /// <param name="tramiteId">Corresponde al Id del tramite</param>
    /// <returns>Retorna el tramite quue se consulta, en caso que no exista se devuelve null </returns>
    public Tramite? ConsultarTramite(int tramiteId)
    {
        var listaTramites = ListarTramites();
        if (listaTramites is not null)
        {
            foreach (var tramite in listaTramites)
            {
                if (tramite.TramiteId == tramiteId)
                    return tramite;
            }
        }
        throw new RepositorioException($"No se ecuentra un tramite con el id: {tramiteId}");
    }

    /// <summary>
    /// Este método modifica un trámite existente en el archivo de texto. Lee todos los trámites del archivo, 
    /// modifica el trámite deseado, y luego escribe todos los trámites de nuevo en el archivo.
    /// </summary>
    /// <param name="tramiteNuevo">Corresponde al objeto tipo Tramite</param>
    public void ModificarTramite(Tramite tramiteNuevo)
    {
        var listaTramites = ListarTramites();
        if (listaTramites is not null)
        {
            for (int i = 0; i < listaTramites.Count; i++)
            {
                if (listaTramites[i].TramiteId == tramiteNuevo.TramiteId)
                {
                    listaTramites[i] = tramiteNuevo;
                    break;
                }
            }
            guardarTramites(listaTramites);
        }
    }

    /// <summary>
    ///  Este método devuelve una lista de todos los trámites que tienen una etiqueta 
    ///  específica en el archivo de texto.
    /// </summary>
    /// <param name="etiqueta">Corresponde al enumerativo tipo EtiquetaTramite</param>
    /// <returns>Retorna una lista con los tramites coincidentes segun su etiqueta, sino existe ninguno, devolvera null</returns>
    public List<Tramite>? ConsultarTramitesPorEtiqueta(EtiquetaTramite etiqueta)
    {
        var listaTramites = ListarTramites();
        var listaRetorno = new List<Tramite>();
        if (listaTramites is not null)
        {
            foreach (Tramite tramite in listaTramites)
            {
                if (tramite.Etiqueta == etiqueta)
                {
                    listaRetorno.Add(tramite);
                }
            }
            return listaRetorno;
        }
        return null;
    }
}