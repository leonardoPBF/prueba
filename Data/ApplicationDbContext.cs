using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prueba_Geolocalizacion.Models;

namespace Prueba_Geolocalizacion.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Prueba_Geolocalizacion.Models.Sede> Sedes { get; set; }
    public DbSet<Prueba_Geolocalizacion.Models.Empleado> Empleados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Insertar datos iniciales para la tabla Sedes
        modelBuilder.Entity<Sede>().HasData(
            new Sede
            {
                Id = 1,
                Nombre = "Sede Central - Arequipa",
                Latitud = -16.3988,
                Longitud = -71.5369,
                RadioValidacion = 100
            }
        );

        // Insertar datos iniciales para la tabla Empleados
        modelBuilder.Entity<Empleado>().HasData(
            new Empleado
            {
                Id = 1,
                Nombre = "Juan Pérez",
                SedeId = 1
            }
        );
    }
}
