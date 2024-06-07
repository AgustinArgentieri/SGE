using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion;

public class SGEContext:DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("data source=SGE.sqlite");
    }
}

