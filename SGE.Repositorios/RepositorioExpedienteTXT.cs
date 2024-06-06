using SGE.Aplicacion;

namespace SGE.Repositorios;
/// <summary>
/// Es una implementación concreta de la interfaz IExpedienteRepositorio que utiliza archivos
/// de texto para almacenar y recuperar expedientes.
/// </summary>
public class RepositorioExpedienteTXT : IExpedienteRepositorio
{
    /// <value> Propósito del miembro: : El nombre del archivo de texto para expedientes. </value>
    private static readonly string s_nombreArch = "expediente.txt";
    /// <value> Propósito del miembro:  El nombre del archivo de texto para índices.</value>
    private static readonly string s_indices = "indices.txt";
    /// <value> Propósito del miembro: Representa el último ID calculado para un expediente. </value>
    private int idUltlExp = CalcularIdUltExpediente();

    /// <summary>
    /// Este método agrega un nuevo expediente al archivo de texto. Escribe los detalles del expediente
    /// en el archivo y actualiza el archivo de índices con el nuevo ID del expediente.
    /// </summary>
    /// <param name="exp"> Corresponde a un expediente </param>
    public void AgregarExpediente(Expediente exp)
    {
        using var sw = new StreamWriter(s_nombreArch, true);
        using var swI = new StreamWriter(s_indices, false); //en caso de que haya que guardar otro dato en indices verificar esta linea
        sw.WriteLine(++idUltlExp);
        sw.WriteLine(exp.Caratula);
        sw.WriteLine(exp.FechaCreacion);
        sw.WriteLine(exp.FechaModificacion);
        sw.WriteLine(exp.UsuarioId);
        sw.WriteLine(exp.Estado);
        swI.WriteLine(idUltlExp);
    }
    /// <summary>
    /// Este método privado escribe una lista de expedientes en el archivo de texto. Se utiliza
    /// para actualizar el archivo después de realizar operaciones como eliminar o modificar un expediente.
    /// </summary>
    /// <param name="lista">Corresponde al parametro de tipo List</param>
    private void GuardarDatos(List<Expediente> lista)
    {
        using (var sw = new StreamWriter(s_nombreArch, false))
            foreach (var exp in lista)
            {
                sw.WriteLine(exp.ExpedienteId);
                sw.WriteLine(exp.Caratula);
                sw.WriteLine(exp.FechaCreacion);
                sw.WriteLine(exp.FechaModificacion);
                sw.WriteLine(exp.UsuarioId);
                sw.WriteLine(exp.Estado.ToString());
            }
    }

    /// <summary>
    /// Este metodo elimina el numero de expediente que se envia en <paramref name="ExpedienteId"/>. Se crean dos listas 
    /// en las cuales se manejaran los expedientes que no coinciden con el numero enviado en <paramref name="ExpedienteId"/>
    /// y luego se cargara listanueva en el archivo TXT sin el expediente eliminado.
    /// </summary>
    /// <param name="ExpedienteId"> Corresponde al numero de expediente a eliminar </param>
    public void EliminarExpediente(int ExpedienteId)
    {
        var listaVieja = ListarExpedientes();
        var listaNueva = new List<Expediente>();
        bool elimine = false;
        foreach (Expediente e in listaVieja)
        {
            if (e.ExpedienteId != ExpedienteId) //agrego todos los expedientes e, menos el que deseo eliminar.
                listaNueva.Add(e);
            else
                elimine = true;
        }
        if (!elimine) throw new RepositorioException($"No se encontro el expediete con id: {ExpedienteId}");
        GuardarDatos(listaNueva);
    }

    /// <summary>
    /// Este metodo retorna en una lista todos los expedientes que estan en el archivo TXT.
    /// </summary>
    /// <returns>Retorna una lista de expedientes </returns>
    public List<Expediente> ListarExpedientes()
    {
        var ListaExp = new List<Expediente>();
        using var sr = new StreamReader(s_nombreArch);
        while (!sr.EndOfStream)
        {
            var exp = new Expediente();
            exp.ExpedienteId = int.Parse(sr.ReadLine() ?? "");
            exp.Caratula = sr.ReadLine() ?? "";
            exp.FechaCreacion = DateTime.Parse(sr.ReadLine() ?? "");
            exp.FechaModificacion = DateTime.Parse(sr.ReadLine() ?? "");
            exp.UsuarioId = int.Parse(sr.ReadLine() ?? "");
            exp.Estado = (EstadoExpediente)Enum.Parse(typeof(EstadoExpediente), sr.ReadLine() ?? "");
            ListaExp.Add(exp);
        }
        sr.Close();

        return ListaExp;
    }

    /// <summary>
    ///Este método estático calcula el último ID de expediente utilizado leyendo el archivo de índices.
    /// </summary>
    /// <returns>Reorna la cantidad de expedientes cargados </returns>
    public static int CalcularIdUltExpediente() //que pasa si borro el ultimo expediente y creo uno nuevo? que pasa si borro un expediente que no es el ultimo y creo uno nuevo?
    {
        int cant = 0;
        try
        {
            using var sr = new StreamReader(s_indices);
            cant = int.Parse(sr.ReadLine() ?? "0");
        }
        catch { }
        return cant;
    }


    /// <summary>
    /// Este método devuelve un expediente específico del archivo de texto utilizando su ID.
    /// </summary>
    /// <param name="expedienteId">Corresponde al Id del expediente</param>
    /// <returns>Retorna un expediente</returns>
    /// <exception cref="RepositorioException"></exception>
    public Expediente? ConsultarExpediente(int expedienteId)
    {
        bool encontro = false;
        List<Expediente>? listaExp = new List<Expediente>(ListarExpedientes());
        foreach (Expediente exp in listaExp)
        {
            if (exp.ExpedienteId == expedienteId)
            {
                encontro = true;
                return exp;
            }
        }
        if (!encontro) throw new RepositorioException($"No se encontró el expediente con Id: {expedienteId}");
        return null;
    }

    /// <summary>
    /// Este método modifica un expediente existente en el archivo de texto. Lee todos los expedientes del archivo, 
    /// modifica el expediente deseado, y luego escribe todos los expedientes de nuevo en el archivo.
    /// </summary>
    /// <param name="ExpedienteId">Corresponde al Id del expediente</param>
    /// <param name="UsuarioId">Corresponde al Id del usuario </param>
    /// <param name="fechaM">Corresponde a la fecha de modificacion</param>
    /// <param name="caratula">Corresponde a la caratula del expediente</param>
    /// <exception cref="RepositorioException"></exception>
    public void ModificarExpediente(Expediente exp)
    {
        bool found = false;
        var lista = ListarExpedientes();
        foreach (var expB in lista)
        {
            if (expB.ExpedienteId == exp.ExpedienteId)
            {
                expB.Caratula = exp.Caratula;
                expB.FechaModificacion = exp.FechaModificacion;
                expB.UsuarioId = exp.UsuarioId;
                found = true;
            }
        }
        GuardarDatos(lista);
        if (!found)
        {
            throw new RepositorioException("No se encontro el Expediente con ID: " + exp.ExpedienteId);
        }
    }

    /// <summary>
    /// Este método modifica el estado de un expediente existente en el archivo de texto. Lee todos los expedientes 
    /// del archivo, modifica el estado del expediente deseado, y luego escribe todos los expedientes de nuevo en 
    /// el archivo.
    /// </summary>
    /// <param name="ExpedienteId">Corresponde al Id del expediente</param>
    /// <param name="nuevoEstado">Corresponde al nuevo estado del expediente</param>
    public void ModificarEstadoExpediente(int ExpedienteId, EstadoExpediente nuevoEstado)
    {
        var lista = ListarExpedientes();
        foreach (var exp in lista)
        {
            if (exp.ExpedienteId == ExpedienteId)
            {
                exp.Estado = nuevoEstado;
            }
        }
        GuardarDatos(lista);
    }

}

