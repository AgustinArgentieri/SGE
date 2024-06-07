namespace SGE.Aplicacion;

public class Usuario
{
    public required int UsuarioId {get; set; }
    public required String Nombre {  get;  set; }
    public required String Apellido { get; set; }
    public String? CorreoElectronico { get; set; }
    public required String Contraseña { get; set; }
    public required List<Permiso> Permisos { get; set; }
}

