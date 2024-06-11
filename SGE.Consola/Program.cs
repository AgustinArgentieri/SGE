using SGE.Aplicacion;
using SGE.Repositorios;
using Microsoft.Extensions.DependencyInjection;

var servicios = new ServiceCollection();
servicios.AddSingleton<IServicioAutorizacion,ServicioAutorizacionProvisorio>();
servicios.AddSingleton<IExpedienteRepositorio,SGESqlite>();
servicios.AddSingleton<ITramiteRepositorio,SGESqlite>();
servicios.AddSingleton<IUsuarioRepositorio,SGESqlite>();
var proveedor = servicios.BuildServiceProvider();

var rE = proveedor.GetRequiredService<IExpedienteRepositorio>();
var rT = proveedor.GetRequiredService<ITramiteRepositorio>();
var eV = new ExpedienteValidador();
var tV = new TramiteValidador();
var sA = proveedor.GetRequiredService<IServicioAutorizacion>();
var sU = proveedor.GetRequiredService<IUsuarioRepositorio>();
var eE = new EspecificacionCambioEstado();
var sAE = new ServicioActualizacionEstado(rE,rT,eE);

var agregarExpediente = new CasoDeUsoExpedienteAlta(rE,eV,sA);
var modificarExpediente = new CasoDeUsoExpedienteModificacion(rE,eV,sA);
var eliminarExpediente = new CasoDeUsoExpedienteBaja(rE,rT,sA);
var consultarExpediente = new CasoDeUsoExpedienteConsultaPorId(rE,rT);
var listarExpendientes = new CasoDeUsoExpedienteConsultaTodos(rE);
var agregarTramite = new CasoDeUsoTramiteAlta(rT,tV,sA,sAE);
var eliminarTramite = new CasoDeUsoTramiteBaja(rT,tV,sA,sAE);
var consultarTramite = new CasoDeUsoTramiteConsultaPorEtiqueta(rT);
var modificarTramite = new CasoDeUsoTramiteModificacion(rT,tV,sA,sAE);

SGESqlite.Inicializar();
try
{  
    bool salir = false;
    int expId;
    int traId;
    int op;
    String? caratula;
    String? contenido;
    EtiquetaTramite et;
        
        while (!salir)
        {           
            // Mostrar el menú
            Console.WriteLine("Menú de opciones:");
            Console.WriteLine("1. Agregar Expediente");
            Console.WriteLine("2. Modificar Expediente");
            Console.WriteLine("3. Eliminar Expediente");
            Console.WriteLine("4. Consultar Expediente por Id");
            Console.WriteLine("5. Listar Expedientes");
            Console.WriteLine("6. Agregar Trámite");
            Console.WriteLine("7. Eliminar Trámite");
            Console.WriteLine("8. Consultar Trámite por etiqueta");
            Console.WriteLine("9. Modificar Trámite");
            Console.WriteLine("10. Salir");
            Console.Write("Seleccione una opción: ");

            // Leer la opción seleccionada por el usuario
            String? opcion = Console.ReadLine();
            
            // Ejecutar el caso de uso correspondiente según la opción seleccionada
            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese la caratula del expediente: ");
                    caratula = Console.ReadLine() ?? "";
                    agregarExpediente.Ejecutar(new Expediente(caratula,1));
                    break;
                case "2":
                    Console.Write("Ingrese el ID del expediente a modificar: ");
                    expId = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Ingrese la nueva caratula: ");
                    caratula = Console.ReadLine() ?? "";
                    modificarExpediente.Ejecutar(new Expediente(expId,1,caratula));
                    break;
                case "3":
                    Console.Write("Ingrese el ID del expediente a eliminar: ");
                    expId = int.Parse(Console.ReadLine() ?? "0");
                    eliminarExpediente.Ejecutar(expId,1);
                    break;
                case "4":
                    Console.Write("Ingrese el ID del expediente a buscar: ");
                    expId = int.Parse(Console.ReadLine() ?? "0");
                    Expediente? e = consultarExpediente.Ejecutar(expId);
                    if (e is not null)
                    {
                        Console.WriteLine(e.ToString());
                        if (e.Tramites is not null)
                        {
                            foreach (Tramite t in e.Tramites)
                            {
                                Console.WriteLine(t.ToString());
                            }
                        }
                    }
                        
                    break;
                case "5":
                    List<Expediente>? listaExp = listarExpendientes.Ejecutar();
                    if (listaExp is not null)
                        foreach (Expediente exp in listaExp)
                            Console.WriteLine(exp.ToString());
                    break;
                case "6":
                    Console.Write("Ingrese el ID del expediente a asociar este tramite: ");
                    expId = int.Parse(Console.ReadLine() ?? "0");
                    Console.Write("Ingrese el contenido del tramite: ");
                    contenido = Console.ReadLine() ?? "";
                    Console.WriteLine($"Ingrese una etiqueta a buscar: "+
                    $"1) EscritoPresentado 2) PaseAEstudio 3) Despacho"+
                    $" 4) Resolucion 5) Notificacion 6) PaseAlArchivo");
                    op = int.Parse(Console.ReadLine() ?? "-1");
                    et = (EtiquetaTramite)op - 1;
                    agregarTramite.Ejecutar(new Tramite (expId,contenido,1,et));
                    break;
                case "7":
                    Console.Write("Ingrese el ID del tramite a eliminar");
                    traId = int.Parse(Console.ReadLine() ?? "0");
                    eliminarTramite.Ejecutar(new Tramite (traId,1));
                    break;
                case "8":
                    Console.WriteLine($"Ingrese una etiqueta a buscar: "+
                    $"1) EscritoPresentado 2) PaseAEstudio 3) Despacho"+
                    $"4) Resolucion 5) Notificacion 6) PaseAlArchivo");
                    op = int.Parse(Console.ReadLine() ?? "-1");
                    et = (EtiquetaTramite)op - 1;
                    List<Tramite>? listaTramites = consultarTramite.Ejecutar(et);
                    if (listaTramites is not null)
                    {
                        foreach (Tramite t in listaTramites)
                        {
                            Console.WriteLine(t.ToString());
                        }
                    }
                    break;
                case "9":
                    Console.WriteLine("Ingrese el ID de tramite a modificar: ");
                    traId = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine($"Ingrese una etiqueta: "+
                    $"1) EscritoPresentado 2) PaseAEstudio 3) Despacho"+
                    $"4) Resolucion 5) Notificacion 6) PaseAlArchivo");
                    op = int.Parse(Console.ReadLine() ?? "-1");
                    et = (EtiquetaTramite)op - 1;
                    Console.WriteLine("Ingrese un nuevo contenido:");
                    contenido = Console.ReadLine() ?? "";
                    modificarTramite.Ejecutar(new Tramite(traId,et,contenido));
                    break;
                case "10":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            
        }
        
        Console.WriteLine("Gracias por utilizar el programa!");
    }
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
