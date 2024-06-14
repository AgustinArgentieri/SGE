namespace SGE.Aplicacion;

public class Usuario
{
    public int UsuarioId {get; set; }
    public String Nombre {  get;  set; } = "";
    public String Apellido { get; set; } = "";
    public String CorreoElectronico { get; set; } = "";
    public String Contraseña { get; set; } = "";
    public String Permisos { get; set; } = "";

    public Usuario(){}

}



