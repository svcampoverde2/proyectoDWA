using APIalquilerVehiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIalquilerVehiculo.Data
{
    public class AplicationData :DbContext
    {
        public AplicationData(DbContextOptions<AplicationData> options) : base(options)
        {
        }

        public DbSet<ciudad> ciudad { get; set; }
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<clientes> clientes { get; set; }
        public DbSet<vehiculo> vehiculo { get; set; }
        public DbSet<pago> pago { get; set; }
        public DbSet<estadoPago> estadoPago { get; set; }
        public DbSet<estadoVehiculo> estadoVehiculo { get; set; }
        public DbSet<tipoVehiculo> tipoVehiculo { get; set; }
        public DbSet<tipoUsuario> tipoUsuario { get; set; }
        public DbSet<reserva> reserva { get; set; }
    }
}
