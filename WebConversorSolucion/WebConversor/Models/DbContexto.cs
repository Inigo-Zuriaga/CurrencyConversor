
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

            modelBuilder.Entity<Coin>().HasData(
                new Coin { Id = 1, Name = "Dólar Estadounidense", Symbol = "USD" },
                new Coin { Id = 2, Name = "Euro", Symbol = "EUR" },
                new Coin { Id = 3, Name = "Yen Japonés", Symbol = "JPY" }
            );

        }

    

        /*Declaramos las clases*/
        public DbSet<Coin> Coins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}
