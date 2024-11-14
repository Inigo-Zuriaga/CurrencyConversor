namespace WebConversor.Models;

public class DbContexto : DbContext
{

    public DbContexto(DbContextOptions<DbContexto> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coin>().HasData(
            new Coin { Id = 1, Name = "D�lar Estadounidense", ShortName = "USD", Symbol = "USD" },
            new Coin { Id = 2, Name = "Euro", ShortName = "EUR", Symbol = "EUR" },
            new Coin { Id = 3, Name = "Yen Japon�s", ShortName = "YEN", Symbol = "JPY" }
        );

        modelBuilder.Entity<User>().HasData(
           new User { Id = 1, Name = "Julian", Email = "asda@gmail.com", LastName = "Gomez", Password = "ddd", Img = "dd" },
           new User { Id = 2, Name = "Manuel", Email = "ggrg2@gmail.com", LastName = "Garcia", Password = "fff", Img = "ff" }

       );
        modelBuilder.Entity<History>().HasData(
          // new History { Id = 1, UserId = 2, FromCoin = "EUR", ToCoin = "USD", Result = 20.0, Date = new DateTime(2004, 12, 20) }
          new History { Id = 1, UserId = 1, FromCoin = "EUR", FromAmount = 76 ,ToCoin = "USD",ToAmount = 2, Date = new DateTime(2004, 12, 20) },
          new History { Id = 2, UserId = 2, FromCoin = "USD", FromAmount = 20, ToCoin = "EUR",ToAmount = 16, Date = DateTime.Now },
          new History { Id = 3, UserId = 2, FromCoin = "USD", FromAmount = 45, ToCoin = "PLN",ToAmount = 120, Date = new DateTime(2007, 03, 20) }
        );

    }



    //Declaramos las clases
    public DbSet<Coin> Coins { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<History> ExchangeHistory { get; set; }
}