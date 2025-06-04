using Microsoft.EntityFrameworkCore;

namespace criptoApiProyecto.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet <Cliente> Clientes { get; set; } 
        public DbSet<Transaccion> Transacciones { get; set; }
    }
}
