
namespace WebConversor.Models
{
    public class DbContexto : DbContext
    {

        public DbContexto(DbContextOptions<DbContexto> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Moneda>().HasData(
                new Moneda { Id = 1, Nombre = "Dólar Estadounidense", Simbolo = "USD" },
                new Moneda { Id = 2, Nombre = "Euro", Simbolo = "EUR" },
                new Moneda { Id = 3, Nombre = "Yen Japonés", Simbolo = "JPY" }
            );
        }

        public DbSet<Moneda> Monedas { get; set; }
    }
}
