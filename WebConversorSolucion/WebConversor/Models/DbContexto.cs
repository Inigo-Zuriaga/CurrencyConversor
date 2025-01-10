namespace WebConversor.Models;

public class DbContexto : DbContext
{
    public DbContexto(DbContextOptions<DbContexto> options) : base(options)
    {

    }
    //Declaramos las clases
    public DbSet<Coin> Coins { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<History> ExchangeHistory { get; set; }
}